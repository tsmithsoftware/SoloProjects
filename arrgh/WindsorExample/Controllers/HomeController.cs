    using NLog;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations.Model;
    using System.Diagnostics;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;
    using Castle.Core.Internal;
    using WindsorExample.Filters;
    using WindsorExample.Models;

    namespace WindsorExample.Controllers
    {
    public class HomeController : Controller
    {
    private static Logger logger = LogManager.GetCurrentClassLogger();

    [LogActionFilter]//attribute to add filter
    [ExceptionFilter]//attribute to add exception filter
    public ActionResult Index()
    {
        /**bool isValid = false;
        // create a "principal context" - e.g. your domain (could be machine, too)
        PrincipalContext pc = new PrincipalContext(ContextType.Machine, System.Environment.UserDomainName);
        {
            // validate the credentials
            isValid = pc.ValidateCredentials("TSmith", "8DZwpejk");
        }
        if(!isValid) throw new Exception("Username/Password invalid");//for use with both LoggingAspect and ExceptionFilter**/
        Debug.WriteLine("HellO");
        logger.Debug("Loading Index Page...");
        return View();
    }
        [LogActionFilter]
        [ExceptionFilter]
        public ActionResult Script()
        {
            AssignWorkViewModel model = new AssignWorkViewModel();
            List<ReportViewModel> reports = new List<ReportViewModel>();
            List<KpiViewModel> kpis = new List<KpiViewModel>();
            try
            {
                model = BuildAssignWorkViewModel(reports, kpis);
                //pass model to the view
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return View(e.Message);
            }
            return View(model);
        }

    public ActionResult About()
    {
        ViewBag.Message = "Your application description page.";

        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Your contact page.";

        return View();
    }

        public ActionResult Responsive()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Select2()
        {
            ViewBag.Message = "Your select2 page.";

            return View();
        }

        public ActionResult BiPartite()
        {
            return View(BuildAssignWorkViewModel(new List<ReportViewModel>(), new List<KpiViewModel>()));
        }

        /**
        *Return updated model depending on whether user selects KPI/Report
        *@param "kpi" or "report"
        **/
        public ActionResult Selector(string someValue)
        {
            AssignWorkViewModel model = new AssignWorkViewModel();

            if (someValue.Equals("kpi"))
            {
                model.IsReportsSelected = false;
            }
            else
            {
                model.IsReportsSelected = true;
            }
                model.Kpis = BuildKpis(new List<KpiViewModel>());
                model.Reports = BuildReports(new List<ReportViewModel>());
                //build and return report model
            return View(model);
        }


        //GET/SecondScript
        //http://stackoverflow.com/questions/14853643/fill-select2-dropdown-box-from-database-in-mvc-4
        [HttpGet]
        public ActionResult SecondScript()
        {
            return View(BuildAssignWorkViewModel(new List<ReportViewModel>(), new List<KpiViewModel>()));
           /** return View(new AssignWorkViewModel()
            {
                Reports = BuildReports(new List<ReportViewModel>())
            }
                );**/
        }

        [HttpGet]
        public ActionResult ScriptCopy()
        {
            return View(BuildAssignWorkViewModel(new List<ReportViewModel>(), new List<KpiViewModel>()));
            /** return View(new AssignWorkViewModel()
             {
                 Reports = BuildReports(new List<ReportViewModel>())
             }
                 );**/
        }

        //GET/AssignWork
        [HttpGet]
    public ActionResult AssignWork()
    {
            AssignWorkViewModel model = new AssignWorkViewModel();
            List<ReportViewModel> reports = new List<ReportViewModel>();
            List<KpiViewModel> kpis = new List<KpiViewModel>();
            try
            {
                model = BuildAssignWorkViewModel(reports, kpis);
                //pass model to the view
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return View(e.Message);
            }
            return View(model);
        }

    //POST/AssignWork
    /**
    *Below code would be used to connect and update database in solution, rather than updating view. Maybe to return a "success"/"error" page, or alert and redirect to main
    **/
        [HttpPost]
        public ActionResult AssignWork(AssignWorkViewModel workModel)
        {
            ModelState.Clear();//http://stackoverflow.com/questions/1775170/asp-net-mvc-modelstate-clear
            Debug.WriteLine("Is KPI null or empty: " + workModel.Kpis.IsNullOrEmpty());
            Debug.WriteLine("Is Reports null or empty: " + workModel.Reports.IsNullOrEmpty());
            List<KpiViewModel> kpiViewModels = workModel.Kpis.Where(m => m.IsSelected).ToList();
            List<ReportViewModel> reportViewModels = workModel.Reports.Where(n => n.IsSelected).ToList();
            bool update = updateLinkInDatabase(kpiViewModels,reportViewModels);
            if (!update)
            {
                return View("Error");
            }
            return View(workModel);
        }

        /**
        *Update the link in the database - straight INSERT?
        **/
        private bool updateLinkInDatabase(List<KpiViewModel> kpiViewModels, List<ReportViewModel> reportViewModels)
        {
            List<KpiViewModel> kpisList = kpiViewModels;
            List<ReportViewModel> reportsList = reportViewModels;
            return true;
        }

        public ActionResult UpdateLink(String[] kpi, String[] report)
        {
            Debug.WriteLine("KPI's: ");
            foreach (String kpiName in kpi)
            {
                Debug.WriteLine(kpiName);
            }
            Debug.WriteLine("Reports: ");
            foreach (String reportName in report)
            {
                Debug.WriteLine(reportName);
            }
            return View("Success");
        }

        private AssignWorkViewModel BuildAssignWorkViewModel(List<ReportViewModel> reports, List<KpiViewModel> kpis)
    {
        AssignWorkViewModel model = new AssignWorkViewModel();
        //let's add some values - would be drawn from a database (currently would be the local mcrs database)
        reports = BuildReports(reports);
        kpis = BuildKpis(kpis);

        model.Reports = reports;
        model.Kpis = kpis;

        return model;
    }

        private List<ReportViewModel> BuildReports(List<ReportViewModel> reports)
        {
            reports.Add(createReport("Report on steel production in India", reports.Count));
            reports.Add(createReport("Report on iron production in Zimbabwe", reports.Count));
            reports.Add(createReport("Report on titanium production in Maou", reports.Count));
            return reports;
        }

        private List<KpiViewModel> BuildKpis(List<KpiViewModel> kpis)
        {
            kpis.Add(createKPI("Percentage of workers off sick", kpis.Count));
            kpis.Add(createKPI("Percentage of successful deliveries", kpis.Count));
            kpis.Add(createKPI("Cost/Benefit Ratio", kpis.Count));
            kpis.Add(createKPI("Average Changeover Time", kpis.Count));
            kpis.Add(createKPI("OEE", kpis.Count));
            kpis.Add(createKPI("Mean time between failure", kpis.Count));

            return kpis;
        } 

        private KpiViewModel createKPI(string title, int count)
    {
        KpiViewModel kpi = new KpiViewModel();
        kpi.KpiId = count;
        kpi.IsSelected = false;
        kpi.KpiName = title;

        return kpi;
    }

    private ReportViewModel createReport(string item, int position)
    {
        ReportViewModel reportOne = new ReportViewModel();
        reportOne.ReportId = position;
        reportOne.IsSelected = false;
        reportOne.ReportName = item;
        return reportOne;
    }

        /**The Json controller method below returns a list of serialised Json objects, whose ItemName contains the string 'query' provided (this 'query' comes from the search box in the Select2 drop box).**/
        public JsonResult FetchItems(string query)
        {
            List<ReportViewModel> reportsList = BuildReports(new List<ReportViewModel>()); //fetch list of reports from db table
            List<ReportViewModel> resultsList = new List<ReportViewModel>(); //create empty results list
            foreach (var item in reportsList)
            {
                //if any item contains the query string
                if (item.ReportName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultsList.Add(item); //then add item to the results list
                }
            }
            resultsList.Sort(delegate (ReportViewModel c1, ReportViewModel c2) { return c1.ReportName.CompareTo(c2.ReportName); }); //sort the results list alphabetically by ItemName
            var serialisedJson = from result in resultsList //serialise the results list into json
                                 select new
                                 {
                                     name = result.ReportName, //each json object will have 
                                     id = result.ReportId      //these two variables [name, id]
                                 };
            return Json(serialisedJson, JsonRequestBehavior.AllowGet); //return the serialised results list
        }
    }
    }
     