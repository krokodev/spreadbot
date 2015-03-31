# ============================================================================================== []
Function UploadFeed([string]$feed, [string]$requestId)
{
    $localDir  = $LocalZipPath
    $fileName  = "$feed.$requestId.zip"
    $remoteDir = "$RemotePath$feed/"
    
    UploadFile $localDir $fileName $remoteDir
}

# ============================================================================================== []
Function ZipUploadFeed([string]$feed)
{
    $requestId = MakeRequestId
    Debug "ZipUpload [$feed.$requestId]"

    ZipFeed    $feed $requestId
    UploadFeed $feed $requestId
    $requestId
}

# ============================================================================================== []
# EOF