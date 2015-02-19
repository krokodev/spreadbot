$RemotePath      = "/store/"
$Session         = New-Object WinSCP.Session
$SessionOptions  = New-Object WinSCP.SessionOptions
$TransferOptions = New-Object WinSCP.TransferOptions

Add-Type -Path "WinSCPnet.dll"
InitConnectionOptions

Function InitConnectionOptions()
{
    $SessionOptions.Protocol   = [WinSCP.Protocol]::Sftp
    $SessionOptions.HostName   = "mip.ebay.com"
    $SessionOptions.PortNumber = 22
    $SessionOptions.UserName   = "cyfir"
    $SessionOptions.Password   = gc .\password.txt
    $SessionOptions.GiveUpSecurityAndAcceptAnySshHostKey = $true
  
    $TransferOptions.TransferMode = [WinSCP.TransferMode]::Binary
}

Function UploadFeed([string]$feed)
{
    Log "Upload [$feed]..."

    try
    {
        $Session.Open($SessionOptions)
 
        $sourceFiles    = "$LocalZipPath\$feed.zip"
        $destination    = "$RemotePath$feed/"
        $transferResult = $session.PutFiles($sourceFiles, $destination, $false, $TransferOptions)
 
        $transferResult.Check()
 
        foreach ($transfer in $transferResult.Transfers)
        {
            Log "Upload of $($transfer.FileName) succeeded"
        }
    }
    finally
    {
        $Session.Dispose()
    }
}
