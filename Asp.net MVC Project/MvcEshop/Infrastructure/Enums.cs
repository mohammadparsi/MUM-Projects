using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class Enums
    {
       public enum DeleteTypes { SystemDelete, RemoveFromAccount };
       public enum MessageTypes { failure, success}
       public enum GiveCreditApproaches { BasedOnTariff, Free }
       public enum UploadingType { Group, Single}
    }
}