using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelectJquery.Controllers
{
    public class HomeController : Controller
    {
        //Using a static list to initialise collection. Would be a db call
        public static List<Models.DataModel.CountryDataModel> Countries = new List<Models.DataModel.CountryDataModel>()
         {
        new Models.DataModel.CountryDataModel
                { ID = 1, CountryName = "USA",
                            StateOrProvinceList =
                            new List<Models.DataModel.StateProvinceDataModel>()
                            {
                                new Models.DataModel.StateProvinceDataModel
                                {
                                    ID = 1, StateOrProvinceName = "Michigan",
                                    CityList = new List<Models.DataModel.CityDataModel>()
                                    {
                                        new Models.DataModel.CityDataModel {ID = 1,
                                                                CityName = "Detroit"},
                                        new Models.DataModel.CityDataModel {ID = 2,
                                                                CityName = "Saginaw"},
                                        new Models.DataModel.CityDataModel {ID = 3,
                                                                CityName = "Troy"}
                                    }
                                },
                new Models.DataModel.StateProvinceDataModel
                                {
                                    ID = 2, StateOrProvinceName = "New York",
                                    CityList = new List<Models.DataModel.CityDataModel>()
                                    {
                                        new Models.DataModel.CityDataModel {ID = 1,
                                                                CityName = "Manhatan"},
                                        new Models.DataModel.CityDataModel {ID = 2,
                                                             CityName = "Woodside"},
                                        new Models.DataModel.CityDataModel {ID = 3,
                                                             CityName = "Jackson Height"}
                                    }
                                },
                new Models.DataModel.StateProvinceDataModel
                                {
                                    ID = 3, StateOrProvinceName = "Illinois",
                                    CityList = new List<Models.DataModel.CityDataModel>()
                                    {
                                        new Models.DataModel.CityDataModel {ID = 1,
                                                              CityName = "Chicago"},
                                        new Models.DataModel.CityDataModel {ID = 2,
                                                              CityName = "Aurora"},
                                        new Models.DataModel.CityDataModel {ID = 3,
                                                              CityName = "Benton"}
                                    }
                                }
                            }
                },
        new Models.DataModel.CountryDataModel
                { ID = 2, CountryName = "Canada",
                            StateOrProvinceList =
                            new List<Models.DataModel.StateProvinceDataModel>()
                            {
                                new Models.DataModel.StateProvinceDataModel
                                {
                                    ID = 1, StateOrProvinceName = "Ontario",
                                    CityList = new List<Models.DataModel.CityDataModel>()
                                    {
                                        new Models.DataModel.CityDataModel {ID = 1,
                                                             CityName = "Windsor"},
                                        new Models.DataModel.CityDataModel {ID = 2,
                                                             CityName = "Toronto"},
                                        new Models.DataModel.CityDataModel {ID = 3,
                                                             CityName = "Oshawa"}
                                    }
                                },
                                new Models.DataModel.StateProvinceDataModel
                                {
                                    ID = 2, StateOrProvinceName = "Quibec",
                                    CityList = new List<Models.DataModel.CityDataModel>()
                                    {
                                        new Models.DataModel.CityDataModel {ID = 1,
                                                              CityName = "Montreal"},
                                        new Models.DataModel.CityDataModel {ID = 2,
                                                              CityName = "Quibec City"}
                                    }
                                }
                            }
                }
        };

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return base.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return base.View();
        }

        public ActionResult Select()
        {
            ViewBag.Message = "Your contact page.";

            return base.View();
        }
        /**To populate country data, define an action method name CountryView. This method will populate data from the list of CountryDataModel into CountryViewModel and return the data to CountryView. Code for the action method is as follows: **/
        public static Models.ViewModel.CountryViewModel cvm =
                           new Models.ViewModel.CountryViewModel();

        public ActionResult CountryView()
        {
            cvm.CountryList.Clear();
            foreach (Models.DataModel.CountryDataModel cd in Countries)
            {
                cvm.CountryList.Add(cd);
            }
            return View(cvm);
        }

        /**To populate state/province data, define an action method named StateProvinceView with a parameter for selected country id. This method will populate data from the list of StateProvinceDataModel into StateProvinceViewModel based on the selected country. Code for the action method is as follows: **/
        public static Models.ViewModel.StateProvinceViewModel spvm =
                   new Models.ViewModel.StateProvinceViewModel();

        public ActionResult StateProvinceView(int? countryID)
        {
            spvm.StateProvinceList.Clear();
            if (countryID != null)
            {
                Models.DataModel.CountryDataModel cd =
                               Countries.Find(p => p.ID == countryID);
                foreach (Models.DataModel.StateProvinceDataModel spd
                                     in cd.StateOrProvinceList)
                {
                    spvm.StateProvinceList.Add(spd);
                }
            }
            return View(spvm);
        }

        /**To populate city data, define another action method name CityView with selected country and state/province id as parameters. This method will populate data from the list of CityDataModel into CityViewModel based on the selected country and state/province ids. Code for the action method is as follows: **/
        public static Models.ViewModel.CityViewModel cityvm =
                      new Models.ViewModel.CityViewModel();
        public ActionResult CityView(int? countryID, int? stateprovinceID)
        {
            cityvm.CityList.Clear();
            if (countryID != null && stateprovinceID != null)
            {
                Models.DataModel.CountryDataModel cd =
                                 Countries.Find(p => p.ID == countryID);
                Models.DataModel.StateProvinceDataModel spd =
                         cd.StateOrProvinceList.Find(p => p.ID == stateprovinceID);
                foreach (Models.DataModel.CityDataModel cpd in spd.CityList)
                {
                    cityvm.CityList.Add(cpd);
                }
            }
            return View(cityvm);
        }
    }
}
 
 