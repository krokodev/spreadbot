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

Function Main()
{
    Write-Host "MIP scripts"
  
    Upload("product")
}


Function Upload([string]$feed)
{
    Write-Host "Upload $feed"

    # Load WinSCP .NET assembly
    Add-Type -Path "WinSCPnet.dll"
 
    # Setup session options
    $sessionOptions = New-Object WinSCP.SessionOptions
    $sessionOptions.Protocol = [WinSCP.Protocol]::Sftp
    $sessionOptions.HostName = "mip.ebay.com"
    $sessionOptions.PortNumber = 22
    $sessionOptions.UserName = "cyfir"
    $sessionOptions.Password = "v^1.1#i^1#r^1#I^3#f^0#p^3#t^Ul4yX0RBNzhEMjBDMzM1RTZFNEM2RDc2RUFBMDUyNTNBMkI4I0VeMjYw"
    $sessionOptions.GiveUpSecurityAndAcceptAnySshHostKey = $true
 
    $session = New-Object WinSCP.Session
 
    try
    {
        # Connect
        $session.Open($sessionOptions)
 
        # Upload files
        $transferOptions = New-Object WinSCP.TransferOptions
        $transferOptions.TransferMode = [WinSCP.TransferMode]::Binary
 
        $sourceFiles = ".\..\data\store\$feed\*.zip"
        $destination = "/store/$feed/"

        $transferResult = $session.PutFiles($sourceFiles, $destination, $false, $transferOptions)
 
        # Throw on any error
        $transferResult.Check()
 
        # Print results
        foreach ($transfer in $transferResult.Transfers)
        {
            Write-Host ("Upload of {0} succeeded" -f $transfer.FileName)
        }
    }
    finally
    {
        # Disconnect, clean up
        $session.Dispose()
    }
}




