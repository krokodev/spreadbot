param (
    [string] $source,
    [string] $destination
)

try
{
	if( Test-Path $destination )
	{
		Write-Host "$destination already exists"
		exit 0
	}

	if( -not ( Test-Path $source ) )
	{
		Write-Error "Error : default file not found"
		Write-Host "=========================================================="
		Write-Host ""
		Write-Host "File with default data is not found"
		Write-Host "$source"
		Write-Host ""
		Write-Host "=========================================================="
		exit 0
	}

    Copy-Item $source -Destination $destination
	Write-Warning "File was initialized with deafult content, see output for details"
	Write-Host "=========================================================="
	Write-Host ""
	Write-Host "Please pay attention for this file:"
	Write-Host "$destination"
	Write-Host ""
	Write-Host "It is initialized by"
	Write-Host "$source"
	Write-Host ""
	Write-Host "You are to fill it out with actual data"
	Write-Host ""
	Write-Host "=========================================================="
	
	exit 0
}
catch [Exception]
{
	Write-Error "Error: $_.Message"
	exit 1
}
