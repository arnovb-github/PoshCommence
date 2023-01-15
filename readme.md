# Powershell cmdlets for Commence RM #

## Overview ##
A collection of Powershell cmdlets for use with the Commence RM API. Requires [Vovin.CmcLibNet](https://www.nuget.org/packages/Vovin.CmcLibNet/). You can think of these as convenience methods, since _Vovin.CmcLibNet_ can also be used directly in PowerShell. It assumes a single running instance of Commence .

The most useful cmdlets are probably `Get-CmcData`, which allows you to extract fieldvalues without having to create a view in Commence and `Export-CmcData`, which allows you to export data without having to define an Export Template in Commence. Keep in mind that the Commence API is quite slow.

All examples that specify a categoryname, fieldname, viewname, etc. are for the _Tutorial_ database present in all Commence installations under *Help*.

## Background ##
I botch these together whenever I find I have to do too much work in PS to get what I want. For example, in a project I was working on I needed to retrieve the Name field values for a category repeatedly. Just a few lines of code, but a single cmdLet is even easier.

# CmdLets #

## Getting help ##
Every cmdlet is fully documented, using the [PlatyPS](https://www.powershellgallery.com/packages/platyPS/) package. Simply use `Get-Help [cmdlet]`.

## Argument completion ##
Most parameter values can be tab-completed.

## Available cmdlets ##
```powershell
Clear-CmcControlCharacters
```

Clears control characters in a database. Commence allows for the embedding of control characters in fields where you do not expect them and where they result in flawed exports and other unexoected results. It can be quite a nuisance especially because it can be very hard to find them. Use `Get-Help Clear-CmcControlCharacters` for details.

```powershell
Clear-CmcMetadataCache
```

Clears the auto-complete cache. Useful when switching databases. Use `Get-Help Clear-CmcMetadataCache` for details.

```powershell
Export-CmcData
```

Probably one of the most useful cmdlets in the module. Use it to export Commence data to a variety of formats. Use `Get-Help Export-CmcData` for details. For advanced exporting see [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet). 

```powershell
Find-CmcView
```

Search for Commence views by name, category or viewtype. Use `Get-Help Find-CmcView` for details.

```powershell
Get-CmcActiveViewInfo
```

Get details on the currently showing view (window) in Commence. Use `Get-Help Get-CmcActiveViewInfo` for details.

```powershell
Get-CmcCategories
```

Get all category definitions from a Commence database. Useful for piping. Use `Get-Help Get-CmcCategories` for details.

```powershell
Get-CmcConnectedField
```

Constructs a connected field to retrieve from a Commence category or view. You would use this when defining filters. Use `Get-Help Get-CmcConnectedField` for details. Also see `Get-CmcFilter`.

```powershell
Get-CmcConnectedItemCount
```

Counts the number connected items for a given item. Useful when you have specified a connection to be 'Allow At Most 1 Connected Item' but the database does not respect that. Use `Get-Help Get-CmcConnectedItemCount` for details.

```powershell
Get-CmcConnections
```

Get the connections to a category. Use `Get-Help Get-CmcConnections` for details.

```powershell
Get-CmcData
```

Probably one of the most useful cmdlets in the module. This elaborate cmdlet allows for retrieval of Commence data. Basically this is the command-line method of obtaining Commence database items. Use `Get-Help Get-CmcData` for details.

```powershell
Get-CmcDatabaseDirectory
```

Returns the directory of the currently active Commence database. Use `Get-Help Get-CmcDatabaseDirectory` for details.

```powershell
Get-CmcDatabaseName
```

Returns the name of the currently active Commence database. Use `Get-Help Get-CmcDatabaseName` for details.

```powershell
Get-CmcDbSize
```

Returns the size in bytes of the currently active Commence database. Use `Get-Help Get-CmcDbSize` for details.


```powershell
Get-CmcFields
```

Returns field information (name, type, etc.)on a category. Use `Get-Help Get-CmcFields` for details.

```powershell
Get-CmcFilter
```
Allows for construction of filters to be applied when reading data. This is quite an complex cmcdlet, be sure to also check out the examples. Use `Get-Help Get-CmcFilter` for details.

```powershell
Get-CmcIniFile
```
Gets a `PSObject` pointing to the _data.ini_ file (where Commence RM stores some of its settings). Use `Get-Help Get-CmcIniFile` for details.

```powershell
Get-CmcLogFile 
```
Gets a `PSObject` pointing to the _active.log_ file. Use `Get-Help Get-CmcLogFile` for details.

```powershell
Get-CmcPreference 
```
Gets some preference settings from Commence RM, notably the (-Me-) item the the (-Me-) category and some file locations. Use `Get-Help Get-CmcPreference` for details.

```powershell
Open-CmcView 
```
Typically used in conjunction with [Find-CmcView](Find-CmcView.md). Opens the specified view(s) in Commence RM and puts the window focus on Commence RM. Use `Get-Help Open-CmcView` for details.

```powershell
Test-CmcFilter
```

Used to check if a filter created with `Get-CmcFilter` is syntactically correct (i.e, no portions were omitted when creating them). Use of this cmdlet in production environments is discouraged because it is a resource intensive operation. Use `Get-Help Test-CmcFilter` for details.