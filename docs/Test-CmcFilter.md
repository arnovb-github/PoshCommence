---
external help file: PoshCommence.dll-Help.xml
Module Name: PoshCommence
online version:
schema: 2.0.0
---

# Test-CmcFilter

## SYNOPSIS
Test if filter is valid.

## SYNTAX

```
Test-CmcFilter [-Category] <String> [-Filter] <ICursorFilter> [<CommonParameters>]
```

## DESCRIPTION
Test if filter is syntactically valid by applying it to see if errors are thrown.

## EXAMPLES

### Example 1
```powershell
$f = Get-CmcFilterF 1 accountKey EqualTo 'A'
Test-CmcFilter Account $f -Verbose
```

Test validity of filter `$f`. Assuming the _Tutorial database_ this filter should pass. 

## PARAMETERS

### -Category
Commence category to apply filter on.

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

### -Filter
Filter object

```yaml
Type: ICursorFilter
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Boolean
## NOTES
This cmdlet tests if the filter is syntactically valid. That is, it tests if it *technically works*, not if it returns anything meaningful.

Use of this cmdlet in production environments is discouraged because it is a resource intensive operation.
## RELATED LINKS
[Get-CmcFilterF](Get-CmcFilterF.md)

[Get-CmcFilterCTI](Get-CmcFilterCTI.md)

[Get-CmcFilterCTCF](Get-CmcFilterCTCF.md)

[Get-CmcFilterCTCTI](Get-CmcFilterCTCTI.md)