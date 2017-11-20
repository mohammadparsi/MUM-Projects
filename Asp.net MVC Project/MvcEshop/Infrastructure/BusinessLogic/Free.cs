using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
   public sealed class Free : Infrastructure.BusinessLogic.SingleTariff
    {

        public override int CalculatePrice()
        {
            return 0;
        }


    }
}
