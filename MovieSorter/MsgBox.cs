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

        public string _ID;

        public string MyID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string _Title;

        public string MyTitle
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public void AddLabeles(string lable, int x, int y)
        {
            LinkLabel _tmpLable = new LinkLabel();
            RadioButton _checkBox = new RadioButton();
            var location = new Point(x, y);
            var checkBoxLocation = new Point(x - 15, y - 5);
            _checkBox.Location = checkBoxLocation;
            _tmpLable.Text = lable;
            _tmpLable.AutoSize = true;
            _tmpLable.Location = location;
            var ID = lable.Split('/');
            _checkBox.Name = ID[ID.Length - 1];
            panel1.Controls.Add(_tmpLable);
            panel1.Controls.Add(_checkBox);
            _tmpLable.Click += link_Click;
            //_checkBox.Click += new EventHandler(GetCheckedRadio);


        }

        protected void link_Click(object sender, EventArgs e)
        {
            var link = (LinkLabel)sender;
            Process.Start(link.Text);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var item = GetCheckedRadio(panel1);
            if (item != null)
            {
                if (item.Checked)
                {
                    MyID = item.Name;
                    Dispose();
                }
            }
            else

                MessageBox.Show("You Must Pick One Item");

        }


        private void MsgBox_Load(object sender, EventArgs e)
        {
            this.Text = _Title;
        }

        private RadioButton GetCheckedRadio(Control container)
        {
            foreach (var control in container.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio;
                }
            }

            return null;
        }
    }
}
