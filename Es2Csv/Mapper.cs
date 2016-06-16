using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Es2Csv
{
    public interface IMapper<T> where T : class
    {
        StringBuilder MapToCsv(ISearchResponse<T> response, Dictionary<string, string> mappings);
    }

    //public class MapperSearchLogger : IMapper<SearchLogger>
    //{
    //    public StringBuilder MapToCsv(ISearchResponse<SearchLogger> response, Dictionary<string, string> mappings)
    //    {
    //        StringBuilder builder = new StringBuilder();



    //        var csvFile = builder.AppendLine("\nTimestamp,QueryApplication,QuerySearchText,QuerySearchDirection,QueryLevel," +
    //                                      "QueryDimension,QueryBookIds,ResultHits,ResultIsCached,ResultBookIds,ClientIp,ClientType," +
    //                                      "SessionId,TimeElapsed,CustomerId,CompanyId,LoginProvider,Domain,Webserver\n");

    //        foreach (var doc in response.Documents)
    //        {
    //            var LDM = doc.Logdata.Message;

    //            if (string.IsNullOrEmpty(LDM.CallDateTime))
    //                continue;

    //            csvFile.AppendLine(
    //                $"{LDM.CallDateTime}," +
    //                $"GOO," +
    //                 WrapText($"{LDM.Query.SearchText}") +
    //                 WrapText($"{LDM.Query.Params.SearchDirection}") +
    //                 WrapText($"{LDM.Query.Level}") +
    //                 WrapText($"{LDM.Query.DimensionType}") +
    //                 WrapText($"{LDM.Query.BookIds}") +
    //                $"{LDM.Result.Hits}," +
    //                $"{LDM.Result.IsCached}," +
    //                 WrapText($"{LDM.Result.BookIds}") +
    //                 WrapText($"{LDM.Session.ClientIp.Trim()}") +
    //                 WrapText($"{LDM.Session.ClientType}") +
    //                 WrapText($"{LDM.Session.SessionId}") +
    //                 WrapText($"{LDM.CallDuration}") +
    //                 WrapText($"{LDM.Session.CustomerId}") +
    //                 WrapText($"{LDM.Session.CompanyId}") +
    //                 WrapText($"{LDM.Session.LoginProvider}") +
    //                WrapText($"{LDM.HostName}") +
    //                $"IP-0A0001E3");
    //        }

    //        return csvFile;
    //    }

    //    public string WrapText(string text)
    //    {
    //        return $"\"{text}\",";
    //    }
    //}


    public class Mapper : IMapper<Object>
    {
        /// <summary>
        ///     Map using test (inline) mappings
        /// </summary>

        //public StringBuilder MapToCsv2(ISearchResponse<object> response)
        //{
        //    StringBuilder builder = new StringBuilder();

        //    var csvFormat = "Timestamp,QueryApplication,QuerySearchText,QuerySearchDirection,QueryLevel," +
        //                    "QueryDimension,QueryBookIds,ResultHits,ResultIsCached,ResultBookIds,ClientIp,ClientType," +
        //                    "SessionId,TimeElapsed,CustomerId,CompanyId,LoginProvider,Domain,Webserver";

        //    var csvFile = builder.AppendLine($"\n{csvFormat}\n");

        //    foreach (var doc in response.Documents)
        //    {

        //        csvFile.AppendLine("");
        //    }

        //    return csvFile;
        //}

        public StringBuilder MapToCsv(ISearchResponse<object> response, Dictionary<string, string> mappings)
        {
            StringBuilder builder = new StringBuilder();

            var raw = response?.Hits.Select(x => x.Source).ToList();

            var csvFormat = string.Join(",", mappings.Values);
            builder.AppendLine(csvFormat);

            // foreach log/json row
            if (raw != null)
                foreach (var o in raw)
                {
                    Dictionary<string, string> csv = new Dictionary<string, string>();
                    var dict = JsonHelper.DeserializeAndFlatten(o.ToString());
                    var flat = JObject.FromObject(dict);

                    // for each property
                    foreach (var mappingFrom in mappings.Keys)
                    {
                        // check if the json object contains the property, select the token
                        JToken token;
                        flat.TryGetValue(mappingFrom, StringComparison.CurrentCultureIgnoreCase, out token);

                        if (token != null)
                        {
                            // if it does,..replace the old propertyname with the one defined in our mapping
                            string mappingTo;
                            if (mappings.TryGetValue(mappingFrom, out mappingTo))
                                csv.Add(mappingTo, $"\"{token.Value<string>()?.Trim()}\"");
                        }
                        else
                        {
                            string mappingTo;
                            if (mappings.TryGetValue(mappingFrom, out mappingTo))
                                csv.Add(mappingTo, $"\"{mappingFrom}\"");
                        }
                    }
                    builder.AppendLine(string.Join(",", csv.Values));
                }
            return builder;
        }
        public string WrapText(string text)
        {
            return $"\"{text}\",";
        }
    }
}
