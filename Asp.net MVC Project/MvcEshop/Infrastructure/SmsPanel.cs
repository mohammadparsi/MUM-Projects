using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;


namespace Infrastructure
{
    class SmsPanel
    {
        const String ApiKey = "5243374B47776F377143375A6D54765061626A572F413D3D";
        const String SenderLine = "30006703323323";
        public static void SendSMS(string receptorNumber, string message)
        {
            MvcEshop.Models.Message msg = new MvcEshop.Models.Message();
            msg.Sender = SenderLine;
            msg.Receptor = receptorNumber;
            msg.Text = message;
            var sendResult = Send(msg);
            if (sendResult == null) return;
            foreach (long f in sendResult)
            {
                Console.WriteLine("SmsRefrenceID:" + f.ToString());
            }

            //List<long> list = new List<long>();

            //foreach (long item in sendResult.ToArray())
            //{
            //    list.Add(item); 
            //}

            //list = (MvcEshop.com.kavenegar.api.ArrayOfLong)list;

            var smsstatus = Status(sendResult);
            foreach (int st in smsstatus)
            {
                switch (st)
                {
                    case 1:
                        Console.WriteLine("Queued to send");
                        break;
                    case 2:
                        Console.WriteLine("Scheduled");
                        break;
                    case 4:
                        Console.WriteLine("Sent to Operator");
                        break;
                    case 10:
                        Console.WriteLine("Sent to Receptor (Delivered)");
                        break;
                    case 11:
                        Console.WriteLine("Undelivered");
                        break;
                    case 14:
                        Console.WriteLine("Receptor number has been blocked..");
                        break;
                    case 100:
                        Console.WriteLine("Refrence ID not found");
                        break;
                }
            }

            Console.WriteLine("Press Enter to close");
            Console.ReadKey();
        }
        public static MvcEshop.com.kavenegar.api.ArrayOfLong Send(MvcEshop.Models.Message message)
        {
            try
            {
                MvcEshop.com.kavenegar.api.v1SoapClient client = new MvcEshop.com.kavenegar.api.v1SoapClient();
                int status_code = 0;
                String status_message = "";
                
                string [] array=message.Receptor.Split(',');
                List<string> list = new MvcEshop.com.kavenegar.api.ArrayOfString();

                foreach (string item in array )
                {
                    list.Add(item);
                }

                MvcEshop.com.kavenegar.api.ArrayOfString receptors = (MvcEshop.com.kavenegar.api.ArrayOfString)list;

                var result = client.SendSimpleByApikey(ApiKey, message.Sender, message.Text, receptors, 0, 0, ref status_code, ref status_message);
                if (status_code != 200)
                {
                    Console.WriteLine(String.Format("Returned [{0}] Message:[{1}]", status_code, status_message));
                    return null;
                }
                else return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static MvcEshop.com.kavenegar.api.ArrayOfInt Status(MvcEshop.com.kavenegar.api.ArrayOfLong ids)
        {
            try
            {
                MvcEshop.com.kavenegar.api.v1SoapClient client = new MvcEshop.com.kavenegar.api.v1SoapClient();
                int status_code = 0;
                String status_message = "";
                var result = client.GetStatusByApikey(ApiKey, ids, ref status_code, ref status_message);
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

    }
}
