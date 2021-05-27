---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcIniFile

## SYNOPSIS
Get Commence settings file

## SYNTAX

```
Get-CmcIniFile [<CommonParameters>]
```

## DESCRIPTION
Returns a `PSobject` object pointing to Commence's data.ini settings file.

## EXAMPLES

### Example 1
```powershell
Get-CmcIniFile | Get-Content
```

Gets the contents of the file.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### PSObject
## NOTES

## RELATED LINKS
