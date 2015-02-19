Add-Type -Path "WinSCPnet.dll"
$SessionOptions  = New-Object WinSCP.SessionOptions
$TransferOptions = New-Object WinSCP.TransferOptions
$RemotePath      = "/store/"

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
    $session = New-Object WinSCP.Session

    try
    {
        $session.Open($SessionOptions)
 
        $sourceFiles    = "$LocalZipPath\$feed.zip"
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

InitConnectionOptions
