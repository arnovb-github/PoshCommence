---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Clear-CmcMetadataCache

## SYNOPSIS
Reset Commence metadata cache.

## SYNTAX

```
Clear-CmcMetadataCache [<CommonParameters>]
```

## DESCRIPTION
Will reset the cache holding Commence schema information that is being built up as you use this module. Use this cmdlet when the Commence database schema has changed, for example when a field was added.

## EXAMPLES

### Example 1
```powershell
Clear-CmcMetadataCache
```

Will clear the cache holding Commence schema information.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None
## NOTES
When you switch databases, the cache is automatically cleared.

## RELATED LINKS
