#Requires -RunAsAdministrator
#Requires -Version 5.0

Import-Module SitecoreInstallFramework

Invoke-Command -ScriptBlock {
  Set-Location -Path $PSScriptRoot

  $Prefix = 'Commerce91'
  $ConfigurationPath = $PSScriptRoot
  $SolrSchemasPath = Join-Path -Path $PSScriptRoot -ChildPath 'SolrSchemas'
  $SolrPath = ('{0}\solr-7.2.1' -f $env:HOMEDRIVE)
  $SolrUrl = 'https://localhost:8983/solr'
  $SolrServiceName = 'solr 7.2.1'
  
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