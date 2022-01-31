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
    public partial class DatePickers : Form
    {
        public DateTime to;
        public DateTime from;
        public DatePickers(DateTime min, DateTime max)
        {
            InitializeComponent();
            toDate.MinDate = min;
            toDate.MaxDate = max;
            toDate.Value = max;

            fromDate.MinDate = min;
            fromDate.MaxDate = max;
            fromDate.Value = min;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            to = toDate.Value;
            from = fromDate.Value;
            this.Hide();
        }
    }
}
