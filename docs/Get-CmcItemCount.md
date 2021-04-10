---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcItemCount

## SYNOPSIS
Get item count for category.

## SYNTAX

```
Get-CmcItemCount [-CategoryName] <String> [<CommonParameters>]
```

## DESCRIPTION
Gets the number of items in a Commence category. A value of -1 means that the category was not found.

## EXAMPLES

### Example 1
```powershell
Get-CmcItemCount Account
```

Returns the number of items in category 'Account'.

### Example 2
```powershell
Get-CmcCategories -pv cat | Get-CmcItemCount | % { Write-Output $cat.CategoryName $_ }
```

Display all categorynames and their item count.

## PARAMETERS

### -CategoryName
Commence category name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: c

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Int32
## NOTES

## RELATED LINKS
