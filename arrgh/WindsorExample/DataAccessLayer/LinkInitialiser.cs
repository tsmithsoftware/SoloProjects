using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WindsorExample.Models;

namespace WindsorExample.DataAccessLayer
{
    public class LinkInitialiser:System.Data.Entity.DropCreateDatabaseAlways<LinkContext>
    {
        public override void InitializeDatabase(LinkContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
            , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        protected override void Seed(LinkContext context)
        {
            var reports = new List<ReportViewModel>();

            for (int i = 0; i < 4; i++)
            {
                reports.Add(createNewReportViewModel("Report" + i, i));
            }
            
            reports.ForEach(s => context.Reports.Add(s));
            context.SaveChanges();

            var kpis = new List<KpiViewModel>();

            for (int i = 0; i < 16; i++)
            {
                kpis.Add(createNewKpiViewModel("Kpi" + i, i));
            }

            kpis.ForEach(s => context.Kpis.Add(s));
            context.SaveChanges();

            var links = new List<Link>()
            {
               CreateNewLink(1,1,1),
               CreateNewLink(1,2,2),
               CreateNewLink(2,3,2),
               CreateNewLink(3,4,3),
               CreateNewLink(4,5,4),
            };

            links.ForEach(s=>context.Links.Add(s));
        }

        private Link CreateNewLink(int reportId, int linkId, int kpiId)
        {
            return new Link()
            {
                ReportID = reportId,
                LinkID = linkId,
                KpiID = kpiId
            };
        }

        private KpiViewModel createNewKpiViewModel(string kpiName, int id)
        {
            return new KpiViewModel()
            {
                IsSelected = false,
                KpiId = id,
                KpiName = kpiName
            };
        }

        private ReportViewModel createNewReportViewModel(string reportName, int id)
        {
            return new ReportViewModel()
            {
                IsSelected = false,
                ReportId =id,
                ReportName = reportName
            };
        }
    }
}