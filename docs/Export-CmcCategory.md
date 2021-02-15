---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Export-CmcCategory

## SYNOPSIS
Export a category to file.

## SYNTAX

```
Export-CmcCategory [-CategoryName] <String> -OutputPath <String> [-ExportFormat <ExportFormat>]
 [-Filters <ICursorFilter[]>] [-FieldNames <String[]>] [-SkipConnectedItems] [-UseThids] [<CommonParameters>]
```

## DESCRIPTION
Use this to export the data in a Commence category to a file.

## EXAMPLES

### Example 1
```powershell
PS C:\> Export-CmcCategory Account accounts.xml
```

Simple example (using _Tutorial database_): Export the entire Account category to file account.xml using default settings.

### Example 2
```powershell
# create array of filters
$filters = @((Get-CmcFilterF 1 accountKey 0 Wing),
            (Get-CmcFilterCTI 2 'Relates to' 'salesTeam' 'Team 1' -OrFilter))
# perform the export
Export-CmcCategory Account accounts.json -ExportFormat Json -Filters $filters -FieldNames accountKey, Address, City, zipPostal, Country, 'Relates to Employee'
```

Advanced example (using _Tutorial database_): export address fields and list of Sales person of items in Account that have 'Wing' in their name or are connected to 'salesTeam' item 'Team 1' to a Json file.

## PARAMETERS

### -CategoryName
Name of Commence category to export from.

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

### -ExportFormat
File type to export to.

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
Array of fieldnames to include in the export. Does no accept related fields.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filters
Array of `CursorFilter`s to apply to the category.

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

### -SkipConnectedItems
Do not export connected items.

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

### -UseThids
Include THID in export.

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

### -OutputPath
Path and filename to export to.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None.
You cannot pipe input to this cmdlet.

## OUTPUTS

### None
Writes to a file.

## NOTES
Use this cmdlet for simple exports. For advanced exporting, see [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet).

## RELATED LINKS
[Get-CmcFilterF](Get-CmcFilterF.md)

[Get-CmcFilterCTI](Get-CmcFilterCTI.md)

[Get-CmcFilterCTCF](Get-CmcFilterCTCF.md)

[Get-CmcFilterCTCTI](Get-CmcFilterCTCTI.md)

[Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet)