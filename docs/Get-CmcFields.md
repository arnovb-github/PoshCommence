---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFields

## SYNOPSIS
Get field defitions form Commence category.

## SYNTAX

```
Get-CmcFields [-CategoryName] <String> [<CommonParameters>]
```

## DESCRIPTION
Gets the fieldnames from the specfied Commence category.

## EXAMPLES
### Example 1
```powershell
Get-CmcFields Account
```

Gets all field definitions for the fields in category 'Account'.

### Example 2
```powershell
(Get-CmcFields Account).Name
```

List the fieldnames in category 'Account'.

### Example 3
```powershell
Get-CmcFields Account | Where-Object { $_.Type -eq "Telephone" }
```

Get the fields of type 'Telephone' for category 'Account'.

### Example 4
```powershell
Get-CmcFields Account | Where-Object { $_.Type -eq [Vovin.CmcLibNet.Database.CommenceFieldType]::Telephone }
```

Same as previous example, but this time using the predefined enumeration in `[Vovin.CmcLibNet.Datbase.FieldType]`. 

### Example 5
```powershell
Get-CmcFields Account | Where-Object { $_.Type -eq 3 }
```

Same as previous example, but this time using the numerical value from the predefined enumeration in [Vovin.CmcLibNet.Datbase.FieldType]. 

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS
