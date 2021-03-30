---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Find-CmcView

## SYNOPSIS
Find a Commence view.

## SYNTAX

```
Find-CmcView [[-Name] <String>] [-CategoryName <String>] [-Type <String>] [<CommonParameters>]
```

## DESCRIPTION
Finds 

## EXAMPLES

### Example 1
```powershell
Find-CmcView
```

Without parameters it will output the complete list of views in Commence.

### Example 2
```powershell
Find-CmcView prod
Find-CmcView -Name prod
```

The first parameter will search the list of views for views that have names that contain the argument 'prod'. The `-Name` parameter supports tab-completion after the first run.

### Example 3
```powershell
Find-CmcView prod -Category Account -Type "Report Viewer"
```

Will list the views that match 'prod' in category 'Account' and of type 'Report Viewer'. The `-Category` and `-Type` parameters both have tab-completion.

## PARAMETERS

### -CategoryName
Commence category to filter on.

```yaml
Type: String
Parameter Sets: (All)
Aliases: c

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
(Part of) the view name to find.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The Commence viewtype to filter result on.

```yaml
Type: String
Parameter Sets: (All)
Aliases: t

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Vovin.CmcLibNet.Database.Metadata.IViewDef
## NOTES
Will not return Commence MultiViews because they are not exposed by Commence.

## RELATED LINKS
