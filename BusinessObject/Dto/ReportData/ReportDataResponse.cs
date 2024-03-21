﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.ReportData
{
    public class ReportDataResponse
    {
        public string Data { get; set; } = null!;
        public string DataUnit { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}