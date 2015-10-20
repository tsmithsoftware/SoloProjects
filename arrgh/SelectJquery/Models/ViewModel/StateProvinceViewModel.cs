using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelectJquery.Models.ViewModel
{
    /**StateProvinceList contains the list of state/provinces based on the selection of country. SelectedStateProvinceID keeps the user selection of state/province and StateProvinceIEnum converts the list of state/province to SelectList, which is needed by drop down list.**/
    public class StateProvinceViewModel
    {
        public List<Models.DataModel.StateProvinceDataModel> StateProvinceList = new List<Models.DataModel.StateProvinceDataModel>();
        public Int32 SelectedStateProvinceID { get; set; }
        public IEnumerable<SelectListItem> StateProvinceIEnum
        {
            get
            {
                return new SelectList(StateProvinceList, "ID", "StateOrProvinceName");
            }
        }
    }
}