using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcEshop;

namespace BusinessLogic
{
    class Float:Tariff
    {

        public override int CalculatePrice(MvcEshop.Models.Tariff tariff)
        {
            
        }



        public override int CalculatePrice()
        {
            throw new NotImplementedException();
        }
    }
}
