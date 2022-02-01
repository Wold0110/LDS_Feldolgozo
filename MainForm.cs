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
        string sourceFile;
        string outputPath;

        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sourceFile == "")
                    throw new NoLDSexportException("�res string.");
                if (outputPath == "")
                    throw new NoExportPathException("�res string.");

                textBox1.AppendText("Folyamatban...\r\n");
                string outputFile = outputPath + "\\" + 
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xlsx";
                ExcelSource es = new ExcelSource(sourceFile);
                es.Read();

                ExcelOutput.createExcel(outputFile);
                ExcelOutput eo = new ExcelOutput(outputFile, es.lines, es.from, es.to);

                int mode = 0;
                mode = sumMode.Checked ? 1 : mode;
                mode = dayByDayMode.Checked ? 2 : mode;
                
                eo.Write(mode, doGroups.Checked, doABC.Checked);
                eo.Close();

                textBox1.AppendText("Sikeres �r�s!\r\n");
            }
            catch (NoLDSexportException ex)
            {
                textBox1.AppendText("Nincs kijel�lt forr�s f�jl! Pr�b�lja �jra miut�n kijel�lte.\r\n");
            }
            catch (NoExportPathException ex)
            {
                textBox1.AppendText("Nincs kijel�lve kimeneti f�jl! Pr�b�lja �jra miut�n kijel�lte.\r\n");
            }
            catch (Exception ex)
            {
                textBox1.AppendText(ex.Message);
            }

        }
        private void newfileBtn_Click(object sender, EventArgs e)
        {
            //�j f�jl?
            FolderBrowserDialog dir = new FolderBrowserDialog();
            DialogResult res = dir.ShowDialog();
            if (res == DialogResult.OK) //&& !string.IsNullOrWhiteSpace(dir.SelectedPath)
            {
                outputPath = dir.SelectedPath;
                textBox1.AppendText("Kimenet kijel�lve.\r\n");
            }
            else
            {
                MessageBox.Show("Kimenet nem lett mentve!\r\n");
                outputPath = "";
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
                sourceFile = file.FileName;
                textBox1.AppendText("Olvas�ra kijel�lve.\r\n");
            }
            else
            {
                MessageBox.Show("Az olvas�s nem t�rt�nt meg!\r\n");
                sourceFile = "";
            }
        }
    }
}