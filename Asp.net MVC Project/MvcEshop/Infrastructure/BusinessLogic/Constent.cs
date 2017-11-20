using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public sealed class Constent : Infrastructure.BusinessLogic.SingleTariff
    {

        public override int CalculatePrice()
        {
            base.FetchTariffFromDatabase();

            int? intPaymentAmount = this.IrandocTariff + this.InternetTariff;

            return intPaymentAmount.Value;
        }


    }
}
