// using System;
// using System.Windows.Forms;
// using Map_Creation_Tool.src.Model;
//
// namespace Map_Creation_Tool.src.View
// {
//     public partial class PathSelectionForm : Form
//     {
//         public PathType SelectedPathType { get; private set; }
//
//         public PathSelectionForm()
//         {
//             InitializeComponent();
//             SelectedPathType = PathType.SHORTEST_PATH; // Default option
//         }
//
//         private void btnFastest_Click(object sender, EventArgs e)
//         {
//             SelectedPathType = PathType.FASTEST_PATH;
//             this.DialogResult = DialogResult.OK;
//             this.Close();
//         }
//
//         private void btnShortest_Click(object sender, EventArgs e)
//         {
//             SelectedPathType = PathType.SHORTEST_PATH;
//             this.DialogResult = DialogResult.OK;
//             this.Close();
//         }
//
//         private void btnCancel_Click(object sender, EventArgs e)
//         {
//             this.DialogResult = DialogResult.Cancel;
//             this.Close();
//         }
//     }
// }