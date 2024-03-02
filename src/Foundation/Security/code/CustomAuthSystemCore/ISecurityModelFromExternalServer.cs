namespace DreamTeam.Foundation.Security.CustomAuthSystemCore
{
    using DreamTeam.Foundation.Security.Model;
    using System.Security.Claims;

    public interface ISecurityModelFromExternalServer
    {
        SecurityEntitlementModel GetSecuirtyEntitiesByUserId(ClaimsIdentity userIdentity);
    }
}