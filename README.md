﻿# AzureFunctionBlogTrigger

The application contains logic for an Azure function which is triggered when there is a change in Azure blog storage which then causes an Azure web application to restart.

# Dependencies

In order for me to create this function and run tests locally I used the following applications/software:
1. SQL Express - required for Azure storage emulator.
2. Azure Storage Emulator
3. Azure Storage Explorer
4. VS Code
5. An Azure service principal for use with the Azure Management SDK
6. Azure CLI

# Service Principal
```bash
az login
az account list
az account set --subscription {name|ID}
az ad sp create-for-rbac --sdk-auth
```
The above command creates a service principal account on the default selected subscription.
The output of the last command is required for configuration used within `PostAddedTrigger.cs`. 

The configuration values need to be set either in the local.setting.json if testing locally or in the application settings of the Azure function app. Replacing all the placeholders with the correct values.
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "[STORAGE_ACCOUNT_CONNECTION_STRING]",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "blob_STORAGE": "[STORAGE_ACCOUNT_CONNECTION_STRING]",
    "clientId": "[CLIENT_ID]",
    "clientSecret": "[CLIENT_SECRET]",
    "tenantId": "[TENANT_ID]",
    "webAppName": "[WEB_APP_NAME]",
    "webAppResourceGroupName": "[WEB_APP_RESOURCE_GROUP_NAME]"
  }
}
```
