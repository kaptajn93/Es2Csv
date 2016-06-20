# Es2Csv

Es2Csv is a program that searches through all documents of a specifyed type, in an index, and takes out the propperties and map them to an csv-file.


For this program to work, you must specify the fallowing in the Es2Csv.Config file:
- Uri to your elasticsearch node.
- Index to search in.
- Type to search for in index.
- Filepath to specify where you want the csv-file to be placed (name is given by the index).
- Mappings of the propperties you want to map to csv.

You are also able to change the default values of "From", "Size" and "SortBy", where the "SortBy" can be set to a field-name like "@timestamp" or other fields you want to sort your search by. - default is set to _doc, which is the fastest way to get get all documents, but not in a given order.

#### How the configFile should look like:
```
{
  "uri": "your-Elasticsearch-Uri",
  "from": 0,
  "size": 10000,
  "index": "your-index-name",
  "type": "your-type",
  "sortBy": "_doc",
  "filePath": "c:\\users\\user\\documents\\Es2Csv-Files\\{index}.csv",
  "mappings": {
   	"@timestamp": "Timestamp",
	"GOO": "QueryApplication",
	"logdata.Foo.Bar.SearchText": "QuerySearchText",
	"logdata.message.Query.Params.searchDirection": "QuerySearchDirection",
	"logdata.message.Query.Level": "QueryLevel",
	"logdata.message.Query.DimensionType": "DimensionType",
	"logdata.message.Query.BookIds": "QueryBookIds",
	"logdata.message.Result.Hits": "ResultHits",
	"logdata.message.Result.IsCached": "ResultIsCached",
	"logdata.message.Result.BookIds": "ResultBookIds",
	"logdata.message.Session.ClientIp": "ClientIp",
	"logdata.message.Session.ClientType": "ClientType",
	"logdata.message.Session.SessionId": "SessionId",
	"logdata.message.CallDuration": "TimeElapsed",
	"logdata.message.Session.CustomerId": "CustomerId",
	"logdata.message.Session.CompanyId": "CompanyId",
	"logdata.message.Session.LoginProvider": "LoginProvider",
	"logdata.message.HostName": "Domain",
	"IP-0A0001E3": "Webserver"
   } 
}
```
As you can see it is possible to go down nested objects as well as setting default values like i did in the Webserver and QueryApplication.
 - proppertes to the left are your propperties from elasticsearch.
 - propperties to the right are what you are mapping them to be in the csv-file.


### To run the program do the fallowing:

- open cmd prompt.
- navigate to where the Es2Csv.exe file is using cd.

now writhe the fallowing in cmd prompt:
```sh
Es23Csv.exe -c "C:\your-file-path\Es2Csv.Config"
```
Now the csv file should be placed under the folder you specifyed under filePath.

hope this helped you out.

#### Cheers!
