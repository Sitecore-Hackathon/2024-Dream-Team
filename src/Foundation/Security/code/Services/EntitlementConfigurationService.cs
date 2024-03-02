namespace DreamTeam.Foundation.Security.Services
{
    using Sitecore.Configuration;

    public class EntitlementConfigurationService : IEntitlementConfigurationService
    {
        private readonly bool _EntitlementFeatureEnabled = Settings.GetBoolSetting("EntitlementFeatureEnabled", false);

        public bool IsEntitlementFeatureEnabled()
        {
            return _EntitlementFeatureEnabled;
        }
    }
}