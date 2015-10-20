using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelectJquery.Models.DataModel
{
    public class StateProvinceDataModel
    {
        public Int32 ID { get; set; }
        public String StateOrProvinceName { get; set; }
        public List<CityDataModel> CityList = new List<CityDataModel>();
    }
}