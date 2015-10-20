using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WindsorExample.Models
{
    public class KpiViewModel
    {
        [Key]
        public int KpiId { get; set; }

        //used to bind the checkbox value in the view
        public bool IsSelected { get; set; }

        public string KpiName { get; set; }

        public virtual ICollection<ReportViewModel> Reports { get; set; }
    }
}