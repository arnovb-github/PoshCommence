# PSCommenceModules

A collection of Powershell cmdlets for use with Commence RM. Requires [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet).

This is all in an experimental stage. None of this code is production-ready.

Usage: `using module <path>\PSCommenceModules.dll`

`Get-CmcFieldValues` returns `PSCommenceModules.CommenceField[]`, i.e. a list of `CommenceField` objects for every database row.

Sample usage:
```powershell
Get-CmcFieldValues CategoryName FieldName | ForEach-Object {
    $_.FieldValue # returns all fieldvalues for the row
}
```

That is a silly `'Hello, world'` example.

There is more to `Get-CmcFieldValues`; you can provide an array of direct fields and related fields. The complete syntax is:

`Get-CmcFieldValues [-CategoryOrViewName] <string> [[-FieldNames] <string[]>] [-UseView] [-UseThids] [-Filters <ICursorFilter[]>] [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]`

Providing an array of direct fields is as simple as doing:

`Get-CmcFieldValues CategoryName Field1Name, Field2Name`

Providing (an array of) related columns involves some more work. You have to define them.

```powershell
$rc = New-Object -TypeName PSCommenceModules.RelatedColumn
$rc.Connection = 'Relates to'
$rc.ToCategory = 'Contact'
$rc.FieldName = 'accountKey'
$rc1 = New-Object -TypeName PSCommenceModules.RelatedColumn
$rc1.Connection = 'Relates to'
$rc1.ToCategory = 'Contact'
$rc1.FieldName = 'emailBusiness'
```

It is up to you how you want to code this. Using strong-typing for defining related columns is mandatory, this is by design.

`Get-CmcFieldValues CategoryName Field1Name, Field2Name -RelatedColumns $rc, $rc1`

So, we can read columns from a category with ease. How about filters?

For every filtertype there is a cmdlet:

`Get-CmcFilterF [-ClauseNumber] <int> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCTI [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-Connection2] <string> [-Category2] <string> [-Item] <string> [-Except] [-OrFilter] [<CommonParameters>]`

`Get-CmcFilterCTCF [-ClauseNumber] <int> [-Connection] <string> [-Category] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]`

So getting a Field filter would be:

```powershell
$filter = Get-CmcFilterF 1 Name 0 test -Verbose -Except -MatchCase
```

For brevity I used the `int` value of the filterqualifier. In the real world you should use the actual enum value, in this case `[Vovin.CmcLibNet.Database.FilterQualifier]::Contains`.

Important: you specify the filter conjunction in the filter. It defaults to `AND`, specify `-OrFilter` for `OR`. This is different from how you do it in Commence.

Please note: these cmcdlets are just conveniece methods, they do not check for correctness of the parameters.
