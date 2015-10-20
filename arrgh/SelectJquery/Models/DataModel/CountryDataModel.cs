using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelectJquery.Models.DataModel
{
    public class CountryDataModel
    {
        public Int32 ID { get; set; }
        public String CountryName { get; set; }
        public List<StateProvinceDataModel> StateOrProvinceList = new List<StateProvinceDataModel>();
    }
}