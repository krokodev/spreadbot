$DebugMode = $false

Function Log([string]$str)
{
    Write-host -ForegroundColor DarkCyan $str
}

Function Header([string]$str)
{
    Write-host -BackgroundColor Cyan -ForegroundColor Black $str
}

Function Value([string]$name, [string]$value)
{
    Write-host -ForegroundColor Cyan "$name = [$value]"
}

Function Debug([string]$str)
{
    if($DebugMode)
    {
        Write-host $str -ForegroundColor DarkYellow
    }
}

Function Error([Exception]$exception)
{
    Write-host -BackgroundColor Red -ForegroundColor Yellow $exception.Message
    Write-Error $exception.Message
}

