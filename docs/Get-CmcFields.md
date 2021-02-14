---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFields

## SYNOPSIS
Get fieldnames form Commence category

## SYNTAX

```
Get-CmcFields [-CategoryName] <String> [<CommonParameters>]
```

## DESCRIPTION
Gets the fieldnames from the specfied Commence category.

## EXAMPLES

### Example 1
```powershell
Get-CmcFields Account | Where-Object { $_.Type -eq Name }
```

Get the Name field for category Account. The Name argument has special meaning. See the next example.

### Example 2
```powershell
Get-CmcFields Account | Where-Object { $_.Type -eq [Vovin.CmcLibNet.Database.CommenceFieldType]::Name }
```

Get the Name field for category Account using the predefined enumeration in [Vovin.CmcLibNet.Datbase.FieldType]. 

### Example 3
```powershell
Get-CmcFields Account | Where-Object { $_.Type -eq 11 }
```

Get the Name field for category Account using the numerical value from the predefined enumeration in [Vovin.CmcLibNet.Datbase.FieldType]. 

## PARAMETERS

### -CategoryName
Commence category name

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.String[]
Returns an array of strings.

## NOTES

## RELATED LINKS
