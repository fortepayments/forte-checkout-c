using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using ForteLibrary;



namespace Forte
{
    public partial class CheckOut : System.Web.UI.Page
    {
        ForteLibrary.Gateway.Signature cliSignature = new ForteLibrary.Gateway.Signature();     // This is the worker object that takes the values and performs functions.
        public string return_string = "Initial";
        public string source = "";

        // Capture the time in ticks.
        public string utc_time = DateTime.Now.Ticks.ToString();       
        public string pay_now_single_return_string;
        public string pay_schedule_amount_return_string;
        public string pay_range_select_amount_return_string;
        public string pay_range_select_amount_labels;
        public string api_loginID;

        protected void Page_Load(object sender, EventArgs e)
        {


            cliSignature.api_login_id = "YourAPILoginID";            //Add your API login ID.
            cliSignature.secure_trans_key = "YourSecureTransKey";    //Add your secure transaction key.


            cliSignature.pay_now_single_payment = cliSignature.api_login_id + "|sale|1.0|10.00|" + utc_time + "|A1234||";
            cliSignature.pay_schedule_amount = cliSignature.api_login_id + "|schedule|1.0|1-9.5;5|" + utc_time + "|A1234|" + cliSignature.customer_token + "|" + cliSignature.payment_token;
            cliSignature.pay_range_select_amount = cliSignature.api_login_id + "|sale|1.0|{20,40,60,80,100,0};20-1000|" + utc_time + "|||";
            cliSignature.pay_range_select_amount_labels = cliSignature.api_login_id + "|sale|1.0|{1375.23,1573.66,56.99,0|Total outstanding,Last statement balance,Minimum balance,Specify different amount}|" + utc_time + "|||";
            api_loginID = cliSignature.api_login_id;
            pay_now_single_return_string = Gateway.EndPoint(cliSignature, "CREATESIGNATUREPAYSINGLEAMOUNT"); ;
            pay_schedule_amount_return_string = Gateway.EndPoint(cliSignature, "CREATESIGNATURESCHEDULE");
            pay_range_select_amount_return_string = Gateway.EndPoint(cliSignature, "CREATESIGNATURERANGE");
            pay_range_select_amount_labels = Gateway.EndPoint(cliSignature, "CREATESIGNATURERANGELABEL");

        }

    }

}

namespace ForteLibrary
{

    public static class Gateway
    {

        #region CONSTANTS

        public class Signature
        {
            public string api_login_id { get; set; }
            public string secure_trans_key { get; set; }
            public string pay_now_single_payment { get; set; }
            public string pay_schedule_amount { get; set; }
            public string pay_range_select_amount { get; set; }
            public string pay_range_select_amount_labels { get; set; }
            public string hash_method { get; set; }
            public string utc_time { get; set; }
            public string customer_token { get; set; }
            public string payment_token { get; set; }
        }

        #endregion

        static Signature objSign = new Signature();
        static Operation myoper = new Operation();
        static string mySignature = "Error";

        public static string EndPoint(Signature SignClient, string Operation)
        {
            objSign = SignClient;

            switch (Operation)
            {

                case "CREATESIGNATUREPAYSINGLEAMOUNT":
                    mySignature = myoper.CreateSignature(objSign.pay_now_single_payment, objSign.secure_trans_key);
                    break;

                case "CREATESIGNATURESCHEDULE":
                    mySignature = myoper.CreateSignature(objSign.pay_schedule_amount, objSign.secure_trans_key);
                    break;

                case "CREATESIGNATURERANGE":
                    mySignature = myoper.CreateSignature(objSign.pay_range_select_amount, objSign.secure_trans_key);
                    break;

                case "CREATESIGNATURERANGELABEL":
                    mySignature = myoper.CreateSignature(objSign.pay_range_select_amount_labels, objSign.secure_trans_key);
                    break;
            }

            return mySignature;

        }
    }
    internal class Operation
    {
        public Operation()
        {

        }
        internal string CreateSignature(string strSource, string key)
        {
            string Signature = "error";

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(strSource);
            byte[] hashmessage;


            hashmessage = null;
            Signature = null;
            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            hashmessage = hmacmd5.ComputeHash(messageBytes);
            Signature = ByteToString(hashmessage);


            return Signature;

        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

    }
}
