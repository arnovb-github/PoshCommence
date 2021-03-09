---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFilterF

## SYNOPSIS
Get filter on a field value.

## SYNTAX

```
Get-CmcFilterF [-ClauseNumber] <Int32> [-FieldName] <String> [-Qualifier] <FilterQualifier>
 [-FieldValue] <String> [[-FieldValue2] <String>] [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]
```

## DESCRIPTION
Get a Field (F) filter. Filters for a field value.

## EXAMPLES

### Example 1
```powershell
Get-CmcFilterF 1 accountKey Between 'A' 'B'
```

(Assumes _Tutorial database_, category 'Account') Filter for items where the 'accontKey' field is between 'A' and 'B'.

### Example 2
```powershell
Get-CmcFilterF 1 accountKey [Vovin.CmcLibNet.Database.FilterQualifier]::Contains 'A'
```

(Assumes _Tutorial database_, category 'Account') Filter for items where the 'accontKey' field contains 'A'. Uses the fully qualified FilterQualifier value.

### Example 3
```powershell
Get-CmcFilterF 1 accountKey 0 'A'
```

Identical to Example 2, but uses the numeric value of the FilterQualifier.

## PARAMETERS

### -ClauseNumber
Filter clause number (1-8).

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Except
Invert filter.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldName
Commence field name to filter on.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldValue
Field value to filter for.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldValue2
Second field value to filter for in case of a `Between` filter.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MatchCase
Match string case-sensitive. Applies only to text-fields.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
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
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Qualifier
Filter qualifier (i.e. the filter condition).

```yaml
Type: FilterQualifier
Parameter Sets: (All)
Aliases:
Accepted values: Contains, DoesNotContain, On, At, EqualTo, NotEqualTo, LessThan, GreaterThan, Between, True, False, Checked, NotChecked, Yes, No, Before, After, Blank, Shared, Local, One, Zero

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Vovin.CmcLibNet.Database.CursorFilterF
## NOTES
The object-based approach to filtering in `Vovin.CmcLibNet` does not require setting the filter conjunction after the fact like you would when using the Commence RM API directly. You set the conjunction in the filter itself. It defaults to 'AND', use the `-OrFilter` switch for 'OR'. That will 'OR' the filters coming afterward, according to Commence's special grouping of filters, where for example 'OR' in filter 3 will 'OR' filter 4, but 'OR' in filter 4 will 'OR' the group of filters 5 to 8. The grouping is as follows: `[ [ [1] AndOr [2] ] AndOr [ [3] AndOr [4] ] ] AndOr [ [ [5] AndOr [6] ]  AndOr [ [7] AndOr [8] ] ]`. Have fun and good luck!

## RELATED LINKS
