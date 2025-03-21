using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Map_Creation_Tool.src.View
{

    public enum ToolType : byte
    {
        Brush,
        Eraser,
        Rectangle,
        Ellipse,
        Line,
        Fill
    }



    public class MapCreationForm : Form
    {
        private const short WIDTH = 1280;
        private const short HEIGHT = 720;

        private Bitmap canvas;
        private PictureBox pictureBox;
        private Color currentColor;
        private Point? startPoint; //for drawing shapes
        private int brushSize; //or eraser size
        private ToolType currentTool;
        private Bitmap tempDrawing;
        private Pen previewPen;
        //gridSnapping mode
        private bool gridSnap;
        private int gridSize;
        //Undo and Redo Operations
        private Stack<Bitmap> undoStack = new Stack<Bitmap>();
        private Stack<Bitmap> redoStack = new Stack<Bitmap>();

        public MapCreationForm()
        {
            //Initializations of the attributes
            currentColor = Color.Gray;
            currentTool = ToolType.Brush;
            startPoint = null;
            brushSize = 4;
            tempDrawing = null;
            previewPen = new Pen(Color.Black, 1) { DashStyle = DashStyle.Dash };
            gridSnap = false;
            gridSize = 20;
            undoStack = new Stack<Bitmap>();
            redoStack = new Stack<Bitmap>();

            InitializeComponent();
            InitializeCanvas(WIDTH, HEIGHT);
        }

        private void InitializeComponent()
        {
            this.Text = "Map Drawer";
            this.Size = new Size(WIDTH, HEIGHT);

            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.Z) Undo();
                if (e.Control && e.KeyCode == Keys.Y) Redo();
            };


            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;


            // Toolbar Panel
            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.LightGray
            };

            // Color Buttons
            AddColorButton(toolbar, "Path", Color.White);
            AddColorButton(toolbar, "Busy", Color.FromArgb(241,216 , 167));
            AddColorButton(toolbar, "Very Busy", Color.FromArgb(223,145,158));
            AddColorButton(toolbar, "Place", Color.FromArgb(229 , 229 , 228));
            AddColorButton(toolbar, "Obstacle", Color.Black);
            

            // Tool Buttons
            AddToolButton(toolbar, "Brush", ToolType.Brush);
            AddToolButton(toolbar, "Eraser", ToolType.Eraser);
            AddToolButton(toolbar, "Rectangle", ToolType.Rectangle);
            //AddToolButton(toolbar, "Ellipse", ToolType.Ellipse);
            AddToolButton(toolbar, "Line", ToolType.Line);
            AddToolButton(toolbar, "Fill", ToolType.Fill);

            var btnNew = new Button { Text = "New", Width = 60 };
            btnNew.Click += (s, e) => InitializeCanvas(WIDTH, HEIGHT);

            var btnLoad = new Button { Text = "Load", Width = 60 };
            btnLoad.Click += LoadMap;

            var btnSave = new Button { Text = "Save", Width = 60 };
            btnSave.Click += SaveMap;

            var btnUndo = new Button { Text = "Undo", Width = 60 };
            btnUndo.Click += (s, e) => Undo();

            var btnRedo = new Button { Text = "Redo", Width = 60 };
            btnRedo.Click += (s, e) => Redo();

            var sizeTrack = new TrackBar { Width = 100, Minimum = 1, Maximum = 20, Value = brushSize };
            sizeTrack.ValueChanged += (s, e) => brushSize = sizeTrack.Value;

            var gridCheck = new CheckBox { Text = "Snap to Grid", AutoSize = true };
            gridCheck.CheckedChanged += (s, e) => gridSnap = gridCheck.Checked;

            toolbar.Controls.AddRange(new Control[] { btnNew,btnLoad, btnSave, btnUndo, btnRedo, sizeTrack, gridCheck });

            // Drawing Area
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Cursor = Cursors.Cross
            };

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.Paint += PictureBox_Paint;

            this.Controls.Add(pictureBox);
            this.Controls.Add(toolbar);
        }

        private void AddColorButton(Control parent, string text, Color color)
        {
            var btn = new Button
            {
                BackColor = color,
                Text = text,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            btn.Font = new Font(btn.Font, FontStyle.Bold);
            btn.Click += (s, e) => currentColor = color;
            parent.Controls.Add(btn);
        }

        private void AddToolButton(Control parent, string text, ToolType tool)
        {
            var btn = new Button
            {
                Text = text,
                Width = 80,
                Tag = tool,
                FlatStyle = FlatStyle.Flat
            };
            btn.Click += (s, e) =>
            {
                currentTool = tool;
                UpdateButtonStates();
            };
            parent.Controls.Add(btn);
        }

        private void UpdateButtonStates()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button btn && btn.Tag is ToolType)
                {
                    btn.BackColor = (ToolType)btn.Tag == currentTool ?
                        Color.SteelBlue : SystemColors.Control;
                }
            }
        }

        private void InitializeCanvas(int width, int height)
        {
            canvas = new Bitmap(width, height);
            using (var g = Graphics.FromImage(canvas))
                g.Clear(Color.White);
            pictureBox.Image = canvas;
            undoStack.Clear();
            redoStack.Clear();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            SaveState();
            startPoint = SnapPoint(e.Location);

            if (currentTool == ToolType.Fill)
            {
                FloodFill(SnapPoint(e.Location));
                return;
            }

            if (currentTool == ToolType.Brush || currentTool == ToolType.Eraser)
            {
                DrawFreehand(SnapPoint(e.Location));
            }
            else
            {
                tempDrawing = (Bitmap)canvas.Clone();
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPoint.HasValue)
            {
                if (currentTool == ToolType.Brush || currentTool == ToolType.Eraser)
                {
                    DrawFreehand(SnapPoint(e.Location));
                }
                else
                {
                    ShowShapePreview(SnapPoint(e.Location));
                }
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (startPoint.HasValue && currentTool != ToolType.Brush &&
                currentTool != ToolType.Eraser && currentTool != ToolType.Fill)
            {
                CommitShape(SnapPoint(e.Location));
            }
            startPoint = null;
            tempDrawing = null;
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (gridSnap)
            {
                using (var gridPen = new Pen(Color.FromArgb(50, Color.Black)))
                {
                    for (int x = 0; x < pictureBox.Width; x += gridSize)
                        e.Graphics.DrawLine(gridPen, x, 0, x, pictureBox.Height);
                    for (int y = 0; y < pictureBox.Height; y += gridSize)
                        e.Graphics.DrawLine(gridPen, 0, y, pictureBox.Width, y);
                }
            }
        }

        private void DrawFreehand(Point point)
        {
            using (var g = Graphics.FromImage(canvas))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(GetDrawingColor(), brushSize))
                {
                    pen.StartCap = pen.EndCap = LineCap.Round;

                    if (startPoint.HasValue)
                        g.DrawLine(pen, startPoint.Value, point);
                    else
                        g.FillEllipse(pen.Brush, point.X - brushSize / 2, point.Y - brushSize / 2, brushSize, brushSize);
                }
            }
            startPoint = point;
            pictureBox.Invalidate();
        }

        private void ShowShapePreview(Point endPoint)
        {
            if (tempDrawing == null) return;

            pictureBox.Image = tempDrawing;
            using (var g = Graphics.FromImage(pictureBox.Image))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = GetRectangle(startPoint.Value, endPoint);

                switch (currentTool)
                {
                    case ToolType.Rectangle:
                        g.DrawRectangle(previewPen, rect);
                        break;
                    case ToolType.Ellipse:
                        g.DrawEllipse(previewPen, rect);
                        break;
                    case ToolType.Line:
                        g.DrawLine(previewPen, startPoint.Value, endPoint);
                        break;
                }
            }
            pictureBox.Refresh();
        }

        private void CommitShape(Point endPoint)
        {
            using (var g = Graphics.FromImage(canvas))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = GetRectangle(startPoint.Value, endPoint);

                using (var brush = new SolidBrush(GetDrawingColor()))
                using (var pen = new Pen(brush, brushSize))
                {
                    switch (currentTool)
                    {
                        case ToolType.Rectangle:
                            g.FillRectangle(brush, rect);
                            break;
                        case ToolType.Ellipse:
                            g.FillEllipse(brush, rect);
                            break;
                        case ToolType.Line:
                            g.DrawLine(pen, startPoint.Value, endPoint);
                            break;
                    }
                }
            }
            pictureBox.Image = canvas;
        }

        private void FloodFill(Point point)
        {
            var targetColor = canvas.GetPixel(point.X, point.Y);
            if (targetColor.ToArgb() == currentColor.ToArgb()) return;

            var stack = new Stack<Point>();
            stack.Push(point);

            while (stack.Count > 0)
            {
                var p = stack.Pop();
                if (p.X < 0 || p.X >= canvas.Width || p.Y < 0 || p.Y >= canvas.Height)
                    continue;

                if (canvas.GetPixel(p.X, p.Y) == targetColor)
                {
                    canvas.SetPixel(p.X, p.Y, currentColor);
                    stack.Push(new Point(p.X - 1, p.Y));
                    stack.Push(new Point(p.X + 1, p.Y));
                    stack.Push(new Point(p.X, p.Y - 1));
                    stack.Push(new Point(p.X, p.Y + 1));
                }
            }
            pictureBox.Invalidate();
        }

        private void SaveState()
        {
            undoStack.Push((Bitmap)canvas.Clone());
            redoStack.Clear();
        }

        private void Undo()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push((Bitmap)canvas.Clone());
                canvas = (Bitmap)undoStack.Pop().Clone();
                pictureBox.Image = canvas;
            }
        }

        private void Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push((Bitmap)canvas.Clone());
                canvas = (Bitmap)redoStack.Pop().Clone();
                pictureBox.Image = canvas;
            }
        }

        private Point SnapPoint(Point p) => gridSnap ?
            new Point(p.X / gridSize * gridSize, p.Y / gridSize * gridSize) : p;

        private Color GetDrawingColor() =>
            currentTool == ToolType.Eraser ? Color.White : currentColor;

        private Rectangle GetRectangle(Point p1, Point p2) => new Rectangle(
            Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y),
            Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

        private void LoadMap(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Image Files|*.png;*.jpg";
                openDialog.Title = "Load Map Image";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    canvas = new Bitmap(openDialog.FileName);
                    pictureBox.Image = canvas;
                }
            }
        }

        private void SaveMap(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                saveDialog.Title = "Save Map Image";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = saveDialog.FileName.EndsWith(".jpg") ?
                        ImageFormat.Jpeg : ImageFormat.Png;

                    canvas.Save(saveDialog.FileName, format);
                    MessageBox.Show("Map saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}