using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Tariff
{
    public class ShowLastTariffStatus
    {
        public string CurrentTariffType { get; set; }
        public string CurrentTariffTypeFullDescription { get; set; }
        public int InternetTariff { get; set; }
        public int IrandocTariff { get; set; }
    }
}