using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace LDS_Feldolgozo
{
    internal class ExcelOutput
    {
        //globális változók
        Excel trg;
        List<Line> linesInput;
        DateTime to;
        DateTime from;
        MainForm form;
        public ExcelOutput(string target, List<Line> lines, DateTime from, DateTime to)
        {
            trg = new Excel(target);
            this.to = to;
            this.from = from;
            this.linesInput = lines;
        }
        public void Close()
        {
            trg.Close();
        }
        //statikus függvény, létrehoz egy fájlt
        public static void createExcel(string path)
        {
            Excel res = new Excel();
            res.wb.SaveAs(path);
            res.wb.Close();
        }
        //@DEPRECATED: adatok ellenőrzésére használt függvény volt, tesztelési célokra bent marad
        public string PrintDemo(List<Line> lines)
        {
            string res = "";
            foreach (Line l in lines)
            {
                res += (l.name + "\r\n" + l.getOee() + "\r\n" + l.getGood() + "\r\nshift db:" + l.shifts.Count + "\r\n");
                List<Downtime> dtl = l.topNdowntime(10);

                for (int i = 0; i < Math.Min(dtl.Count, 10); i++)
                    res += (dtl[i].reason + " " + Functions.secToString(dtl[i].time) + "\r\n");
                res += ("\r\n");
            }
            return res;
        }
        //egy sort ír ki, l -> sor, mode -> bontás/szumma, cycle -> eltolás
        private void printLine(Line l, int mode, int cycle)
        {
            form.progressBar.Value = cycle;
            if (mode == 1)
            {
                //formatting
                int shift = cycle * 12 + (int)Math.Floor((double)cycle / 4) * 4;
                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[3 + shift, 2]].Merge();
                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[3 + shift, 2]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                switch (l.type)
                {
                    case LineType.Area:
                        trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[3 + shift, 2]].Interior.ColorIndex = 3;
                        trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[3 + shift, 2]].Font.ColorIndex = 2;

                        break;
                    case LineType.Group:
                        trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[3 + shift, 2]].Interior.ColorIndex = 4;
                        break;
                }

                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[7 + shift, 1]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                trg.ws.Range[trg.ws.Cells[2 + shift, 2], trg.ws.Cells[7 + shift, 2]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);

                trg.ws.Range[trg.ws.Cells[9 + shift, 1], trg.ws.Cells[12 + shift, 1]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                trg.ws.Range[trg.ws.Cells[9 + shift, 2], trg.ws.Cells[12 + shift, 2]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);


                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[12 + shift, 1]].Font.Bold = true;

                trg.ws.Cells[2 + shift, 1].WrapText = true;

                trg.ws.Cells[2 + shift, 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                trg.ws.Cells[2 + shift, 1].VerticalAlignment = XlVAlign.xlVAlignCenter;

                //data

                trg.ws.Cells[2 + shift, 1].Value2 = l.displayName;

                trg.ws.Cells[4 + shift, 1].Value2 = "Target: " + Math.Round(l.oeeTarget, 2);
                trg.ws.Cells[4 + shift, 2].Value2 = "Valós: " + Math.Round(l.getOee(), 2);
                trg.ws.Cells[4 + shift, 2].HorizontalAlignment = XlHAlign.xlHAlignRight;

                trg.ws.Cells[5 + shift, 1].Value2 = "Jó";
                trg.ws.Cells[5 + shift, 2].Value2 = l.getGood();

                trg.ws.Cells[6 + shift, 1].Value2 = "Rossz";
                trg.ws.Cells[6 + shift, 2].Value2 = l.getBad();

                trg.ws.Cells[7 + shift, 1].Value2 = "Futási idő:";
                trg.ws.Cells[7 + shift, 2].Value2 = Functions.minToString(l.getRuntime());

                trg.ws.Cells[9 + shift, 1].Value2 = "áll g";
                trg.ws.Cells[9 + shift, 2].Value2 = Functions.secToString(l.downtimeG());
                trg.ws.Cells[10 + shift, 1].Value2 = "áll gnm";
                trg.ws.Cells[11 + shift, 1].Value2 = "áll k";
                trg.ws.Cells[12 + shift, 1].Value2 = "hiány";

                //downtime
                trg.ws.Cells[2 + shift, 4].Value2 = "Hiba";
                trg.ws.Cells[2 + shift, 5].Value2 = "Idő";

                trg.ws.Cells[2 + shift, 4].Font.Bold = true;
                trg.ws.Cells[2 + shift, 5].Font.Bold = true;

                List<Downtime> dtl = l.topNdowntime(10);
                int x = 0;
                foreach (Downtime dt in dtl)
                {
                    trg.ws.Cells[3 + shift + x, 4].Value2 = dt.reason;
                    trg.ws.Cells[3 + shift + x, 5].Value2 = Functions.secToString(dt.time);
                    ++x;
                }
            }
            //day-by-day
            if (mode == 2)
            {
                int shift = cycle * 13;
                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[2 + shift, 11]].Merge();
                trg.ws.Cells[2 + shift, 1].Value2 = l.displayName;
                trg.ws.Cells[2 + shift, 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                trg.ws.Cells[2 + shift, 1].VerticalAlignment = XlVAlign.xlVAlignCenter;
                trg.ws.Cells[2 + shift, 1].Font.Bold = true;
                switch (l.type)
                {
                    case LineType.Area:
                        trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[2 + shift, 11]].Interior.ColorIndex = 3;
                        trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[2 + shift, 11]].Font.ColorIndex = 2;

                        break;
                    case LineType.Group:
                        trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[2 + shift, 11]].Interior.ColorIndex = 4;
                        break;
                }
                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[12 + shift, 1]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                trg.ws.Range[trg.ws.Cells[2 + shift, 1], trg.ws.Cells[2 + shift, 11]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);

                string[] t = "OEE;Jó;Rossz;MTTR;MTBF;Állás 1;Állás 2;Állás 3".Split(';');
                for (int k = 0; k < 8; ++k)
                {
                    trg.ws.Cells[5 + shift + k, 1].Value2 = t[k];
                    trg.ws.Cells[5 + shift + k, 1].Font.Bold = true;
                }
                int x = 0;
                for (DateTime i = from; i <= to; i = i.AddDays(1))
                {
                    trg.ws.Range[trg.ws.Cells[3 + shift, 2 + x], trg.ws.Cells[12 + shift, 4 + x]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);

                    trg.ws.Range[trg.ws.Cells[3 + shift, 2 + x], trg.ws.Cells[3 + shift, 4 + x]].Merge();
                    trg.ws.Cells[3 + shift, 2 + x].Value2 = i.ToString("yyyy-MM-dd");
                    trg.ws.Cells[3 + shift, 2 + x].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    trg.ws.Cells[3 + shift, 2 + x].Font.Bold = true;

                    for (int k = 0; k < 3; ++k)
                    {
                        trg.ws.Cells[4 + shift, 2 + x + k].Value2 = k == 0 ? "DE" : (k == 1 ? "DU" : "ÉJ");
                        trg.ws.Cells[4 + shift, 2 + x + k].Font.Bold = true;

                        trg.ws.Cells[5 + shift, 2 + x + k].Value2 = l.getOee(k + 1, i);
                        trg.ws.Cells[6 + shift, 2 + x + k].Value2 = l.getGood(k + 1, i);
                        trg.ws.Cells[7 + shift, 2 + x + k].Value2 = l.getBad(k + 1, i);
                        double mtbf = l.getMTBF(k + 1, i);
                        string mtbfString = "*MTBF*";
                        switch (mtbf)
                        {
                            case -1:
                                mtbfString = "No Run";
                                break;
                            case -2:
                                mtbfString = "No Downtime";
                                break;
                            default:
                                mtbfString = mtbf + "";
                                break;
                        }
                        double mttr = l.getMTTR(k + 1, i);
                        string mttrString = "*MTTR*";
                        switch (mttr)
                        {
                            case -2:
                                mttrString = "No Downtime";
                                break;
                            default:
                                mttrString = mttr + "";
                                break;
                        }
                        trg.ws.Cells[8 + shift, 2 + x + k].Value2 = mttrString; //MTTR - Mean time to repair
                        trg.ws.Cells[8 + shift, 2 + x + k].HorizontalAlignment = XlHAlign.xlHAlignFill;
                        trg.ws.Cells[9 + shift, 2 + x + k].Value2 = mtbfString; //MTBF - Mean time between failures
                        trg.ws.Cells[9 + shift, 2 + x + k].HorizontalAlignment = XlHAlign.xlHAlignFill;

                        List<Downtime> dtl = new List<Downtime>();
                        dtl = l.topNdowntime(3, k + 1, i);
                        for (int y = 0; y < Math.Min(dtl.Count, 3); ++y)
                        {
                            trg.ws.Cells[10 + shift + y, 2 + x + k].Value2 = Functions.secToString(dtl[y].time) + " - " + dtl[y].reason;
                            trg.ws.Cells[10 + shift + y, 2 + x + k].HorizontalAlignment = XlHAlign.xlHAlignFill;
                        }
                    }
                    x += 3;
                }
            }
        }
        //publikus író függvény, ez megy végig a lineokot és írja ki őket
        public void Write(int mode, bool doGroup, bool abc,bool doFilter, MainForm form)
        {

            //MessageBox.Show("write "+mode+" "+lines.Count);
            /* modes
             * 
             * 1 sum
             * 2 day-by-day
             * 
             */

            List<Line> lines;
            if (doFilter)
            {
                FilterForm ff = new FilterForm(linesInput);
                ff.ShowDialog();
                lines = ff.Result;
            }
            else
            {
                lines = linesInput;
            }

            this.form = form;
            this.form.statusText.Hide();
            this.form.progressBar.Show();
            this.form.progressBar.Minimum = 0;
            int max = lines.Count;

            #region grouping
            //amennyiben szükséges csoportosítja a sorokat, TODO: 'egyébb' csoport létrehozása
            List<Area> areas = new List<Area>();
            if (doGroup)
            {
                string[] t = File.ReadAllLines("csoportok.csv", Encoding.UTF8);
                for (int x = 1; x < t.Length; ++x)
                {
                    string[] tmp = t[x].Split(',');
                    string area = tmp[tmp.Length - 1];
                    string group = tmp[tmp.Length - 2];
                    string name = "";
                    for (int i = 0; i <= tmp.Length - 3; ++i)
                        name += "," + tmp[i];
                    name = name.Substring(1).Replace("\"", "");

                    int areaIndex = Area.Exists(area, areas);
                    if (areaIndex != -1)
                    {
                        int groupIndex = Group.Exists(group, areas[areaIndex].groups);
                        if (groupIndex != -1)
                        {
                            foreach (Line l in lines)
                            {
                                if (String.Compare(l.displayName, name) == 0)
                                {
                                    areas[areaIndex].groups[groupIndex].lines.Add(l);
                                }
                            }
                        }
                        else
                        {
                            Group groupTmp = new Group(group);
                            foreach (Line l in lines)
                            {
                                if (String.Compare(l.displayName, name) == 0)
                                {
                                    groupTmp.lines.Add(l);
                                }
                            }
                            areas[areaIndex].groups.Add(groupTmp);
                        }
                    }
                    else
                    {
                        Area areaTmp = new Area(area);
                        Group groupTmp = new Group(group);
                        foreach (Line l in lines)
                        {
                            if (String.Compare(l.displayName, name) == 0)
                            {
                                groupTmp.lines.Add(l);
                            }
                        }
                        areaTmp.groups.Add(groupTmp);
                        areas.Add(areaTmp);
                    }
                }

                // 'egyéb' csoport
                Area etc = null;
                foreach (Line l in lines)
                {
                    bool found = false;
                    foreach (Area a in areas)
                    {
                        foreach (Group g in a.groups)
                        {
                            foreach (Line tmp in g.lines)
                            {
                                if (String.Compare(tmp.displayName, l.displayName) == 0)
                                {
                                    found = true;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        if (etc == null)
                        {
                            etc = new Area("Egyéb");
                            Group groupTmp = new Group("Egyéb");
                            groupTmp.lines.Add(l);
                            etc.groups.Add(groupTmp);
                            areas.Add(etc);
                        }
                        else
                        {
                            etc.groups[0].lines.Add(l);
                        }
                    }
                }

                if (etc != null)
                {
                    areas.Add(etc);
                }
                //üres group/area törlése
                for (int x = 0; x < areas.Count; ++x)
                {
                    for (int y = 0; y < areas[x].groups.Count; ++y)
                    {
                        if (areas[x].groups[y].lines.Count == 0)
                        {
                            areas[x].groups.RemoveAt(y);
                            --y;
                        }
                    }

                }
                for (int x = 0; x < areas.Count; ++x)
                {
                    if (areas[x].groups.Count == 0)
                    {
                        areas[x].groups.RemoveAt(0);
                        --x;
                    }
                }
                foreach (Area a in areas)
                {
                    max += 1 + a.groups.Count;
                }
            }
            #endregion grouping
            
            
            #region setup
            //sum
            trg.ws.Cells.Clear();
            if (mode == 1)
            {
                trg.ws.Cells.Font.Size = 11;
                for (int i = 1; i < 3; i++)
                {
                    trg.ws.Columns[i].ColumnWidth = 15;
                }
                trg.ws.Columns[3].ColumnWidth = 5;

                trg.ws.Columns[4].ColumnWidth = 40;
                trg.ws.Columns[5].ColumnWidth = 10;
                trg.ws.Columns[6].ColumnWidth = 12;

                trg.ws.Rows.RowHeight = 15;
            }
            //day-by-day
            if (mode == 2)
            {
                DatePickers dp = new DatePickers(from, to);
                dp.ShowDialog();

                trg.ws.Columns.ColumnWidth = 5;

                trg.ws.Rows.RowHeight = 12;
                trg.ws.Cells.Clear();
                trg.ws.Cells.Style.Font.Size = 8;

                trg.ws.Range[trg.ws.Cells[1, 4], trg.ws.Cells[1, 5]].Merge();
                trg.ws.Cells[1, 4].Value2 = from.ToString("yyyy-MM-dd");

                trg.ws.Range[trg.ws.Cells[1, 6], trg.ws.Cells[1, 7]].Merge();
                trg.ws.Cells[1, 6].Value2 = to.ToString("yyyy-MM-dd");

                trg.exe.ActiveWindow.Zoom = 115;
            }
            #endregion setup

            if (mode != 0)
            {
                this.form.progressBar.Maximum = max;
                if (doGroup)
                {
                    int index = 0;
                    areas.Sort((a, b) => a.name.CompareTo(b.name));
                    foreach (Area a in areas)
                    {
                        if (abc)
                        {
                            a.groups.Sort((a, b) => a.name.CompareTo(b.name));
                        }
                        printLine(a.FakeLine(), mode, index++);
                        foreach (Group g in a.groups)
                        {
                            if (abc)
                            {
                                g.lines.Sort((a, b) => a.name.CompareTo(b.name));
                            }
                            printLine(g.FakeLine(), mode, index++);
                            foreach (Line l in g.lines)
                            {
                                printLine(l, mode, index++);
                            }
                        }
                    }
                }
                else
                {
                    if (abc)
                    {
                        lines.Sort((a, b) => a.displayName.CompareTo(b.displayName));
                    }
                    for (int i = 0; i < lines.Count; i++)
                    {
                        printLine(lines[i], mode, i);
                    }
                }
            }
            this.form.statusText.Show();
            this.form.progressBar.Hide();
            this.form.statusText.Text = "";
            this.form.textBox1.AppendText("Írás kész!\r\n");
        }
    }
    //olvasó osztály
    internal class ExcelSource
    {
        public List<Line> lines = new List<Line>();
        
        List<LDSexport> exports = new List<LDSexport>();
        List<bool> status = new List<bool>();
        List<Thread> threads = new List<Thread>();
        int threadNum = SettingsForm.threadNum;

        private string sourcePath;
        
        public DateTime to;
        public DateTime from;

        MainForm form;
        public string path
        {
            get
            {
                return sourcePath;
            }
        }
        public ExcelSource(string path)
        {
            sourcePath = path;
        }
        //egy adott indexű sort olvas ki
        private bool readProdLine(int i, LDSexport src)
        {
            try
            {
                string name = src.readString(0, 1 + i, src.prod);
                if (name == null)
                    return false;
                double date = src.readDouble(3, 1 + i, src.prod);
                double shift = src.readDouble(4, 1 + i, src.prod);
                double runtime = src.readDouble(7, 1 + i, src.prod);
                double good = src.readDouble(10, 1 + i, src.prod);
                double repair = src.readDouble(11, 1 + i, src.prod);
                double bad = src.readDouble(12, 1 + i, src.prod);
                double scrap = src.readDouble(13, 1 + i, src.prod);
                double oee = src.readDouble(19, 1 + i, src.prod);
                double sur = src.readDouble(20, 1 + i, src.prod);
                double oeeTarget = src.readDouble(24, 1 + i, src.prod);
                int index = Line.Exits(name, lines);
                if (index == -1)
                {
                    //add new
                    Line tmp = new Line(name);
                    tmp.AddShift(oeeTarget, sur,
                        new Shift(shift, DateTime.FromOADate(date), oee, good, bad, runtime));
                    lines.Add(tmp);
                }
                else
                {
                    //append
                    lines[index].AddShift(oeeTarget, sur,
                        new Shift(shift, DateTime.FromOADate(date), oee, good, bad, runtime));
                }
                to = DateTime.FromOADate(date) > to ? DateTime.FromOADate(date) : to;
                from = DateTime.FromOADate(date) < from ? DateTime.FromOADate(date) : from;

                return true;
            }
            catch(ExitClose ex) { throw new ExitClose(); }
        }
        //egy adott indexű sort olvas ki
        private bool readDownLine(int i, LDSexport src)
        {
            string name = src.readString(4, 1 + i, src.downtime);
            if (name == null)
            {
                return false;
            }
            double date = src.readDouble(9, 1 + i, src.downtime);
            string[] timeArr = src.readString(8, 1 + i, src.downtime).Split(':');
            double timeDouble = (((Convert.ToDouble(timeArr[0]) * 60)
                + Convert.ToDouble(timeArr[1])) * 60) + Convert.ToDouble(timeArr[2]);
            double shift = src.readDouble(0, 1 + i, src.downtime);
            string reason = src.readString(5, 1 + i, src.downtime);
            int index = Line.Exits(name,lines);
            if (index != -1)
                lines[index].AddDowntime(shift, DateTime.FromOADate(date), new Downtime(reason, timeDouble));
            return true;
        }
        //publikus olvasó függvény
        private void wait(bool running)
        {
            while (running)
            {
                running = false;
                foreach (bool b in status)
                    running = running || b;
                Thread.Sleep(1000);
            }
        }
        public void Read(MainForm form)
        {
            try
            {
                this.form = form;
                lines.Clear();
                from = DateTime.Now; //hogy legyen minél kisebb, különben 1900-on marad
                for (int j = 0; j < threadNum && !form.closing; ++j)
                {
                    status.Add(true);
                    exports.Add(new LDSexport(sourcePath));
                    int x = j;
                    Thread t = new Thread(() => readThreadProd(x));
                    t.Start();
                    threads.Add(t);
                }
                wait(!form.closing);
                for (int j = 0; j < threadNum && !form.closing; ++j)
                {
                    status[j] = true;
                    int x = j;
                    threads[j] = new Thread(() => readThreadDown(x));
                    threads[j].Start();
                }
                wait(!form.closing);
                //close
                foreach(var x in exports)
                {
                    x.Close();
                }
                form.wait = false;
            }
            catch{ form.statusText.Text = "error"; }
        }

        private void readThreadProd(int threadIndex)
        {
            try
            {
                bool running = true;
                LDSexport exp = exports[threadIndex];
                int i = 0 + threadIndex;
                while (running)
                {
                    form.Update("prod");
                    running = running && readProdLine(i, exp); i += threadNum;
                }

                status[threadIndex] = false;
            }
            catch(ExitClose ex) {}
        }
        private void readThreadDown(int threadIndex)
        {
            bool running = true;
            LDSexport exp = exports[threadIndex];
            int i = 0 + threadIndex;
            while (running)
            {
                form.Update("downtime");
                running = running && readDownLine(i, exp); i += threadNum;
            }

            status[threadIndex] = false;
        }
    }
    //standard Excel osztály írásra/olvasásra
    internal class Excel
    {
        public _Application exe = new _Excel.Application();

        string path;
        public Workbook wb;
        public Worksheet ws;
        public Excel(string path)
        {
            this.path = path;
            this.wb = this.exe.Workbooks.Open(path);
            this.ws = this.wb.Worksheets[1];
        }
        public Excel()
        {
            this.wb = exe.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        }
        public static void KillSpecificExcelFileProcess(string excelFileName)
        {
            try
            {
                var processes = from p in Process.GetProcessesByName("EXCEL")
                                select p;
                //specific  -> Microsoft Excel - asd.xlsx
                //embed     -> ""
                foreach (var process in processes)
                {
                    if (excelFileName == process.MainWindowTitle)
                        process.Kill();
                }
            }
            catch { /* ¯\_(ツ)_/¯   */}
        }
        public void Close()
        {
            Marshal.FinalReleaseComObject(ws);

            wb.Close(true, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(wb);

            exe.Quit();
            Marshal.FinalReleaseComObject(exe);

            KillSpecificExcelFileProcess(""); 
            GC.Collect();
        }
        public void writeString(int x, int y,string value) {
            ws.Cells[y + 1, x + 1].value2 = value;
        }
        public void writeDouble(int x, int y, double value)
        {
            ws.Cells[y + 1, x + 1].value2 = value;
        }
    }
    //specializált LDS export olvasó osztály, jövőben cserélhető lenne egy SQL kapcsolatra.
    internal class LDSexport {
        string path;
        _Application exe = new _Excel.Application();
        Workbook wb;
        public Worksheet prod;
        public Worksheet downtime;
        public LDSexport(string path) { 
            this.path = path;
            this.wb   = this.exe.Workbooks.Open(path);
            foreach(Worksheet x in wb.Sheets)
            {
                switch (x.Name) {
                    case "Production Summary":
                        prod = x;
                        break;
                    case "Downtime Reasons":
                        downtime = x;
                        break;
                }
            }
        }
        public void Close()
        {
            wb.Close(0);
            exe.Quit();
            Marshal.ReleaseComObject(exe);
        }
        public string readString(int x, int y, Worksheet ws)
        {
            try { return ws.Cells[y + 1, x + 1].Value2; }
            catch { throw new ExitClose(); }
        }
        public double readDouble(int x, int y, Worksheet ws)
        {
            try
            {
                return ws.Cells[y + 1, x + 1].Value2;
            }
            catch { throw new ExitClose(); }
        }
    }
}
