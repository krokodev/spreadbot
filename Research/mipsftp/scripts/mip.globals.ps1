$LocalPath       = ".\..\data\store"
$LocalSrcPath    = "$LocalPath\src"
$LocalZipPath    = "$LocalPath\zip"
$RemotePath      = "/store/"
$Session         = New-Object WinSCP.Session
$SessionOptions  = New-Object WinSCP.SessionOptions
$TransferOptions = New-Object WinSCP.TransferOptions