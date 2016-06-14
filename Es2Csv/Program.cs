using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Es2Csv
{
    class Program
    {
        static ElasticManager _manager = new ElasticManager();

        public static void Main(string[] args)

        {
            int from = 0;
            string index = "logstash-2016.06.14";
            int size = 1000;
            string type = "searchlogger";

            try
            {
                _manager.EntrySearchByTimestamp(from, size, index, type);
            }
            catch (Exception ex)
            {

                throw new NullReferenceException(ex.ToString());
            }

        }

    }
}
