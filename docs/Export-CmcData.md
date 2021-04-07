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
 [-Filters <ICursorFilter[]>] [-FieldNames <String[]>] [-UseThids] [<CommonParameters>]
```

### ByView
```
Export-CmcData [-ViewName] <String> [-OutputPath] <String> [-ExportFormat <ExportFormat>] [-UseColumnNames]
 [<CommonParameters>]
```

## DESCRIPTION
Exports Commence data to file.

## EXAMPLES

### Example 1 (Categry)
```powershell
Export-CmcData -CategoryName Account -OutputPath account.xml
```

Exports all fields in category 'Account' to file 'account.xml'.

### Example 2 (Categry)
```powershell
# create array of filters
$filters = @((Get-CmcFilterF 1 accountKey 0 Wing),
            (Get-CmcFilterCTI 2 'Relates to' 'salesTeam' 'Team 1' -OrFilter))
# perform the export
Export-CmcCategory -c Account accounts.json -ExportFormat Json -Filters $filters -FieldNames accountKey, Address, City, zipPostal, Country, 'Relates to Employee'
```

Advanced example (using _Tutorial database_): export address fields and list of Sales person of items in Account that have 'Wing' in their name or are connected to 'salesTeam' item 'Team 1' to a JSON file.

### Example 3 (View)
```powershell
Export-CmcView -v 'Contact List' -OutputPath contactlist.xml
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

### -UseColumnNames
Use the columnlabel as node/key/column header. If omitted, the underlying Commence fieldname is used.

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

### None
Writes data to a file.

## NOTES

## RELATED LINKS

[Get-CmcFilter](Get-CmcFilter.md)