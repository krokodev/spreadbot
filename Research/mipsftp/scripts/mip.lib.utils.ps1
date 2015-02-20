Function MakeRequestId()
{
    #[System.Guid]::NewGuid()
    $random = New-Object System.Random
    $random.Next(1000,9999)
}

Function MakeDataDirName()
{
    "Feb-20-2015"
}