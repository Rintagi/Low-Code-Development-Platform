using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Reflection;

namespace RO.Common3
{
    /* sample calls 
     * 
                //Square square_connect = new Square(token_string_enrypted_with_RO_encryption);
                //SquareItemCategory cat = square_connect.AddItemCategory("Category 2");
                //square_connect.AddFee("Fed Tax", "0.04",true);
                //SquareItem i = square_connect.AddItem("test 2A", "test 2A desc", "test2A",cat.square_id, "CAD", 1.00, "sku 2A","sku 2A name", 10, true, null);
                //byte[] img = System.IO.File.ReadAllBytes(Request.MapPath("images\\ArrowDn.png"));
                //square_connect.UpdItemImage(i.square_id, "ArrowDn.png", img, "image/png");
                //i.name = "test 3"; i.invn.price = 2.00;
                //square_connect.V1AddInvnMovement(i.invn.square_id, 0, "MANUAL_ADJUST");
                //square_connect.DelItem(i.square_id);
                //square_connect.V1GetItems();
                //square_connect.V1GetInventory();
                //square_connect.V1GetFee();
                //square_connect.V1GetPayments(null,null);
                //square_connect.GetFees();
                //square_connect.GetItemCategories();
                //square_connect.GetItems();
                //square_connect.GetItemInventory(); 
     */
    public class NullPropertiesConverter : JavaScriptConverter
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

