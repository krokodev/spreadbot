Function UploadFeed([string]$feed, [string]$requestId)
{
    $session = New-Object WinSCP.Session

    try
    {
        $session.Open($SessionOptions)
 
        $sourceFiles    = "$LocalZipPath\$feed.$requestId.zip"
        $destination    = "$RemotePath$feed/"
        $transferResult = $session.PutFiles($sourceFiles, $destination, $false, $TransferOptions)
 
        $transferResult.Check()
 
        if($transferResult.Transfers.Count -lt 1)
        {
            throw [string]::Format("No files were upload for feed [{0}]", $feed)
        }
    }
    finally
    {
        $session.Dispose()
    }
}


Function ZipUploadFeed([string]$feed)
{
    $requestId = MakeRequestId
    Debug "ZipUpload [$feed.$requestId]"

    ZipFeed    $feed $requestId
    UploadFeed $feed $requestId
    $requestId
}