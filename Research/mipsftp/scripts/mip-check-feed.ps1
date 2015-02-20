param (
    [Parameter(Mandatory=$True)] [string] $feed,
    [Parameter(Mandatory=$True)] [string] $requestId
)

. .\mip.lib.ps1

Function Main()
{
    CheckStatus $feed $requestId
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
