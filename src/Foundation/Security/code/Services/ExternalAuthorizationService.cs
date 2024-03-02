using DreamTeam.Foundation.Security.Providers;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;
using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DreamTeam.Foundation.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DreamTeam.Foundation.Security.Exceptions;
using Sitecore.Configuration;
using System;
using System.Runtime.Caching;
using DreamTeam.Foundation.Security.Helpers;
using Sitecore.Data;
using DreamTeam.Foundation.Security.Entitlement.Entities;

namespace DreamTeam.Foundation.Security.Services
{
    public class ExternalAuthorizationService : IExternalAuthorizationService
    {
        public AccessResult GetAccess(ISecurable entity, Account account)
        {
            Assert.ArgumentNotNull(account, "account");

            return new AccessResult(AccessPermission.NotSet, new AccessExplanation("Skiped by External Authorization system due to lack of entitlement restrictions or no data for particular item."));
        }
    }
}