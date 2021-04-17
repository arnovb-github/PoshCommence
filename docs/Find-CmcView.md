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
Find-CmcView [-ViewName <String>] [-CategoryName <String>] [-ViewType <String>] [<CommonParameters>]
```

## DESCRIPTION
Can be used to search for Commence views by name, category or viewtype.

## EXAMPLES

### Example 1
```powershell
Find-CmcView
```

Without parameters it will output all views in Commence.

### Example 2
```powershell
Find-CmcView -ViewName prod
```

Will search the list of views for views that have a name that starts with 'prod'. The `-ViewName` parameter supports tab-completion after the first run. Note that while view names in Commence are case-sensitive, this argument is not.

### Example 3
```powershell
# using Tutorial database
Find-CmcView prod -CategoryName Product -ViewType Report
```

Will list the views that start with 'prod' in category 'Product' and are of type 'Report'. The `-CategoryName` and `-ViewType` parameters both support tab-completion.

## PARAMETERS

### -CategoryName
Commence category to filter views on.

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

### -ViewName
(First characters of) the view name to find.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ViewType
The Commence viewtype to filter views on.

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
Will not return views of type MultiView or Document because they are not exposed by Commence.

## RELATED LINKS
