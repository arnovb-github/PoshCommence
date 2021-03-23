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
Open-CmcView [-View] <IViewDef[]> [-NewCopy] [<CommonParameters>]
```

## DESCRIPTION
Used in conjunction with [Find-CmcView](Find-CmcView.md). Opens the view(s) in Commence and puts the window focus on Commence.

## EXAMPLES

### Example 1
```powershell
Find-CmcView -Name Product | Open-CmcView
```

Will open all views that contain 'Product' in their name. __NOTE__ If the number of views passed to this cmdlet exceeds a threshold, only the first number of views up to that threshold will be opened. At the time of writing this threshold was 10.

## PARAMETERS

### -NewCopy
Opens a copy of the view if it was already opened.

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

### -View
(Array of) IViewDef object(s) returned by `Find-CmcView`.

```yaml
Type: IViewDef[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Vovin.CmcLibNet.Database.Metadata.IViewDef

## OUTPUTS

### None
## NOTES
There is a maximum to the total number of views that Commence can show. In order to not make Commence blow up in your face immediately, there is a hard-coded limit to the amount of views this cmdlet will open. It may still blow up, just not immediately.

## RELATED LINKS

[Find-CmcView](Find-CmcView.md)
