using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
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

        public ISearchResponse<Object> EntrySearch(int from, int size, string index, string type, Dictionary<string, string> mappings)
        {
            Settings = new ConnectionSettings(NodeUri);
            Client = new ElasticClient(Settings);
            var scroll = 1;

            var response = Client.Search<Object>(e => e
               .Index(index)
               .Type(type)
               .From(from)
               .Size(size)
               .Sort(s => s.Field(f => f.Field("@timestamp")))
               .SearchType(SearchType.Scan)
               .Scroll(scroll)
           );

            if (response.Hits.Any())
            {
                //do
                //{
                //    var result = response;
                //    result = Client.Scroll<Object>(s => s
                //        .Scroll(scroll)
                //        .ScrollId(result.ScrollId)
                //    );
                //    if (response.Documents.Any())
                //        indexResult = this.IndexSearchResults(searchResult, observer, toIndex, page);
                //    page++;


                //      se:         http://stackoverflow.com/questions/31327814/scroll-example-in-elasticsearch-nest-api
                //}

                var mapper = new Mapper();
                var csv = mapper.MapToCsv(response, mappings);
                string csvString = csv.ToString();
                var filename = $"c:\\users\\hsm\\documents\\visual studio 2015\\Projects\\Es2Csv\\Es2Csv\\Logs\\searchlogger.{index}.csv";
                File.AppendAllText(filename, csvString, Encoding.UTF8);
                return response;
            }
            else
            {
                var msg = "could not find any searchResponse, please check your config-file or Elasticsearch data";
                Console.WriteLine(msg);
                return null;
            }

        }
    }
}