using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS_Feldolgozo
{
    /*
     *  Area 1
     *      Group 1
     *          Line 1
     *              ...
     *          Line 2
     *              ...
     *      Group 2
     *          ...
     *      Group 3
     *          ...
     *  Area 2
     *      ...
     */
    internal class Area
    {
        public string name;
        public List<Group> groups;
        public Area(string name)
        {
            this.name = name;
            groups = new List<Group>();
        }
        public static int Exists(string name, List<Area> l)
        {
            for (int i = 0; i < l.Count; ++i)
                if (name == l[i].name)
                    return i;
            return -1;
        }
        //könnyű kiírás céljából
        public Line FakeLine()
        {
            Line res = new Line(name);
            res.type = LineType.Area;
            int lc = 0;
            foreach(Group g in groups)
            {
                Line tmp = g.FakeLine();
                res.oeeTarget+=tmp.oeeTarget;
                res.sur +=tmp.sur;
                foreach (Shift s in tmp.shifts)
                    res.shifts.Add(s);
                lc+=g.lines.Count;
            }
            res.oeeTarget /= lc;
            res.sur /= lc;
            return res;
        }
    }
    internal class Group
    {
        public string name;
        public List<string> lineNames;
        public List<Line> lines;
        public Group(string name)
        {
            this.name = name;
            lines = new List<Line>();
            lineNames = new List<string>();
        }
        public static int Exists(string name, List<Group> l)
        {
            for (int i = 0; i < l.Count; ++i)
                if (name == l[i].name)
                    return i;
            return -1;
        }
        //könnyű kiírás céljából
        public Line FakeLine()
        {
            Line res = new Line(name);
            res.type = LineType.Group;
            foreach (Line l in lines)
            {
                res.oeeTarget += l.oeeTarget;
                res.sur += l.sur;
                foreach(Shift s in l.shifts)
                    res.shifts.Add(s);
            }
            res.oeeTarget /= lines.Count;
            res.sur/=lines.Count;
            return res;

        }
    }
}
