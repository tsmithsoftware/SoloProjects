using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WindsorExample.Models
{
    public class ReportViewModel
    {
        [Key]
        public int ReportId { get; set; }

        //used to bind the checkbox value in the view
        public bool IsSelected { get; set; }

        public string ReportName { get; set; }

        public virtual ICollection<KpiViewModel> Kpis { get; set; }
    }
}