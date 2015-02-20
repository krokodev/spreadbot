Function Log([string]$rec)
{
    Write-Host $rec
}


Function Error([Exception]$exception)
{
    Write-Host $exception.Message
}

