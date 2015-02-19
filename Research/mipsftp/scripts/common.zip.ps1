Add-Type -Assembly System.IO.Compression.FileSystem
Add-Type -Assembly System.IO

function ZipFiles([string]$sourcedir, [string]$zipfilename)
{
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
 
   [System.IO.File]::Delete($zipfilename)
   [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)
}
