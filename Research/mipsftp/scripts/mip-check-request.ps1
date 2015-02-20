param (
    [Parameter(Mandatory=$True)] [string] $feed,
    [Parameter(Mandatory=$True)] [string] $reqid
)

. .\mip.lib.ps1

Function Main()
{
    $DebugMode = $true
    Log "Checking request [$feed.$reqid]"
    ($done, $content) = GetRequestDoneContent $feed $reqid
    Log "Request [$feed.$reqid] checked"
    Value "Done   " $done
    Value "Content" $content
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
