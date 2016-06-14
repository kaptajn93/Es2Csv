using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Xunit;

namespace Es2Csv
{
    public class Test : IDisposable
    {

        ElasticManager _manager;

        public Test()
        {
            _manager = new ElasticManager();
        }

        [Theory]
        [InlineData("*")]
        public void GetLemmaDocumentById(string lemmaId)
        {
            var response = _manager.Client.Search<LemmaDocument>(s => s
                .From(0)
                .Size(10)
                .Query(q =>
                    q.Term(p => p.LemmaId, lemmaId))
                );
            Assert.Equal(response.Hits?.Count(), 10);
        }

        //second search - in entry
        [Theory]
        [InlineData("grine ad en vittighed", "0", "da", "dan-sko-ret")]
        public ISearchResponse<EntryDocument> EntrySearchByHeadWordExact(string searchword, int from, string index, string searchInBooks)
        {
            string[] booksFromUser = searchInBooks.Split(' ');
            string books = string.Join(" || ", booksFromUser);

            var response = _manager.Client.Search<EntryDocument>(e => e
                .Index(index)
                .Type("entrydocument")
                .From(from)
                .Size(10)
                .Query(q1 => q1
                   .Bool(b => b
                        .Filter(f => f.Match(m => m.Field(fi => fi.IdBook).Query(books)))
                        .Must(mu =>
                            mu.Match(ma => ma.Field(fi => fi.HeadWordExact).Query(searchword))
                                &&
                            mu.Term(ma => ma.Unbound, true)
                        )
                   )
                ));
            return response;
        }


        //second search - in entry
        [Theory]
        [InlineData(" ", "0", "da", "spda-mini")]
        public ISearchResponse<EntryDocument> EntrySearchByTimestamp(string searchword, int from, string index, string searchInBooks)
        {
            string startDate = "2016-06-13T11:46:04.8026665Z";

            var response = _manager.Client.Search<EntryDocument>(e => e
                .Index(index)
                .Type("entrydocument")
                .From(from)
                .Size(15)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Match(ma => ma.Field(f => f.HeadWord).Query(" ")))))
                    .Sort(s => s.Field(fi => fi.Field(fie => fie.TimeStamp))));

            foreach (var doc in response.Documents)
            {
                Console.WriteLine("Time: " + doc.TimeStamp + " | HeadWord: " + doc.HeadWord);
            }
            return response;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
