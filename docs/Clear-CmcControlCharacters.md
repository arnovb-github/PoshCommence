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
Commence in some cases allows for entering control characters in fields where there should be none. In various cases this can be troublesome. This cmdlet clears or changes any control characters from fields which should not contain them. This cmdlet changes values in the Commence database. Take appropriate precautions to prevent potential data loss. Also, it is recommended that you start the Commence process with the /noagents command line parameter prior to running this cmdlet, or Commence may lock up and crash.

## EXAMPLES

### Example 1
```powershell
Clear-CmcControlCharacters Account
```

Will prompt user to remove control characters from fields in category 'Account'. A log will be written to '%AppData%\Local\PoshCommence'.

### Example 2
```powershell
Clear-CmcControlCharacters -CategoryName Account -WhatIf -LogDir C:\TEMP
```

(Recommended to run first) Will do a dummy run on category 'Account', writing potential changes to a log file in 'C:\TEMP'.

### Example 3
```powershell
Clear-CmcControlCharacters -CategoryName Account -ColumnDelimiter 'VeryBad' -Force
```

DO NOT DO THIS. Use the `ColumnDelimiter` only if you are an expert user *and* you find that the (hard-coded) default does not work properly. The ColumnDelimiter is NOT some casual CSV thingie, it is used to tell columns apart in a Commence database query. As for `Force`, the name itself implies danger.

### Example 4
```powershell
Clear-CmcControlCharacters -CategoryName Account -WhatIf -LogDir C:\TEMP
Clear-CmcControlCharacters -CategoryName Account -LogDir C:\TEMP
```

Recommended usage. The first command will do a dry run. A logfile of pending changes will be written to 'C:\TEMP' in Json format. Json is a way to concisely represent control characters in text format and it will allow you to do analysis by reading it back into objects if so desired.

### Example 5
```powershell
Get-CmcCategories | Clear-CmcControlCharacters -WhatIf -LogDir C:\TEMP
```

Analyze the entire database by piping all categories to the cmdlet. A logfile of pending changes will be written to 'C:\TEMP' in Json format. Json is a way to concisely represent control characters in text format and it will allow you to do analysis by reading it back into objects if so desired.

### Example 6
```powershell
Get-CmcCategories | Clear-CmcControlCharacters -LogDir C:\TEMP -Force
```

You are absoluty certain you want to clean up all categories, so you pass in all category names and you override the confirmation with `-Force`.

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
Do not touch this unless you are a Commence wizard. It is the delimiter passed to a RowSets `GetRow()` method. You only ever need to change this if the default for some reason gives unwanted results. The default is defined in [Vovin.CmcLibNet](https://github.com/arnovb-github/CmcLibNet).

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
Shows what would happen if the cmdlet runs.

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
Depending on the Commence version, some Commence fields will accept control characters when they should not. They can either be entered directly in the UI, copy/pasted, or written by the API. Depending on the fieldtype, this can cause unwanted side-effects, ranging from simply not being able to call a URL with a CRLF in it to Commence crashing on enrolls. This cmdlet will strip text-based fields from control characters. Tabs are replaced with spaces. Line endings and carriage returns are either removed completely or replaced with Windows line-endings.

## RELATED LINKS
