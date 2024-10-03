# Setting Up Auth0 Configuration for Seed

This guide will help you configure the Auth0 settings for the REST API.

## Steps to Configure Auth0

1. **Create an Auth0 Account and Tenant:**
   - Visit [Auth0](https://auth0.com/) and sign up for an account if you haven't already.
   - Once logged in, create a new tenant. This will be your isolated environment where you can manage your applications and configurations.

2. **Set Up a New Application in Auth0:**
   - In your Auth0 Dashboard, navigate to the 'Applications' section.
   - Click 'Create Application'. Choose a name for your application and select its type (e.g., 'Regular Web Application').
   - Once the application is created, you will be provided with a 'Client ID' and 'Client Secret'. Keep these confidential.

3. **Configure Callback URLs:**
   - In the settings of your Auth0 application, look for the 'Allowed Callback URLs' field.
   - Add the following Callback URLs, ensuring they match your deployment settings:
     - `https://localhost/callback`
     - `https://your-project.ssot.me:42123/callback`
     - `https://your-project.ssot.me:42123/pkce/callback`
     - `https://your-project.ssot.me:42123/account/callback`     
   - Also add these Logout URLS:
     - `https://localhost/logout`
     - `https://your-project.ssot.me:42123/logout`
     - `https://your-project.ssot.me:42123/pkce/logout`
     - `https://your-project.ssot.me:42123/account/logout`

## Update Application Settings File

Rename the file appsettings.default.json -> appsettings.json (which is .gitignored so that secrets are not committed)

After setting up your Auth0 application, you need to update the application settings file in your project. The file format is as follows:

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",

    "Auth0": {
        "Domain": "{your-auth0-domain-goes-here}.us.auth0.com",
        "ClientId": "{auth0 client id}",
        "ClientSecret": "{auth0 client secret}",
         "PKCERootUrl": "https://localhost:42123"
    },
    "Kestrel": {
        "EndPoints": {
            "Https": {
                "Url": "https://localhost:42123",
                "Certificate": {
                    "Path": "./iis-export.pfx",
                    "Password": "Password123!"
                }
            }
        }
    }
}
```


# Running Kestral Server (Custome port & ssl cert)

You will need to provide your own iis-expoert.pfx (or equivelant).  The react app shows how to get an auth0 url, and then to turn it into a JWT that works with the rest of the APIs (role based).


# Auth0 integration

The only thing that Auth0 is used for is actually to securely determine the user's email address.  Then - based on that email address, it looks up an AppUser in the db with that email address, or creates a new one in that table, with the default User role.  This is Player for the tic-tac-toe demo.

# Effortless API Project CLI

This roast_and_toast_game_info.xlsx will create a fully functional, secure implementation with access to your
EffortlessAPI endpoint for easy automation on the scripting platform of your choice.

## Installation with the EffortlessAPI CLI `npm install -g eejai42/eapi-cli`
> eapi -cloneEAPISeed airtable-direct-cli ProjectXYCli your-project-alias

## Manual Installation
Creating a CLI for your project involves the following steps:
1. Clone this repository to a folder called `/your-cli` (assuming your EffortlessAPI Project is called `bob-your-project` for example.)
2. Update `/SSoTmeProject.json` to replace `roast_and_toast_game_info.xlsx` with `bob-your-project`
3. Update `/DotNet/CLIClassLibrary/EAPICLIHandler.cs` to replace `roast_and_toast_game_info.xlsx` with `bob-your-project`.
4. Update `/package.json` to replace `roast_and_toast_game_info.xlsx` with `bob-your-project`.
5. Build the ssot.me code: `ssotme -build`
6. Install the NPM package locally to test `npm install . -g`
7. Test the package: `your-cli help`
   
See below for further help/instructions.

## Usage
The following behaviors are common across all CLI's.  The specifics beyond what
is described here will depend on the specific EffortlessAPI project being
managed.

## Airtable API Key & EAPI CLI Authentication
There are two types of keys used by the AirtableDirect CLI.  One is related to who I am with respect to airtable, which is tied
to an Airtable SSO Email ADdress.  For example, I sign into airtable as either a personal Airtable User, or as a Business Airtable 
user - each with it's own email address.

All Airtable Direct CLI's need to be able to set/save the API Key associated with this email address - but could be done actually
be done with the


your-atd-cli -emailAddress=your-airtable-user@domain.com -apiKey=key123abc321 -makeDefault

This key will be saved into a file stored in your profile as follows:
`> ~/.atdcli/{emailAddress}/airtable.key`

If the `-makeDefault` parameter is provided, then it will also write that key here:
`> ~/.atdcli/airtable.key`

All ATD CLI commands will then use that key from the data access layer, which will be used to provide authenticated access
as described below.  Without a proxy server these rules will be implemented locally - so while they won't *actually* be secure
they will behave as though they are secure - and an EFfortlessAPI proxy server can be plugged in at any point
to implement the requests somewhere else, at which point it will *actually* be secure. :)


### CLI Authentication
Any CLI will include simple commands for authenticating with any of the methods supported

> your-cli -authenticate you@domain.com -demoPassword Password123
> your-cli -authenticate you@domain.com -sha256HashedPassword xyzq23k321zyx
> your-cli -authenticate -withGoogle
> your-cli -authenticate -withFacebook
> your-cli -authenticate -withTwitter

This authentication should be saved to this location:
`> ~/.atdcli/{baseId}/{runas}/accessToken.key

