using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace LDS_Feldolgozo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //adatok állítása mert publish után nem minden marad meg
            textBox1.ReadOnly = true;
            dayByDayMode.Text = "Napi lebontás";
            doGroups.Text = "Csoportosítás";
            doABC.Text = "ÁBÉCÉ sorrend";
            this.Size = new Size(450, 320);
        }

        //globális fájl útvonalak
        string sourceFile;
        string outputPath;

        //OK gomb
        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //kiválasztotta-e a szükséges fájlokat
                if (sourceFile == "")
                    throw new NoLDSexportException("Üres string.");
                if (outputPath == "")
                    throw new NoExportPathException("Üres string.");

                textBox1.AppendText("Folyamatban...\r\n");
                string outputFile = outputPath + "\\" + 
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xlsx";
                //olvasás objektum
                ExcelSource es = new ExcelSource(sourceFile);
                es.Read();

                textBox1.AppendText("Olvasás kést, írás kezdõdik...");
                //írás objektum
                ExcelOutput.createExcel(outputFile);
                ExcelOutput eo = new ExcelOutput(outputFile, es.lines, es.from, es.to);

                //mód váltás - szumma vagy napi lebontás
                int mode = 0;
                mode = sumMode.Checked ? 1 : mode;
                mode = dayByDayMode.Checked ? 2 : mode;
                
                
                eo.Write(mode, doGroups.Checked, doABC.Checked);
                eo.Close();
                //Close() menti is
                textBox1.AppendText("Sikeres írás!\r\n");
            }
            //egyedi exceptionök
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

        //fájl kimenet
        private void newfileBtn_Click(object sender, EventArgs e)
        {
            //mappa dialógus a kimenethez
            FolderBrowserDialog dir = new FolderBrowserDialog();
            DialogResult res = dir.ShowDialog();
            if (res == DialogResult.OK)
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
            file.Filter = "Excel fájlok (*.xlsx)|*.xlsx|All files (*.*)|*.*"; //Excel filter
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