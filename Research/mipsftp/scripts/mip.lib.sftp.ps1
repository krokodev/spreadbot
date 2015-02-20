. .\mip.lib.sftp.init.ps1
. .\mip.lib.sftp.feed.ps1
. .\mip.lib.sftp.request.ps1


function GetRemoteFileContent([string]$filePathName)
{
    $filePathName
}


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