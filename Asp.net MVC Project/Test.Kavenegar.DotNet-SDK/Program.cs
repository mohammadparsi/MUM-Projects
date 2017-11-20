using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using Kavenegar.SDK;
using Kavenegar.SDK.Exceptions;
using Kavenegar.SDK.Models;
using Kavenegar.SDK.Models.Enums;

namespace Kavenegar.SDK.Test
{
    public class Program
    {
        static KavenegarApi Api;
        static string _receptor;
        static string _message;

        static void Main(string[] args)
        {
            SendSMS("09102205526", "hello");
        }
        public static void SendSMS(string receptor, string message)
        {

            //try
            //{
            //    KavenegarApi api = new KavenegarApi("5243374B47776F377143375A6D54765061626A572F413D3D");
            //    var res = api.Send("3000123432", "09102205526", "خدمات پیام کوتاه کاوه نگار");

            //    //foreach (SendResult r in ret1)
            //    //{
            //    //    Console.Write("r.Messageid.ToString()");  //Collect MessageId(s) and store them
            //    //}
            //}
            //catch (Exceptions.ApiException ex)
            //{
            //    // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
            //    Console.Write("Message : " + ex.Message);
            //}
            //catch (Exceptions.HttpException ex)
            //{
            //    // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
            //    Console.Write("Message : " + ex.Message);
            //}

            //Console.ReadKey();


            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            Api = new KavenegarApi("2F524F50565A507664637342482B594F6973685A52513D3D");

            _receptor = receptor;
            _message = message;

            Send();
            //Send_2();

            //SendArray();
            //SendArray_2();

            //Status();
            //Status(new List<String> { "56571749", "56567804" });

            //StatusLocalMessageId();
            //StatusLocalMessageId(new List<String> { "5959", "5957" });

            //Select();
            //Select(new List<String> { "56571749", "56567804" });

            //CountInbox();

            //CountOutbox();

            //SelectOutbox();

            //LatestOutbox();

            //Receive();

            //CountPostalCode();

            //AccountInfo();

            //AccountConfig();

            //VerifyLookup();

            //Console.ReadKey();
        }

