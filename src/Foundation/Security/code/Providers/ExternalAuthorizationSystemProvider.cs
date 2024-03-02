using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using System.Linq;
using Sitecore.Diagnostics;
using System.Security.Claims;
using DreamTeam.Foundation.Security.CustomAuthSystemCore;
using System;

namespace DreamTeam.Foundation.Security.Providers
{
    public class ExternalAuthorizationSystemProvider : IExternalAuthorizationSystemProvider
    {
        public bool IsUserAuthorizedToGetSpecificItemAccess(Item entity, User user)
        {
            Assert.ArgumentNotNull(entity, "entity");
            Assert.ArgumentNotNull(user, "user");

            //Only Admin use has full granted access
            if (user.IsAdministrator)
            {
                return true;
            }

            //TODO
            var userIdentity = user.Identity as ClaimsIdentity;

            var externalSecurityModel = SecurityEntitlement.GetSecurityModelByUserId(userIdentity);

            return externalSecurityModel?.Entities?
                        .FirstOrDefault(securityModel => entity[Templates._ExternalId.Fields.Urn]
                        .StartsWith(securityModel.Urn, StringComparison.InvariantCultureIgnoreCase))?.IsAllowed ?? false;
        }
    }
}