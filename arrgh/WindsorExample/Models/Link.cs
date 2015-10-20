using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindsorExample.Models
{
    public class Link
    {
        [Key]
        public int LinkID { get; set; }
        [ForeignKey("Kpis")]
        public int KpiID { get; set; }
        [ForeignKey("Reports")]
        public int ReportID { get; set; }

        public virtual KpiViewModel Kpis { get; set; }
        public virtual ReportViewModel Reports { get; set; }
    }
}