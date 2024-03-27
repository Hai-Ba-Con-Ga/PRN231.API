using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.ReportData
{
    public class DataReportResponse
    {
        public string DataValue { get; set; } = null!;
        public string DataUnit { get; set; } = null!;
        public int TypeId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
