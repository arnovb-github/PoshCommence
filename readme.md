# PSCommenceModules

A collection of Powershell cmdlets for use with Commence RM. Requires [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet).

This is all in an experimental stage. None of this code is production-ready.

Usage: `using module <path>\PSCommenceModules.dll`

`Get-CmcFieldValues` returns `PSCommenceModules.CommenceField[]`, i.e. a list of `CommenceField` objects for every database row.

Sample usage:
```powershell
Get-CmcFieldValues CategoryName Field1Name, Field2Name | ForEach-Object {
    $_.FieldValue
}
```