---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcLogFile

## SYNOPSIS
Get Commence active.log file

## SYNTAX

```
Get-CmcLogFile [<CommonParameters>]
```

## DESCRIPTION
Returns a `FileInfo` object pointing to Commence's active.log file.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-Content (Get-CmcLogFile).FullName -Tail 10
```

Returns the last 10 lines of the Commence log file.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.IO.FileInfo

## NOTES

## RELATED LINKS