. .\mip.globals.ps1
. .\mip.utils.ps1
. .\mip.zip.ps1
. .\mip.sftp.ps1

Function Main()
{
    Log "MIP scripts"

    ZipUploadFeed("product")
    ZipUploadFeed("distribution")
    ZipUploadFeed("availability")
}

Function ZipUploadFeed([string] $feed)
{
    Log "ZipUpload [$feed]..."

    ZipFeed    $feed
    UploadFeed $feed
}

try
{
    Main
    exit 0
}
catch [Exception]
{
    Write-Host $_.Exception.Message
    exit 1
}
