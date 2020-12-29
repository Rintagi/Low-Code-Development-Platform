this is a demo setup for ip restricted directory browsing access for IIS

detail here

https://docs.microsoft.com/en-us/iis/configuration/system.webserver/security/ipsecurity/

and must delegate the feature at the main IIS level(not site but machine) in order for the web.config to override

https://docs.microsoft.com/en-us/iis/manage/managing-your-configuration-settings/an-overview-of-feature-delegation-in-iis

