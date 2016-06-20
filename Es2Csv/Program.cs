using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                else if (_config.SortBy == null)
                    WriteMissingArg("sortBy");
                else if (_config.FilePath == null)
                    WriteMissingArg("filePath");

                #endregion

                else
                {
                    var index = config.Index;
                    var size = config.Size;
                    var from = config.From;
                    var type = config.Type;
                    var mappings = config.Mappings;
                    var sortBy = config.SortBy;
                    var filePath = config.FilePath;
                    _manager.NodeUri = config.Uri;
                    try
                    {
                        Measure(() =>
                        {
                            _manager.EntrySearch(from, size, index, type, mappings, sortBy, filePath);
                        });

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
        }

        private static void Measure(Action action)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            action.Invoke();

            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("Elapsed: " + elapsedTime);

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

