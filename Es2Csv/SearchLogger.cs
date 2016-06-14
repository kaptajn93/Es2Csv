using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es2Csv;

namespace Es2Csv
{



    public class SearchLogger
    {
        public SearchLogger()
        {
            Beat = new Beat();
            Logdata = new Logdata();
        }

        public SearchLogger(string id)
        {
        }

        public string Timestamp { get; set; }
        public Beat Beat { get; set; }
        public int Count { get; set; }
        public string Input_type { get; set; }
        public string Offset { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public string Logtime { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public Logdata Logdata { get; set; }
    }

    public class Beat
    {
        public string Hostname { get; set; }
        public string Name { get; set; }
    }
    public class Logdata
    {
        public Logdata()
        {
            Message = new Message();
        }
        public Message Message { get; set; }
    }

    public class Message
    {
        public Message()
        {
            Session = new Session();
            Query = new Query();
            Result = new Result();
        }
        public Session Session { get; set; }
        public Query Query { get; set; }
        public Result Result { get; set; }

        public string Verb { get; set; }
        public string StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string AuthenticationType { get; set; }
        public string RequestUri { get; set; }
        public string TrackingId { get; set; }
        public string CallDateTime { get; set; }
        public string CallDuration { get; set; }
        public bool Failed { get; set; }
        public string ErrorMessage { get; set; }
        public string HostName { get; set; }
        public string UserAgent { get; set; }
    }

    public class Session
    {
        public string SessionId { get; set; }
        public string LoginProvider { get; set; }
        public string ValidatedIp { get; set; }
        public string ClientIp { get; set; }
        public string CustomerId { get; set; }
        public string CompanyId { get; set; }
        public string ClientType { get; set; }
        public string Institution { get; set; }
    }

    public class Query
    {
        public Query()
        {
            Params = new Params();
        }

        public string DimensionType { get; set; }
        public string SearchText { get; set; }
        public string BookIds { get; set; }
        public string Level { get; set; }
        public Params Params { get; set; }
    }

    public class Params
    {
        public string SearchDirection { get; set; }
        public int PageNumber { get; set; }
    }

    public class Result
    {
        public int Hits { get; set; }
        public bool IsCached { get; set; }
        public string BookIds { get; set; }
    }

    public class GeoIp
    {
        public string Ip { get; set; }
        public string Country_Code2 { get; set; }
        public string Country_Code3 { get; set; }
        public string Country_Name { get; set; }
        public string Continent_Code { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Timezone { get; set; }
        public List<string> Location { get; set; }
    }
}

