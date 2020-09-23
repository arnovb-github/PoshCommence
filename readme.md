# PSCommenceModules

A collection of Powershell cmdlets for use with Commence RM. Requires [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet).

This is all in an experimental stage. None of this code is production-ready.

Usage: `using module <path>\PSCommenceModules.dll` (`using` statements must be on the top of a script)

(You can also use `Import-Module` (at least in PS 5.1). I still recommend `using module` because it ensures classes exposed by the module are pulled in. I read somewhere that with `Import-Module` that is not always the case, but it may have referred to classes defined in PS modules (`. psm1` files), and `PSCommenceModules` is a binary assembly.)

`Get-CmcFieldValues` returns `PSCommenceModules.CommenceField[]`, i.e. a list of `CommenceField` objects for every database row. A `CommenceField` object has 3 properties: `CategoryName`, `FieldName` and `FieldValue`.

Sample usage for 1 field:
```powershell
Get-CmcFieldValues CategoryName FieldName
```
This will return the fieldvalues for `FieldName`.

When you request more than one field, the values are wrapped into objects:

```powershell
Get-CmcFieldValues CategoryName FieldName | ForEach-Object {
    $_.FieldValue # returns all fieldvalues for all rows
}
```

There is more to `Get-CmcFieldValues`, the complete syntax is:

`Get-CmcFieldValues [-CategoryOrViewName] <string> [[-FieldNames] <string[]>] [-UseView] [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]`

Providing an array of direct fields is as simple as doing:

`Get-CmcFieldValues CategoryName Field1Name, Field2Name`

If you specify a Commence viewname instead of a categoryname, use the `-UseView` switch.

If you want THIDs, specify the `-UseThids` switch. (This only works on category-cursors.). You get an additional `CommenceField` object with fieldname 'THID' for every row.

Providing (an array of) related columns involves some more work. These are the columns you would set by the `cursor.SetRelatedColumn(…)` method when you program against the Commence API.
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

It is up to you how you want to code this. Using [strong-typing](https://en.wikipedia.org/wiki/Strong_and_weak_typing) for defining related columns is mandatory, this is by design.

Usage:
`Get-CmcFieldValues CategoryName Field1Name, Field2Name -RelatedColumns $rc1, $rc2`

You can also supply filters with `-Filters`. For every filtertype there is a cmdlet:

`Get-CmcFilterF [-ClauseNumber] <int> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Connection2] <string> [-Category2] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCF [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

So getting a Field filter could be:

```powershell
$filter = Get-CmcFilterF 1 Name 0 test -Except -MatchCase -Verbose
```

For brevity I used the `int` value (0) of the filterqualifier. In the real world you should use the actual enum value, in this case `[Vovin.CmcLibNet.Database.FilterQualifier]::Contains`.

Important: you specify the filter conjunction in the filter. It defaults to `AND`, specify `-OrFilter` for `OR`. This is different from how you do it in Commence.

Please note: these cmcdlets are just convenience methods, they do **not** check for correctness of the parameters.

There is a cmdlet to try out filters. 

`Resolve-CmcFilter [-Category] <string> [-Filter] <ICursorFilter> [<CommonParameters>]`

This will tell you if the filter *technically* works on the specified category (as in: Commence is technically okay with this filter, regardless of results). It is recommended you do not use this cmdlet in a production environment, because it is a very resource-expensive call.