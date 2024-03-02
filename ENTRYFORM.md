
# Hackathon Submission Entry form

## Team name
⟹ -== Dream Team ==-

## Category
⟹ Best Module for XM/XP or XM Cloud

## Description
⟹ For this season of Sitecore Hackathon session I decide to choose category: *Best Module for XM/XP or XM Cloud*. While some time ago I thought about Sitecore security model and a ways how to extend it. Based on a fact that security model can be build around core content elements driven by business side. 

Lets simple imagine a situation that we have some batch of products or services which our business selling to the *end-users* (**B2C** or **B2B** selling model). Our goal (and at the same time a problem which we need to solve) distinguash products and services based on, for example, *user role* or *subscription*.

Of course, we can *manually* setup security for the particular items via Content Editor BUT what if wee need to dial with complex and sophisticated security model represented by data from PIM (Product Information Model) repository which allocated somewhere outside Sitecore infrastructure and expose own authorization and authentification system! Exactly for this purpose I create this module and solving security integrity between Sitecore and External Security System (ESS).

This module simply injected into Sitecore <authorization> pipeline and become solely pipe for security access decision. While we a trying to get access to specific item with base template: ***_EncourageByEntitlements***, module requested security information from External Authorization System (aka EAS) using external URN field value for interconnection between Sitecore and EAS in security decision making. 

This module consists of middleware tiers:
 - IExternalAuthorizationService
 - IExternalAuthorizationSystemProvider
 - ISecurityModelFromExternalServer
 - ISecurityModelFromExternalServer

As a ways of improvements I'd like to admire following:
 - Recursively apply security for related items
 - Caching data on all levels
 - Dynamic calculation
 - Extend logic using pipeline for more complex security access decision rather than based on `Urn` field

## Video link
⟹ [https://youtu.be/vxyOLUYqeXo](#video-link)

## Installation instructions
⟹ Installation for this module requires Docker image builds:

 1. Run init script for docker `docker_init.ps1`, where provide pass to the license file
 2. Run `up.ps1`script
 3. Serialized items and users will be installed as part of `up.ps1` script
 4. If you missed step #3, please, run `push_items.ps1` script, instead
 5. If you need repeat module's installlation from scratch, please, run `stop.ps1` script and `/docker/clean.ps1` script, right after previous command

## Usage instructions
⟹ HOWTO instruction for this module is pretty simple. All major things are installed via SCS and deployment process. 

User has ability to check how module works in following way. There are two predefined users both has the same `b` password:
![Predefined Users](docs/images/predefined_users.png?raw=true "Predefined Users")
`admin_user` granted with Sitecore Administrator right and `regular_user` has permishion to logic and view content:
![Admin User](docs/images/admin_user.png?raw=true "Admin User")
![Regular User](docs/images/regular_user.png?raw=true "Regular User")
While you login with one of the predefined users you can observe how content items list changed under `Product Repo`folder. And there are no `Security Rights` field assgined. 
![Admin User View](docs/images/admin_user_view.png?raw=true "Admin User View")
![Regular User View](docs/images/regular_user_view.png?raw=true "Regular User View")

Fake data from the code which emulate response from EAS control restriction for viewing content for each user type.
![Regular User View](docs/images/fake_data_from_EAS.png?raw=true "Regular User View")
