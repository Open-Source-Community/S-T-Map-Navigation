using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_Creation_Tool.src.View
{
    public partial class MenuScreen : Form
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuideForm guide = new GuideForm();
            this.Hide();
            guide.ShowDialog(); // Blocks until MenuScreen is closed
            this.Show(); // Show EntryScreen again after MenuScreen closes
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MapCreationForm createForm = new MapCreationForm();
            this.Hide();
            createForm.ShowDialog(); // Blocks until MenuScreen is closed
            this.Show(); // Show EntryScreen again after MenuScreen closes
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShortestPathForm shortestPathForm= new ShortestPathForm();
            this.Hide();
            shortestPathForm.ShowDialog(); // Blocks until MenuScreen is closed
            this.Show(); // Show EntryScreen again after MenuScreen closes
        }
    }
}
