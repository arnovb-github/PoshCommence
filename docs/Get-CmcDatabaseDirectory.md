---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcDatabaseDirectory

## SYNOPSIS
Get Commence database directory.

## SYNTAX

```
Get-CmcDatabaseDirectory [<CommonParameters>]
```

## DESCRIPTION
Get the database directory of the currently active Commence database.

## EXAMPLES

### Example 1
```powershell
PS C:\> "The full database path is " + (Get-CmcDatabaseDirectory).FullName
```

Get the database directory of the currently active Commence database.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.IO.DirectoryInfo
## NOTES
When Commence is not running, or more than one instance of Commence is running, this cmdlet will throw an error.

## RELATED LINKS
