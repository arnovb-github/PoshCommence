---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcConnectedItemCount

## SYNOPSIS
Get the number of connected items in Commence.

## SYNTAX

```
Get-CmcConnectedItemCount [-FromCategory] <String> [-ConnectionName] <String> [-ToCategory] <String>
 [[-FromItem] <String>] [<CommonParameters>]
```

## DESCRIPTION
Returns the number of connected items for the specified connection in Commence.

## EXAMPLES

### Example 1
```powershell
Get-CmcConnectedItemCount Account 'Relates to' Contact | Where-Object { $_.Count -gt 1 } | Select-Object -Property Itemname, Count
```

Get the itemnames and count from Contact items connected to Account via the 'Relates to' connection where more than 1 such connected item exists (_Tutorial database_).

## PARAMETERS

### -ConnectionName
Name of the Commence connection (case-sensitive!).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FromCategory
Name of the primary Commence category.

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

### -FromItem
Name of the item to return connected item count on. Do not provide a clarified itemname.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ToCategory
Name of connected Commence category to count connections of.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
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
A return value of -1 means that the item was not found.

## RELATED LINKS