# Source code scanning

## PREREQUISITE
### Create Repository Secrets
`REGISTRY_LOGIN_SERVER`
`REGISTRY_USERNAME`
`REGISTRY_PASSWORD`

1. Login to Azure portal 

[Azure_Portal](https://azure.microsoft.com/en-us/get-started/azure-portal/)

2. Select Azure cloud Shell
<img width="1103" alt="Screen Shot 2565-08-23 at 20 35 17" src="https://user-images.githubusercontent.com/46469458/186177823-b282391b-3737-4e11-90ea-e1137378f0ef.png">

3. First command to login to ACR. 
 
 ```console
 
export RESOURCE_GROUP="$(az group list --query "[?location=='eastasia']" | jq -r '.[0].name')"
export REPO_NAME="$(az acr list | jq -r '.[].name')"

 groupId=$(az group show \
   --name ${RESOURCE_GROUP} \
   --query id --output tsv)
 ```

4. Second command to login to ACR. create the service principal (Copy clientId and clientSecret please see detail in step 7.)
 
  ```console
 az ad sp create-for-rbac \
  --scope $groupId \
  --role Contributor \
  --sdk-auth
  ```
 
 <img width="761" alt="Screen Shot 2565-08-23 at 22 35 05" src="https://user-images.githubusercontent.com/46469458/186200948-9cfecd01-e02e-4fa1-a861-d2a8fb24e64c.png">

5. Third command to login to ACR. Please change <registry-name> to your registry name from step 4.

Please change `<registry-name>` with result from this command 
 
 ```console
 echo $REPO_NAME
 ```

```console
 registryId=$(az acr show \
   --name <registry-name> \
   --query id --output tsv)
 ```
   
 
6. Fourth command to login to ACR. Please change <ClientId> to your clientId or app id from step 4. And please keep the result.

  ```console
 az role assignment create \
  --assignee <ClientId> \
  --scope $registryId \
  --role AcrPush
  ```
 
<img width="1512" alt="Screen Shot 2565-08-23 at 20 04 07" src="https://user-images.githubusercontent.com/46469458/186165619-ac871267-2a51-4aed-bc55-60612e7e48c7.png">
 
7. Create Github Repo.

 In the GitHub UI, navigate to your forked repository and select Settings > Secrets > Actions and Select New repository secret to add the following secrets:

![image](https://user-images.githubusercontent.com/46469458/190967242-e4224148-6a45-4588-8415-f8451e8d431d.png)

Reference : https://docs.microsoft.com/en-us/azure/container-instances/container-instances-github-action

---

## Exercise 1
### Deploy to Dev Slot
1. Login to Azure Portal, goto `App Services` then select the App service from the list
2. Add `dev` slot, go to `Deployment slots` then click `Add slot`, set name as `dev` and clone the setting fromt an existing one

3. Click on deployment slot `dev` and then Go to `Overview` menu and click `Get publish profile` to download the publish profile
![Download Publish profile](./assets/get-publish-profile-01.PNG)


4. Create Secret name `AZURE_WEBAPP_PUBLISH_PROFILE` with the content from file that download
from step 2
5. Add another jobs to deploy to dev slot
```yaml
deploy-dev:
    name: Deploy to Dev Slot
    runs-on: ubuntu-latest
    needs: containerized

    steps:
      - name: deploy dev slot
        uses: azure/webapps-deploy@v2
        with:
          app-name: 'simple-service'
          # Add publish profile from secret that created from publish profile which we downloaded from Azure Portal
          publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
          images: 'repo-name/simple-service:${{ github.sha }}'
```
6. Commit and push the code, and see how the workflow is running
7. Login to Azure portal, App Service and see the changes


## Exercise 2
### Deploy to production slot


