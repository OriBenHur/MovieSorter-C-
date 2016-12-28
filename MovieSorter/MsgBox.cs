using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MovieSorter
{
    public partial class MsgBox : Form
    {
        public MsgBox()
        {
            InitializeComponent();
        }
        public void AddLabeles(string lable, int x, int y )
        {
            LinkLabel _tmpLable = new LinkLabel();
            RadioButton _checkBox = new RadioButton();
            var location = new Point(x, y);
            var checkBoxLocation = new Point(x - 15, y-5);
            _checkBox.Location = checkBoxLocation;
            _tmpLable.Text = lable;
            _tmpLable.AutoSize = true;
            _tmpLable.Location = location;

            panel1.Controls.Add(_tmpLable);
            panel1.Controls.Add(_checkBox);
            _tmpLable.Click += link_Click;
            _checkBox


        }
        
        protected void link_Click(object sender, EventArgs e)
        {
            var link = (LinkLabel) sender;
            Process.Start(link.Text);
        }
        private void OK_Click(object sender, EventArgs e)
        {
            var radio = (RadioButton) sender;
            if (radio.Checked)
            {
                MessageBox.Show("ddd");
            }
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {

        }

        private void Checkbox_Chenge(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                var file = ((RadioButton)sender).Tag.ToString();
            }
        }
    }
}
