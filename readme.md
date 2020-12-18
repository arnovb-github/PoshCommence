# PSCommenceModules

## Overview ##
A collection of Powershell cmdlets for use with Commence RM. Requires [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet). You can think of these as convenience methods, since _Vovin.CmcLibNet_ can also be used directly in any PowerShell script.

This is all in an experimental stage. None of this code is production-ready.

Usage: `using module <path>\PSCommenceModules.dll` (`using` statements must be at the top of a script).

## Getting multiple field values ##
`Get-CmcFieldValues` returns a list of `CommenceField` objects for every database row. Think of it as a table:

| Category | FieldName 1 | FieldName 2 | ... | 
| - | - | - | - | 
| row 0 | FieldValue 0 | FieldValue 1 | ... |
| row 1 | FieldValue 0 | FieldValue 1 | ... |
| row 2 | FieldValue 0 | FieldValue 1 | ... |

A `CommenceField` has these properties: `CategoryName`, `FieldName` and `FieldValue`. 

Example:

```powershell
Get-CmcFieldValues CategoryName FieldName1, FieldName2 | ForEach-Object {
    $_.FieldValue # returns all fieldvalues for all rows
}
```

The complete syntax of `Get-CmcFieldValues` is:

`Get-CmcFieldValues [-CategoryOrViewName] <string> [[-FieldNames] <string[]>] [-UseView] [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]`

### Use a view ###
If you specify a Commence viewname instead of a categoryname, use the `-UseView` switch. Note that you still have to specify which fields you want from the view.

### Get THIDs ###
If you want THIDs, specify the `-UseThids` switch. You get an additional `CommenceField` object with fieldname 'THID' for every row. This switch does not work on views.

### Related columns ###
Providing related columns involves some more work. These are the columns you would set by the `cursor.SetRelatedColumn(…)` method when you program against the Commence API.
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

Note: you cannot yet specify only related columns, you need to specify at least 1 direct column (simply ignore it).

### Filters ###
You can also supply filters with `-Filters`. For every filtertype there is a cmdlet:

`Get-CmcFilterF [-ClauseNumber] <int> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Connection2] <string> [-Category2] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCF [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

So getting a Field filter could be:

```powershell
$filter = Get-CmcFilterF 1 Name 0 test -Except -MatchCase -Verbose
```

For brevity I used the `int` value 0 for the filterqualifier. In the real world you should use its constant value, in this case `[Vovin.CmcLibNet.Database.FilterQualifier]::Contains`. It is not immediately obvious that you have access to that namespace, but you do. That is a hard thing to get your head around, so I created the `Show-CmcFilterQualifiers` cmdlet. It will show you all qualifiers and their numerical equivalents.

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

You can check the connection count for a known item by specifying the itemname as the `-FromItem` parameter. It does not (yet?) accept clarified itemnames. A return value of `-1` means that the item was not found.

## Getting name and paths ##
Get the name of the currently active Commence database:

`Get-CmcDatabaseName [<CommonParameters>]`

Get the Commence log file:

`Get-CmcLogFile [<CommonParameters>]`

This will return a `System.IO.FileInfo` object.

Example of use:
```powershell
Get-Content (Get-CmcLogFile).FullName
```

Get the Commence ini file:

`Get-CmcIniFile [<CommonParameters>]`

Example of use:
```powershell
Get-Content (Get-CmcIniFile).FullName
```

Get the database directory:

`Get-CmcDatabaseDirectory [<CommonParameters>]`

This will return a `System.IO.DirectoryInfo` object.

Example of use:
```powershell
"The full database path is " + (Get-CmcDatabaseDirectory).FullName
```

## Debugging ##
Some Cmdlets make _Vovin.CmcLibNet_ issue DDE commands to Commence. These can be extraordinarily hard to debug. Display the last DDE error thrown in Commence with:

`Get-CmcLastDDEError [<CommonParameters>]`

I do not have an immediate use case to show, but combined with the Commence Help files it may be useful nonetheless.