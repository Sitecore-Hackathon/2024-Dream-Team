namespace DreamTeam.Foundation.Security.CustomAuthSystemCore
{
    using Sitecore.Diagnostics;
    using Sitecore.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using System.Security.Claims;
    using DreamTeam.Foundation.Security.Model;

    public static class SecurityEntitlement
    {
        private static readonly ISecurityModelFromExternalServer _securityModelServerRequester;

        private static readonly object _lockObj;

        static SecurityEntitlement()
        {
            _securityModelServerRequester = ServiceLocator.ServiceProvider.GetService<ISecurityModelFromExternalServer>();

            Assert.ArgumentNotNull(_securityModelServerRequester, nameof(_securityModelServerRequester));

            _lockObj = new object();
        }

        public static SecurityEntitlementModel GetSecurityModelByUserId(ClaimsIdentity userIdentity)
        {
            Assert.ArgumentNotNull(userIdentity, nameof(userIdentity));
            //TODO
            var azureADUserId = userIdentity.FindFirst("oid")?.Value;
            if (!string.IsNullOrWhiteSpace(azureADUserId))
            {
                lock (_lockObj)
                {
                    return TransferRequestToFakeSecurityEntitlementModel(userIdentity) ?? new SecurityEntitlementModel();
                }
            }

            return null;
        }

        private static SecurityEntitlementModel TransferRequestToFakeSecurityEntitlementModel(ClaimsIdentity userIdentity)
        {
            Assert.ArgumentNotNull(userIdentity, nameof(userIdentity));

            return _securityModelServerRequester.GetSecuirtyEntitiesByUserId(userIdentity);
        }
    }
}