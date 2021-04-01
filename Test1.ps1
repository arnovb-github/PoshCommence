$f = Get-CmcFields -CategoryName CategoryA | % {$_.Name}
# $rf = @((Get-CmcRelatedColumn 'Connect Self', 'CategoryA', Numberfield),
# (Get-CmcRelatedColumn 'Relates to', 'CategoryB', "Name"),
# (Get-CmcRelatedColumn 'Relates to', 'CategoryB', "TimeField"),
# (Get-CmcRelatedColumn 'Relates to', 'CategoryB', "LargeText")
# )
$filter = Get-CmcFilter -ClauseNumber 1 -FilterType Field -Qualifier Contains -FieldName Name -FieldValue '20'

Export-CmcData -CategoryName CategoryA -OutputPath "e:\temp\testcat.xml" -Filters $filter -FieldNames $f -Verbose
Export-CmcData -CategoryName CategoryA -OutputPath "e:\temp\testcat.json" -ExportFormat Json -Filters $filter -FieldNames $f -Verbose
#Export-CmcCategory -CategoryName CategoryA -OutputPath "e:\temp\testcat.xml" -Filters $filter -FieldNames $f