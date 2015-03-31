param (
    [string] $error,
    [string] $warning,
    [string] $info
)

try
{
	Write-Output "test-output.ps1"

	Write-Warning $info
	Write-Warning $warning

	throw $error
}
catch
{
	Write-Error "Error: $_"
}
