---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFilterCTCF

## SYNOPSIS
Get a Commence CTCF filter.

## SYNTAX

```
Get-CmcFilterCTCF [-ClauseNumber] <Int32> [-Connection] <String> [-ToCategoryName] <String>
 [-FieldName] <String> [-Qualifier] <FilterQualifier> [-FieldValue] <String> [[-FieldValue2] <String>]
 [-MatchCase] [-Except] [-OrFilter] [<CommonParameters>]
```

## DESCRIPTION
Get a Commence Connection To Connected Field (CTCF) filter. Filters for items in A that have a connected items in B where field X matched the filter value.

## EXAMPLES

### Example 1
```powershell
Get-CmcFilterCTCF 1 'Relates to' Contact lastName EqualTo Douglas -MatchCase
```

(Assumes _Tutorial database_, category 'Account') Set filter 1 to get items in 'Account' that have connected items in connection 'Relates to' to category 'Contact' where field 'lastName' is equal to string 'Douglas', case-sensitive.

## PARAMETERS

### -ClauseNumber
Position of the filter (1-8).

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

### -Connection
Commence connection name (case sensitive).

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
Commence field name in the connected category.

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

### -FieldValue
Field value to match on.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldValue2
Field value to match on in case of a `Between` filter.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MatchCase
Match case sensitive.

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
Specify how to match.

```yaml
Type: FilterQualifier
Parameter Sets: (All)
Aliases:
Accepted values: Contains, DoesNotContain, On, At, EqualTo, NotEqualTo, LessThan, GreaterThan, Between, True, False, Checked, NotChecked, Yes, No, Before, After, Blank, Shared, Local, One, Zero

Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ToCategoryName
{{ Fill ToCategoryName Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases: c

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

### Vovin.CmcLibNet.Database.CursorFilterCTCF
## NOTES
The object-based approach to filtering in `Vovin.CmcLibNet` does not require setting the filter conjunction after the fact like you would when using the Commence RM API directly. You set the conjunction in the filter itself. It defaults to 'AND', use the `-OrFilter` switch for 'OR'. That will 'OR' the filters coming afterward, according to Commence's special grouping of filters, where for example 'OR' in filter 3 will 'OR' filter 4, but 'OR' in filter 4 will 'OR' the group of filters 5 to 8. The grouping is as follows: `[ [ [1] AndOr [2] ] AndOr [ [3] AndOr [4] ] ] AndOr [ [ [5] AndOr [6] ]  AndOr [ [7] AndOr [8] ] ]`. Have fun and good luck!

## RELATED LINKS
