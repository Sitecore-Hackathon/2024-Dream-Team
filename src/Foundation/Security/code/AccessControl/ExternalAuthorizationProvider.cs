using Sitecore.Data.DataProviders.Sql;
using Sitecore.Security.AccessControl;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;

namespace DreamTeam.Foundation.Security.AccessControl
{
    public class ExternalAuthorizationProvider : SqlAuthorizationProvider
	{
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

			accessResult = this.GetAccessResultFromCache(entity, account, accessRight) ?? this.GetAccessCore(entity, account, accessRight);
			
			this.AddAccessResultToCache(entity, account, accessRight, accessResult, PropagationType.Entity);

			return accessResult;
		}
	}
}