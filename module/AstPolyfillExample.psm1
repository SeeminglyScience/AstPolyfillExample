if ($PSVersionTable.PSVersion.Major -le 5) {
    Add-Type -Path $PSScriptRoot\AstPolyfiller.5.dll
} else {
    Add-Type -Path $PSScriptRoot\AstPolyfiller.7.dll
}

Import-Module $PSScriptRoot\AstPolyfillExample.dll

Export-ModuleMember -Cmdlet Find-Ternary
