using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es2Csv
{

    public abstract class BaseDocument
    {
        public DateTime TimeStamp { get; set; }

        protected BaseDocument()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }



    [Nest.ElasticsearchType]
    public class LemmaDocument
    {
        /// <summary>
        ///     Afspejler documentet som skal indsættes i ElasaticSearch
        /// </summary>
        public LemmaDocument()
        {
            LemmaPos = new LemmaPos();
            LemmaGender = new LemmaGender();
            LemmaInflection = new LemmaInflection();
            LemmaVariants = new LemmaVariants();
            LemmaAccessoryDatas = new List<LemmaAccessoryData>();
            Usage = new LemmaUsage();
            PronuciatetionAll = new LemmaPronuciatetionAll();
            Illustration = new LemmaIllustration();
            AbbreviationFor = new LemmaAbbreviationFor();

        }

        public LemmaDocument(string id) : this()
        {

        }


        public string LemmaLanguage { get; set; }
        public string LemmaId { get; set; }
        public string LemmaOrtography { get; set; }
        public LemmaPos LemmaPos { get; set; }
        public LemmaGender LemmaGender { get; set; }
        public LemmaInflection LemmaInflection { get; set; }
        public LemmaVariants LemmaVariants { get; set; }
        public string LemmaMeAsFirst { get; set; }
        public string LemmaMeAsLast { get; set; }
        public IList<LemmaAccessoryData> LemmaAccessoryDatas { get; set; }
        public LemmaUsage Usage { get; set; }
        public LemmaPronuciatetionAll PronuciatetionAll { get; set; }
        public LemmaIllustration Illustration { get; set; }
        public LemmaAbbreviationFor AbbreviationFor { get; set; }

        public int SoundfilesCount { get; set; }
        public int IllustrationsCount { get; set; }

    }

}


public class LemmaPos
{
    public string PosNameDan { get; set; }
    public string PosNameGyl { get; set; }
    public string PosNameEng { get; set; }
    public string PosNameLat { get; set; }
    public string PosShortNameDan { get; set; }
    public string PosShortNameGyl { get; set; }
    public string PosShortNameEng { get; set; }
    public string PosShortNameLat { get; set; }
}

public class LemmaGender
{
    public string GenNameDan { get; set; }
    public string GenNameGyl { get; set; }
    public string GenNameEng { get; set; }
    public string GenNameLat { get; set; }
    public string GenShortNameDan { get; set; }
    public string GenShortNameGyl { get; set; }
    public string GenShortNameEng { get; set; }
    public string GenShortNameLat { get; set; }
}

public class LemmaVariants
{
    public string LemmaVariantsRefPos { get; set; }
    public string LemmaVariantsRefRef { get; set; }
}

public class LemmaAccessoryData
{
    public string CategoryDan { get; set; }
    public string CategoryEng { get; set; }
    public IList<LemmaReference> LemmaAccessDataReferencesRefs { get; set; }
}

public class LemmaReference
{
    public string LemmaPos { get; set; }
    public string LemmaRef { get; set; }
}

public class LemmaInflection
{
    public string CompactPresentation { get; set; }

    public IList<LemmaTablePresentation> TablePresentations { get; set; }
    /// <summary>
    ///     Contains the entire table-presentation element. 
    /// </summary>
    public IList<LemmaSearchableParadigm> SearchableParadigms { get; set; }
}
public class LemmaSearchableParadigm
{
    public IList<LemmaInflectedForm> LemmaInflectedForms { get; set; }

}
public class LemmaInflectedForm
{
    public string LeaveOut { get; set; }
    public IList<LemmaInflectedFormCategory> InflectedFormCategories { get; set; }
    public string InflectedFormFullForm { get; set; }
    public string InflectedFormCompactForm { get; set; }
}
public class LemmaInflectedFormCategory
{
    public string InfCatNameDan { get; set; }
    public string InfCatNameGyl { get; set; }
    public string InfCatNameEng { get; set; }
    public string InfCatNameLat { get; set; }
    public string InfCatShortNameDan { get; set; }
    public string InfCatShortNameGyl { get; set; }
    public string InfCatShortNameEng { get; set; }
    public string InfCatShortNameLat { get; set; }
}

public class LemmaTablePresentation
{
    public IList<LemmaTpRow> LemmaTpRows { get; set; }
}
public class LemmaTpRow
{
    public IList<LemmaTpRowCells> LemmaTpRowCellses { get; set; }
}
public class LemmaTpRowCells
{
    public string CellType { get; set; }
    public string CellName { get; set; }

}

public class LemmaUsage
{
    public string Usage { get; set; }
}

public class LemmaPronuciatetionAll
{
    public IList<LemmaPronuciatetionVariant> ProVariants { get; set; }
}
public class LemmaPronuciatetionVariant
{
    public IList<LemmaVariantDescription> VariantDescriptions { get; set; }
    public IList<LemmaPronunciation> LemmaPronunciations { get; set; }
}
public class LemmaVariantDescription
{
    public string TechLang { get; set; }
    public string VariantsDescription { get; set; }
    public string LemmaProDescription { get; set; }
    public IList<LemmaLangVariant> LangVariants { get; set; }
}
public class LemmaLangVariant
{
    public string LangVariant { get; set; }
}

public class LemmaPronunciation
{
    public string SoundFile { get; set; }
    public string IPA { get; set; }
    public string IPApart { get; set; }
    public string Stress { get; set; }
}

public class LemmaIllustration
{
    public IList<LemmaIllustrationFile> IllustrationFiles { get; set; }
}
public class LemmaIllustrationFile
{
    public string IllFileType { get; set; }
    public string IllFileRef { get; set; }
}

public class LemmaAbbreviationFor
{
    public IList<LemmablindRef> LemmablindRefs { get; set; }
    public IList<LemmaAbbrevationRef> AbbrevationRefs { get; set; }
}

public class LemmablindRef
{
    public string LemmaBlindRef { get; set; }
}

public class LemmaAbbrevationRef
{
    public string AbbDescRef { get; set; }
    public string AbbPos { get; set; }
    public string AbbRef { get; set; }
}