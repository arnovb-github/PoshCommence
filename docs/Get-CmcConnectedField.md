---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version: https://github.com/arnovb-github/CmcLibNet
schema: 2.0.0
---

# Get-CmcConnectedField

## SYNOPSIS
Get a related column (field).

## SYNTAX

```
Get-CmcConnectedField [-ConnectionName] <String> [-ToCategoryName] <String> [-FieldName] <String>
 [<CommonParameters>]
```

## DESCRIPTION
Defines a connected field to retrieve from a Commence category or view.

## EXAMPLES

### Example 1
```powershell
Get-CmcConnectedField 'Relates to' Contact contactKey
```

Get a related column pointing to the 'contactKey' field in connection 'Relates to' to category 'Contact'.

## PARAMETERS

### -ConnectionName
Commence connection name (case-sensitive).

```yaml
Type: String
Parameter Sets: (All)
Aliases: cn

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldName
Commence fieldname in the connected category.

```yaml
Type: String
Parameter Sets: (All)
Aliases: f

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ToCategoryName
Connected Commence category name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: c

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

### PoshCommence.ConnectedField
## NOTES

## RELATED LINKS
