---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFilter

## SYNOPSIS
Create a filter object used to filter Commence data.

## SYNTAX

```
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Except] [-OrFilter] -FieldName <String>
 -Qualifier <FilterQualifier> -FieldValue <String> [-FieldValue2 <String>] [-MatchCase] [<CommonParameters>]
```

## DESCRIPTION
**Note** This is a dynamic command. For every `-FilterType` value, there is a separate set of parameters. See the examples for a full overview (Get-Help Get-CmcFilter -Example). This command creates a filter object of a specific type for filtering Commence data.

## EXAMPLES

### Get syntax help
```powershell
Get-Command Get-CmcFilter -syntax -Args '-FilterType', ConnectionToItem
```

Displays the syntax used with the 'ConnectionToItem' filtertype.

### Syntax for type Field
```powershell
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Except] [-OrFilter] -FieldName <String> -Qualifier <FilterQualifier> -FieldValue <String> [-FieldValue2 <String>] [-MatchCase] [<CommonParameters>]
```

Syntax for FilterType Field (F). This is the default FilterType.

### Usage of type Field
```powershell
Get-CmcFilter -ClauseNumber 1 -FilterType Field -FieldName "accountKey" -Qualifier Between -FieldValue "A*" -FieldValue2 "K*" -MatchCase
```

(_Tutorial database_) Sets filter with clause 1 on field 'accountKey' to filter for a value that starts with a 'A' up to and including values that start with 'K', case-sensitive.

### Usage of type Field (short form)
```powershell
Get-CmcFilter 1 0 accountKey Between "A*" "K*" -MatchCase
```

(_Tutorial database_) The same filter in its shortest form. Note that the 0 repesents the numeric value of the 'Field' `FilterType`.

### Syntax for type ConnectionToItem
```powershell
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Connection] <String> [-ToCategoryName] <String> [-Item] <String> [-Except] [-OrFilter] [<CommonParameters>]
```

Syntax for FilterType ConnectionToItem (CTI).

### Usage of type ConnectionToItem
```powershell
Get-CmcFilter -ClauseNumber 3 -FilterType ConnectionToItem -Connection 'Relates to' -ToCategoryName 'Contact' -Item 'Rubbel.John'
```

(_Tutorial database_) Sets a filter with clause 2 that returns items in 'Account' that are connected to to item 'Rubbel.John' over connection 'Relates to' to category 'Contact'.

### Usage of type ConnectionToItem (short form)
```powershell
Get-CmcFilter 3 1 'Relates to' 'Contact' 'Rubbel.John'
```

(_Tutorial database_) The same filter in its shortest form. Note that the 1 repesents the numeric value of the 'ConnectionToItem' `FilterType`.

### Syntax for type ConnectionToCategoryToItem
```powershell
Get-CmcFilter [-ClauseNumber] <Int32> [-FilterType] <FilterType> [-Connection] <String> [-ToCategoryName] <String> [-Connection2] <String> [-ToCategoryName2] <String> [-Item] <String> [-Except] [-OrFilter] [<CommonParameters>]
```

Syntax for Filtertype ConnectionToCategoryToItem (CTCTI).

### Usage of type ConnectionToCategoryToItem
```powershell
Get-CmcFilter -ClauseNumber 5 -FilterType ConnectionToCategoryToItem -Connection 'Relates To' -ToCategoryName 'Contact' -Connection2 'Handheld Device' -ToCategoryName2 'Employee' -Item 'Devol.Eric.L'
```

(_Tutorial database_) Sets a filter with clause 3 that returns items in 'Account' that are connected to to items in 'Contact over connection 'Relates to' that are connected to an item ''Devol.Eric.L' in category 'Employee' over connection 'Hanheld Device'.

### Usage of type ConnectionToCategoryToItem (short form)
```powershell
Get-CmcFilter 5 2 'Relates To' 'Contact' 'Handheld Device' 'Employee' 'Devol.Eric.L'
```

(_Tutorial database_) The same filter in its shortest form. Note that the 2 repesents the numeric value of the 'ConnectionToCategoryToItem' `FilterType`.

