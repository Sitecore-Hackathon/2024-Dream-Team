namespace DreamTeam.Foundation.Extensions
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Data.Templates;
    using Sitecore.Diagnostics;
    using Sitecore.Links.UrlBuilders;
    using Sitecore.Resources.Media;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public static class ItemExtensions
    {
        public static Item FirstChildInheritingFrom(this Item item, ID templateId)
        {
            return item?.Children.FirstOrDefault(i => i.InheritsFrom(templateId));
        }

        public static bool InheritsFrom(this Item item, ID[] templateIds)
        {
            return templateIds.Any(tid => item.Template.DoesTemplateInheritFrom(tid));
        }

        public static bool InheritsFrom(this Item item, ID templateId)
        {
            return item.Template.DoesTemplateInheritFrom(templateId);
        }

        public static bool IsTemplateItem(this Item item)
        {
            return item.TemplateID == TemplateIDs.Template || (item.Name.Equals("__Standard Values", StringComparison.InvariantCultureIgnoreCase) && item.Paths.Path.StartsWith("/sitecore/templates"));
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            if (item == null)
            {
                return false;
            }

            return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
        }

        private static bool IsDerived(this Item item, Item templateItem)
        {
            if (item == null)
            {
                return false;
            }

            if (templateItem == null)
            {
                return false;
            }

            var itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
        }

        public static bool DoesTemplateInheritFrom(this TemplateItem template, ID templateId)
        {
            return template != null && !templateId.IsNull && (template.ID == templateId || template.DoesTemplateInheritFrom(TemplateManager.GetTemplate(templateId, template.Database)));
        }

        private static bool DoesTemplateInheritFrom(this TemplateItem template, Template baseTemplate)
        {
            if (baseTemplate == null)
                return false;

            return TemplateManager.GetTemplate(template.ID, template.Database).DescendsFromOrEquals(baseTemplate.ID);
        }
    }
}