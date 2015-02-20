Function CheckStatus([string]$feed, [string]$requestId)
{
    IsRequestInProcess $feed $requestId
    IsRequestInOutput  $feed $requestId
}

Function IsRequestInOutput([string]$feed, [string]$requestId)
{
    $dataDirName = MakeDataDirName
    RequestExists $feed "output/$dataDirName" $requestId
}


Function IsRequestInProcess([string]$feed, [string]$requestId)
{
    RequestExists $feed "inprocess" $requestId
}

Function RequestExists([string]$feed, [string]$subdir, [string]$requestId)
{
    $feedReqId = "$feed.$requestId"
    $session  = New-Object WinSCP.Session

    Log "Checking [$feedReqId] in [$subdir]"

    try
    {
        $session.Open($SessionOptions)
 
        $remoteDir = "$RemotePath$feed/$subdir"
        $directory = $Session.ListDirectory($remoteDir)
 
        foreach ($fileInfo in $directory.Files)
        {
            if($fileInfo.Name.Contains($feedReqId))
            {
                ($true, $fileInfo.Name)
                return
            }
        }
        ($false, "")
    }
    catch [Exception]
    {
        if(-not $_.Exception.Message.Contains("Error listing directory"))
        {
            Error $_.Exception.Message
        }
        ($false, "")
    }
    finally
    {
        $session.Dispose()
    }
}