        /*
        AccountConfig
     */
        static void AccountConfig()
        {
            try
            {
                string apilogs = "enabled";
                string dailyreport = "enabled";
                string debugmode = "enabled";
                String defaultsender = "30006703323323";
                int? mincreditalarm = 1000;
                string resendfailed = "enabled";
                var ret1 = Api.AccountConfig(apilogs, dailyreport, debugmode, defaultsender, mincreditalarm, resendfailed);
                //Console.WriteLine(ret1.ApiLogs.ToString() + "\n");
                //Console.WriteLine(ret1.DailyReport.ToString() + "\n");
                //Console.WriteLine(ret1.DebugMode.ToString() + "\n");
                //Console.WriteLine(ret1.DefaultSender.ToString() + "\n");
                //Console.WriteLine(ret1.MinCreditAlarm.ToString() + "\n");
                //Console.WriteLine(ret1.ResendFailed.ToString() + "\n");
            }
            catch (Kavenegar.SDK.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (Kavenegar.SDK.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }

        /*
            VerifyLookup
        */
        static void VerifyLookup()
        {
            try
            {
                string receptor = "09361234567";
                string token = "123";
                String template = "test";
                var ret1 = Api.VerifyLookup(receptor, token, template);
                if (ret1 != null)
                {
                    //Console.WriteLine(ret1.Cost.ToString() + "\n");
                    //Console.WriteLine(ret1.Date.ToString() + "\n");
                    //Console.WriteLine(ret1.Message.ToString() + "\n");
                    //Console.WriteLine(ret1.Receptor.ToString() + "\n");
                    //Console.WriteLine(ret1.Sender.ToString() + "\n");
                    //Console.WriteLine(ret1.Status.ToString() + "\n");
                    //Console.WriteLine(ret1.StatusText.ToString() + "\n");
                }
            }
            catch (Kavenegar.SDK.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (Kavenegar.SDK.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }

        /*
            Send
        */
        static void Send()
        {
            try
            {
                String sender = "30006703323323";
                String receptor = _receptor;
                String message = _message;
                DateTime date = DateTime.Now;
                MessageType type = MessageType.MobileMemory;
                String localid = "";
                SendResult res = Api.Send(sender, receptor, message, localid);
                //Console.Write("Messageid : " + res.Messageid + "\r\n");
                //Console.Write("Message : " + res.Message + "\r\n");
                //Console.Write("Status : " + res.Status + "\r\n");
                //Console.Write("Statustext : " + res.StatusText + "\r\n");
                //Console.Write("Receptor : " + res.Receptor + "\r\n");
                //Console.Write("Sender : " + res.Sender + "\r\n");
                //Console.Write("Date : " + res.Date + "\r\n");
                //Console.Write("Cost : " + res.Cost + "\r\n");
                //Console.Write("\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }
        static void Send_2()
        {
            try
            {
                String sender = "30006703323323";
                List<String> receptors = new List<String>() { "09362985956", "09362985956" };
                String message = "Test CSharp SDK : Send";
                String localid = "94751643";
                List<SendResult> res = Api.Send(sender, receptors, message, localid);
                foreach (SendResult r in res)
                {
                    //Console.Write("Messageid : " + r.Messageid + "\r\n");
                    //Console.Write("Message : " + r.Message + "\r\n");
                    //Console.Write("Status : " + r.Status + "\r\n");
                    //Console.Write("Statustext : " + r.StatusText + "\r\n");
                    //Console.Write("Receptor : " + r.Receptor + "\r\n");
                    //Console.Write("Sender : " + r.Sender + "\r\n");
                    //Console.Write("Date : " + r.Date + "\r\n");
                    //Console.Write("Cost : " + r.Cost + "\r\n");
                    //Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }

        /*
            SendArray
        */
        static void SendArray()
        {
            try
            {
                var senders = new List<string>() { "10008284" };
                var receptors = new List<string>() { "09125089676" };
                var messages = new List<string>() { "خدمات پیام کوتاه کاوه نگار" };
                List<SendResult> res = Api.SendArray(senders, receptors, messages);
                foreach (SendResult r in res)
                {
                    var messageids = new List<String>() { r.Messageid.ToString() };
                    Status(messageids);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }
        static void SendArray_2()
        {
            try
            {
                List<string> senders = new List<string>() { "10008284" };
                List<string> receptors = new List<string>() { "09125089676" };
                List<string> messages = new List<string>() { "خدمات پیام کوتاه کاوه نگار" };
                string localmessageids = "94761458";
                List<SendResult> res = Api.SendArray(senders, receptors, messages, localmessageids);
                foreach (SendResult r in res)
                {
                    var messageids = new List<String>() { r.Messageid.ToString() };
                    Status(messageids);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }

        /*
            Status
        */
        static void Status()
        {
            try
            {
                string messageid = "56571749";
                StatusResult result = Api.Status(messageid);
                //Console.Write("Messageid : " + result.Messageid + "\r\n");
                //Console.Write("Status : " + result.Status + "\r\n");
                //Console.Write("Statustext : " + result.Statustext + "\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }
        static void Status(List<String> messageids)
        {
            try
            {
                List<StatusResult> results = Api.Status(messageids);
                foreach (StatusResult result in results)
                {
                    //Console.Write("Messageid : " + result.Messageid + "\r\n");
                    //Console.Write("Status : " + result.Status + "\r\n");
                    //Console.Write("Statustext : " + result.Statustext + "\r\n");
                    //Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }

        /*
            StatusLocalMessageId
        */
        static void StatusLocalMessageId()
        {
            try
            {
                String localid = "5956";
                StatusLocalMessageIdResult result = Api.StatusLocalMessageId(localid);
                //Console.Write("Messageid : " + result.Messageid + "\r\n");
                //Console.Write("Status : " + result.Status + "\r\n");
                //Console.Write("Statustext : " + result.Statustext + "\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }
        static void StatusLocalMessageId(List<String> localids)
        {
            try
            {
                List<StatusLocalMessageIdResult> results = Api.StatusLocalMessageId(localids);
                foreach (StatusLocalMessageIdResult result in results)
                {
                    //Console.Write("Messageid : " + result.Messageid + "\r\n");
                    //Console.Write("Status : " + result.Status + "\r\n");
                    //Console.Write("Statustext : " + result.Statustext + "\r\n");
                    //Console.Write("\r\n");

                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                //Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                //Console.Write("Message : " + ex.Message);
            }
        }


        /*
            Select
        */
        static void Select()
        {
            try
            {
                string messageid = "56571749";
                SendResult result = Api.Select(messageid);
                Console.Write("Messageid : " + result.Messageid + "\r\n");
                Console.Write("Message : " + result.Message + "\r\n");
                Console.Write("Status : " + result.Status + "\r\n");
                Console.Write("Statustext : " + result.StatusText + "\r\n");
                Console.Write("Receptor : " + result.Receptor + "\r\n");
                Console.Write("Sender : " + result.Sender + "\r\n");
                Console.Write("Date : " + result.Date + "\r\n");
                Console.Write("Cost : " + result.Cost + "\r\n");
                Console.Write("\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Select(List<String> messageids)
        {
            try
            {

                List<SendResult> results = Api.Select(messageids);
                foreach (SendResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
                SelectOutbox
        */
        static void SelectOutbox()
        {
            try
            {
                DateTime stardate = new DateTime(2015, 09, 21, 10, 11, 12);
                DateTime enddate = DateTime.Now;
                string sender = "30006703323323";
                List<SendResult> results = Api.SelectOutbox(stardate, enddate, sender);
                foreach (SendResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
            LatestOutbox
        */
        static void LatestOutbox()
        {
            try
            {
                long pagesize = 50;
                string sender = "30006703323323";
                List<SendResult> results = Api.LatestOutbox(pagesize, sender);
                foreach (SendResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
                CountOutbox
        */
        static void CountOutbox()
        {
            try
            {
                DateTime stardate = new DateTime(2015, 09, 21, 10, 11, 12);
                DateTime enddate = DateTime.Now;
                int status = 10;
                CountOutboxResult re = Api.CountOutbox(stardate, enddate, status);

                Console.Write("StartDate : " + re.StartDate);
                Console.Write("EndDate : " + re.EndDate);
                Console.Write("SumPart : " + re.SumPart);
                Console.Write("SumCount : " + re.SumCount);
                Console.Write("Cost : " + re.Cost);
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
            Cancel
        */
        static void Cancel()
        {
            try
            {
                string messageid = "5956";
                StatusResult result = Api.Cancel(messageid);
                Console.Write("Messageid : " + result.Messageid);
                Console.Write("Status : " + result.Status);
                Console.Write("Statustext : " + result.Statustext);
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Cancel(List<string> messageids)
        {
            try
            {
                List<StatusResult> results = Api.Cancel(messageids);
                foreach (StatusResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid);
                    Console.Write("Status : " + result.Status);
                    Console.Write("Statustext : " + result.Statustext);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
            Receive
        */
        static void Receive()
        {
            try
            {
                string linenumber = "30006703323323";
                int isread = 1;
                List<ReceiveResult> res = Api.Receive(linenumber, isread);
                foreach (ReceiveResult re in res)
                {
                    Console.Write("Messageid : " + re.MessageID);
                    Console.Write("message : " + re.Message);
                    Console.Write("sender : " + re.Sender);
                    Console.Write("receptor : " + re.Receptor);
                    Console.Write("date : " + re.Date);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
                CountInbox
        */
        static void CountInbox()
        {
            try
            {
                DateTime stardate = new DateTime(2015, 09, 21, 10, 11, 12);
                DateTime enddate = DateTime.Now;
                string linenumber = "30006703323323";
                int isread = 10;
                CountInboxResult res = Api.CountInbox(stardate, enddate, linenumber, isread);
                Console.Write("StartDate : " + res.StartDate);
                Console.Write("EndDate : " + res.EndDate);
                Console.Write("SumCount : " + res.SumCount);

            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
        CountPostalCode
        */
        static void CountPostalCode()
        {
            try
            {
                long postalcode = 441585;
                List<CountPostalCodeResult> res = Api.CountPostalCode(postalcode);
                foreach (CountPostalCodeResult r in res)
                {
                    Console.Write("StartDate : " + r.Section + "\r\n");
                    Console.Write("EndDate : " + r.Value + "\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


        /*
        AccountInfo
        */
        static void AccountInfo()
        {
            try
            {
                AccountInfoResult res = Api.AccountInfo();
                Console.Write("RemainCredit : " + res.RemainCredit + "\r\n");
                Console.Write("Expiredate : " + res.Expiredate + "\r\n");
                Console.Write("Type : " + res.Type + "\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }


    }
}
