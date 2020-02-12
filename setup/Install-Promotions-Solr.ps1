#Requires -RunAsAdministrator
#Requires -Version 5.0

Import-Module SitecoreInstallFramework

Invoke-Command -ScriptBlock {
  Set-Location -Path $PSScriptRoot

  $Prefix = 'sc92'
  $ConfigurationPath = $PSScriptRoot
  $SolrSchemasPath = Join-Path -Path $PSScriptRoot -ChildPath 'SolrSchemas'
  $SolrPath = 'C:\tools\solr-7.5.0'
  $SolrUrl = 'https://solr750:8750/solr'
  $SolrServiceName = 'solr-7.5.0'
  
  $commerceEngineConfigurationPath = Join-Path -Path $ConfigurationPath -ChildPath 'promotions-commerce-solr.json'
  
  $params = @{
    Path                                        = $commerceEngineConfigurationPath
    SolrUrl                                     = $SolrUrl
    SolrRoot                                    = $SolrPath
    SolrService                                 = $SolrServiceName
    SolrSchemas                                 = $SolrSchemasPath
    CorePrefix                                  = ('{0}_' -f $Prefix)
  }
  
  Write-Verbose -Message 'Installing Promotions Solr Core with params:'
  $params

  Install-SitecoreConfiguration @params -Verbose

}