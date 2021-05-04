---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Export-CmcData

## SYNOPSIS
Export Commence data to file.

## SYNTAX

### ByCategory (Default)
```
Export-CmcData [-CategoryName] <String> [-OutputPath] <String> [-ExportFormat <ExportFormat>]
 [-Filters <ICursorFilter[]>] [-FieldNames <String[]>] [-ConnectedFields <ConnectedField[]>]
 [-SkipConnectedItems] [-UseThids] [-PreserveAllConnections] [<CommonParameters>]
```

### ByView
```
Export-CmcData [-ViewName] <String> [-OutputPath] <String> [-ExportFormat <ExportFormat>] [-SkipConnectedItems]
 [-PreserveAllConnections] [-UseColumnNames] [<CommonParameters>]
```

## DESCRIPTION
Exports Commence data to file.

## EXAMPLES

### Example 1 (Category)
```powershell
Export-CmcData -CategoryName Account -OutputPath account.xml
```

Exports all fields in category 'Account' to file 'account.xml'.

### Example 2
```powershell
# Uses Tutorial dabase
# define fields we want to get
$fields = "accountKey", "Address", "cityStateZip", "businessNumber"
# define array of related columns
$rc = @((Get-CmcConnectedField 'Relates to' Contact contactKey),
    (Get-CmcConnectedField 'Relates to' Contact emailBusiness))
# define a Field (type F) filter for items where the field called accountKey does not contain string 'Leap', case-sensitive
$filter = Get-CmcFilter 1 Field accountKey Contains Leap -Except -MatchCase
Export-CmcData -c Account -FieldNames $fields -Filters $filter -ConnectedFields $rc -ExportFormat Json -Outputpath account.json
```

Export specified fields to a Json file

Advanced example (using _Tutorial database_): export 

### Example 3 (View)
```powershell
Export-CmcData -v 'Contact List' -OutputPath contactlist.xml
```

Export view 'Contact List` to file 'contactlist.xml' using default settings.

## PARAMETERS

### -CategoryName
Commence category name.

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
Parameter Sets: ByCategory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExportFormat
File format to export to.

```yaml
Type: ExportFormat
Parameter Sets: (All)
Aliases:
Accepted values: Xml, Json, Html, Text, Excel, GoogleSheets, Event

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FieldNames
Array of fieldnames to include in the export.

```yaml
Type: String[]
Parameter Sets: ByCategory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filters
List of filters to apply to the category.

```yaml
Type: ICursorFilter[]
Parameter Sets: ByCategory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputPath
Path and filename to export to.

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

### -PreserveAllConnections
{{ Fill PreserveAllConnections Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipConnectedItems
{{ Fill SkipConnectedItems Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseColumnNames
Use the columnlabel as node/key/column header instead of the Commence fieldname.

```yaml
Type: SwitchParameter
Parameter Sets: ByView
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseThids
Include THIDs.

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
Commence view name (case-sensitive!).

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
[Get-CmcConnectedField](Get-CmcConnectedField.md)