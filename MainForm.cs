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
            dayByDayMode.Text = "Napi lebontás";
            doGroups.Text = "Csoportosítás";
            doABC.Text = "ÁBÉCÉ sorrend";
            this.Size = new Size(450, 300);
        }
        string sourceFile;
        string outputPath;

        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sourceFile == "")
                    throw new NoLDSexportException("Üres string.");
                if (outputPath == "")
                    throw new NoExportPathException("Üres string.");

                textBox1.AppendText("Folyamatban...\r\n");
                string outputFile = outputPath + "\\" + 
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xlsx";
                ExcelSource es = new ExcelSource(sourceFile);
                es.Read();
                textBox1.AppendText("Olvasás kést, írás kezdõdik...");
                ExcelOutput.createExcel(outputFile);
                ExcelOutput eo = new ExcelOutput(outputFile, es.lines, es.from, es.to);

                int mode = 0;
                mode = sumMode.Checked ? 1 : mode;
                mode = dayByDayMode.Checked ? 2 : mode;
                
                eo.Write(mode, doGroups.Checked, doABC.Checked);
                eo.Close();

                textBox1.AppendText("Sikeres írás!\r\n");
            }
            catch (NoLDSexportException ex)
            {
                textBox1.AppendText("Nincs kijelölt forrás fájl! Próbálja újra miután kijelölte.\r\n");
            }
            catch (NoExportPathException ex)
            {
                textBox1.AppendText("Nincs kijelölve kimeneti fájl! Próbálja újra miután kijelölte.\r\n");
            }
            catch (Exception ex)
            {
                textBox1.AppendText(ex.Message);
            }

        }
        private void newfileBtn_Click(object sender, EventArgs e)
        {
            //új fájl?
            FolderBrowserDialog dir = new FolderBrowserDialog();
            DialogResult res = dir.ShowDialog();
            if (res == DialogResult.OK) //&& !string.IsNullOrWhiteSpace(dir.SelectedPath)
            {
                outputPath = dir.SelectedPath;
                textBox1.AppendText("Kimenet kijelölve.\r\n");
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
            file.Title = "LDS-bõl exportált Excel tábla keresése...";
            file.DefaultExt = "xlsx";
            file.CheckFileExists = true;
            file.CheckPathExists = true;
            file.Multiselect = false;
            file.Filter = "Excel fájlok (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            DialogResult res = file.ShowDialog();
            if(res == DialogResult.OK)
            {
                sourceFile = file.FileName;
                textBox1.AppendText("Olvasára kijelölve.\r\n");
            }
            else
            {
                MessageBox.Show("Az olvasás nem történt meg!\r\n");
                sourceFile = "";
            }
        }
    }
}