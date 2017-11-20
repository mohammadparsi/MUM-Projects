using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{
    public sealed class Float : Infrastructure.BusinessLogic.SingleTariff
    {

        public int CalculatePrice(bool mustSearchInIrandoc, bool mustSearchInInternet)
        {
            int intIrandoc = 0;
            int intInternet = 0;

            if (mustSearchInIrandoc)
            {
                intIrandoc = 1;    
            }

            if (mustSearchInInternet)
            {
                intInternet = 1;
            }

            base.FetchTariffFromDatabase();

            string strText =MVC.Assignment.GetTextByFileNameInServer(this.AssignmentFile.FileNameInServer);

           int? intPaymentAmount =
                System.Text.ASCIIEncoding.Unicode.GetByteCount(strText) / 1000 *
                (intIrandoc * this.IrandocTariff + intInternet * this.InternetTariff);

           return intPaymentAmount.Value;
        }
            
         public static double CalculateVolume(int paidAmountInRial, bool mustSearchInIrandoc, bool mustSearchInInternet, int irandocTariff, int internetTariff)
        {
            int intIrandoc = 0;
            int intInternet = 0;

            if (mustSearchInIrandoc)
            {
                intIrandoc = 1;
            }

            if (mustSearchInInternet)
            {
                intInternet = 1;
            }

            double dblVolume =
                (double)paidAmountInRial / (intIrandoc * irandocTariff + intInternet * internetTariff);

            return dblVolume;
        }



         public static int CalculateMoney(double usedVolume, bool mustSearchInIrandoc, bool mustSearchInInternet, int irandocTariff, int internetTariff)
         {
             int intIrandoc = 0;
             int intInternet = 0;

             if (mustSearchInIrandoc)
             {
                 intIrandoc = 1;
             }

             if (mustSearchInInternet)
             {
                 intInternet = 1;
             }

             //double dblVolume =
             //    (double)paidAmountInRial / (intIrandoc * irandocTariff + intInternet * internetTariff);

             int intPaidAmountInRial = (int)usedVolume * (intIrandoc * irandocTariff + intInternet * internetTariff); 

             return intPaidAmountInRial;
         }


         public override int CalculatePrice()
         {
             throw new NotImplementedException();
         }
    }
}
