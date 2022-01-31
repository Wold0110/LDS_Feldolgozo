using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace LDS_Feldolgozo

{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {

        }
        private void newfileBtn_Click(object sender, EventArgs e)
        {
            //�j f�jl?
            FolderBrowserDialog dir = new FolderBrowserDialog();
            DialogResult res = dir.ShowDialog();
            if (res == DialogResult.OK) //&& !string.IsNullOrWhiteSpace(dir.SelectedPath)
            {
                string target = dir.SelectedPath+"\\"+ DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".xlsx";
                ExcelSource.createExcel(target);
                int mode = 0;
                mode = sumMode.Checked ? 1 : mode;
                mode = dayByDayMode.Checked ? 2 : mode;
                ExcelSource.write(target,mode,doGroups.Checked,doABC.Checked);
                textBox1.AppendText("Sikeres �r�s!\r\n");
            }
            else
            {
                MessageBox.Show("F�jl nem lett mentve!");
            }
        }
        private void readBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "LDS-b�l export�lt Excel t�bla keres�se...";
            file.DefaultExt = "xlsx";
            file.CheckFileExists = true;
            file.CheckPathExists = true;
            file.Multiselect = false;
            file.Filter = "Excel f�jlok (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            DialogResult res = file.ShowDialog();
            if(res == DialogResult.OK)
            {
                ExcelSource.read(file.FileName);
                textBox1.AppendText("Olvas�s k�sz!\r\n");
            }
            else
            {
                MessageBox.Show("Az olvas�s nem t�rt�nt meg!");
            }

            
        }
    }
}