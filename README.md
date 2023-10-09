# Paged Cached oData Example

A demonstration of oData serving 100,000s records using paging to be consumed by PowerBI

## Create the project

`dotnet new webapi -o src/PagedCached.oData -controllers --use-program-main -f net8.0 -au SingleOrg --tenant-id 092f1e77-1f13-4057-a665-ece954058d06 --client-id 640e76ca-15a8-4cc0-9021-7e74cc8f79b3`

## oData

[ODataRoutingSample](https://github.com/OData/AspNetCoreOData/tree/main/sample/ODataRoutingSample)

[OData, CKAN AND Microsoft Azure: How OData typically provides access to large data sets](https://learn.microsoft.com/en-us/archive/msdn-magazine/2013/government-special-issue/odata-harness-open-data-with-ckan-odata-and-windows-azure)
