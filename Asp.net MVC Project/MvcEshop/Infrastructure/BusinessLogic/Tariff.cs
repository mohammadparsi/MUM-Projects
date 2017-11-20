using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessLogic
{

    public abstract class SingleTariff
    {
        //private SingleTariff()
        //{

        //}

        private static System.Guid _assignmentFileId;

        public static System.Guid AssignmentFileId
        {
            //get { return _assignmentFileId; }
            set { _assignmentFileId = value; }
        }



        public int? PaymentAmount
        {
            get { return _paymentAmount; }
            set { _paymentAmount = value; }
        }

        public int? InternetTariff
        {
            get { return _internetTariff; }
            //set { _internetTariff = value; }
        }

        public int? IrandocTariff
        {
            get { return _irandocTariff; }
            //set { _irandocTariff = value; }
        }

        private static int? _paymentAmount = 0;
        private static int? _internetTariff = 0;
        private static int? _irandocTariff = 0;

        private static MvcEshop.Models.DFEntities _dfEntities = MVC.User.DFEntities;

        //private static readonly Infrastructure.BusinessLogic.SingleTariff _singleTariff =
        //    new Infrastructure.BusinessLogic.SingleTariff();

        private static MvcEshop.Models.Tariff _tariff = _dfEntities.Tariffs.FirstOrDefault();

        private static MvcEshop.Models.AssignmentFile _assignmentFile;
        //public static Infrastructure.BusinessLogic.SingleTariff SingleTariffInstance
        //{
        //    get
        //    {
        //        return _singleTariff;
        //    }
        //}

        public static MvcEshop.Models.Tariff Tariff
        {
            get
            {
                return _tariff;
            }

            set
            {
                _tariff = value;
            }
        }

        public MvcEshop.Models.AssignmentFile AssignmentFile
        {
            get { return _assignmentFile; }

        }

        //public MvcEshop.Models.DFEntities DFEntities
        //{

        //    set { _dfEntities = value; }
        //}

        public abstract int CalculatePrice();

        public void FetchTariffFromDatabase()
        {
            _assignmentFile = _dfEntities.AssignmentFiles
                 .Where(current => current.FileId == _assignmentFileId)
                     //&& current.IsDeleted == false)
                 .ToList()
                 .FirstOrDefault();

            //if search-in-Irandoc has been chosen by user
            if (_assignmentFile.SearchInIrandoc)
            {
                _irandocTariff = _dfEntities.Resources
                       .Where(current => current.Id == 1)
                       .Select(current => current.Price)
                       .FirstOrDefault();

            }

            //if search-in-Internet has been chosen by user
            if (_assignmentFile.SearchInInternet)
            {
                _internetTariff = _dfEntities.Resources
                       .Where(current => current.Id == 2)
                       .Select(current => current.Price)
                       .FirstOrDefault();

            }
        }
    }
}
