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
    public partial class FilterForm : Form
    {
        List<Line> l;
        List<Line> result = new List<Line>();
        List<lineUserControll> uc = new List<lineUserControll>();
        public FilterForm(List<Line> l)
        { 
            InitializeComponent();
            this.l = l;
            loadPanel();
        }
        private void loadPanel()
        {
            linePanel.Controls.Clear();
            linePanel.AutoScroll = true;
            linePanel.VerticalScroll.Enabled = true;
            linePanel.VerticalScroll.Visible = true;
            linePanel.HorizontalScroll.Enabled = false;
            linePanel.HorizontalScroll.Visible = false;
            lineUserControll.w = linePanel.Width*2;
            for(int i= 0; i < l.Count; i++)
            {
                lineUserControll luc = new lineUserControll(l[i].displayName);
                luc.Location =  new Point(0,i*luc.Height);
                uc.Add(luc);
                linePanel.Controls.Add(luc);
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            List<string> lineNames = new List<string>();
            foreach(lineUserControll luc in uc)
            {
                if (luc.Checked)
                {
                    lineNames.Add(luc.LineName);
                }
            }
            foreach(string lineName in lineNames)
            {
                foreach(Line line in l)
                {
                    if(line.displayName == lineName)
                    {
                        result.Add(line);
                        break;
                    }
                }
            }
            this.Hide();
        }
        public List<Line> Result { get { return result; } }

        private void allBtn_Click(object sender, EventArgs e)
        {
            foreach(lineUserControll luc in uc)
            {
                luc.Checked = true;
            }
        }

        private void noneBtn_Click(object sender, EventArgs e)
        {
            foreach (lineUserControll luc in uc)
            {
                luc.Checked = false;
            }
        }

        private void flipBtn_Click(object sender, EventArgs e)
        {
            foreach (lineUserControll luc in uc)
            {
                luc.Checked = !luc.Checked;
            }
        }
    }
}
