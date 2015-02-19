. .\common.zip.ps1

Function ZipFeed([string]$feed)
{
    $feedFolder = "$LocalSrcPath\$feed"
    $feedFile   = "$LocalZipPath\$feed.zip"

    ZipFiles $feedFolder $feedFile
}
