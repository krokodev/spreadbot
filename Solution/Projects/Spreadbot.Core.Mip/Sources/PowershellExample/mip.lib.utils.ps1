# ============================================================================================== []
Function MakeRequestId()
{
    #[System.Guid]::NewGuid()
    $random = New-Object System.Random
    $random.Next(1000,9999)
}

# ============================================================================================== []
Function MakeDataDirName()
{
    $offset = -7 
    $utcNow = [DateTime]::UtcNow;
    $mipNow = $utcNow.AddHours($offset)

    Value "UtcNow  " $utcNow
    Value "MIP time" $mipNow

    $mipNow.Date.ToString("MMM-dd-yyy", [CultureInfo]::CreateSpecificCulture("en-US"))
}

# ============================================================================================== []
# EOF