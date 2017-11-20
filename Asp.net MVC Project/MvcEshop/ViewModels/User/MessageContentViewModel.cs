using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.User
{
    public class MessageContentViewModel
    {
        public MessageContentViewModel()
        {

        }

        public Infrastructure.Enums.MessageTypes MessageType { get; set; }
        public string Message { get; set; }
    }
}