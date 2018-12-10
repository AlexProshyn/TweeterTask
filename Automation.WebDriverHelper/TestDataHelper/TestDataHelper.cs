using Automation.Contracts;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace Automation.Utilities.TestDataHelper
{
    public class TestDataHelper
    {
        public static LoginParameters GetLoginDataFromJson(string json, string userType)
        {
            JObject parsedJson = JObject.Parse(json);

            JArray types = (JArray)parsedJson["logins"];

            if (types == null)
            {
                throw new NullReferenceException("Json root element isn't found");
            }

            JToken typeToken = types.FirstOrDefault(j => (string)j["type"] == userType);

            if (typeToken == null)
            {
                throw new NullReferenceException("Json test data type with name " + userType + "isn't found");
            }

            string loginData = typeToken["credentials"].ToString();

            return JsonConvert.DeserializeObject<LoginParameters>(loginData);
        }
    }
}
