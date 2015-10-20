using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindsorExample.Models
{
    public class AssignWorkViewModel
    {
        public List<ReportViewModel> Reports { get; set; }
        public List<KpiViewModel> Kpis { get; set; } 
        public bool IsReportsSelected { get; set; }
    }
}