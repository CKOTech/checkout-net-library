using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Checkout.Helpers
{
    /// <summary>
    /// Converts content from one type to another e.g. object to json string
    /// </summary>
    public class ContentAdaptor
    {
        public static string ConvertToJsonString(object model)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(model)).Result;
            //return JsonConvert.SerializeObjectAsync(model).Result;
        }

        public static T JsonStringToObject<T>(string jsonString)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(jsonString)).Result;
            //return JsonConvert.DeserializeObjectAsync<T>(jsonString).Result;
        }

        public static void Setup()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        DateFormatString = "yyyy-MM-ddTHH:mm:ssZ"
                    };
        }

        #region Xml Support
        //public static XmlDocument ParseXml(string xmlString)
        //{
        //    var xmlDocument = new XmlDocument();
        //    xmlDocument.LoadXml(xmlString);
        //    return xmlDocument;
        //}

        //public static JObject ParseJson(string jsonString)
        //{
        //    return JObject.Parse(jsonString);
        //}

        //public static XmlDocument ConvertToXmlDocument(string jsonString)
        //{
        //    return JsonConvert.DeserializeXmlNode(jsonString, "data");
        //}

        //public static string ConvertToXmlString(object model)
        //{
        //    var jsonString = ConvertToJsonString(model);
        //    var xmlDocument = ConvertToXmlDocument(jsonString);

        //    return xmlDocument.OuterXml;
        //}

        //public static T XmlStringToObject<T>(string xmlString, bool skippRoot = true)
        //{
        //    var jsonString = JsonConvert.SerializeXmlNode(ParseXml(xmlString), Newtonsoft.Json.Formatting.None, skippRoot);
        //    return JsonStringToObject<T>(jsonString);
        //}
        #endregion
    }
}
