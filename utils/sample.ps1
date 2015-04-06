try
{
	# settings
	$rootPath        = "p:\Projects\crocodev"
	$solutionName    = "Crocodev"
	$projectName     = "Crocodev.Common"
	$projectConfig   = "Release"

	# devenv path
	$env:Path += ";C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\"
	
	# values 
	$solutionPath    = "$rootPath\$solutionName.sln"
	$assemblyPath    = "$rootPath\projects\$projectName\bin\$projectConfig\$projectName.dll"
	$packageName     = $projectName.ToLower()

	# build assembly
	devenv.exe $solutionPath /rebuild $projectConfig
	if ($LASTEXITCODE -eq 1)
	{
		throw "build assembly failed"
	}

	# get version
	$assembly        = [Reflection.Assembly]::Loadfile($assemblyPath)
	$assemblyName    = $assembly.GetName()
	$assemblyVersion = $assemblyName.Version.ToString()
	if ($LASTEXITCODE -eq 1)
	{
		throw "get version failed"
	}

	# make package
	.\NuGet.exe pack ..\projects\$projectName\$projectName.csproj -IncludeReferencedProjects -Prop Configuration=Release
 	if ($LASTEXITCODE -eq 1)
	{
		throw "build package failed"
	}

	# deploy package
	$nugetApiKey = gc .\nuget.apikey
	$deployName  = "$packageName.$assemblyVersion.nupkg"
	.\NuGet.exe setApiKey $NugetApiKey
	.\NuGet.exe push $deployName
	if ($LASTEXITCODE -eq 1)
	{
		throw "deploy package failed"
	}
}
catch
{
	Write-Error $_
	Write-Host "Press enter..."
	read-host
}
