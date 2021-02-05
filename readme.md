# Posh Commence CmdLets #

## Overview ##
A collection of Powershell cmdlets for use with the Commence RM API. Requires [Vovin.CmcLibNet](https://www.nuget.org/packages/Vovin.CmcLibNet/). You can think of these as convenience methods, since _Vovin.CmcLibNet_ can also be used directly in PowerShell.

This is all experimental.

## Background ##
I botch these together whenever I find I have to do too much work in PS to get what I want. For example, in a project I was working on I needed to retrieve the Name field values for a category repeatedly. Just a few lines of code, but a single CmdLet is even easier.

This is a binary assembly, but it could all have been done in just PS. I am simply more familiar with C# than PS and I wanted to play around with writing C# code in VS Code rather than Visual Studio.

# CmdLets #
## Exploring the database ##
Get the name of the currently active Commence database:

`Get-CmcDatabaseName [<CommonParameters>]`

Get the Commence _active.log_ file:

`Get-CmcLogFile [<CommonParameters>]`

This will return a `System.IO.FileInfo` object.

Example:
```powershell
# get last 10 lines of log file
Get-Content (Get-CmcLogFile).FullName -Tail 10
```

**Side note** In the vast majority of cases, you will want to look at contents of the log file. So why not simply make the cmdlet return the contents of the file? The reason for that is two-fold: consistency and retaining information. A file is actually an object, with properties. Like *size*, or *last modified*. `Get-CmcLogFile` does just what is says: return the file object. By doing so, it keeps true to the spirit of Powershell in which everything you work with is an object. Just returning the contents of the file, or even the name of the file, would ultimately make it harder to work with the cmdlet.

Get the Commence _data.ini_ file:

`Get-CmcIniFile [<CommonParameters>]`

This will return a `System.IO.FileInfo` object.

Example:
```powershell
# get contents of data.ini
Get-Content (Get-CmcIniFile).FullName 
```

Get the database directory:

`Get-CmcDatabaseDirectory [<CommonParameters>]`

This will return a `System.IO.DirectoryInfo` object.

Example:
```powershell
"The full database path is " + (Get-CmcDatabaseDirectory).FullName
```

List all categories:

`Get-CmcCategories [<CommonParameters>]`

Example:
```powershell
# list the category names
Get-CmcCategories | Select-Object -Property Name 
```

List all fields in a category:

`Get-CmcFields [-CategoryName] <string> [<CommonParameters>]`

Example:
```powershell
# get the Name field for category Account
Get-CmcFields Account | Where-Object { $_.Type -eq 'Name' }
```
The `Name` argument here has special meaning. It is in fact part of an enumeration in _Vovin.CmcLibNet_. You could (and probably should) also have written this as:

```powershell
# get the Name field for category Account
Get-CmcFields Account | Where-Object { $_.Type -eq [Vovin.CmcLibNet.Database.CommenceFieldType]::Name }
```
Or even as:

```powershell
# get the Name field for category Account
Get-CmcFields Account | Where-Object { $_.Type -eq 11 } # 11 is the numerical identifier of the Name field
```

Because this module requires _Vovin.CmcLibNet_ all of _Vovin.CmcLibNet_'s functionality gets pulled in. That may not be immediately obvious. For that reason, there is the `Get-CmcFieldTypes` cmdlet:

`Get-CmcFieldTypes [<CommonParameters>]`

It returns both the names and the associated numbers for field types you can use.

## Getting field values ##
`Get-CmcFieldValues` returns a list of `CommenceField` objects for every database row. Think of it as a table:

| Category | FieldName 1 | FieldName 2 | ... | 
| - | - | - | - | 
| row 1 | FieldValue 1 | FieldValue 2 | ... |
| row 2 | FieldValue 1 | FieldValue 2 | ... |
| row 3 | FieldValue 1 | FieldValue 2 | ... |

A `CommenceField` has properties: `CategoryName`, `FieldName` and `FieldValue`. 

Example:

```powershell
# return all fieldvalues for all rows
Get-CmcFieldValues CategoryName FieldName1, FieldName2 | ForEach-Object {
    $_.FieldValue 
}
```

The complete syntax of `Get-CmcFieldValues` is:

`Get-CmcFieldValues [-CategoryOrViewName] <string> [[-FieldNames] <string[]>] [-UseView] [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]`

### Use a view ###
If you specify a Commence viewname instead of a categoryname, use the `-UseView` switch. Note that you still have to specify which fields you want from the view.

### Get THIDs ###
If you want THIDs, specify the `-UseThids` switch. You get an additional `CommenceField` object with fieldname 'THID' for every row. This switch does not work on views.

### Related columns ###
Providing related columns involves some more work. These are the columns you would set by the `cursor.SetRelatedColumn(…)` method when you program against the Commence API directly.
You have to explicitly define them:

```powershell
$rc1 = New-Object -TypeName PSCommenceModules.RelatedColumn
$rc1.Connection = 'Relates to' # from the Tutorial database
$rc1.ToCategory = 'Contact' # from the Tutorial database
$rc1.FieldName = 'accountKey' # from the Tutorial database

$rc2 = New-Object -TypeName PSCommenceModules.RelatedColumn
$rc2.Connection = 'Relates to' # from the Tutorial database
$rc2.ToCategory = 'Contact' # from the Tutorial database
$rc2.FieldName = 'emailBusiness' # from the Tutorial database
```

You can also use this shorter syntax:

```powershell
$rc1 = [PSCommenceModules.RelatedColumn]::New('Relates to', 'Contact','accountKey')
```

**Important**: connection names in Commence are case-sensitive!

Usage:
`Get-CmcFieldValues CategoryName FieldName1, FieldName2 -RelatedColumns $rc1, $rc2`

Note: you cannot yet specify only related columns, you need to specify at least 1 direct column (simply ignore it in the output).

### Filters ###
You can also supply filters with `-Filters`. For every filtertype there is a cmdlet:

`Get-CmcFilterF [-ClauseNumber] <int> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Connection2] <string> [-Category2] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCF [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

So getting a `Field (F)` filter could be:

```powershell
$filter = Get-CmcFilterF 1 Name 0 test -Except -MatchCase -Verbose
```

For brevity I used the `int` value `0` for the filterqualifier. In the real world you should use its constant value, in this case `[Vovin.CmcLibNet.Database.FilterQualifier]::Contains`. It may not be immediately obvious that you have access to that namespace, but you do. That is a hard thing to get your head around, so I created the `Get-CmcFilterQualifiers` cmdlet. It returns all qualifiers and their numerical equivalents.

**Important**: You specify any filter conjunction in the filters themselves. The default is **AND**. Set the `-OrFilter` switch for **OR**. There is no need to specify the filter conjunction separately. This is different from how you do it in Commence!

The `Get-CmcFilter…` cmcdlets do **not** check for correctness of the parameters, but there is a cmdlet to try out filters:

`Resolve-CmcFilter [-Category] <string> [-Filter] <ICursorFilter> [<CommonParameters>]`

This will tell you if Commence accepts the filter as valid, regardless of results. Using this cmdlet in a production environment is not recommended, because it is very resource-expensive.

## Getting values from a single field ##
There is also the `Get-CmcFieldValue` cmdlet. It is like the baby brother of `Get-CmcFieldValues` (note the singular vs plural). You would use it when you quickly want to get the values of a single field, without the overhead that `Get-CmcFieldValues` creates. It only supports direct fields, and does not support filtering or THIDs. It does support using views, so you can still use filters, just set them in Commence.

`Get-CmcFieldValue [-CategoryOrViewName] <string> [[-FieldName] <string>] [-UseView] [<CommonParameters>]`

## Count connected items ##
This can be useful for example when the database specifies at most a single connection, but multiple connections exist. (Commence does not always enforce that setting).

`Get-CmcConnectedItemCount [-FromCategory] <string> [-ConnectionName] <string> [-ToCategory] <string> [[-FromItem] <string>] [<CommonParameters>]`

Example of finding all items in the _Account_ category that have more than 1 connection to the _Contact_ category (using the _Tutorial database_):

```powershell
Get-CmcConnectedItemCount Account 'Relates to' Contact | Where-Object { $_.Count -gt 1 } | Select-Object -Property Itemname, Count
```

You can check the connection count for a known item by specifying the itemname as the `-FromItem` parameter. It does not (yet) accept clarified itemnames. A return value of `-1` means that the item was not found.

## Exporting ##
The `Export-CmcCategory` CmdLet allows you to do simple exporting directly from the command-line. For advanced exporting see [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet). 

`Export-CmcCategory [-CategoryName] <string> [-Path] <string> [-ExportFormat <ExportFormat>] [-Filters <ICursorFilter[]>] [-FieldNames <string[]>] [-SkipConnectedItems] [<CommonParameters>]`

Simple example (_Tutorial database_): export the entire _Account_ category to  file _account.xml_.
```powershell
Export-CmcCategory Account accounts.xml
```

Advanced example: export address fields and list of Sales person of items in _Account_ that have 'Wing' in their name or are connected to _salesTeam_ item 'Team 1' to a Json file.
```powershell
# create array of filters
$filters = @((Get-CmcFilterF 1 accountKey 0 Wing),
            (Get-CmcFilterCTI 2 'Relates to' 'salesTeam' 'Team 1' -OrFilter))
# perform the export
Export-CmcCategory Account accounts.json -ExportFormat 1 -Filters $filters -FieldNames accountKey, Address, City, zipPostal, Country, 'Relates to Employee'
```
Note that for brevity I used `1` to indicate I wanted Json format. The fully qualified value is `[Vovin.CmcLibNet.Export.ExportFomat]::Json`. You can use the `Get-CmcExportFormats` CmdLet to get the full list of available formats. If you are not interested in connected values you can specify `-SkipConnectedItems`, which will significantly boost performance.

## Debugging ##
Some Cmdlets make _Vovin.CmcLibNet_ issue DDE commands to Commence. These can be extraordinarily hard to debug. Display the last DDE error thrown in Commence with:

`Get-CmcLastDDEError [<CommonParameters>]`

I do not have an immediate use case to show, but combined with the Commence Help files it may be useful nonetheless.