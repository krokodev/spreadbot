param (
    [Parameter(Mandatory=$True)] [string] $feed
)

. .\mip.lib.ps1

Function Main()
{
    $requestId = ZipUploadFeed $feed
    log "Feed $feed uploaded with RequestId = $requestId"
}


try
{
    Main
    exit 0
}
catch [Exception]
{
    Log $_.Exception.Message
    exit 1
}
