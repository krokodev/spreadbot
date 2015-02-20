# ============================================================================================== []
. .\mip.lib.sftp.init.ps1
. .\mip.lib.sftp.feed.ps1
. .\mip.lib.sftp.request.ps1

# ============================================================================================== []
function GetRemoteFileContent([string]$remoteDir, [string]$fileName)
{
    Debug "Downoading [$localPath\$fileName]"

    $localPath = $LocalInboxPath
    DownloadFile $remoteDir $fileName $localPath ([WinSCP.TransferMode]::Ascii)
    gc "$localPath\$fileName"
}

# ============================================================================================== []
function DownloadFile ([string]$remoteDir, [string]$fileName, [string]$localPath, [WinSCP.TransferMode]$transferMode=[WinSCP.TransferMode]::Binary)
{
    $session         = New-Object WinSCP.Session
    $transferOptions = New-Object WinSCP.TransferOptions
    $transferOptions.TransferMode = $transferMode
    try
    {
        $session.Open($SessionOptions) 
        $Session.GetFiles("$remoteDir/$fileName", "$localPath\$fileName", $false, $transferOptions).Check()
    }
    finally
    {
        $session.Dispose()
    }
}

# ============================================================================================== []
Function UploadFile([string]$localPath, [string]$fileName, [string]$remoteDir, [WinSCP.TransferMode]$transferMode=[WinSCP.TransferMode]::Binary)
{
    $session         = New-Object WinSCP.Session
    $transferOptions = New-Object WinSCP.TransferOptions
    $transferOptions.TransferMode = $transferMode
    try
    {
        $session.Open($SessionOptions)
        $transferResult = $session.PutFiles("$localPath\$fileName", $remoteDir, $false, $transferOptions)
        $transferResult.Check()
        if($transferResult.Transfers.Count -lt 1)
        {
            throw "Can't upload file [$localPath\$fileName] to [$remoteDir]"
        }
    }
    finally
    {
        $session.Dispose()
    }
}

# ============================================================================================== []
function GetRemoteDirFiles([string]$remoteDir)
{
    $session  = New-Object WinSCP.Session
    try
    {
        $session.Open($SessionOptions) 
        $Session.ListDirectory($remoteDir).Files
    }
    catch [Exception]
    {
        if(-not $_.Exception.Message.Contains("Error listing directory"))
        {
            Error $_.Exception.Message
        }
        New-Object WinSCP.RemoteFileInfoCollection
    }
    finally
    {
        $session.Dispose()
    }
}

# ============================================================================================== []
# EOF