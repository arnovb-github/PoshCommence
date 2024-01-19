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
Find-CmcFieldInForms [-CategoryName] <String> [-FieldName] <String> [<CommonParameters>]
```

## DESCRIPTION
This command will list all Item Detail Forms on a specified Commence field is present, even when disabled or invisible.

## EXAMPLES

### Example 1
```powershell
Find-CmcFieldInForms Account accountKey
```

Lists all detail forms where the Name-type field named 'accountKey' of the 'Account' category.
When you use a Name-type field, it will also list the detail forms on which this field is shown via a connection, including connections to the category itself.

### Example 2
```powershell
Find-CmcFieldInForms -c Account -f Address
```

Lists all detail forms where the field named 'Address' of the 'Account' category, using aliases for parameters.

### Example 3
```powershell
Find-CmcFieldInForms -CategoryName Account -FieldName Address
```

Lists all detail forms where the field named 'Address' of the 'Account' category using full parameter names.

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
