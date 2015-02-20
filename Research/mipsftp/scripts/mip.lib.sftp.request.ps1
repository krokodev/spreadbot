# ============================================================================================== []
Function GetRequestDoneContent([string]$feed, [string]$requestId)
{
    ($done, $remoteDir, $contentFileName) = IsRequestDone $feed $requestId
    if ($done)
    {
        $content = GetRemoteFileContent "$remoteDir/$contentFileName"
        ($true, $content)
        return 
    }
    ($false, "Request [$feed.$requestId] is in [$remoteDir]")
}

# ============================================================================================== []
Function IsRequestDone([string]$feed, [string]$requestId)
{
    ($found, $remoteDir, $fileName) = IsRequestInProcess $feed $requestId
    if($found)
    {
        ($false, $remoteDir, $fileName)
        return 
    }

    ($found, $remoteDir, $fileName) = IsRequestInOutput  $feed $requestId
    if($found)
    {
        ($true, $remoteDir, $fileName)
        return        
    }

    throw "Unknown status for [$feed.$requestId]"
}

# ============================================================================================== []
Function IsRequestInOutput([string]$feed, [string]$requestId)
{
    $dataDirName = MakeDataDirName
    RequestExists $feed "output/$dataDirName" $requestId
}

# ============================================================================================== []
Function IsRequestInProcess([string]$feed, [string]$requestId)
{
    RequestExists $feed "inprocess" $requestId
}

# ============================================================================================== []
Function RequestExists([string]$feed, [string]$subdir, [string]$requestId)
{
    Debug "Checking [$feed.$requestId] in [$subdir]"

    $remoteDir = "$RemotePath$feed/$subdir"
    $files     = GetRemoteDirFiles $remoteDir

    foreach ($fileInfo in $files)
    {
        if($fileInfo.Name.Contains("$feed.$requestId"))
        {
            ($true, $remoteDir, $fileInfo.Name)
            return
        }
    }

    ($false, $remoteDir, "")
}

# ============================================================================================== []
# EOF