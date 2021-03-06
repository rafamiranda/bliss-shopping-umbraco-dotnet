# APFIPP: Associação Portuguesa de Fundos de Investimento, Pensões e Patrimónios #

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/a85b80996e7e4c31b8603c4a3f095db8)](https://www.codacy.com?utm_source=bitbucket.org&amp;utm_medium=referral&amp;utm_content=blissapps/apfipp-umbraco-dotnet&amp;utm_campaign=Badge_Grade) [![Build Status](https://dev.azure.com/blissapps/APFIPP/_apis/build/status/ApfippDevelopment?branchName=develop&label=build)](https://dev.azure.com/blissapps/APFIPP/_build/latest?definitionId=6&branchName=develop)

## Summary ##

*  This project covers the Umbraco Website for APFIPP.

## Environments
  *  **Development** 
     * [![Deployment Status](https://vsrm.dev.azure.com/blissapps/_apis/public/Release/badge/7b7f561a-7854-487f-80af-14146d994747/1/1)](https://dev.azure.com/blissapps/APFIPP/_release?view=mine&_a=releases&definitionId=1)
     *  Frontend: [https://dev.web.apfipp.blissapplications.com](https://dev.web.apfipp.blissapplications.com)
     *  Umbraco: [https://dev.web.apfipp.blissapplications.com/umbraco](https://dev.web.apfipp.blissapplications.com/umbraco)
        *  If you require an access to Umbraco, send a request to **Tiago Braz**
  *  **Staging/QA**
     *  [![Deployment Status](https://vsrm.dev.azure.com/blissapps/_apis/public/Release/badge/7b7f561a-7854-487f-80af-14146d994747/3/3)](https://dev.azure.com/blissapps/APFIPP/_release?view=mine&_a=releases&definitionId=3)
     *  https://qa.apfipp.blissapplications.com/
  *  **Production**
     *  [https://www.apfipp.pt/](https://www.apfipp.pt/) (**OLD WEBSITE**)

## Branching Strategy
| NAME | BRANCH TYPE | BRANCH OBJECTIVE | SOURCE | TARGET | ENVIRONMENT  | ADDRESS |
|----------|-----------|------------|-----------|------------|--------------|---------------------------------------------|
| `bugfix/{ISSUE_ID}` | Dynamic | Bug to be fixed and released in next planned version | `develop` | `develop` | Dynamic | - |
| `feature/{ISSUE_ID}` | Dynamic | Feature to be implemented and released in next planned version | `develop` | `develop` | Dynamic | - |
| `feature/devops` | Static | Test CI/CD pipelines & other devops integrations  | `develop` | `develop` | `Develop` | https://dev.web.apfipp.blissapplications.com |
| `develop` | Static | Join of all features and/or bugs to be released in next planned version |  |  | `Develop` | https://dev.web.apfipp.blissapplications.com |
| `quality` | Static | Join of all features and/or bugs to be released to production |  |  | `Quality` | https://qa-site-apfipp.azurewebsites.net |
| `master` | Static | Code running in production |  | (tag) `version/{NEXT_VERSION}` | `Production` | - |

## How do I get set up ##

*  Requirements
*  Summary of set up
*  Configuration
*  Dependencies
*  Database configuration
*  How to run tests

## CI / CD
| Environment | Trigger | Actions | Platform | Provision Setup |
|-------------|---------|---------|----------|-----------------|
| CI          | Push to: `feature/* bugfix/* hotfix/*` or PR's to `develop` or `master` branches | Build Only | [Azure DevOps](https://dev.azure.com/blissapps/APFIPP/_build) |
| Development | Push to `develop` branch | Build & Deploy | [Azure DevOps](https://dev.azure.com/blissapps/APFIPP/_build) | [Internal Terraform Provisions](https://dev.azure.com/blissapps/Bliss%20Internal%20Terraform%20Provisions) |
| QA | push to `quality` branch | Build & Deploy * | [Azure DevOps](https://dev.azure.com/blissapps/APFIPP/_build?definitionId=55) | `terraform` branch
| Production | - | - | - |

\* **Important: `quality` Pipeline fetches frontend assets (js, css) from `quality` branch frontend pipeline artifacts before building solution**

To skip build & deploy triggers, add [skip ci] to your commit message, eg:

`git commit -m "chore: just an insignificant change [skip ci]"`

#   b l i s s - s h o p p i n g - u m b r a c o - d o t n e t  
 