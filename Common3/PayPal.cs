using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Collections.Specialized;
using System.Web;
using System.Globalization;

namespace RO.Common3
{
    public class PaypalCreditCardAddressInfo
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string phone { get; set; }
    }
    public class PaypalPaymentResult
    {
        public string id { get; set; }
        public DateTime create_time { get; set; }
        public string state { get; set; }
        public string currency { get; set; }
        public double amount { get; set; }
        public string salesId { get; set; }
        public string avs_code { get; set; }
        public string cvv_code { get; set; }

    }
    public class PaypalPayoutResult
    {
        public string status { get; set; }
        public string currency { get; set; }
        public double amount { get; set; }
        public string fee_currency { get; set; }
        public double fee { get; set; }
        public string recipient_email { get; set; }
        public string payout_batch_id { get; set; }
        public string time_completed { get; set; }
        public string link { get; set; }
        public string sender_batch_id { get; set; }
    }
    public class PaypalRefundResult
    {
        public string id { get; set; }
        public DateTime create_time { get; set; }
        public string state { get; set; }
        public string currency { get; set; }
        public double amount { get; set; }
        public string salesId { get; set; }

    }
    public class PaypalException : Exception
    {
        public PaypalException(string message)
            : base(message)
        {
        }
        public PaypalException(string message, Exception e)
            : base(message,e)
        {
        }
    }
    public class Paypal : RO.Common3.Encryption
    {
        private string _encSecret;
        private string _encAccessToken;
        private DateTime _AccessTokenExpiry;
        private string _clientID;
        private string _baseUrl = "https://api.sandbox.paypal.com";

        private class V1PaypalAccessToken
        {
            public string scope {get;set;}
            public string access_token {get;set;}
            public string token_type {get;set;}
            public string app_id {get;set;}
            public int expires_in {get;set;}
        }
        private class V1PayPalAddressInfo
        {
            public string line1 { get; set; }
            public string line2 { get; set; }
            public string city { get; set; }
            public string country_code { get; set; }
            public string postal_code { get; set; }
            public string state { get; set; }
            public string phone { get; set; }
        }
        private class V1PaypalCCPayment
        {
            public string number {get;set;}
            public string type {get;set;}
            public string expire_month {get;set;}
            public string expire_year {get;set;}
            //public int expire_month {get;set;}
            //public int expire_year {get;set;}
            public string cvv2 {get;set;}
            public string first_name {get;set;}
            public string last_name {get;set;}
            public V1PayPalAddressInfo billing_address { get;set; }
        }
        private class V1PaypalFundingInstrument
        {
            public V1PaypalCCPayment credit_card;
        }
        public class V1PaypalAmount {
            public string total {get;set;}
            public string currency {get;set;}

        }
        public class V1PaypalTransaction
        {
            public V1PaypalAmount amount {get;set;}
            public string description {get;set;}
        }
        private class V1PaypalPayer {
            public string payment_method {get;set;}
            public List<V1PaypalFundingInstrument> funding_instruments { get; set; }
        }
        private class V1PaypalPayment
        {
            public string intent {get;set;}
            public V1PaypalPayer payer {get;set;}
            public List<V1PaypalTransaction> transactions {get;set;}
        }
        private class V1PaypalLink
        {
            public string href {get;set;}
            public string rel {get;set;}
            public string method {get;set;}
        }
        private class V1PaypalSalesDetail
        {
            public string id { get; set; }
            public string create_time { get; set; }
            public string update_time { get; set; }
            public string state { get; set; }
            public V1PaypalAmount amount { get; set; }
            public string parent_payment { get; set; }
            public List<V1PaypalLink> links { get; set; }
            public Dictionary<string, string> processor_response { get; set; }
        }
        private class V1PaypalSalesItem
        {
            public V1PaypalSalesDetail sale { get; set; }
        }
        private class V1PaypalSalesTransaction
        {
            public V1PaypalAmount amount { get; set; }
            public string description { get; set; }
            public List<V1PaypalSalesItem> related_resources { get; set; }
        }
        private class V1PaypalPaymentResult
        {
            /* true example return
var x = {
    "id": "PAY-4JK32260WM104101PKZLEFNI",
    "create_time": "2015-11-25T23:22:29Z",
    "update_time": "2015-11-25T23:22:31Z",
    "state": "approved",
    "intent": "sale",
    "payer": {
        "payment_method": "credit_card",
        "funding_instruments":
            [
                {
                    "credit_card": { "type": "visa", "number": "xxxxxxxxxxxx8008", "expire_month": "10", "expire_year": "2020", "first_name": "Joe", "last_name": "Shopper", "billing_address": { "line1": "address 1", "line2": "address 2", "city": "city", "state": "NY", "postal_code": "10001", "country_code": "US", "phone": "+16041234567" } }
                }
            ]
    },
    "transactions": [
        {
            "amount":
              {
                  "total": "1234.56",
                  "currency": "USD",
                  "details": { "subtotal": "1234.56" }
              },
            "description": "test",
            "related_resources": [
                {
                    "sale": {
                        "id": "0PX768180G633922V",
                        "create_time": "2015-11-25T23:22:29Z",
                        "update_time": "2015-11-25T23:22:31Z",
                        "amount": { "total": "1234.56", "currency": "USD" },
                        "state": "completed",
                        "parent_payment": "PAY-4JK32260WM104101PKZLEFNI",
                        "links": [
                            { "href": "https://api.sandbox.paypal.com/v1/payments/sale/0PX768180G633922V", "rel": "self", "method": "GET" },
                            { "href": "https://api.sandbox.paypal.com/v1/payments/sale/0PX768180G633922V/refund", "rel": "refund", "method": "POST" },
                            { "href": "https://api.sandbox.paypal.com/v1/payments/payment/PAY-4JK32260WM104101PKZLEFNI", "rel": "parent_payment", "method": "GET" }],
                        "fmf_details": {},
                        "processor_response": { "avs_code": "X", "cvv_code": "M" }
                    }
                }
            ]
        }
    ],
    "links": [{ "href": "https://api.sandbox.paypal.com/v1/payments/payment/PAY-4JK32260WM104101PKZLEFNI", "rel": "self", "method": "GET" }]
}
             */
            public string id {get;set;}
            public string create_time {get;set;}
            public string update_time {get;set;}
            public string state {get;set;}
            public string intent {get;set;}
            public V1PaypalPayer payer {get;set;}
            public List<V1PaypalSalesTransaction> transactions { get; set; }
            public List<V1PaypalLink> links {get;set;}
        }
        private class V1PaypalRefundResult
        {
            public string id { get; set; }
            public string create_time { get; set; }
            public string update_time { get; set; }
            public string state { get; set; }
            public V1PaypalAmount amount { get; set; }
            public string sale_id { get; set; }
            public string parent_payment { get; set; }
            public List<V1PaypalLink> links { get; set; }
        }
        private class V1PaypalErrorDetail
        {
            public string field { get; set; }
            public string issue { get; set; }
            public List<V1PaypalLink> links { get; set; }
        }
        private class V1PaypalError
        {

            public string name { get; set; }
            public List<Dictionary<string, string>> details { get; set; }
            public string message { get; set; }
            public string information_link { get; set; }
            public string debug_id {get;set;}
        }
        private class V1PaypalErrorAlt
        {

            public string name { get; set; }
            public List<V1PaypalErrorDetail> details { get; set; }
            public string message { get; set; }
            public string information_link { get; set; }
            public string debug_id { get; set; }
        }
        private class V1PaypalPayoutAmount
        {
            public string value { get; set; }
            public string currency { get; set; }
        }
        private class V1PaypalPayoutItem
        {
            public string recipient_type { get; set; }
            public V1PaypalPayoutAmount amount { get; set; }
            public string note { get; set; }
            public string sender_item_id { get; set; }
            public string receiver { get; set; }

        }
        private class V1PaypalSenderBatch
        {
            public string sender_batch_id { get; set; }
            public string email_subject { get; set; }
        }
        private class V1PaypalPayout
        {
            public V1PaypalSenderBatch sender_batch_header { get; set; }
            public List<V1PaypalPayoutItem> items { get; set; }

        }
        private class V1PayoutItemResult {
            public string payout_item_id { get; set; }
            public string transaction_id { get; set; }
            public string transaction_status { get; set; }
            public V1PaypalPayoutAmount payout_item_fee { get; set; }
            public string payout_batch_id { get; set; }
            public V1PaypalPayoutItem payout_item { get; set; }
            public string time_processed { get; set; }
            public List<V1PaypalLink> links { get; set; }
        }
        private class V1PaypalPayoutResultBatch {
            public V1PaypalSenderBatch sender_batch_header {get;set;}
            public V1PaypalPayoutAmount amount { get; set; }
            public V1PaypalPayoutAmount fees { get; set; }
            public string payout_batch_id {get;set;}
            public string batch_status {get;set;}
            public string time_created {get;set;}
            public string time_completed {get;set;}
        }
        private class V1PaypalPayoutResult {
            public V1PaypalPayoutResultBatch batch_header { get; set; }
            public List<V1PayoutItemResult> items { get; set; }
            public List<V1PaypalLink> links { get; set; }
        }
        private class NullPropertiesConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                var jsonExample = new Dictionary<string, object>();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    //check if decorated with ScriptIgnore attribute
                    bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

                    var value = prop.GetValue(obj, BindingFlags.Public, null, null, null);
                    if (value != null && !ignoreProp)
                        jsonExample.Add(prop.Name, value);
                }

                return jsonExample;
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return GetType().Assembly.GetTypes(); }
            }
        }

        private string _GetAccessToken()
        {
            return DecryptString(_encAccessToken);
        }
        private void _V1Login()
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "v1/oauth2/token";
            Uri uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            string secret = DecryptString(_encSecret);
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_clientID + ":" + secret));
            webRequest.Headers.Add("Authorization", "Basic " + encoded);
            webRequest.PreAuthenticate = true;
            webRequest.Accept = "application/json";
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] data = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            webRequest.ContentLength = data.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(data,0, data.Length);
            dataStream.Close();

            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                var token = new JavaScriptSerializer().Deserialize<V1PaypalAccessToken>(json);
                _encAccessToken = base.EncryptString(token.access_token);
                _AccessTokenExpiry = DateTime.Now.AddSeconds(token.expires_in);
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["error"] + " " + error["error_description"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }

        }
        private string _V1Action(string url, string json)
        {
            string token = _GetAccessToken();
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            byte[] data = Encoding.UTF8.GetBytes(json);
            webRequest.ContentLength = data.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var sr = new StreamReader(responseStream, Encoding.UTF8);
                var result = sr.ReadToEnd();
                return result;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> general_error = null;
                            V1PaypalError app_error = null;
                            V1PaypalErrorAlt app_error_alt = null;
                            try
                            {
                                general_error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            }
                            catch
                            {
                                try
                                {
                                    app_error_alt = new JavaScriptSerializer().Deserialize<V1PaypalErrorAlt>(text);
                                    app_error = new V1PaypalError
                                    {
                                        debug_id = app_error_alt.debug_id
                                        ,
                                        message = app_error_alt.message
                                        ,
                                        information_link = app_error_alt.information_link
                                        ,
                                        name = app_error_alt.name
                                        ,
                                        details = new List<Dictionary<string, string>> { new Dictionary<string, string> { { "field", app_error_alt.details[0].field }, { "issue", app_error_alt.details[0].issue } } }
                                    };
                                }
                                catch 
                                {
                                    app_error = new JavaScriptSerializer().Deserialize<V1PaypalError>(text);
                                }
                            }
                            if (general_error != null)
                            {
                                if (general_error.ContainsKey("error"))
                                {
                                    throw new PaypalException(general_error["error"] + " " + general_error["error_description"]);
                                }
                                else if (general_error.ContainsKey("name"))
                                {
                                    throw new PaypalException(general_error["name"] + " " + general_error["message"] + general_error["information_link"]);
                                }
                                else throw new PaypalException(text);
                            }
                            if (app_error != null)
                            {
                                string errMsg = (app_error.details[0]["field"]??"") + " " + app_error.details[0]["issue"];
                                throw new PaypalException(app_error.message + " " + errMsg);
                            }
                            else throw new Exception(text);
                        }
                        else
                        {
                            throw new Exception(text);
                        }
                    }
                }
            }
        }
        private V1PaypalPaymentResult _V1CCPayment(string trans_desc, string currency, double amount, string ccNbr, string ccType, string firstName, string lastName, int expiry_month, int expiry_year, string cvv2, PaypalCreditCardAddressInfo addr)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "v1/payments/payment";

            V1PaypalPayment payment = new V1PaypalPayment
            {
                intent = "sale",
                payer = new V1PaypalPayer
                {
                    funding_instruments = new List<V1PaypalFundingInstrument> {
                        new V1PaypalFundingInstrument{
                            credit_card = new V1PaypalCCPayment{
                             number = ccNbr, type = ccType, expire_month = expiry_month.ToString("D2"), expire_year = expiry_year.ToString()
                             , cvv2 = cvv2
                             , first_name = firstName, last_name = lastName
                             , billing_address = addr == null ? null :
                                new V1PayPalAddressInfo
                                    {
                                         line1 = addr.line1, line2 = addr.line2, city = addr.city, country_code = addr.country_code, postal_code = addr.postal_code, state=addr.state, phone = addr.phone
                                    }
                        }
                        }
                    }, payment_method = "credit_card"
                },
                transactions = new List<V1PaypalTransaction>{
                    new V1PaypalTransaction {
                        amount = new V1PaypalAmount{
                             currency = currency, total = amount.ToString("0.00")
                        }
                        , description = trans_desc
                    } 
                }
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string payment_json = serializer.Serialize(payment);
            //payment_json = @"{'intent':'sale','payer':{'payment_method':'credit_card','funding_instruments':[{'credit_card':{'number':'5500005555555559','type':'mastercard','expire_month':12,'expire_year':2018,'cvv2':'111','first_name':'Betsy','last_name':'Buyer'}}]},'transactions':[{'amount':{'total':'7.47','currency':'USD'},'description':'This is the payment transaction description.'}]}";
            string return_json = _V1Action(url, payment_json);
            V1PaypalPaymentResult result = serializer.Deserialize<V1PaypalPaymentResult>(return_json);
            return result;
        }

        private V1PaypalPayoutResult _V1CCPayout(string payout_batch_id, string recipient_email, string emailSubject, string payout_note, string currency, double amount)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "v1/payments/payouts?sync_mode=true";

            V1PaypalPayout payout = new V1PaypalPayout
            {
                sender_batch_header = new V1PaypalSenderBatch
                {
                    email_subject = emailSubject,
                    sender_batch_id = payout_batch_id
                },
                items = new List<V1PaypalPayoutItem>
                {
                    new V1PaypalPayoutItem{
                         amount = new V1PaypalPayoutAmount{
                              currency = currency, value = amount.ToString("0.00")
                         },
                         note = payout_note,
                         receiver = recipient_email,
                         recipient_type = "EMAIL",
                         sender_item_id = payout_batch_id + "_01"
                    }
                }
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string payout_json = serializer.Serialize(payout);
            //payment_json = @"{'intent':'sale','payer':{'payment_method':'credit_card','funding_instruments':[{'credit_card':{'number':'5500005555555559','type':'mastercard','expire_month':12,'expire_year':2018,'cvv2':'111','first_name':'Betsy','last_name':'Buyer'}}]},'transactions':[{'amount':{'total':'7.47','currency':'USD'},'description':'This is the payment transaction description.'}]}";
            string return_json = _V1Action(url, payout_json);
            V1PaypalPayoutResult result = serializer.Deserialize<V1PaypalPayoutResult>(return_json);
            return result;
        }

        private V1PaypalRefundResult _V1CCRefund(string salesId, string currency, double amount)
        {
            /* partial refund */
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "v1/payments/sale/" + salesId + "/refund";
            V1PaypalAmount refund_amount = new V1PaypalAmount { currency = currency, total = amount.ToString("0.00") };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string refund_json = serializer.Serialize(refund_amount);
            string return_json = _V1Action(url, refund_json);
            V1PaypalRefundResult result = serializer.Deserialize<V1PaypalRefundResult>(return_json);
            return result;

        }
        private V1PaypalRefundResult _V1CCRefund(string paymentId)
        {
            /* full refund */
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "v1/payments/sale/" + paymentId + "/refund";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string return_json = _V1Action(url, "{}");
            V1PaypalRefundResult result = serializer.Deserialize<V1PaypalRefundResult>(return_json);
            return result;

        }

        public Paypal(string baseUrl, string clientID, string encSecret)
        {
            _encSecret = encSecret;
            _clientID = clientID;
            _baseUrl = baseUrl;
        }
        public Paypal(string clientID, string encSecret)
        {
            _encSecret = encSecret;
            _clientID = clientID;
        }
        public void Login()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            _V1Login();
        }
        public PaypalPaymentResult Payment(string trans_desc, string currency, double amount, string ccNbr, string ccType, string firstName, string lastName, int expiry_month, int expiry_year, string cvv2)
        {
            V1PaypalPaymentResult result = _V1CCPayment(trans_desc, currency, amount, ccNbr, ccType, firstName, lastName, expiry_month, expiry_year, cvv2,null);
            return new PaypalPaymentResult
            {
                id = result.id,
                amount = double.Parse(result.transactions[0].amount.total),
                currency = result.transactions[0].amount.currency,
                state = result.state,
                create_time = DateTime.Parse(result.create_time),
                salesId = result.transactions[0].related_resources[0].sale.id,
                avs_code = result.transactions[0].related_resources[0].sale.processor_response["avs_code"],
                cvv_code = result.transactions[0].related_resources[0].sale.processor_response["cvv_code"]
            };
        }

        public PaypalPaymentResult Payment(string trans_desc, string currency, double amount, string ccNbr, string ccType, string firstName, string lastName, int expiry_month, int expiry_year, string cvv2, PaypalCreditCardAddressInfo addr)
        {
            V1PaypalPaymentResult result = _V1CCPayment(trans_desc, currency, amount, ccNbr, ccType, firstName, lastName, expiry_month, expiry_year, cvv2,addr);
            return new PaypalPaymentResult
            {
                id = result.id,
                amount = double.Parse(result.transactions[0].amount.total),
                currency = result.transactions[0].amount.currency,
                state = result.state,
                create_time = DateTime.Parse(result.create_time),
                salesId = result.transactions[0].related_resources[0].sale.id,
                avs_code = result.transactions[0].related_resources[0].sale.processor_response["avs_code"],
                cvv_code = result.transactions[0].related_resources[0].sale.processor_response["cvv_code"]
            };
        }
        public PaypalRefundResult Refund(string salesId)
        {
            V1PaypalRefundResult result;
//            if (currency != null) result = _V1CCRefund(salesId, currency, amount);
//            else result = _V1CCRefund(salesId);
            result = _V1CCRefund(salesId);
            return new PaypalRefundResult { id = result.id, amount = double.Parse(result.amount.total), currency = result.amount.currency, create_time = DateTime.Parse(result.create_time), state = result.state };
        }
        public PaypalPayoutResult Payout(string payout_batch_id, string recipient_email, string emailSubject, string payout_note, string currency, double amount)
        {
            V1PaypalPayoutResult result = _V1CCPayout(payout_batch_id.Left(25),recipient_email,emailSubject,payout_note,currency,amount);
            return new PaypalPayoutResult
            {
                status = result.batch_header.batch_status
                , amount = double.Parse(result.batch_header.amount.value)
                , currency = result.batch_header.amount.currency
                , fee = double.Parse(result.batch_header.fees.value)
                , fee_currency = result.batch_header.fees.currency
                , recipient_email = recipient_email
                , payout_batch_id = result.batch_header.payout_batch_id
                , time_completed =  result.batch_header.time_completed
                , link = result.links[0].href
                , sender_batch_id = result.batch_header.sender_batch_header.sender_batch_id
                
            };

        }
    }

    public class PaypalExpress : RO.Common3.Encryption
    {
        private bool _bTestMode;
        private string _apiUrl;
        private string _checkoutUrl;
        private string _user;
        private string _pwd;
        private string _signature;

        private string PayPalAPISandboxUrl = "https://api-3t.sandbox.paypal.com/nvp";
        private string PayPalExpressCheckoutSandboxUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
        private string PayPalAPIUrl = "https://api-3t.paypal.com/nvp";
        private string PayPalExpressCheckoutUrl = "https://www.paypal.com/cgi-bin/webscr";
        public string PaypalUrl { get { return _bTestMode ? "https://www.sandbox.paypal.com" : "https://www.paypal.com"; } }
        public PaypalExpress(bool bTestMode, string user, string endPwd, string endSignature)
        {
            _bTestMode = bTestMode;
            _apiUrl = bTestMode ? PayPalAPISandboxUrl : PayPalAPIUrl;
            _checkoutUrl = bTestMode ? PayPalExpressCheckoutSandboxUrl : PayPalExpressCheckoutUrl;
            _user = user;
            _pwd = DecryptString(endPwd);
            _signature = DecryptString(endSignature);
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        }
        private NameValueCollection PayPalAPISetup(NameValueCollection nvp)
        {
            //if (bTestMode)
            //{
            //    nvp["USER"] = "nelson.lin-facilitator_api1.robocoder.com";
            //    nvp["PWD"] = "1376180256";
            //    nvp["SIGNATURE"] = "AgOEssZaS3nhQUUPh6-YAvwHAO.5AUb-cpe570R-nKPePZUaXMNYPVPs";
            //}
            //else
            //{
            //    nvp["USER"] = "nelson.lin_api1.robocoder.com";
            //    nvp["PWD"] = "A2AG5AG2VGL9SEDN";
            //    nvp["SIGNATURE"] = "AVBywpkl-Wa0DEdbWVmiBa31qAb7A2F5DOf7inYOm2hQYHyNnmwrTq4a";
            //}
            nvp["USER"] = _user;
            nvp["PWD"] = _pwd;
            nvp["SIGNATURE"] = _signature;
            nvp["VERSION"] = "93";
            return nvp;
        }
        private NameValueCollection PayPalMakePaymentRequest(NameValueCollection nvp, string currency, string payee0, string amt0, string itemamt0, string tax0, string desc0, string reqId0, string payee1, string amt1, string itemamt1, string tax1, string desc1, string reqId1)
        {
            if (reqId0 == reqId1)
            {
                throw new Exception("paypal split payment must have unique request reference for each leg");
            }
            nvp["PAYMENTREQUEST_0_CURRENCYCODE"] = currency;
            nvp["PAYMENTREQUEST_0_SELLERPAYPALACCOUNTID"] = payee0;
            nvp["PAYMENTREQUEST_0_AMT"] = amt0;
            nvp["PAYMENTREQUEST_0_TAXAMT"] = tax0;
            nvp["PAYMENTREQUEST_0_ITEMAMT"] = itemamt0;
            nvp["PAYMENTREQUEST_0_DESC"] = desc0;
            nvp["PAYMENTREQUEST_0_PAYMENTREQUESTID"] = reqId0;
            float amt = 0.0f;
            float.TryParse(amt1, out amt);
            if (amt > 0.0)
            {
                nvp["PAYMENTREQUEST_1_CURRENCYCODE"] = currency;
                nvp["PAYMENTREQUEST_1_SELLERPAYPALACCOUNTID"] = payee1;
                nvp["PAYMENTREQUEST_1_AMT"] = amt1;
                nvp["PAYMENTREQUEST_1_TAXAMT"] = tax1;
                nvp["PAYMENTREQUEST_1_ITEMAMT"] = itemamt1;
                nvp["PAYMENTREQUEST_1_DESC"] = desc1;
                nvp["PAYMENTREQUEST_1_PAYMENTREQUESTID"] = reqId1;
            }

            return nvp;
        }
        public NameValueCollection PayPalSetExpressCheckout(string returnUrl, string cancelUrl, string payerEmail, string currency, string payee0, double amt0, double itemamt0, double tax0, string desc0, string reqId0, string payee1, double amt1, double itemamt1, double tax1, string desc1, string reqId1)
        {
            WebClient wc = new WebClient();

            NameValueCollection nvp = new NameValueCollection();
            PayPalAPISetup(nvp);
            nvp["METHOD"] = "SetExpressCheckout";
            nvp["RETURNURL"] = returnUrl;
            nvp["CANCELURL"] = cancelUrl;
            nvp["SOLUTIONTYPE"] = "Sole"; // guest checkout with credit card without paypal, may need account setup 
            nvp["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
            if (amt1 > 0) { nvp["PAYMENTREQUEST_1_PAYMENTACTION"] = "Sale"; }
            //nvp["L_BILLINGTYPE0"] = "RecurringPayments";
            //nvp["BILLINGAGREEMENTDESCRIPTION0"] = desc0;
            //nvp["L_BILLINGTYPE1"] = "RecurringPayments";
            //nvp["BILLINGAGREEMENTDESCRIPTION1"] = desc1;
            if (!string.IsNullOrEmpty(payerEmail)) { nvp["EMAIL"] = payerEmail; }
            PayPalMakePaymentRequest(nvp, currency, payee0, Math.Round(amt0, 2).ToString(CultureInfo.InvariantCulture), Math.Round(itemamt0, 2).ToString(CultureInfo.InvariantCulture), Math.Round(tax0, 2).ToString(CultureInfo.InvariantCulture), desc0, reqId0, payee1, Math.Round(amt1, 2).ToString(CultureInfo.InvariantCulture), Math.Round(itemamt1, 2).ToString(CultureInfo.InvariantCulture), Math.Round(tax1, 2).ToString(CultureInfo.InvariantCulture), desc1, reqId1);
            wc.QueryString = nvp;
            string x = wc.DownloadString(_apiUrl);
            NameValueCollection result = HttpUtility.ParseQueryString(x);
            if (result["ACK"] == "Success")
            {
                result["PayPalPaymentUrl"] = _checkoutUrl + "?" + "cmd=_express-checkout&token=" + HttpUtility.UrlEncode(result["TOKEN"]);
            }
            return result;
        }
        public NameValueCollection PayPalSetExpressCheckout(string returnUrl, string cancelUrl, string payerEmail, string currency, string payee, double amt, double itemamt, double tax, string desc, string reqId)
        {
            return PayPalSetExpressCheckout(returnUrl, cancelUrl, payerEmail, currency, payee, amt, itemamt, tax, desc, reqId, "", 0, 0, 0, "", "");
        }
        public NameValueCollection PayPalGetExpressCheckoutDetails(string token)
        {
            WebClient wc = new WebClient();

            NameValueCollection nvp = new NameValueCollection();
            PayPalAPISetup(nvp);
            nvp["METHOD"] = "GetExpressCheckoutDetails";
            nvp["TOKEN"] = token;
            wc.QueryString = nvp;
            string x = wc.DownloadString(_apiUrl);
            NameValueCollection result = HttpUtility.ParseQueryString(x);
            return result;
        }
        public NameValueCollection PayPalDoExpressCheckoutPayment(string token, string payerId, string currency, string payee0, double amt0, double itemamt0, double tax0, string desc0, string reqId0, string payee1, double amt1, double itemamt1, double tax1, string desc1, string reqId1)
        {
            WebClient wc = new WebClient();

            NameValueCollection nvp = new NameValueCollection();
            PayPalAPISetup(nvp);
            nvp["METHOD"] = "DoExpressCheckoutPayment";
            nvp["VERSION"] = "93";
            nvp["TOKEN"] = token;
            nvp["PAYERID"] = payerId;
            nvp["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
            if (amt1 > 0.0)
            {
                nvp["PAYMENTREQUEST_1_PAYMENTACTION"] = "Sale";
            }
            PayPalMakePaymentRequest(nvp, currency, payee0, Math.Round(amt0, 2).ToString(CultureInfo.InvariantCulture), Math.Round(itemamt0, 2).ToString(CultureInfo.InvariantCulture), Math.Round(tax0, 2).ToString(CultureInfo.InvariantCulture), desc0, reqId0, payee1, Math.Round(amt1, 2).ToString(CultureInfo.InvariantCulture), Math.Round(itemamt1, 2).ToString(CultureInfo.InvariantCulture), Math.Round(tax1, 2).ToString(CultureInfo.InvariantCulture), desc1, reqId1);
            wc.QueryString = nvp;
            string x = wc.DownloadString(_apiUrl);
            NameValueCollection result = HttpUtility.ParseQueryString(x);
            return result;
        }
        public NameValueCollection PayPalDoExpressCheckoutPayment(string token, string payerId, string currency, string payee, double amt, double itemamt, double tax, string desc, string reqId)
        {
            return PayPalDoExpressCheckoutPayment(token, payerId, currency, payee, amt, itemamt, tax, desc, reqId, "", 0, 0, 0, "", "");
        }

        /* The following works but not being used because recurring cannot handle split payment:
        public NameValueCollection PayPalCreateRecurringPaymentsProfile(string token, string payerEmail, DateTime startOn, string currency, string payee0, string amt0, string itemamt0, string tax0, string desc0, string reqId0, string payee1, string amt1, string itemamt1, string tax1, string desc1, string reqId1)
        {
            WebClient wc = new WebClient();

            NameValueCollection nvp = new NameValueCollection();
            PayPalAPISetup(nvp);
            nvp["METHOD"] = "CreateRecurringPaymentsProfile";
            nvp["VERSION"] = "93";
            nvp["TOKEN"] = token;
            nvp["EMAIL"] = payerEmail;
            nvp["CURRENCYCODE"] = currency;
            nvp["PROFILESTARTDATE"] = startOn.Date.ToString("o"); // ISO8601
            nvp["BILLINGPERIOD"] = "Monthly";
            nvp["BILLINGFREQUENCY"] = "12";
            nvp["AMT"] = itemamt0;
            nvp["TAXAMT"] = tax0;
            wc.QueryString = nvp;
            string x = wc.DownloadString(_apiUrl);
            NameValueCollection result0 = HttpUtility.ParseQueryString(x);
            nvp["AMT"] = itemamt1;
            nvp["TAXAMT"] = tax1;
            x = wc.DownloadString(_apiUrl);
            NameValueCollection result1 = HttpUtility.ParseQueryString(x);

            result0.Add(result1);
            return result0;
        }
        */
    }
}
