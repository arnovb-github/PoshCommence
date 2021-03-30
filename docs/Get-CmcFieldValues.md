---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcFieldValues

## SYNOPSIS
Gets fieldvalues from Commence.

## SYNTAX

### ByCategory (Default)
```
Get-CmcFieldValues [-CategoryName] <String> [-FieldNames] <String[]> [-UseThids] [-Filters <ICursorFilter[]>]
 [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]
```

### ByView
```
Get-CmcFieldValues [-ViewName] <String> [-FieldNames] <String[]> [-Filters <ICursorFilter[]>]
 [-RelatedColumns <RelatedColumn[]>] [<CommonParameters>]
```

## DESCRIPTION
Gets the specfied fieldvalues from a Commence category.

## EXAMPLES

### Example 1
```powershell
Get-CmcFieldValues CategoryName FieldName1, FieldName2
```

Gets all fieldvalues for fields FieldName1 and Fieldame2.

### Example 2
```powershell
# Example for Tutorial dabase
# define some fields we want to get
$fields = "accountKey", "Address", "cityStateZip", "businessNumber"
# define related columns we want to get
$rc1 = Get-CmcRelatedColumn 'Relates to' Contact contactKey
$rc2 = Get-CmcRelatedColumn 'Relates to' Contact emailBusiness
# define a Field (type F) filter for items where the field called Name does not contain string 'test', case-sensitive
# the `0` represents the numerical value of enum value [Vovin.CmcLibNet.Database.FilterQualifier]::Contains
$filter = Get-CmcFilterF 1 "accountKey" 0 test -Except -MatchCase
Get-CmcFieldValues -c Account -FieldNames $fields -Filters $filter -RelatedColumns $rc1, $rc2
```

Advanced example using filters and related columns.

### Example 3
```
powershell
# Example for Tutorial dabase
Get-CmcFieldValues -v 'Account Default' (Get-CmcFields Account).Name -UseThids -UseView | Out-GridView
```

Getting funky: show your own "Commence view" based on the "Account Default" view but with all available fields and the THID of all items in the "Account" category. This may appear a little silly, but a `GridView` allows for quick filtering on any field and toggling of fields.

## PARAMETERS

### -CategoryName
Commence category to get data from

```yaml
Type: String
Parameter Sets: ByCategory
Aliases: c

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldNames
Commence fieldnames

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filters
Filters to apply.

```yaml
Type: ICursorFilter[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedColumns
Related columns to get.

```yaml
Type: RelatedColumn[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseThids
Include the Commence THID. Ignored when `-UseView` is set.

```yaml
Type: SwitchParameter
Parameter Sets: ByCategory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ViewName
Commence view name to get data from (case-sensitive!).

```yaml
Type: String
Parameter Sets: ByView
Aliases: v

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

### PSObject
## NOTES

## RELATED LINKS

[Get-CmcFilter](Get-CmcFilter.md)

[Get-CmcRelatedColumn](Get-CmcRelatedColumn.md)