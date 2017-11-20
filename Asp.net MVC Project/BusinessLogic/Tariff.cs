using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public abstract class Tariff
    {
        public abstract int CalculatePrice(MvcEshop.Models.Tariff tariff);
        public MvcEshop.Models.Tariff Tariff;
    }
}
