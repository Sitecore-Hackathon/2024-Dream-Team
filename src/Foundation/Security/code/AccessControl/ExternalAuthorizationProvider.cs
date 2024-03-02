using Sitecore.Data.DataProviders.Sql;
using Sitecore.Security.AccessControl;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DreamTeam.Foundation.Security.Services;
using DreamTeam.Foundation.Extensions;

namespace DreamTeam.Foundation.Security.AccessControl
{
    public class ExternalAuthorizationProvider : SqlAuthorizationProvider
	{
		private static readonly IExternalSecurityConfigurationService _externalSecurityConfigurationService;

		static ExternalAuthorizationProvider()
        {
            _externalSecurityConfigurationService = ServiceLocator.ServiceProvider.GetService<IExternalSecurityConfigurationService>();
		}

		public ExternalAuthorizationProvider(SqlDataApi api) : base(api) 
		{
		}

		/// <summary>
		/// Determines whether the specified operation is allowed.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="accessRight">The requested operation.</param>
		/// <param name="account">The account.</param>
		/// <returns>
		/// 	<c>true</c> if the specified entity is allowed; otherwise, <c>false</c>.
		/// </returns>
		public override AccessResult GetAccess(ISecurable entity, Account account, AccessRight accessRight)
		{
			Assert.ArgumentNotNull(entity, "entity");
			Assert.ArgumentNotNull(account, "account");
			Assert.ArgumentNotNull(accessRight, "accessRight");

			var accessResult = this.GetStateAccess(entity, account, accessRight) ?? this.GetSpecialAccess(entity, account, accessRight);
			if (accessResult != null)
			{
				return accessResult;
			}

			//Donotate Sitecore Security with External Security Provider
			if (ShouldBeValidatedByExternalAuhtorizationProvider(accessResult, entity))
			{
                return accessResult;
			}

			accessResult = this.GetAccessResultFromCache(entity, account, accessRight) ?? this.GetAccessCore(entity, account, accessRight);
			
			this.AddAccessResultToCache(entity, account, accessRight, accessResult, PropagationType.Entity);

			return accessResult;
		}

		private static bool ShouldBeValidatedByExternalAuhtorizationProvider(AccessResult accessResult, ISecurable entity)
		{
			if (!_externalSecurityConfigurationService.IsEntitlementFeatureEnabled())
				return false;

            return accessResult == null && entity is Item item && item.ID != item.TemplateID && item.Database.Name.ToLower() != "core" && !item.IsTemplateItem();
        }
	}
}