### Syntax for ConnectionToCategoryField
```powershell
Get-CmcFilter [-ClauseNumber] <int> [-FilterType] <FilterType> [-Connection] <string> [-ToCategoryName] <string> [-FieldName] <string> [-Qualifier] <FilterQualifier> [-FieldValue] <string> [[-FieldValue2] <string>] [-Except] [-OrFilter] [-MatchCase] [<CommonParameters>]
```

Syntax for Filtertype ConnectionToCategoryField (CTCF). 

### Usage of type ConnectionToCategoryField
```powershell
Get-CmcFilter -ClauseNumber 7 -FilterType ConnectionToCategoryField -Connection 'Relates to' -ToCategoryName 'Employee' -FieldName 'Title' -Qualifier EqualTo -FieldValue 'Telemarketer'
```

(_Tutorial database_) Filter category 'Account' for connected items in category 'Employee' that have a 'Title' field value equal to 'Telemarketer'.

### Usage of type ConnectionToCategoryField
```powershell
Get-CmcFilter 7 3 'Relates to' Employee Title EqualTo 'Telemarketer'
```

(_Tutorial database_) The same filter in its shortest form. Note that the 3 repesents the numeric value of the 'ConnectionToCategoryField' `FilterType`.

### Get FilterType names and values
```powershell
[Enum]::GetValues([Vovin.CmcLibNet.Database.FilterType]) | % { Write-Host $_  $_.Value__}
```

When you specify a `-FilterType` value, you can use tab-completion to cycle through all named values. This example outputs the names and the corresponding numerical values numbers of the [FilterType] enum.

### Get Qualifier names and values
```powershell
[Enum]::GetValues([Vovin.CmcLibNet.Database.FilterQualifier]) | % { Write-Host $_  $_.Value__}
```

When you specify a `-Qualifier` value, you can use tab-completion to cycle through all named values. This example outputs the names and the corresponding numerical values numbers of the [FilterQualifier] enum.

### Get the filterstring
```powershell
(Get-CmcFilter 1 0 accountKey Between "A*" "K*" -MatchCase).ToString()
# a more convenient way
$f = Get-CmcFilter 1 0 accountKey Between "A*" "K*" -MatchCase
$f # ToString() is implicit
```

Output:
'[ViewFilter(1,F,,"accountKey","Between","A*","K*")]'

Seasoned Commence API users at this point may go 'WTF is happening here? Create objects for filtering Commence data? How does that work?'

What happens under the hood is, when a filter is applied, the filter object is turned into a specially formatted string that is passed to the `ICommenceCursor.SetFilter(...)` method.

## PARAMETERS

### -ClauseNumber
Clause number.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:
Accepted values: 1, 2, 3, 4, 5, 6, 7, 8

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Except
Invert filter. Same as checking the 'Except' checkbox in the Commence filter UI.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -FilterType
Filter type. For each type there is a different set of parameters.

```yaml
Type: FilterType
Parameter Sets: (All)
Aliases: t, Type
Accepted values: Field, ConnectionToItem, ConnectionToCategoryToItem, ConnectionToCategoryField

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrFilter
Set filter conjunction to OR (if omitted, defaults to AND).

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldName
Commence fieldname. Only used in F and CTCF filters.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldValue
Commence field value. Only used in F and CTCF filters.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldValue2
Commence field value used with the Between qualifier. Only used in F and CTCF filters.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MatchCase
Match case-sensitive. Only used in F and CTCF filters.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Qualifier
Commence filter qualifier. Only used in F and CTCF filters. See the examples.

```yaml
Type: FilterQualifier
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### Vovin.CmcLibNet.Database.ICursorFilterF (Field)
### Vovin.CmcLibNet.Database.ICursorFilterCTI (ConnectionToItem)
### Vovin.CmcLibNet.Database.ICursorFilterCTCF (ConnectionToCategoryField)
### Vovin.CmcLibNet.Database.ICursorFilterCTCTI (ConnectionToCategoryToItem)

## NOTES
Most paramaters support automatic argument-completion.

## RELATED LINKS
