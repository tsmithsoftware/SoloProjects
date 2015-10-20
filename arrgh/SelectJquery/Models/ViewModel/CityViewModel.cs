using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelectJquery.Models.ViewModel
{
    /**CityList contains the list of cities based on the selection of country and state/province. SelectedCityID keeps the user selection of city and CityIEnum converts the List of Cities to SelectList, which is needed by dropdown list.**/
    public class CityViewModel
    {
        public List<Models.DataModel.CityDataModel> CityList = new List<Models.DataModel.CityDataModel>();
        public Int32 SelectedCityID { get; set; }
        public IEnumerable<SelectListItem> CityIEnum
        {
            get
            {
                return new SelectList(CityList, "ID", "CityName");
            }
        }
    }
}