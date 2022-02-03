using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDS_Feldolgozo
{
    public partial class lineUserControll : UserControl
    {
        public static int w = 500;
        static int idg = 0;
        string name;
        public lineUserControll(string lineName)
        {
            InitializeComponent();
            idg++;
            this.name = lineName;
            checkBox.Text = idg+". "+lineName;
            this.AutoScroll = true;
            this.Width = w;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
        }
        public bool Checked { get { return checkBox.Checked; } set { checkBox.Checked = value; } }
        public string LineName { get { return name; } }
    }
}
