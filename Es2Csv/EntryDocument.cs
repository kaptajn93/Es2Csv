using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Es2Csv
{

    /// <summary>
    ///     Afspejler documentet som skal indsættes i ElasaticSearch
    /// </summary>
    [Nest.ElasticsearchType]
    public class EntryDocument : BaseDocument
    {
        public EntryDocument()
        {
        }

        public EntryDocument(string id)
        {
        }

        //id
        public string IdBook { get; set; }
        public string IdEntry { get; set; }
        public EntryIdLemma EntryIdLemma { get; set; }
        // /id

        public bool Unbound { get; set; }
        public IList<PrioritizeWhenLemma> PrioritizeWhenLemma { get; set; }

        //head

        //skal ikke lagres
        public string HeadWordExact { get; set; }
        public string HeadWord { get; set; }

        public string HeadPosShortNameGyl { get; set; }
        //body
        public string Blob { get; set; }
        //public string BodyHeadWordRef { get; set; }
        // public IList<TargetGroup> Subsense { get; set; }
        //public IList<AnnotatedTarget> TargetGroup { get; set; }

        //test counts
        public int SenseCount { get; set; }
        //public int SubsenseCount { get; set; }
        // 
    }

    public class EntryIdLemma
    {
        public string IdLemmaPos { get; set; }
        public string IdLemmaRef { get; set; }
        public string IdLemmaDescriptionRef { get; set; }
        public string LemmaIdRef { get; set; }
    }

    public class PrioritizeWhenLemma
    {
        public string PrioritizeLemmaPos { get; set; }
        public string PrioritizeLemmaRef { get; set; }
        public string PrioritizeLemmaDescRef { get; set; }
        public string PrioritizeLemmaIdRef { get; set; }
    }
}
