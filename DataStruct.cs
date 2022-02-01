using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace LDS_Feldolgozo
{
    public enum LineType
    {
        Line,
        Group,
        Area
    }
    public class Line
    {
        public string name;
        public double oeeTarget;
        public double sur;
        public List<Shift> shifts;
        public LineType type;
        public Line(string name) {
            this.name = name;
            shifts = new List<Shift>();
            type = LineType.Line;
        }
        public void AddShift(double oeeTarget, double sur, Shift s) {
            shifts.Add(s);
            if (this.oeeTarget == 0)
                this.oeeTarget = oeeTarget;
            if (this.sur == 0)
                this.sur = sur;
        }
        public void AddDowntime(double shift,DateTime date,Downtime dt)
        {
            foreach(Shift s in shifts)
            {
                if(s.shiftID == shift && s.time == date)
                {
                    s.downtimes.Add(dt);
                    break;
                }
            }
        }
        public static int exits(string name, List<Line> l)
        {
            for (int i = 0; i < l.Count; i++)
                if (String.Compare(l[i].name, name) == 0)
                    return i;
            return -1;
        }
        #region line_property
        public double getOee()
        {
            double sum = 0;
            double div = 0;
            foreach (Shift s in shifts)
            {
                sum += s.oee > 2 ? s.oee : 0;
                div += s.oee > 2 ? 1 : 0;
            }
            if (div == 0) return 0;
            else return sum / div;
        }
        public double getOee(int shift, DateTime date)
        {
            double sum = 0;
            double div = 0;
            foreach (Shift s in shifts)
            {
                sum += s.oee > 2 && s.time.Date == date.Date && s.shiftID == shift ? s.oee : 0;
                div += s.oee > 2 && s.time.Date == date.Date && s.shiftID == shift ? 1 : 0;
            }
            if (div == 0) return 0;
            else return sum / div;
        }
        public double getGood()
        {
            double sum = 0;
            foreach (Shift s in shifts)
                sum += s.sumGood;
            return sum;
        }
        public double getGood(int shift, DateTime date)
        {
            double sum = 0;
            foreach (Shift s in shifts)
                sum += s.time.Date == date.Date && s.shiftID == shift ? s.sumGood : 0;
            return sum;
        }
        public double getBad()
        {
            double sum = 0;
            foreach (Shift s in shifts)
                sum += s.sumBad;
            return sum;
        }
        public double getBad(int shift, DateTime date)
        {
            double sum = 0;
            foreach (Shift s in shifts)
                sum += date.Date == s.time.Date && s.shiftID == shift ? s.sumBad : 0;
            return sum;
        }
        public double getRuntime()
        {
            double sum = 0;
            foreach (Shift s in shifts)
                sum += s.oee > 2 ? s.runtime : 0;
            return sum;
        }
        public double getRuntime(int shift, DateTime date)
        {
            double sum = 0;
            foreach (Shift s in shifts)
                sum += s.oee > 2 && s.time.Date == date.Date && s.shiftID == shift ? s.runtime : 0;
            return sum;
        }
        public double downtimeG()
        {
            double sum = 0;
            foreach(Shift s in shifts)
            {
                foreach(Downtime dt in s.downtimes)
                {
                    sum += dt.time;
                }
            }
            return sum;
        }
        public double downtimeG(int shift, DateTime date)
        {
            double sum = 0;
            foreach (Shift s in shifts)
            {
                foreach (Downtime dt in s.downtimes)
                {
                    sum += date.Date == s.time.Date && s.shiftID == shift ? dt.time : 0;
                }
            }
            return sum;
        }
        public double getMTBF(int shift, DateTime date)
        {
            double c = 0;
            foreach(Shift s in shifts)
                if (s.shiftID == shift && date.Date == s.time.Date)
                    c = s.downtimes.Count;
            double t = getRuntime(shift, date);
            if (t == 0)
                return -1; //nem futott
            if (c == 0)
                return -2; //nem állt le
            return t/c;
        }
        public double getMTTR(int shift, DateTime date)
        {
            double c = 0;
            double t = 0;
            foreach(Shift s in shifts)
            {
                if(s.shiftID == shift && date.Date == s.time.Date)
                {
                    c = s.downtimes.Count;
                    foreach(Downtime dt in s.downtimes)
                    {
                        t += dt.time;
                    }
                }
            }
            if (c == 0)
                return -2;
            return (t/60) / c;
        }
        public List<Downtime> topNdowntime(int n)
        {
            List<Downtime> result = new List<Downtime>();
            foreach (Shift s in shifts)
            {
                foreach (Downtime dt in s.downtimes)
                {
                    bool found = false;
                    foreach (Downtime r in result)
                    {
                        if (r.reason == dt.reason)
                        {
                            found = true;
                            r.time+=dt.time;
                        }
                    }
                    if (!found)
                        result.Add(new Downtime(dt.reason,dt.time));
                }
            }
            result.Sort((a, b) => b.time.CompareTo(a.time));
            return result.Take(n).ToList();
        }
        public List<Downtime> topNdowntime(int n,int shift, DateTime date)
        {
            List<Downtime> result = new List<Downtime>();
            foreach (Shift s in shifts)
            {
               if(s.shiftID == shift && s.time.Date == date.Date)
               {
                    foreach (Downtime dt in s.downtimes)
                    {
                        bool found = false;
                        foreach (Downtime r in result)
                        {
                            if (r.reason == dt.reason)
                            {
                                found = true;
                                r.time += dt.time;
                            }
                        }
                        if (!found)
                            result.Add(new Downtime(dt.reason,dt.time));
                    }
               }
            }
            result.Sort((a, b) => b.time.CompareTo(a.time));
            return result.Take(n).ToList();
        }
        #endregion line_property

    }
    public class Shift {
        public DateTime time;
        public double shiftID;

        public double oee;
        public double sumGood;
        public double sumBad;
        public double runtime;

        public List<Downtime> downtimes;
        public Shift(double shiftID, DateTime time, double oee, double sumGood, double sumBad, double runtime) {
            this.shiftID = shiftID;   
            this.time = time;
            this.oee = oee;
            this.sumGood = sumGood;
            this.sumBad = sumBad;
            this.runtime = runtime;
            downtimes = new List<Downtime>();
        }
    }
    public class Downtime {
        public string reason;
        public double time;
        public Downtime(string reason, double time) { 
            this.reason = reason;
            this.time = time;  
        }
    }
}
