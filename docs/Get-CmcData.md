---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcData

## SYNOPSIS
Gets fieldvalues from Commence.

## SYNTAX

### ByCategory (Default)
```
Get-CmcData [-CategoryName] <String> [-FieldNames] <String[]> [-UseThids] [-Filters <ICursorFilter[]>]
 [-ConnectedFields <ConnectedField[]>] [<CommonParameters>]
```

### ByView
```
Get-CmcData [-ViewName] <String> [[-FieldNames] <String[]>] [-Filters <ICursorFilter[]>]
 [-ConnectedFields <ConnectedField[]>] [<CommonParameters>]
```

## DESCRIPTION
Gets the specfied fieldvalues from a Commence category. Can use a category or view as datasource.

## EXAMPLES

### Example 1
```powershell
Get-CmcData Account accountKey, businessNumber
```

Gets the fieldvalues for the 'accountKey' and 'businessNumber' from the 'Account' category.

### Example 2
```powershell
# Using Tutorial dabase
# define fields we want to get
$fields = "accountKey", "Address", "cityStateZip", "businessNumber"
# define related columns we want to get
$rc1 = Get-CmcConnectedField 'Relates to' Contact contactKey
$rc2 = Get-CmcConnectedField 'Relates to' Contact emailBusiness
# define a Field (type F) filter for items where the field called accountKey does not contain string 'Leap', case-sensitive
$filter = Get-CmcFilter 1 Field "accountKey" Contains Leap -Except -MatchCase
Get-CmcData -c Account -FieldNames $fields -Filters $filter -ConnectedFields $rc1, $rc2
```

Advanced example using filters and related columns.

### Example 3
```powershell
powershell
# Uses Tutorial dabase
Get-CmcData -v 'Account Default' (Get-CmcFields Account).Name | Out-GridView
```

Getting funky: show your own "Commence view" using the data in the "Account Default" view but with all available fields in the "Account" category. This may appear a little silly, but a `GridView` allows for quick filtering on any field.

## PARAMETERS

### -CategoryName
Commence category to get data from.

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

### -ConnectedFields
Fields from connections.

```yaml
Type: ConnectedField[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldNames
Commence fieldnames.

```yaml
Type: String[]
Parameter Sets: ByCategory
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String[]
Parameter Sets: ByView
Aliases:

Required: False
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
Connected data are returned just as Commence returns them. That means that they are either comma-delimited or newline-delimited strings, depending on the type of the underlying data source. Different viewtypes return different formats. This can make working with related data quite hard. When you want to work with connected items it is probably easier to export the data to a file first. The export engine will treat connected items as seperarate entities (if possible). You need to export to XML or Json format for this.

## RELATED LINKS

[Get-CmcFilter](Get-CmcFilter.md)

[Get-CmcConnectedField](Get-CmcConnectedField.md)