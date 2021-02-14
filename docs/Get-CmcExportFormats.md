---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcExportFormats

## SYNOPSIS
Get available export formats.

## SYNTAX

```
Get-CmcExportFormats [<CommonParameters>]
```

## DESCRIPTION
Get the export formats available for the Export cmdlets.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-CmcExportFormats
```

Get the export formats available for the Export cmdlets.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Vovin.CmcLibNet.Export.ExportFormat
Enum values of the available export formats

## NOTES
Not all export formats may be implemented or suitable for use in Powershell.

## RELATED LINKS
