---
external help file: PoshCommence.dll-Help.xml
Module Name: poshcommence
online version:
schema: 2.0.0
---

# Find-CmcFieldInForms

## SYNOPSIS
Find item detail forms where a specific field is being used.

## SYNTAX

```
Find-CmcDuplicates [-CategoryName] <String> [-FieldName] <String> [<CommonParameters>]
```

## DESCRIPTION
This command will list all Item Detail Forms on a specified Commence field is present, even when disabled or invisible.

## EXAMPLES

### Example 1
```powershell
Find-CmcDuplicates Account accountKey
```

Returns TRUE is there are items in category 'Account' that have the same value in the 'accountKey' field.

## PARAMETERS

### -CategoryName
Commence category name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: c

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldName
Commence field name. It should be in the specified category.

```yaml
Type: String
Parameter Sets: (All)
Aliases: f

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

### PSObject
## NOTES

## RELATED LINKS