### Registration
If your Guest role has Create permission on your `User` object, then your
CLI will include a `-regisiter you@domain.com` command line option.

### -as Role
By default, the first commands issued will be assumed to come from `-as Guest`, however, Any command can be updated to also include `-as Employee`, or `-as AnyRole` which will override the default.

It will also "remember" that request, and any subsequent requests which do not
specify the `-as ExplicitRole` will be invoked using the previous role.

### -help or help
Any any time you can request help as follows:
> your-cli -help GetInv
> your-cli -help
> your-cli help
> your-cli help -as Employee
> your-cli help -as Guest

This will list any commands which the given user can access.

> your-cli help GetCustomers -as Admin

Because this matches a specific method that Admin can reference, the
help will provide a detailed description of the method/payload, response, etc.

```Help for Admin.

Available Actions Matching: getcustomers
 - GetCustomers

* * * * * * * * * * * * * * * * * * * * * * * * * * *
* *  Customer     *
* *     - A customer of company XYZ, mostly online sales.  Created
* *       from Quickbooks
* * * * * * * * * * * * * * * * * * * * * * * * * * *

CRUD      - CustomerId
CRUD      - Name
CRUD      - Notes
CRUD      - Attachments
CRUD      - Status
CRUD      - Invoices
CRUD      - EmailAddress
CRUD      - User
CRUD      - UserEmail
```

There will also be a detailed Html description of how to interact with the
CLI as well at `/cli.html`.



# Steps to create the SSoT
1. Install the EffortlessAPI Chrome Extension
2. Create/Open an Airtable
3. Open the Docs for the airtable - where the Chrome Extension will capture the size/shape of the Airtable
4. Install eapi NPM Module: `> npm install eejai42/eapi-cli -g'
5. Copy the airtable baseId - `app123abc321`
6. Authenticate your EffortlessAPI CLI: `> eapi -authenticate you@domain.com -demoPassword yourDemoPassword -as Developer`
6. Create an Airtable EFfortlessAPI Project: `> eapi createAirtableProject roast_and_toast_game_info.xlsx -baseId app123abc321`
7. Log into https://EffortlessAPI.com
8. Open the newly created project 'roast_and_toast_game_info.xlsx'
9. Navigate to `Design` | `Sync` and click the `Synchronize` button to update the project with your Airtable Schema. 
10. Navigate to `Publish` and Click the Publish button to publish the EffortlessAPI project online


# Steps to Create the CLI
### - based on an EffortlessAPI project
1. Create the CLI: `> eapi -cloneEAPISeed airtable-direct-cli your-cli roast_and_toast_game_info.xlsx -betaRepo 
2. Navigate to the root folder and install the CLI as a global NPM package
3: `cd roast_and_toast_game_info.xlsx`
4. `npm install -g`
5. Run the cli `> roast_and_toast_game_info.xlsx -as admin -help` for example

# Next Steps - Secure deployment architecture...

Get the .net REST API up and running integrated with Auth0 authentication, so that users can
authenticate to that auth0 endpoint, and then access api methods using a single api key for all users
of that REST endpoint.

Then - the CLI can be configured to, rather than accessing the Airtable API directly - can instead 
be configured to route it's requests to the locally hosted REST API - that can act as a single 
proxy server for a whole office of users.


### Layered Approach to the `airtable-direct-cli`

# airtable-direct-cli Tool Overview

The `airtable-direct-cli` tool provides a versatile utility designed to facilitate secure and role-specific interactions with Airtable databases. Here's a breakdown of its configurations and operations:

## 1. Direct Access Mode
- This configuration allows direct interaction with an Airtable using a provided API key.
- Users specify their role using the `-as` flag. This mode is trust-based. For instance, a user with a write-access API key could falsely claim to be an 'admin' instead of an 'employee', leading to potential unauthorized actions. Suitable for trusted actors.

## 2. Server Proxy Mode
- To ensure a wider and secure distribution, a proxy server is integrated.
- This server is a .NET Core Web API application, dynamically generated by the `airtable-direct-cli`.
- Only the server holds the Airtable API key, effectively shielding it from the end-users.
- Users undergo an authentication process against this server using Auth0. Once authenticated, the server verifies the user�s role, ensuring they can only execute API calls matching their role. 
- This mode acts as a secure gateway, preventing any direct, unauthorized access to Airtable.

## 3. REST Endpoint Mode
- An advanced extension of the Server Proxy Mode.
- In this mode, the tool doesn't communicate directly with the proxy server but instead with a pre-defined REST endpoint.
- Users authenticate using the `-authenticate` command, subsequently receiving a JWT (JSON Web Token) that encapsulates their role.
- Commands, like `my-app getwidgets -as employee`, are then forwarded to the REST endpoint alongside the JWT.
- The server, upon receiving the request, validates the JWT, ascertains the user's role, and responds in line with that role.

## Summary
The `airtable-direct-cli` tool is adept at:
- Direct interfacing with an Airtable for environments with trusted users.
- Generating a role-authenticating proxy server to validate and route requests.
- Serving as a client that securely communicates with a RESTful endpoint, always ensuring role-specific access and security.

The codebase, both on the client and server-side, can be made open source. Sensitive keys (e.g., Airtable API key, Auth0 credentials) are not embedded but are instead supplied during runtime or setup. This approach ensures layered security, catering to both small trusted groups and larger public audiences while guaranteeing data access integrity.
