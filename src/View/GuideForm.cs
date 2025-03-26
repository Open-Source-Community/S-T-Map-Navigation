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
    public partial class GuideForm : Form
    {
        const string BACKGROUND_PATH = @"D:\برمجة\Backend\WinFormsApp1OSC\res\Assets\firstScreen.png";

        public GuideForm()
        {
            InitializeComponent();
        }

        private void GuideForm_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
