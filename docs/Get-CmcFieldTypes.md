---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFieldTypes

## SYNOPSIS
Get Commence field types.

## SYNTAX

```
Get-CmcFieldTypes [<CommonParameters>]
```

## DESCRIPTION
Gets the Commence fieldtypes as specified in `Vovin.CmcLibNet.Database.CommenceFieldType`. Numerical values are equal to the native Commence RM API values.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-CmcFieldTypes
```

Gets the Commence fieldtypes as specified in `Vovin.CmcLibNet.Database.CommenceFieldType`.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Vovin.CmcLibNet.Datase.CommenceFieldType
Enum values of the field types.

## NOTES

## RELATED LINKS