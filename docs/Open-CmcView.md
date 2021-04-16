---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Open-CmcView

## SYNOPSIS
Open a view in Commence.

## SYNTAX

```
Open-CmcView -Name <String> [-NewCopy] [-Max <Int32>] [<CommonParameters>]
```

## DESCRIPTION
Typically used in conjunction with [Find-CmcView](Find-CmcView.md). Opens the view(s) in Commence and puts the window focus on Commence.

## EXAMPLES

### Example 1
```powershell
Find-CmcView -Name Product | Open-CmcView
```

Will open all views that contain 'Product' in their name. __NOTE__ If the number of views passed to this cmdlet exceeds a threshold, only the first number of views up to that threshold will be opened. At the time of writing this threshold is 5.

### Example 2
```powershell
Open-CmcView 'All Accounts'
```

Will open the view called 'All Accounts'. View names are case-sensitive.
## PARAMETERS

### -NewCopy
Opens a copy of the view even if it was already opened.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Max
Maximum number of views to open. Keep this member small or Commence or your brain will explode.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 5
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Commence view name. Case-sensitive.

```yaml
Type: String
Parameter Sets: (All)
Aliases: ViewName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Vovin.CmcLibNet.Database.Metadata.IViewDef

## OUTPUTS

### None
## NOTES

## RELATED LINKS

[Find-CmcView](Find-CmcView.md)
