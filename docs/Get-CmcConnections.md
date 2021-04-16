---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcConnections

## SYNOPSIS
Get the connections from Commence.

## SYNTAX

```
Get-CmcConnections [-CategoryName] <String> [<CommonParameters>]
```

## DESCRIPTION
Returns the connectionnames and their categoryname to the specfied category in Commence.

## EXAMPLES

### Example 1
```powershell
Get-CmcConnections Account
```

Returns the connections and their categoryname for the 'Account' category.

## PARAMETERS

### -CategoryName
Commence category name to get connections for.

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
