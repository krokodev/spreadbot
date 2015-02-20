param (
    [Parameter(Mandatory=$True)] [string] $feed
)

. .\mip.lib.ps1

Function Main()
{
    Log "Uploading feed [$feed]"
    $requestId = ZipUploadFeed $feed
    Log "Feed [$feed] uploaded"
    Value "RequestId" $requestId
}


try
{
    Main
    exit 0
}
catch [Exception]
{
    Error $_.Exception
    exit 1
}
