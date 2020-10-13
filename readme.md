# PSCommenceModules

A collection of Powershell cmdlets for use with Commence RM. Requires [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet). (Binary included in the project.)

This is all in an experimental stage. None of this code is production-ready.

Usage: `using module <path>\PSCommenceModules.dll` (`using` statements must be at the top of a script).

`Get-CmcFieldValues` returns a list of `CommenceField` objects for every database row. Think of it as a table:

| | | | | 
| - | - | - | - | 
| row 0 | CommenceField 0 | CommenceField 1 | ... |
| row 1 | CommenceField 0 | CommenceField 1 | ... |
| row 2 | CommenceField 0 | CommenceField 1 | ... |

A `CommenceField` has these properties: `CategoryName`, `FieldName` and `FieldValue`. 

Example:

```powershell
Get-CmcFieldValues CategoryName FieldName1, FieldName2 | ForEach-Object {
    $_.FieldValue # returns all fieldvalues for all rows
}
```

The complete syntax of `Get-CmcFieldValues` is:

`Get-CmcFieldValues [-CategoryOrViewName] <string> [[-FieldNames] <string[]>] [-UseView] [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]`

If you specify a Commence viewname instead of a categoryname, use the `-UseView` switch. Note that you still have to specify which fields you want from the view.

If you want THIDs, specify the `-UseThids` switch. You get an additional `CommenceField` object with fieldname 'THID' for every row. This switch does not work on views.

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

You can also supply filters with `-Filters`. For every filtertype there is a cmdlet:

`Get-CmcFilterF [-ClauseNumber] <int> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Connection2] <string> [-Category2] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCF [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

So getting a Field filter could be:

```powershell
$filter = Get-CmcFilterF 1 Name 0 test -Except -MatchCase -Verbose
```

For brevity I used the `int` value 0 for the filterqualifier. In the real world you should use its constant value, in this case `[Vovin.CmcLibNet.Database.FilterQualifier]::Contains`.

**Important**: You specify any filter conjunction in the filters themselves. The default is **AND**. Set the `-OrFilter` switch for **OR**. There is no need to specify the filter conjunction separately. This is different from how you do it in Commence!

The `Get-CmcFilter…` cmcdlets do **not** check for correctness of the parameters, but there is a cmdlet to try out filters:

`Resolve-CmcFilter [-Category] <string> [-Filter] <ICursorFilter> [<CommonParameters>]`

This will tell you if the filter *technically* works on the specified category (as in: Commence accepts this filter as valid, regardless of results). Using this cmdlet in a production environment is not recommended, because it is very resource-expensive.

There is also the `Get-CmcFieldValue` cmdlet. It is like the baby brother of `Get-CmcFieldValues` (note the singular vs plural). You would use it when you quickly want to get the values of a single field, without the overhead that `Get-CmcFieldValues` creates. It only supports direct fields, and does not support filtering or THIDs. It does support using views, so you can still use filters, just set them in Commence.

`Get-CmcFieldValue [-CategoryOrViewName] <string> [[-FieldName] <string>] [-UseView] [<CommonParameters>]`