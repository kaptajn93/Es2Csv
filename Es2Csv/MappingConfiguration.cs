using System;
using System.Collections.Generic;

namespace Es2Csv
{
    public class MappingConfiguration   
    {
        public Uri Uri { get; set; }
        public Dictionary<string, string> Mappings { get; set; }
        public string Index { get; set; }
        public int From { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string SortBy { get; set; }
        public string FilePath { get; set; }
    }
}