using Sitecore.Data;

namespace DreamTeam.Foundation.Security
{
    public static class Templates
    {
        public struct _ExternalId
        {
            public static readonly ID ID = new ID("{8B311DB1-DE95-42BE-BDEA-F71F3CAEA3D5}");

            public struct Fields
            {
                public static readonly ID Urn = new ID("{D0A309DA-6032-44E9-8331-B234CFEBB870}");
            }
        }

        public struct _EncourageByEntitlements
        {
            public static readonly ID ID = new ID("{30D1BE63-E408-41F7-8B5E-3E0700B791DA}");
        }
    }
}