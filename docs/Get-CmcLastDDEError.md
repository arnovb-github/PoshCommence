---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcLastDDEError

## SYNOPSIS
Get last DDE error thrown in Commence

## SYNTAX

```
Get-CmcLastDDEError [<CommonParameters>]
```

## DESCRIPTION
Use this if you want to see the last DDE error thrown in Commence, for example when you try to read or write data via DDE (don't do that :).

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-CmcLastDDEError
```

Gets last DDE error

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.String
## NOTES
You can also just look at the log file.

## RELATED LINKS
