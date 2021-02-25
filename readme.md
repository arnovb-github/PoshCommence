# Powershell cmdlets for Commence RM #

## Overview ##
A collection of Powershell cmdlets for use with the Commence RM API. Requires [Vovin.CmcLibNet](https://www.nuget.org/packages/Vovin.CmcLibNet/). You can think of these as convenience methods, since _Vovin.CmcLibNet_ can also be used directly in PowerShell. It assumes a single running instance of Commence .

This is all experimental.

The most useful cmdlets are probably `Get-CmcFieldValues`, which allows you to extract fieldvalues without having to create a view in Commence and `Export-CmcCategory`, which allows you to export data without having to define an Export Template in Commence.

## Background ##
I botch these together whenever I find I have to do too much work in PS to get what I want. For example, in a project I was working on I needed to retrieve the Name field values for a category repeatedly. Just a few lines of code, but a single CmdLet is even easier.

This is a binary assembly. It could all have been done in plain PS, I just wanted to play around with doing it in C# code using VS Code.

# CmdLets #

## Getting help ##
There is full `Get-Help` for every cmdlet, created with the great [PlatyPS](https://www.powershellgallery.com/packages/platyPS/) package.

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
Get-CmcFields Account | Where-Object { $_.Type -eq Name }
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

List all connections for a category:
`Get-CmcConnections [-CategoryName] <string> [<CommonParameters>]`

This is self-explanatory. Note that connection names in Commence are case-sensitive.

## Getting field values ##
`Get-CmcFieldValues` returns an object for every database row. Think of it as a hashtable with the fieldname and correspondig values(s):

Example (_Tutororial database_):

```powershell
# return  fieldvalues for fields "accountKey" and "businessNumber"
Get-CmcFieldValues Account accountKey, businessNumber
```

output:
```
accountKey                businessNumber
----------                --------------
Aviaonics Inc             330-555-1905
Commence Corporation
Concorde Aviation Ltd     412-555-7890
First Class Inc           416-781-1209 
... 
```

The complete syntax of `Get-CmcFieldValues` is:

`Get-CmcFieldValues [-CategoryOrViewName] <string> [[-FieldNames] <string[]>] [-UseView] [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]`

### Use a view ###
If you specify a Commence viewname instead of a categoryname, use the `-UseView` switch. Note that you still have to specify which fields you want from the view.

### Get THIDs ###
If you want THIDs, specify the `-UseThids` switch. You get an additional `CommenceField` object with fieldname 'THID' for every row. This switch does not work on views.

### Related columns ###
Providing related columns involves some more work. These are the columns you would set by the `cursor.SetRelatedColumn(…)` method in the Commence API.

```powershell
# create object directly
$rc1 = [PoshCommence.RelatedColumn]::New('Relates to', 'Contact','accountKey')
# or better: use the cmdlet for it
$rc2 = Get-CmcRelatedColumn 'Relates to', 'Contact','emailBusiness'
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

Set the first filter a a `Field (F)` filter for items where the Name field does not contain 'test', case-sensitive:

```powershell
$filter = Get-CmcFilterF 1 Name 0 test -Except -MatchCase -Verbose
```

For brevity I used the `int` value `0` for the filter qualifier. In the real world you should use its `enum` value, in this case `[Vovin.CmcLibNet.Database.FilterQualifier]::Contains`. `Vovin.CmcLibNet` is an assembly that is used by this module. That may not be immediately obvious; use the `Get-CmcFilterQualifiers` cmdlet to view all qualifier options. It returns all qualifiers and their numerical equivalents. We could have also used `1` (_does noet contain_) and omit the `-Except` in this case.

**Important**: You specify the filter conjunction in the filters themselves. The default is **AND**. Set the `-OrFilter` switch for **OR**. There is no need to specify the filter conjunction separately. __This is different from how you do it in Commence!__

The `Get-CmcFilter…` cmcdlets do **not** check for correctness of the parameters, but there is a cmdlet to try out filters:

`Test-CmcFilter [-Category] <string> [-Filter] <ICursorFilter> [<CommonParameters>]`

This will tell you if Commence accepts the filter as valid, regardless of results. Using this cmdlet in a production environment is not recommended, because it is very resource-expensive.

## Count connected items ##
This can be useful for example when the database specifies at most a single connection, but multiple connections exist. (Commence does not always enforce that setting).

`Get-CmcConnectedItemCount [-FromCategory] <string> [-ConnectionName] <string> [-ToCategory] <string> [[-FromItem] <string>] [<CommonParameters>]`

Example of finding all items in the _Account_ category that have more than 1 connection to the _Contact_ category (using the _Tutorial database_):

```powershell
Get-CmcConnectedItemCount Account 'Relates to' Contact | Where-Object { $_.Count -gt 1 } | Select-Object -Property Itemname, Count
```

You can check the connection count for a known item by specifying the itemname as the `-FromItem` parameter. It does not (yet) accept clarified itemnames. A return value of `-1` means that the item was not found.

## Exporting ##
The `Export-CmcCategory` cmdlet allows you to do simple exporting directly from the command-line. For advanced exporting see [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet). 

`Export-CmcCategory [-CategoryName] <string> [-Path] <string> [-ExportFormat <ExportFormat>] [-Filters <ICursorFilter[]>] [-FieldNames <string[]>] [-SkipConnectedItems] [-UseThids] [<CommonParameters>]`

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
Note that for brevity I used `1` to indicate I wanted Json format. The fully qualified value is `[Vovin.CmcLibNet.Export.ExportFomat]::Json`. You can use the `Get-CmcExportFormats [<CommonParameters>]` cmdlet to get the full list of available formats. If you are not interested in connected values you can specify `-SkipConnectedItems`, which will significantly boost performance. The `-UseThids` switch will give you thids. You can use it if you know what they are :).

The `Export-CmcView` will export a view as-is (provided the view supports exporting, not all viewtypes in Commence do). Notice that the column order may not be retained, connected values may come last. This is by design. View names in Commence are case-sensitive!

`Export-CmcView [-ViewName] <string> [-Path] <string> [-ExportFormat <ExportFormat>] [<CommonParameters>]`

## Debugging ##
Some Cmdlets make _Vovin.CmcLibNet_ issue DDE commands to Commence. These can be extraordinarily hard to debug. Display the last DDE error thrown in Commence with:

`Get-CmcLastDDEError [<CommonParameters>]`

I do not have an immediate use case to show, but combined with the Commence Help files it may be useful nonetheless.