# Promotions Search

This is a sample solution that can enable indexing and search functionality for promotions in sitecore commerce. 

For indexing any other entities, use this as a base sample and replace promotions with the relevant entities.

## How to use this

1. Perform the initial setup of the Solr core by running the powershell script from setup folder `.\Install-Promotions-Solr.ps1`.
    *Note: Please update the variables to match you local environment*
1. Integrate project `Plugin.Sample.PromotionsSearch` into your solution
1. Integrate Policy Set `Plugin.Sample.PromotionsSearch.PolicySet-1.0.0.json` into your solution
1. Integrate Policy Set `Plugin.Sample.PromotionsSearch.Minions.PolicySet-1.0.0.json` into your solution
1. Add `Entity-PolicySet-PromotionsSearchPolicySet` to your Authoring, Shops and Minions environments
    ```JSON
      {
        "$type": "Sitecore.Commerce.Core.PolicySetPolicy, Sitecore.Commerce.Core",
        "PolicySetId": "Entity-PolicySet-PromotionsSearchPolicySet"
      }    
    ```
1. Add `Entity-PolicySet-PromotionsSearchMinionsPolicySet` to your Minions Environment
    ```JSON
      {
        "$type": "Sitecore.Commerce.Core.PolicySetPolicy, Sitecore.Commerce.Core",
        "PolicySetId": "Entity-PolicySet-PromotionsSearchMinionsPolicySet"
      }    
    ```
1. Build, Deploy and Bootstrap commerce engine.
1. Import `Promotions Search.postman_collection.json` postman collection
1. Run the `Full Index Minion - Promotions`
