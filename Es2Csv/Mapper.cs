using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Es2Csv
{
    public interface IMapper<T> where T : class
    {
        StringBuilder MapToCsv(ISearchResponse<T> response);
    }


    public class MapperEntryDocument : IMapper<EntryDocument>
    {
        public StringBuilder MapToCsv(ISearchResponse<EntryDocument> response)
        {
            var csv = new StringBuilder();
            string csvFormatEntry = "IdLemmaPos,IdLemmaRef,IdLemmaDescriptionRef,LemmaIdRef,HeadPosShortNameGyl," +
                                    "HeadWord,IdBook,IdEntry,PrioritizeWhenLemma.Any(),SenseCount,TimeStamp,Unbound";

            csv.Append(csvFormatEntry + "\n \n");

            if (response.Hits != null)
                foreach (var doc in response.Documents)
                {
                    var csvFormatResponseEntry = string.Format(
                         "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}\n",
                         doc.EntryIdLemma.IdLemmaPos, doc.EntryIdLemma.IdLemmaRef,
                         doc.EntryIdLemma.IdLemmaDescriptionRef, doc.EntryIdLemma.LemmaIdRef, doc.HeadPosShortNameGyl,
                         doc.HeadWord, doc.IdBook, doc.IdEntry, doc.PrioritizeWhenLemma.Any(), doc.SenseCount,
                         doc.TimeStamp, doc.Unbound);

                    csv.Append(csvFormatResponseEntry);
                }
            return csv;

        }
    }


    public class MapperSearchLogger : IMapper<SearchLogger>
    {
        public StringBuilder MapToCsv(ISearchResponse<SearchLogger> response)
        {
            StringBuilder builder = new StringBuilder();

            var csvFile = builder.AppendLine("\nTimestamp,QueryApplication,QuerySearchText,QuerySearchDirection,QueryLevel," +
                                          "QueryDimension,QueryBookIds,ResultHits,ResultIsCached,ResultBookIds,ClientIp,ClientType," +
                                          "SessionId,TimeElapsed,CustomerId,CompanyId,LoginProvider,Domain,Webserver");

            foreach (var doc in response.Documents)
            {
                var LDM = doc.Logdata.Message;

                if(string.IsNullOrEmpty(LDM.CallDateTime))
                    continue;

                csvFile.AppendLine(
                    $"{LDM.CallDateTime}," +
                    $"GOO," +
                    $"{LDM.Query.SearchText}," +
                    $"{LDM.Query.Params.SearchDirection}," +
                    $"{LDM.Query.Level}," +
                    $"{LDM.Query.DimensionType}," +
                    $"{LDM.Query.BookIds}," +
                    $"{LDM.Result.Hits}," +
                    $"{LDM.Result.IsCached}," +
                    $"{LDM.Result.BookIds}," +
                    $"{LDM.Session.ClientIp}," +
                    $"{LDM.Session.ClientType}," +
                    $"{LDM.Session.SessionId}," +
                    $"{LDM.CallDuration}," +
                    $"{LDM.Session.CustomerId}," +
                    $"{LDM.Session.CompanyId}," +
                    $"{LDM.Session.LoginProvider}," +
                    $"{LDM.HostName}," +
                    $"IP-0A0001E3");
            }

            return csvFile;
        }
    }


}
