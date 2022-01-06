---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Clear-CmcControlCharacters

## SYNOPSIS
Clears control characters from Commence category items. USE WITH CAUTION.

## SYNTAX

```
Clear-CmcControlCharacters [-CategoryName] <String> [[-LogDir] <String>] [-MaxRows <Int32>]
 [[-ColumnDelimiter] <String>] [-Force] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Commence allows for entering control characters in fields where there should be none. This can cause issues in various scenarios. This cmdlet clears or changes any control characters from fields which should not contain them. This cmdlet makes changes to the Commence database so take appropriate precautions to prevent potential data loss. It is *strongly* recommended that you start the Commence application with the /noagents command line parameter prior to running this cmdlet, or Commence may lock up and crash.

## EXAMPLES

### Example 1
```powershell
Clear-CmcControlCharacters Account
```

(Simplest, but see example 2) Will prompt user to remove control characters from fields in category 'Account'. A log will be written to '%AppData%\Local\PoshCommence'.

### Example 2
```powershell
Clear-CmcControlCharacters -CategoryName Account -WhatIf -LogDir C:\TEMP
```

(Recommended usage upon first run) Will do a dummy run on category 'Account' and write suggested changes to a log file in 'C:\TEMP'.

### Example 3
```powershell
Clear-CmcControlCharacters -CategoryName Account -WhatIf -LogDir C:\TEMP
Clear-CmcControlCharacters -CategoryName Account -LogDir C:\TEMP
```

Recommended usage. The first command will do a dry run. A logfile of pending changes will be written to 'C:\TEMP' in Json format. If you are satisfied with the changes, run the second command.

### Example 4
```powershell
Get-CmcCategories | Clear-CmcControlCharacters -WhatIf -LogDir C:\TEMP
```

Analyze all categories in the database by piping them to the cmdlet. A logfile of pending changes will be written to 'C:\TEMP' in Json format.

### Example 5
```powershell
Get-CmcCategories | Clear-CmcControlCharacters -LogDir C:\TEMP -Force
```

If you are absolutely certain you want to clean up all categories, pass in all category names and override any confirmation.

### Example 6
```powershell
Clear-CmcControlCharacters -CategoryName Account -ColumnDelimiter 'vErYbAD' -Force
```

EXPERTS ONLY. The 'ColumnDelimiter' parameter is **not** some casual CSV thingie, it is used to tell columns apart in a Commence database query. It is very unlikely that anyone should ever have to use the `ColumnDelimiter` parameter, I may in fact remove this option in future versions.

## PARAMETERS

### -CategoryName
Commence category name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: c

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ColumnDelimiter
Do not touch this unless you are a true Commence wizard. It is the delimiter passed to a RowSet's `GetRow()` method. You only ever need to change this if the default for some reason gives unwanted results. The default is defined in [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Overrides confirmation of running the cmdlet.

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

### -LogDir
Directory to write logfile to.

```yaml
Type: String
Parameter Sets: (All)
Aliases: l

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxRows
Number of Commence database rows to process at a time. Defaults to 100. Change this to 1 if an error occurs and you need to pinpoint the exact item that raised the error. There is a significant performance penalty when lowering this.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: m

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Writes proposed changes to the log file but does not actually make changes to the database.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### None
## NOTES
Some Commence fields will accept control characters when they should not. They can either be entered directly in the UI, copy/pasted, or written by the API. Depending on the fieldtype, this can cause unwanted side-effects, ranging from simply not being able to call a URL containing an unexpected CR/LF in it to Commence crashing on enrolls. This cmdlet will strip control characters from text-based fields. Tabs are replaced with spaces. Line endings and carriage returns are either removed completely or replaced with Windows line-endings.

## RELATED LINKS
