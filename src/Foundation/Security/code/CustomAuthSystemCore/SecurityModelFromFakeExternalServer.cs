namespace DreamTeam.Foundation.Security.CustomAuthSystemCore
{
    using DreamTeam.Foundation.Security.Entities;
    using DreamTeam.Foundation.Security.Model;
    using Sitecore.Diagnostics;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class SecurityModelFromFakeExternalServer : ISecurityModelFromExternalServer
    {
        /// <summary>
        /// This method implement ISecurityModelFromExternalServer iterface.
        /// This method call external auth system, get response and convert security model to 'SecurityEntitlementModel' class, magically :)
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public SecurityEntitlementModel GetSecuirtyEntitiesByUserId(ClaimsIdentity userIdentity)
        {
            //TODO: Request to external system
            //TODO: Getting response from external system

            try
            {
                //TODO: Convert response object to 'SecurityEntitlementModel' class

                //Return fake data
                return new SecurityEntitlementModel() 
                { 
                    Entities = new List<EntityModel>
                    {
                        new EntityModel() { Urn = "Product 1", IsAllowed = false },
                        new EntityModel() { Urn = "Product 2", IsAllowed = true },
                        new EntityModel() { Urn = "Product 3", IsAllowed = true },
                    }
                };
            }
            catch
            {
                Log.Error($"Exception over fetching data from External System", this);
            }

            return null;
        }
    }
}