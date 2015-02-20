. .\mip.lib.globals.ps1
. .\mip.lib.utils.ps1
. .\mip.lib.zip.ps1
. .\mip.lib.sftp.ps1

Log "MIP scripts"

Function ZipUploadFeed([string] $feed)
{
    $requestId = MakeRequestId
    Log "ZipUpload [$feed.$requestId]..."

    ZipFeed    $feed $requestId
    UploadFeed $feed $requestId
    $requestId
}

Function MakeRequestId()
{
    #[System.Guid]::NewGuid()
    $random = New-Object System.Random
    $random.Next(1000,9999)
}
