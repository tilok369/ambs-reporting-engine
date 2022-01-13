using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambs.Reporting.Engine.Model
{
    public class AmbsExportData
    {
        public List<string> Columns { get; set; }
        public List<List<string>> Rows { get; set; }
        public List<AmbsDataLayer> Layers { get; set; }
    }
}
