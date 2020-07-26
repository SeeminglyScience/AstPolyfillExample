dotnet publish
$constructedModule = Join-Path $PSScriptRoot -ChildPath 'ConstructedModule'
if (Test-Path $constructedModule) {
    Remove-Item $constructedModule -Recurse -Force -ErrorAction Stop
}

$null = New-Item $constructedModule -ItemType Directory
Copy-Item $PSScriptRoot/module/* -Destination $constructedModule
Copy-Item $PSScriptRoot/src/AstPolyfiller5/bin/Debug/netstandard2.0/AstPolyfiller.dll $constructedModule/AstPolyFiller.5.dll
Copy-Item $PSScriptRoot/src/AstPolyfiller7/bin/Debug/net5.0/AstPolyfiller.dll $constructedModule/AstPolyFiller.7.dll
Copy-Item $PSScriptRoot/src/AstPolyfillExample/bin/Debug/netstandard2.0/AstPolyfillExample.dll $constructedModule
