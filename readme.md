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
There is full `Get-Help` for every cmdlet, created with the [PlatyPS](https://www.powershellgallery.com/packages/platyPS/) package.

## Exploring the database ##
Get the name of the currently active Commence database:

```powershell
Get-CmcDatabaseName [<CommonParameters>]
```

Get the Commence _active.log_ file:

```powershell
Get-CmcLogFile [<CommonParameters>]
```

This will return a `System.IO.FileInfo` object.

### Example:
```powershell
# get last 10 lines of log file
Get-Content (Get-CmcLogFile).FullName -Tail 10
```

**Side note** In the vast majority of cases, you will want to look at contents of the log file. So why not simply make the cmdlet return the contents of the file? The reason for that is two-fold: consistency and retaining information. A file is actually an object, with properties. Like *size*, or *last modified*. `Get-CmcLogFile` does just what is says: return the file object. By doing so, it keeps true to the spirit of Powershell in which everything you work with is an object. Just returning the contents of the file, or even the name of the file, would ultimately make it harder to work with the cmdlet.

Get the Commence _data.ini_ file:
```powershell
Get-CmcIniFile [<CommonParameters>]
```

This will return a `System.IO.FileInfo` object.

### Example
Get the _data.ini_ file:
```powershell
# get contents of data.ini
Get-Content (Get-CmcIniFile).FullName 
```

Get the database directory:

```powershell
Get-CmcDatabaseDirectory [<CommonParameters>]
```

This will return a `System.IO.DirectoryInfo` object.

### Example
```powershell
"The full database path is " + (Get-CmcDatabaseDirectory).FullName
```

List all categories:

```powershell
Get-CmcCategories [<CommonParameters>]
```

### Example
```powershell
# list the category names
Get-CmcCategories | Select-Object -Property Name 
```

List all fields in a category:

```powershell
Get-CmcFields [-CategoryName] <string> [<CommonParameters>]
```

### Example
```powershell
# get info on the Name field for category Account
Get-CmcFields Account | Where-Object { $_.Type -eq Name } # 'Name' is a fieldtype in Commence
```

Use the `Get-CmcFieldTypes` cmdlet for getting the list of Commence field types

List all connections for a category:
```powershell
Get-CmcConnections [-CategoryName] <string> [<CommonParameters>]
```

This is self-explanatory. Note that connection names in Commence are case-sensitive.

## Getting Views
`Find-CmcView` returns all Commence views. You can filter the view list:

```powershell
Find-CmcView abc -Category Acccount -Type Report
```

Will output view of type 'Report' in category 'Account' that containg 'abc' in the name. All parameters support tab-completion.

You can pipe results to `Open-CmcView`:
```powershell
Find-CmcView 'All Accounts' | Open-CmcView
```
Will open view 'All Accounts'.

When you pipe more than one view to `Open-CmcView` it will open up to the first 10.

## Getting field values ##
`Get-CmcFieldValues` returns an object for every database row. Think of it as a hashtable with the fieldname and corresponding values(s):

### Example (_Tutorial database_):

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

### Syntax for categories:
```powershell
Get-CmcFieldValues [-CategoryName] <string> [-FieldNames] <string[]> [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]
```

### Syntax for views:
```powershell
Get-CmcFieldValues [-ViewName] <string> [-FieldNames] <string[]> [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]
```

### Get THIDs ###
If you want THIDs, specify the `-UseThids` switch. You get an additional `CommenceField` object with fieldname 'THID' for every row.

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
You can also supply filters with `-Filters`. Use the `Get-CmcFilter`. This is a cmdlet that has a dynamic parameterset for every type of filter:

FilterType Field (F):

```powershell
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Except] [-OrFilter] -FieldName <String>
 -Qualifier <FilterQualifier> -FieldValue <String> [-FieldValue2 <String>] [-MatchCase] [<CommonParameters>]
```

FilterType ConnectionToItem (CTI):
```powershell
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Connection] <String> [-ToCategoryName] <String> [-Item] <String> [-Except] [-OrFilter] [<CommonParameters>]
```

FilterType ConnectionToCategoryToItem (CTCTI):
```powershell
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Connection] <String> [-ToCategoryName] <String> [-Connection2] <String> [-ToCategoryName2] <String> [-Item] <String> [-Except] [-OrFilter] [<CommonParameters>]
```

FilterType ConnectionToCategoryField (CTCF):
```powershell
Get-CmcFilter [-ClauseNumber] <int> [-FilterType] <FilterType> [-Connection] <string> [-ToCategoryName] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-Except] [-OrFilter] [-MatchCase] [<CommonParameters>]
```

### Example
Usage example: set the first filter a a `Field (F)` filter for items where the 'accountKey' field does not contain 'avio', case-sensitive:

```powershell
$filter = Get-CmcFilter 1 -FilterType Field accountKey Contains avio -Except -MatchCase -Verbose
```

**Important**: You specify the filter conjunction in the filters themselves. __This is different from how you do it in Commence!__ The default is **AND**. Set the `-OrFilter` switch for **OR**. There is no need to specify the filter conjunction separately. 

The `Get-CmcFilter` cmcdlet does **not** check for correctness of the parameters (for example: is a qualifier can be appliewd to the supplied field), but there is a cmdlet to try out filters:

`Test-CmcFilter [-Category] <string> [-Filter] <ICursorFilter> [<CommonParameters>]`

This will tell you if Commence accepts the filter as valid, regardless of results. Using this cmdlet in a production environment is not recommended, because it is very resource-expensive.

## Count connected items ##
This can be useful for example when the database specifies at most a single connection, but multiple connections exist. (Commence does not always enforce that setting).

```powershell
Get-CmcConnectedItemCount [-FromCategory] <string> [-ConnectionName] <string> [-ToCategory] <string> [[-FromItem] <string>] [<CommonParameters>]`
```

### Example
Finding all items in the _Account_ category that have more than 1 connection to the _Contact_ category (using the _Tutorial database_):

```powershell
Get-CmcConnectedItemCount Account 'Relates to' Contact | Where-Object { $_.Count -gt 1 } | Select-Object -Property Itemname, Count
```

You can check the connection count for a known item by specifying the itemname as the `-FromItem` parameter. A return value of `-1` means that the item was not found.

## Exporting ##
The `Export-CmcCategory` cmdlet allows you to do simple exporting directly from the command-line. For advanced exporting see [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet). 

### Syntax for ByCategory
```powershell
Export-CmcData [-CategoryName] <String> [-OutputPath] <String> [-ExportFormat <ExportFormat>]
 [-Filters <ICursorFilter[]>] [-FieldNames <String[]>] [-SkipConnectedItems] [-UseThids]
 [-PreserveAllConnections] [<CommonParameters>]
```

### Syntax for ByView
```powershell
Export-CmcData [-ViewName] <String> [-OutputPath] <String> [-ExportFormat <ExportFormat>] [-SkipConnectedItems]
 [-PreserveAllConnections] [-UseColumnNames] [<CommonParameters>]
```

### Example (_Tutorial database_)
Export the entire _Account_ category to  file _account.xml_.
```powershell
Export-CmcData Account accounts.xml
```

### Example (_Tutorial database_)
Advanced example: export address fields and list of Sales person of items in _Account_ that have 'Wing' in their name or are connected to _salesTeam_ item 'Team 1' to a Json file.
```powershell
# create array of filters
$filters = @((Get-CmcFilterF 1 accountKey 0 Wing),
            (Get-CmcFilterCTI 2 'Relates to' 'salesTeam' 'Team 1' -OrFilter))
# perform the export
Export-CmcData Account accounts.json -ExportFormat Json -Filters $filters -FieldNames accountKey, Address, City, zipPostal, Country, 'Relates to Employee'
```
If you are not interested in connected values you can specify `-SkipConnectedItems`, which will significantly boost performance. The `-UseThids` switch will give you thids. You can use it if you know what they are :).

### Example (_Tutorial database_)
Exporting a view:
```powershell
Export-CmcView -v 'Contact List' -OutputPath contactlist.xml
```

Exports view 'Contact List` to file 'contactlist.xml' using default settings.