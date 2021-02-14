---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcDbSize

## SYNOPSIS
Gets the size of the Commence database.

## SYNTAX

```
Get-CmcDbSize [<CommonParameters>]
```

## DESCRIPTION
Get the size in bytes of all files combined in the running Commence database.

## EXAMPLES

### Example 1
```powershell
PS C:\> (Get-CmcDbSize) / 1024
```

Gets the combined size in megabytes of all files in the running Commence database.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Long

## NOTES
Will include all files in the Commence database directory even if they are not part of the database.

## RELATED LINKS
