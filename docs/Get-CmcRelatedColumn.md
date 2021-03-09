---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version: https://github.com/arnovb-github/CmcLibNet
schema: 2.0.0
---

# Get-CmcRelatedColumn

## SYNOPSIS
Get a related column.

## SYNTAX

```
Get-CmcRelatedColumn [-ConnectionName] <String> [-ToCategory] <String> [-FieldName] <String>
 [<CommonParameters>]
```

## DESCRIPTION
Get a related column definition for use in `Get-CmcFieldValues`.

## EXAMPLES

### Example 1
```powershell
Get-CmcRelatedColumn 'Relates to' Contact contactKey
```

Get a related column pointing to the 'contactKey' field in connection 'Relates to' to category 'Contact'.

## PARAMETERS

### -ConnectionName
Commence connection name (case-sensitive).

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
Commence fieldname in connected category.

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

### -ToCategory
Connected Commence category name.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### PoshCommence.RelatedColumn
## NOTES

## RELATED LINKS

[Get-CmcFieldValues](Get-CmcFieldValues.md)