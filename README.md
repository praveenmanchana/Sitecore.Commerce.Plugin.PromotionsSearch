# Promotions Search

This is a sample solution that can enable indexing and search functionality for promotions in sitecore commerce. 

For indexing any other entities, use this as a base sample and replace promotions with the relevant entities

## How to use this

1. Perform the initial setup of the Solr core by running the powershell script from setup folder `.\Install-Promotions-Solr.ps1`.
    *Note: Please update the variables to match you local environment*
1. Integrate the `Plugin.Sample.PromotionsSearch` into your solution
1. Integrate the `PromotionsSearch-PolicySet.json` into your solution
1. Build, Deploy and Bootstrap commerce engine.
1. Import `Promotions Search.postman.json` postman collection
1. Run the `Full Index Promotions Minion`
