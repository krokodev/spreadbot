Add-Type -Path "WinSCPnet.dll"
$SessionOptions  = New-Object WinSCP.SessionOptions
$TransferOptions = New-Object WinSCP.TransferOptions
$RemotePath      = "/store/"

Function InitConnectionOptions()
{
    $SessionOptions.Protocol   = [WinSCP.Protocol]::Sftp
    $SessionOptions.HostName   = "mip.ebay.com"
    $SessionOptions.PortNumber = 22
    $SessionOptions.UserName   = gc .\account\login.txt
    $SessionOptions.Password   = gc .\account\password.txt
    $SessionOptions.GiveUpSecurityAndAcceptAnySshHostKey = $true
  
    $TransferOptions.TransferMode = [WinSCP.TransferMode]::Binary
}

InitConnectionOptions
