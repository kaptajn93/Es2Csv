using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;

namespace Es2Csv
{
    class Program
    {
        static ElasticManager _manager = new ElasticManager();
        static MappingConfiguration _config = new MappingConfiguration();

        public static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                #region -- Validate user command arguments: -- 

                // Values are available here
                if (string.IsNullOrEmpty(options.ConfigurationFile))
                {
                    if (!File.Exists(options.ConfigurationFile))
                    {
                        Console.WriteLine($"{options.ConfigurationFile} is not a valid filepath!");
                        return;
                    }
                }

                #endregion

                #region -- Validate config file: --
                MappingConfiguration config;
                try
                {
                    config = GetFile(options.ConfigurationFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}!");
                    return;

                }

                if (_config.Index == null)
                    WriteMissingArg("index");
                else if (_config.Uri == null)
                    WriteMissingArg("uri");
                else if (_config.Mappings == null)
                    WriteMissingArg("mappings");
                else if (_config.Type == null)
                    WriteMissingArg("type");
                else if (_config.Size <= 0)
                    WriteMissingArg("size");
                #endregion

                else
                {
                    var index = config.Index;
                    var size = config.Size;
                    var from = config.From;
                    var type = config.Type;
                    var mappings = config.Mappings;
                    _manager.NodeUri = config.Uri;
                    try
                    {
                        _manager.EntrySearch(from, size, index, type, mappings);
                    }
                    catch (Exception ex)
                    {
                        var msg = "could not find any searchResponse, please check config-file";
                        NullReferenceException nullReference = new NullReferenceException("msg", ex);
                        throw nullReference;
                    }
                }
            }
            else
            {
                Console.WriteLine("Need filepath to your config-file. Please enter: \"Es2Csv.exe -c \"your-config-file-path\"\"");
            }


            //if (args != null)
            //{
            //    string filepath = $"\"@{args.ToString()}\"";
            //    //string filepath = @"C:\temp\Es2Csv.config";
            //    var config = getFile(filepath);
            //}

        }

        private static void WriteMissingArg(string argument)
        {
            Console.WriteLine($"{argument} is not defined");
        }

        public static MappingConfiguration GetFile(string filepath)
        {
            try
            {
                var contents = File.ReadAllText(filepath);
                _config = JsonConvert.DeserializeObject<MappingConfiguration>(contents);
            }
            catch (Exception ex)
            {
                FileNotFoundException fileNotFound = new FileNotFoundException("invalid filepat", ex);
                throw fileNotFound;
            }





            return _config;
        }
    }
}