    public class SquareItemInventory
    {
        public string square_id { get; set; }
        public string name { get; set; }
        public string square_item_id { get; set; }
        public int? quantity { get; set; }
        public double price { get; set; }
        public string currency { get; set; }
        public string sku { get; set; }
        public string user_data { get; set; }
    }
    public class SquareItem
    {
        public string square_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public string user_data { get; set; }
        public string square_category_id { get; set; }
        public SquareItemCategory category { get; set; } 
        public SquareItemInventory invn { get; set; }
        public SquareItemImage image {get;set;}
        public List<SquareFeeAndTax> feeAndTax {get;set;}

    }
    public class SquareItemImage 
    {
        public string square_id {get;set;}
        public string url {get;set;}
    }
    public class SquareFeeAndTax
    {
        public string square_id { get; set; }
        public string name { get; set; }
        public string rate { get; set; }
    }
    public class SquareItemCategory
    {
        public string square_id { get; set; }
        public string name { get; set; }
    }
    public class V1SquareModifierOption
    {
        public string id {get;set;}
        public string name {get;set;}
        public V1SquareMoney price_money {get;set;}
        public bool on_by_default {get;set;}
        public int ordinal {get;set;}
        public string modifier_list_id {get;set;}
    }
    public class V1SquareModifierList
    {
        public string id {get;set;}
        public string name {get;set;}
        public string selection_type {get;set;}
        public List<V1SquareModifierOption> modifier_optons {get;set;}
    }
    public class V1SquareFee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string rate { get; set; }
        public string calculation_phase { get; set; }
        public string adjustment_type { get; set; }
        public bool applies_to_custom_amounts { get; set; }
        public bool enabled { get; set; }
        public string inclusion_type { get; set; }
        public string type { get; set; }
    }
    public class V1SquareItemImage
    {
        public string id { get; set; }
        public string url { get; set; }
    }
    public class V1SquareMoney
    {
        public int amount { get; set; }
        public string currency_code { get; set; }
    }
    public class V1SquareItemVariation
    {
        public string id { get; set; }
        public string name { get; set; }
        public V1SquareMoney price_money { get; set; }
        public string sku { get; set; }
        public string item_id { get; set; }
        public int ordinal { get; set; }
        public string pricing_type { get; set; }
        public bool track_inventory { get; set; }
        public string inventory_alert_type { get; set; }
        public int inventory_alert_threshold { get; set; }
        public string user_data { get; set; }

    }
    public class V1SquareItemCategory
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class V1SquareItem 
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public string visibility { get; set; }
        public string type { get; set; }
        public string available_online { get; set; }
        public string category_id { get; set; } // only use for create/update 
        public V1SquareItemCategory category { get; set; }
        public List<V1SquareItemVariation> variations { get; set; }
        public V1SquareItemImage master_image { get; set; }
        public List<V1SquareFee> fees { get; set; }
    }
    public class V1SquareInventory
    {
        public string id { get; set; }
        public string variation_id { get; set; }
    }
    public class V1SquarePaymentTax
    {
        public string name { get; set; }
        public V1SquareMoney applied_money { get; set; }
        public string rate { get; set; }
        public string inclusion_type { get; set; }
        public string fee_id { get; set; }
    }

    public class V1SquareTender
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
        public string receipt_url { get; set; }
        public string card_brand { get; set; }
        public string pan_suffix { get; set; }
        public string entry_method { get; set; }
        public string payment_note { get; set; }
        public V1SquareMoney total_money { get; set; }
        public V1SquareMoney tendered_money { get; set; }
        public V1SquareMoney change_back_money { get; set; }
        public V1SquareMoney refunded_money { get; set; }
    }
    public class V1SquareRefund
    {
        public string type { get; set; }
        public string reason { get; set; }
        public V1SquareMoney refunded_money { get; set; }
        public DateTime created_at { get; set; }
        public DateTime processed_at { get; set; }
        public string payment_id { get; set; }
    }
    public class V1SquarePaymentItemDetail
    {
        public string category_name { get; set; }
        public string sku { get; set; }
        public string item_id { get; set; }
        public string item_variation_id { get; set; }
    }
    public class V1SquarePaymentDiscount
    {
        public V1SquareMoney applied_money { get; set; }
        public string discount_id { get; set; }
    }
    public class V1SquarePaymentModifier
    {
        public string name { get; set; }
        public V1SquareMoney applied_money { get; set; }
        public string modified_option_id { get; set; }
    }
    public class V1SquareInventoryEntry
    {
        public string variation_id { get; set; }
        public int quantity_on_hand { get; set; }
    }
    public class V1SquareItemization
    {
        public string name { get; set; }
        public float quantity { get; set; }
        public string itemization_type { get; set; }
        public V1SquarePaymentItemDetail item_detail { get; set; }
        public string notes { get; set; }
        public string item_variation_name { get; set; }
        public V1SquareMoney total_money { get; set; }
        public V1SquareMoney single_quantity_money { get; set; }
        public V1SquareMoney gross_sales_money { get; set; }
        public V1SquareMoney discount_money { get; set; }
        public V1SquareMoney net_sales_money { get; set; }
        public List<V1SquarePaymentTax> taxes { get; set; }
        public List<V1SquarePaymentDiscount> discounts { get; set; }
        public List<V1SquarePaymentModifier> modifiers { get; set; }
    }
    public class V1SquarePayment
    {
        public string id { get; set; }
        public string merchant_id { get; set; }
        public DateTime created_at { get; set; }
        public string creator_id { get; set; }
        public string payment_url { get; set; }
        public string receipt_url { get; set; }
        public V1SquareMoney inclusive_tax_money { get; set; }
        public V1SquareMoney additive_tax_money { get; set; }
        public V1SquareMoney tax_tax_money { get; set; }
        public V1SquareMoney tip_tax_money { get; set; }
        public V1SquareMoney discount_money { get; set; }
        public V1SquareMoney total_collected_money { get; set; }
        public V1SquareMoney processing_fee_money { get; set; }
        public V1SquareMoney net_total_money { get; set; }
        public V1SquareMoney refunded_money { get; set; }
        public List<V1SquarePaymentTax> inclusive_tax { get; set; }
        public List<V1SquarePaymentTax> additive_tax { get; set; }
        public List<V1SquareTender> tender { get; set; }
        public List<V1SquareRefund> refunds { get; set; }
        public List<V1SquareItemization> itemizations { get; set; }
    }
    public class Square : RO.Common3.Encryption
    {
        private string _encAccessToken;
        private string _merchantId = "me";
        private string _baseUrl = "https://connect.squareup.com/v1/";

        private string _GetAccessToken()
        {
            return DecryptString(_encAccessToken);
        }
        public Square(string baseUrl, string merchantid, string encAccessToken)
        {
            _encAccessToken = encAccessToken;
            _merchantId = merchantid;
            _baseUrl = baseUrl;
        }
        public Square(string encAccessToken)
        {
            _encAccessToken = encAccessToken;
        }
        private KeyValuePair<string,string> _V1GetObjects(string url)
        {
            string token = _GetAccessToken();
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            var webResponse = webRequest.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            var pagingLink = webResponse.Headers["Link"];
            var sr = new StreamReader(responseStream, Encoding.Default);
            var json = sr.ReadToEnd();
            return new KeyValuePair<string,string>(pagingLink,json);

        }
        private List<SquareFeeAndTax> _V1FeeAndTax(List<V1SquareFee> f)
        {
            if (f == null) return null;

            List<SquareFeeAndTax> feeandtax = new List<SquareFeeAndTax>();
            foreach (var i in f)
            {
                feeandtax.Add(new SquareFeeAndTax { name = i.name, rate = i.rate, square_id = i.id });
            }
            return feeandtax.Count > 0 ? feeandtax : null;
        }
        public List<SquareItem> V1GetItems()
        {
            string url = _baseUrl +  (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items";
            string nplink = url;
            List<V1SquareInventoryEntry> inventory =  V1GetInventory();
            Dictionary<string, int?> inventoryDict = inventory.ToDictionary<V1SquareInventoryEntry, string, int?>(i => i.variation_id, i => i.quantity_on_hand);
            List<SquareItem> itemList = new List<SquareItem>();
            do
            {
                KeyValuePair<string, string> v = _V1GetObjects(nplink);
                string json = v.Value;
                nplink = v.Key;
                var items = new JavaScriptSerializer().Deserialize<List<V1SquareItem>>(json);
                foreach (V1SquareItem i in items)
                {
                    V1SquareItemVariation invn = i.variations[0];
                    int? quantity;
                    inventoryDict.TryGetValue(invn.id, out quantity);
                    itemList.Add(new SquareItem
                    {
                        square_id = i.id
                        ,
                        abbreviation = i.abbreviation
                        ,
                        category = i.category != null ? new SquareItemCategory { square_id = i.category.id, name = i.category.name } : null
                        ,
                        square_category_id = i.category != null ? i.category.id : null
                        ,
                        description = i.description
                        ,
                        name = i.name
                        ,
                        image = i.master_image != null ? new SquareItemImage { square_id = i.master_image .id, url = i.master_image.url } : null
                        ,
                        feeAndTax = _V1FeeAndTax(i.fees)
                        ,
                        invn = new SquareItemInventory { square_id = invn.id, square_item_id = i.id, sku = invn.sku, name = invn.name, currency = invn.price_money.currency_code, price = invn.price_money.amount / 100.0, quantity = quantity }
                    });
                }
            } 
            while (!string.IsNullOrEmpty(nplink));

            return itemList;
        }
        public List<SquareFeeAndTax> V1GetFee()
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/fees";
            string nplink = url;
            List<SquareFeeAndTax> itemList = new List<SquareFeeAndTax>();
            do
            {
                KeyValuePair<string, string> v = _V1GetObjects(nplink);
                string json = v.Value;
                nplink = v.Key;
                var items = new JavaScriptSerializer().Deserialize<List<V1SquareFee>>(json);
                foreach (V1SquareFee i in items)
                {
                    itemList.Add(new SquareFeeAndTax { square_id = i.id, name = i.name, rate = i.rate});
                }

            }
            while (!string.IsNullOrEmpty(nplink));

            return itemList;
        }
        public List<V1SquareInventoryEntry> V1GetInventory()
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/inventory";
            string nplink = url;
            List<V1SquareInventoryEntry> itemList = new List<V1SquareInventoryEntry>();
            do
            {
                KeyValuePair<string, string> v = _V1GetObjects(nplink);
                string json = v.Value;
                nplink = v.Key;
                List<V1SquareInventoryEntry> items = new JavaScriptSerializer().Deserialize<List<V1SquareInventoryEntry>>(json);
                itemList.AddRange(items);

            }
            while (!string.IsNullOrEmpty(nplink));

            return itemList;
        }
        public List<V1SquarePayment> V1GetPayments(DateTime? from, DateTime? to)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/payments" + (from != null ? "begin_time=" + from.Value.ToUniversalTime().ToString("o") : "") + (from != null ? "end_time=" + from.Value.ToUniversalTime().ToString("o") : "");
            string nplink = url;
            List<V1SquarePayment> itemList = new List<V1SquarePayment>();
            do
            {
                KeyValuePair<string, string> v = _V1GetObjects(nplink);
                string json = v.Value;
                nplink = v.Key;
                var items = new JavaScriptSerializer().Deserialize<List<V1SquarePayment>>(json);
                itemList.AddRange(items);
            }
            while (!string.IsNullOrEmpty(nplink));

            return itemList;
        }
        public bool V1AddItem(SquareItem item)
        {
            string token = _GetAccessToken();
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items";
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            V1SquareItemCategory cat = item.category != null ? new V1SquareItemCategory{name = item.category.name,id = item.category.square_id} : null;

            V1SquareItemVariation invn = new V1SquareItemVariation
            {
                 name = item.invn.name
                 , sku = item.invn.sku
                 , track_inventory = true
                 , ordinal = 0
                 , price_money = new V1SquareMoney{ amount = (int) Math.Round(item.invn.price*100,0), currency_code = item.invn.currency}
                 , pricing_type = "FIXED_PRICING"
                 , inventory_alert_type = "LOW_QUANTITY"
                 , inventory_alert_threshold = 10
            };

            V1SquareItem itm = new V1SquareItem{ 
                    name = item.name
                    , abbreviation = item.abbreviation
                    , description = item.description
                    , category_id = cat != null ? cat.id : item.square_category_id
                    , variations = new List<V1SquareItemVariation> { invn }
                    };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string item_json = serializer.Serialize(itm);
            byte[] content = Encoding.UTF8.GetBytes(item_json);
            webRequest.ContentLength = content.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(content, 0, content.Length);
            dataStream.Close();
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var pagingLink = webResponse.Headers["Link"];
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                var item_returned = new JavaScriptSerializer().Deserialize<V1SquareItem>(json);
                item.square_id = item_returned.id;
                item.invn.square_id = item_returned.variations[0].id;
                item.invn.square_item_id = item_returned.id;
                return true;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["type"] + " " + error["message"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }
        }
        public SquareItem V1GetItem(string square_item_id)
        {
            string token = _GetAccessToken();
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items" + "/" + square_item_id;
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var pagingLink = webResponse.Headers["Link"];
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                var item_returned = new JavaScriptSerializer().Deserialize<V1SquareItem>(json);
                SquareItem item = new SquareItem
                {
                    square_id = item_returned.id
                    ,
                    description = item_returned.description
                    ,
                    name = item_returned.name
                    ,
                    abbreviation = item_returned.abbreviation
                    ,
                    invn = new SquareItemInventory
                    {
                        square_id = item_returned.variations[0].id
                        ,
                        square_item_id = item_returned.id
                        ,
                        price = item_returned.variations[0].price_money.amount / 100.0
                        ,
                        currency = item_returned.variations[0].price_money.currency_code
                        ,
                        name = item_returned.variations[0].name
                        ,
                        sku = item_returned.variations[0].sku
                    }
                };
                return item;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["type"] + " " + error["message"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }

        }
        private string _V1UpdObject(string url, string content_json)
        {
            string token = _GetAccessToken();
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "PUT";
            byte[] content = Encoding.UTF8.GetBytes(content_json);
            webRequest.ContentLength = content.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(content, 0, content.Length);
            dataStream.Close();
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var pagingLink = webResponse.Headers["Link"];
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                return json;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["type"] + " " + error["message"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }

        }
        private bool _V1DelObject(string url)
        {
            string token = _GetAccessToken();
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.Method = "DELETE";
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var pagingLink = webResponse.Headers["Link"];
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                return true;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["type"] + " " + error["message"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }
        }
        public bool V1UpdItem(SquareItem item)
        {
            string token = _GetAccessToken();
            string itemUrl = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items" + "/" + item.square_id;
            string invnUrl = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items" + "/" + item.square_id + "/variations" + "/" + item.invn.square_id;
            var uri = new Uri(itemUrl);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "PUT";
            V1SquareItemCategory cat = item.category != null ? new V1SquareItemCategory { name = item.category.name, id = item.category.square_id } : null;

            V1SquareItemVariation invn = new V1SquareItemVariation
            {
                name = item.invn.name
                 ,
                sku = item.invn.sku
                 ,
                track_inventory = true
                 ,
                ordinal = 0
                 ,
                price_money = new V1SquareMoney { amount = (int)Math.Round(item.invn.price * 100, 0), currency_code = item.invn.currency }
                 ,
                pricing_type = "FIXED_PRICING"
                 ,
                inventory_alert_type = "LOW_QUANTITY"
                 ,
                inventory_alert_threshold = 10
            };

            V1SquareItem itm = new V1SquareItem
            {
                name = item.name
                ,
                abbreviation = item.abbreviation
                ,
                description = item.description
                ,
                category_id = cat != null ? cat.id : item.square_category_id
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string item_json = serializer.Serialize(itm);
            _V1UpdObject(itemUrl, item_json);
            string invn_json = serializer.Serialize(invn);
            _V1UpdObject(invnUrl, invn_json);
            return true;
        }
        public V1SquareItemImage V1UpdItemImage(string square_item_id, string name, byte[] content, string mimeType)
        {
            string token = _GetAccessToken();
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items" + "/" + square_item_id + "/" + "image";
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "multipart/form-data; boundary=BOUNDARY";
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            byte[] header = Encoding.UTF8.GetBytes(
                            "--BOUNDARY" + Environment.NewLine
                            + "Content-Disposition: form-data; name=\"" + "image_data" + "\"; filename=\"" + name + "\"" + Environment.NewLine
                            + "Content-Type: image/png" + Environment.NewLine + Environment.NewLine
                            );
            byte[] footer = Encoding.UTF8.GetBytes(
                            Environment.NewLine + "--BOUNDARY--" + Environment.NewLine
                            );
            //webRequest.ContentLength = header.Length + footer.Length + content.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(header, 0, header.Length);
            dataStream.Write(content, 0, content.Length);
            dataStream.Write(footer, 0, footer.Length);
            dataStream.Close();
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var pagingLink = webResponse.Headers["Link"];
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                var item_returned = new JavaScriptSerializer().Deserialize<V1SquareItemImage>(json);
                return item_returned;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["type"] + " " + error["message"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }


        }
        public bool V1DelItem(string square_item_id)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items" + "/" + square_item_id;
            return _V1DelObject(url);
        }
        public V1SquareFee V1AddFee(string name, string rate)
        {
            string token = _GetAccessToken();
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/fees";
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            V1SquareFee fee = new V1SquareFee { name = name, rate = rate, enabled = true };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string item_json = serializer.Serialize(fee);
            byte[] content = Encoding.UTF8.GetBytes(item_json);
            webRequest.ContentLength = content.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(content, 0, content.Length);
            dataStream.Close();
            var webResponse = webRequest.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            var pagingLink = webResponse.Headers["Link"];
            var sr = new StreamReader(responseStream, Encoding.Default);
            var json = sr.ReadToEnd();
            var item_returned = new JavaScriptSerializer().Deserialize<V1SquareFee>(json);
            return item_returned;

        }
        public bool V1UpdFee(string square_fee_id, string name, string rate)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/fees" + "/" + square_fee_id;
            V1SquareFee fee = new V1SquareFee { name = name, rate = rate, enabled = true };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string item_json = new JavaScriptSerializer().Serialize(fee);
            _V1UpdObject(url, item_json);
            return true;
        }
        public bool V1DelFee(string square_fee_id)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/fees" + "/" + square_fee_id;
            return _V1DelObject(url);
        }
        public List<V1SquareFee> V1GetFees()
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/fees";
            string nplink = url;
            List<V1SquareFee> itemList = new List<V1SquareFee>();
            do
            {
                KeyValuePair<string, string> v = _V1GetObjects(nplink);
                string json = v.Value;
                nplink = v.Key;
                var items = new JavaScriptSerializer().Deserialize<List<V1SquareFee>>(json);
                itemList.AddRange(items);
            }
            while (!string.IsNullOrEmpty(nplink));

            return itemList;

        }
        public bool V1ApplyFee(string square_fee_id, string square_item_id)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/items" + "/" + square_item_id + "/fees" + "/" + square_fee_id;
            _V1UpdObject(url, "");
            return true;
        }
        public int V1AddInvnMovement(string square_invn_id, int delta, string type)
        {
            string token = _GetAccessToken();
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/inventory" + "/" + square_invn_id;
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            string item_json = string.Format(@"{{""quantity_delta"":{0},""adjustment_type"":""{1}""}}", delta, type);
            byte[] content = Encoding.UTF8.GetBytes(item_json);
            webRequest.ContentLength = content.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(content, 0, content.Length);
            dataStream.Close();
            try
            {
                var webResponse = webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                var pagingLink = webResponse.Headers["Link"];
                var sr = new StreamReader(responseStream, Encoding.Default);
                var json = sr.ReadToEnd();
                var item_returned = new JavaScriptSerializer().Deserialize<V1SquareInventoryEntry>(json);
                return item_returned.quantity_on_hand;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        if (response.ContentType.StartsWith("application/json"))
                        {
                            Dictionary<string, string> error = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(text);
                            throw new Exception(error["type"] + " " + error["message"], e);
                        }
                        else
                        {
                            throw new Exception(text, e);
                        }
                    }
                }
            }
        }
        public V1SquareItemCategory V1AddItemCategory(string name)
        {
            string token = _GetAccessToken();
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/categories";
            var uri = new Uri(url);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.PreAuthenticate = true;
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            V1SquareItemCategory fee = new V1SquareItemCategory { name = name };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string item_json = serializer.Serialize(fee);
            byte[] content = Encoding.UTF8.GetBytes(item_json);
            webRequest.ContentLength = content.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(content, 0, content.Length);
            dataStream.Close();
            var webResponse = webRequest.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            var pagingLink = webResponse.Headers["Link"];
            var sr = new StreamReader(responseStream, Encoding.Default);
            var json = sr.ReadToEnd();
            var item_returned = new JavaScriptSerializer().Deserialize<V1SquareItemCategory>(json);
            return item_returned;
        }
        public bool V1UpdItemCategory(string square_category_id, string name)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/categories" + "/" + square_category_id;
            V1SquareItemCategory cat = new V1SquareItemCategory { name = name};
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            string item_json = new JavaScriptSerializer().Serialize(cat);
            _V1UpdObject(url, item_json);
            return true;


        }
        public bool V1DelItemCategory(string square_category_id)
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/categories" + "/" + square_category_id;
            return _V1DelObject(url);
        }
        public List<V1SquareItemCategory> V1GetItemCategories()
        {
            string url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + _merchantId + "/categories";
            string nplink = url;
            List<V1SquareItemCategory> itemList = new List<V1SquareItemCategory>();
            do
            {
                KeyValuePair<string, string> v = _V1GetObjects(nplink);
                string json = v.Value;
                nplink = v.Key;
                var items = new JavaScriptSerializer().Deserialize<List<V1SquareItemCategory>>(json);
                itemList.AddRange(items);
            }
            while (!string.IsNullOrEmpty(nplink));

            return itemList;

        }

        public SquareItem AddItem(string Name, string Description, string Abbreviation, string square_category_id, string Currency, double Price, string Sku, string SkuName, int Quantity, bool bApplyAllFee, string MyData)
        {
            SquareItem item = new SquareItem { name = Name, abbreviation = Abbreviation, description = Description, square_category_id = square_category_id, invn = new SquareItemInventory { name = Name, currency = Currency, price = Price, sku = Sku, quantity = Quantity } };
            V1AddItem(item);
            if (Quantity > 0) V1AddInvnMovement(item.invn.square_id, Quantity, "RECEIVE_STOCK");
            if (bApplyAllFee)
            {
                foreach (var f in V1GetFee())
                {
                    V1ApplyFee(f.square_id, item.square_id);
                }
            }
            return item;
        }
        public SquareItem UpdItem(string square_item_id, string square_invn_id, string Name, string Description, string Abbreviation, string square_category_id, string Currency, double Price, string Sku, string SkuName, int Quantity, string MyData)
        {
            SquareItem item = new SquareItem { square_id = square_item_id, name = Name, abbreviation = Abbreviation, description = Description, square_category_id = square_category_id, invn = new SquareItemInventory { name = Name, square_id = square_invn_id, square_item_id = square_item_id, currency = Currency, price = Price, sku = Sku, quantity = Quantity } };
            return UpdItem(item);
        }
        public SquareItem UpdItem(SquareItem item)
        {
            V1UpdItem(item);
            return item;
        }
        public SquareItemImage UpdItemImage(string square_item_id, string name, byte[] content, string mimeType)
        {
            V1SquareItemImage img = V1UpdItemImage(square_item_id, name, content, mimeType);
            return new SquareItemImage { square_id = img.id, url = _baseUrl };
        }
        public SquareItem GetItem(string square_item_id)
        {
            return V1GetItem(square_item_id);
        }
        public List<SquareItem> GetItems()
        {
            return V1GetItems();
        }
        public List<SquareItemInventory> GetItemInventory()
        {
            List<V1SquareInventoryEntry> i = V1GetInventory();
            List<SquareItemInventory> si = new List<SquareItemInventory>();
            foreach (var x in i)
            {
                si.Add(new SquareItemInventory { square_id = x.variation_id, quantity = x.quantity_on_hand});
            }
            return si;
        }

        public int AdjustInventory(string square_invn_id, int delta, string type)
        {
            return V1AddInvnMovement(square_invn_id, delta, type);
        }
        public int SetInventory(string square_invn_id, int quantity)
        {
            int curr_quantity = V1AddInvnMovement(square_invn_id, 0, "MANUAL_ADJUST");
            return V1AddInvnMovement(square_invn_id, quantity - curr_quantity, "MANUAL_ADJUST");
        }
        public bool DelItem(SquareItem item)
        {
            return V1DelItem(item.square_id);
        }
        public bool DelItem(string square_item_id)
        {
            return V1DelItem(square_item_id);
        }
        public bool AddFee(string name, string rate, bool bAddToItems)
        {
            V1SquareFee fee = V1AddFee(name, rate);
            if (bAddToItems)
            {
                foreach (var i in V1GetItems())
                {
                    V1ApplyFee(fee.id, i.square_id);
                }
            }
            return true;
        }
        public bool UpdFee(string square_fee_id, string name, string rate)
        {
            return V1UpdFee(square_fee_id, name, rate);
        }
        public bool DelFee(string square_fee_id)
        {
            return V1DelFee(square_fee_id);
        }
        public List<SquareFeeAndTax> GetFees()
        {
            List<SquareFeeAndTax> fees = new List<SquareFeeAndTax>();
            foreach (var i in V1GetFees())
            {
                fees.Add(new SquareFeeAndTax { rate = i.rate, name = i.name, square_id = i.id });
            }
            return fees;
        }
        public SquareItemCategory AddItemCategory(string name)
        {
            V1SquareItemCategory cat = V1AddItemCategory(name);
            return new SquareItemCategory { name = cat.name, square_id = cat.id };
        }
        public bool UpdItemCategory(string square_category_id, string name)
        {
            return V1UpdItemCategory(square_category_id, name);
        }
        public bool DelItemCategory(string square_category_id)
        {
            return V1DelItemCategory(square_category_id);
        }
        public List<SquareItemCategory> GetItemCategories()
        {
            List<SquareItemCategory> cats = new List<SquareItemCategory>();
            foreach (var i in V1GetItemCategories())
            {
                cats.Add(new SquareItemCategory { name = i.name, square_id = i.id });
            }
            return cats;
        }
    }
}
