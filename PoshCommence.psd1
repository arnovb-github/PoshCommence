#
# Module manifest for module 'PoshCommence'
#
# Generated by: Arno
#
# Generated on: 16-2-2021
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'PoshCommence.dll'

# Version number of this module.
ModuleVersion = '0.5.0'

# Supported PSEditions
# CompatiblePSEditions = @()

# ID used to uniquely identify this module
GUID = 'ea30a601-c676-44f4-9b05-f074d6447325'

# Author of this module
Author = 'Arno van Boven'

# Company or vendor of this module
CompanyName = 'Vovin IT Services'

# Copyright statement for this module
Copyright = '(c) 2020 - 2021 Arno van Boven. All rights reserved.'

# Description of the functionality provided by this module
Description = 'An experimental set of Powershell cmdlets for use with Commence RM Designer Edition.'

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '5.1'

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# DotNetFrameworkVersion = '4.7.2'

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# CLRVersion = ''

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
# RequiredModules = @()

# Assemblies that must be loaded prior to importing this module
# RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
# FormatsToProcess = @()

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
# NestedModules = @()

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = @()

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @("Clear-CmcMetadataCache","Export-CmcData","Find-CmcView", "Get-CmcCategories","Get-CmcConnectedField","Get-CmcConnectedItemCount","Get-CmcConnections","Get-CmcDatabaseDirectory","Get-CmcData","Get-CmcDatabaseName","Get-CmcDbSize","Get-CmcFields","Get-CmcFilter","Get-CmcIniFile","Get-CmcItemCount","Get-CmcLogFile","Get-CmcPreference","Open-CmcView","Test-CmcFilter")

# Variables to export from this module
VariablesToExport = '*'

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = @()

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @("PoshCommence", "Commence", "Vovin", "CmcLibNet")

        # A URL to the license for this module.
        LicenseUri = 'https://github.com/arnovb-github/PoshCommence/blob/master/LICENSE'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/arnovb-github/PoshCommence'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        ReleaseNotes = 'Another substantial overhaul. Reduced number of cmdlets by combining them, removed some unneeded ones. Renamed a few for consistency.'

    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
HelpInfoURI = 'https://github.com/arnovb-github/PoshCommence/tree/master/docs/en-US'

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

