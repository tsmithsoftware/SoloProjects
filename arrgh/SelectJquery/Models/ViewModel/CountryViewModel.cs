using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelectJquery.Models.ViewModel
{
    /**CountryList contains the list of countries. SelectedCountryID keeps the user selection of country and CountryIEnum converts the list of country to SelectList, which is need by dropdown list. **/
    public class CountryViewModel
    {
        public List<Models.DataModel.CountryDataModel> CountryList =
        new List<Models.DataModel.CountryDataModel>();
        public Int32 SelectedCountryID { get; set; }
        public IEnumerable<SelectListItem> CountryIEnum
        {
            get
            {
                return new SelectList(CountryList, "ID", "CountryName");
            }
        }
    }
}