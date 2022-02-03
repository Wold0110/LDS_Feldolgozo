namespace LDS_Feldolgozo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;

            SettingsForm.Init();
            //adatok állítása mert publish után nem minden marad meg
            //textBox1.ReadOnly = true;
            //dayByDayMode.Text = "Napi lebontás";
            //doGroups.Text = "Csoportosítás";
            //doABC.Text = "ÁBÉCÉ sorrend";
            //this.Size = new Size(450, 320);
            progressBar.Hide();
        }

        //globális fájl útvonalak
        string sourceFile;
        string outputPath;

        public long count = 0;
        public bool wait;
        public bool closing = false;
        ExcelSource es;
        ExcelOutput eo;
        Thread writeThread;
        public void Update(string status) {statusText.Text = status +" "+(count++);}
        private void Wait(string outputFile)
        {
            while (wait) {
                Thread.Sleep(1000);
                if (closing)
                {
                    wait = false;
                }
            }
            if (!closing) { 
                textBox1.AppendText("Írás kezdetét vette...\r\n");
                ExcelOutput.createExcel(outputFile);
                
                int mode = 0;
                mode = sumMode.Checked ? 1 : mode;
                mode = dayByDayMode.Checked ? 2 : mode;
                eo = new ExcelOutput(outputFile, es.lines, es.from, es.to);
                eo.Write(mode, doGroups.Checked, doABC.Checked,doFilter.Checked, this);
                eo.Close();
                //Close() menti is
            }
        }
        //OK gomb
        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //kiválasztotta-e a szükséges fájlokat
                if (sourceFile == "") { throw new NoLDSexportException("Üres string."); }
                if (outputPath == "") { throw new NoExportPathException("Üres string."); }
                textBox1.AppendText("Folyamatban...\r\n");

                wait = true;

                string outputFile = outputPath + "\\" +
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xlsx";

                //olvasás objektum
                es = new ExcelSource(sourceFile);
                new Thread(() => es.Read(this)).Start();

                writeThread = new Thread(() => Wait(outputFile));
                writeThread.Start();
            }
            //egyedi exceptionök
            catch (NoLDSexportException ex) { textBox1.AppendText("Nincs kijelölt forrás fájl! Próbálja újra miután kijelölte.\r\n"); }
            catch (NoExportPathException ex) { textBox1.AppendText("Nincs kijelölve kimeneti fájl! Próbálja újra miután kijelölte.\r\n"); }
            catch (ThreadAbortException ex) { }
            catch (Exception ex) { textBox1.AppendText(ex.Message); }

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
                textBox1.AppendText("Az olvasás nem történt meg!\r\n");
                sourceFile = "";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close event
            //KILLALL
            closing = true;
            textBox1.AppendText("Bezárás folyamatban.\r\n");
            Excel.KillSpecificExcelFileProcess("");
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
        }
    }
}