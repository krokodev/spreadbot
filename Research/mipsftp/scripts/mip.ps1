$LocalPath       = ".\..\data\store\"
$RemotePath      = "/store/"
$Session         = New-Object WinSCP.Session
$SessionOptions  = New-Object WinSCP.SessionOptions
$TransferOptions = New-Object WinSCP.TransferOptions

Function Main()
{
    Log "MIP scripts"
    
    Add-Type -Path "WinSCPnet.dll"

    InitConnectionOptions
  
    Upload "product"
}

Function Log([string]$rec)
{
    Write-Host $rec
}

Function InitConnectionOptions()
{
    $SessionOptions.Protocol = [WinSCP.Protocol]::Sftp
    $SessionOptions.HostName = "mip.ebay.com"
    $SessionOptions.PortNumber = 22
    $SessionOptions.UserName = "cyfir"
    $SessionOptions.Password = gc .\password.txt
    $SessionOptions.GiveUpSecurityAndAcceptAnySshHostKey = $true
  
    $TransferOptions.TransferMode = [WinSCP.TransferMode]::Binary
}


Function Zip([string]$feed)
{
}

Function Upload([string]$feed)
{
    Log "Upload [$feed]..."

    try
    {
        $Session.Open($SessionOptions)
 
        $sourceFiles    = "$LocalPath$feed\*.zip"
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

try
{
    Main
    exit 0
}
catch [Exception]
{
    Write-Host $_.Exception.Message
    exit 1
}
