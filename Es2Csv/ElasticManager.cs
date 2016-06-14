using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Es2Csv
{
    public class Constants
    {
        public class AnalyzerNames
        {
            public const string NgramAnalyzer = "ngramAnalyzer";
            public const string Keyword = "keyword";
            public const string Lowercase = "lowercase";
        }
        public class TokenizerNames
        {
            public const string NoWhitespaceNGram = "noWhitespaceNGram";
        }
        public class IndexNames
        {
            public const string Da = "da";
        }
    }

    public class ElasticManager
    {
        public Uri NodeUri { get; set; }
        public ConnectionSettings Settings { get; set; }
        public ElasticClient Client { get; set; }

        public ElasticManager(Uri node = null)
        {
            //NodeUri = node ?? new Uri("http://192.168.99.100:9200");
            NodeUri = node ?? new Uri("http://54.171.247.56:9200");


            Settings = new ConnectionSettings(NodeUri);
            Client = new ElasticClient(Settings);

        }


        public ISearchResponse<SearchLogger> EntrySearchByTimestamp(int from, int size, string index, string type)
        {
            var response = Client.Search<SearchLogger>(e => e
               .Index(index)
               .Type(type)
               .From(from)
               .Size(size)
               .Sort(s => s.Field(f => f.Field("@timestamp")))
           );
            //var dome = (Newtonsoft.Json.Linq.JObject)response2.Documents.FirstOrDefault();

            var mapper = new MapperSearchLogger();
            var csv = mapper.MapToCsv(response);

            //or

            //IMapper mapper = new MapperGyldendalLogs();
            //var csv = mapper.MapToCsv(response);
            var filename =
                $"c:\\users\\hsm\\documents\\visual studio 2015\\Projects\\Es2Csv\\Es2Csv\\searchlogger.{index}.csv";
            File.AppendAllText(filename, csv.ToString(), Encoding.UTF8);

            return response;
        }
    }
}