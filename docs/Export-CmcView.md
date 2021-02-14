---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Export-CmcView

## SYNOPSIS
Export a Commence view to file.

## SYNTAX

```
Export-CmcView [-ViewName] <String> -OutputPath <String> [-ExportFormat <ExportFormat>] [<CommonParameters>]
```

## DESCRIPTION
Export a Commence view to file if the view supports it.

## EXAMPLES

### Example 1
```powershell
PS C:\> Export-CmcView 'Contact List' -OutputPath contactlist.xml
```

Export view 'Contact List` to file contactlist.xml using default settings.

## PARAMETERS

### -ExportFormat
File format to export to

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

### -ViewName
Commence view name. Case-sensitive!

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

### -OutputPath
{{ Fill OutputPath Description }}

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

### None
You cannot pipe input to this cmdlet.

## OUTPUTS

### None
Writes to a file.

## NOTES
Not all Commence viewtypes support exporting. 

## RELATED LINKS
