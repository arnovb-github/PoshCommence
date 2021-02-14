---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFilterCTI

## SYNOPSIS
Get filter for items connected to an item.

## SYNTAX

```
Get-CmcFilterCTI [-ClauseNumber] <Int32> [-Connection] <String> [-Category] <String> [-Item] <String> [-Except]
 [-OrFilter] [<CommonParameters>]
```

## DESCRIPTION
Get a Connection To Item (CTI) filter. Filters for items in A that have a connection to item X in B.

## EXAMPLES

### Example 1
```powershell
Get-CmcFilterCTI 1 'Relates to' Contact 'Findlay.Howard'
```

(Assumes _Tutorial database_, category 'Account') Filter for items that have a connection to 'Contact' item 'Findlay.Howard'.

## PARAMETERS

### -Category
Connected Commence category name.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Connection
Commence connection name (case-sensitive).

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

### -Item
Connected item value.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Vovin.CmcLibNet.Database.CursorFilterCTI
## NOTES
Does not yet support clarified item names.

The object-based approach to filtering in [Vovin.CmcLibNet] does not require setting the filter conjunction after the fact like you would when using the Commence RM API directly. You set the conjunction in the filter itself. It defaults to 'AND', use the `-OrFilter` switch for 'OR'. That will 'OR' the filters coming afterward, according to Commence's special grouping of filters, where for example 'OR' in filter 3 will 'OR' filter 4, but 'OR' in filter 4 will 'OR' the group of filters 5 to 8. The grouping is as follows: `[ [ [1] AndOr [2] ] AndOr [ [3] AndOr [4] ] ] AndOr [ [ [5] AndOr [6] ]  AndOr [ [7] AndOr [8] ] ]`. Have fun and good luck!
## RELATED LINKS
