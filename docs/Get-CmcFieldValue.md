---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFieldValue

## SYNOPSIS
Get a fieldvalue for items in a Commence category.

## SYNTAX

```
Get-CmcFieldValue [-CategoryOrViewName] <String> [-FieldName] <String> [-UseView] [<CommonParameters>]
```

## DESCRIPTION
Get the values of the specified column (field) in a Commence category.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-CmcFieldValue Account contactKey
```

Returns values of field 'contactKey' in category 'Account'.

## PARAMETERS

### -CategoryOrViewName
Commence category or view name. View names are case-sensitive.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldName
Commence field name to get values from.

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

### -UseView
Use to specify that you are supplying a view name (not categoryname).

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

### System.String
## NOTES

## RELATED LINKS
