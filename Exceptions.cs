using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS_Feldolgozo
{
    public class NoLDSexportException : Exception
    {
        public NoLDSexportException() { }
        public NoLDSexportException(string message) : base(message) { }
        public NoLDSexportException(string message, Exception inner) : base(message, inner) { }
    }
    public class NoExportPathException : Exception
    {
        public NoExportPathException() { }
        public NoExportPathException(string message) : base(message) { }
        public NoExportPathException(string message,Exception inner) : base(message, inner) { }
    }
    public class ExitClose : Exception
    {
        public ExitClose() { }
    }
}
