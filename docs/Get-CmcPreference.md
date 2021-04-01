---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Get-CmcPreference

## SYNOPSIS
Get Commence preference setting.

## SYNTAX

```
Get-CmcPreference [-Preference] <String> [<CommonParameters>]
```

## DESCRIPTION
Gets the preference settings as exposed by Commence.

## EXAMPLES

### Example 1
```powershell
Get-CmcPreference Me
```

Gets the (-Me-) item, if defined.

## PARAMETERS

### -Preference
Preference item to retrieve.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Accepted values: Me, MeCategory, LetterLogDir, ExternalDir

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

### System.String
## NOTES

## RELATED LINKS
