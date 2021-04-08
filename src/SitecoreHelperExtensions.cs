using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Mvc.Helpers;
using Sitecore.Resources.Media;
using System.Web;

namespace SharedSitecore.Foundation.FieldRendering
{
    public static class SitecoreHelperExtensions
    {
        private const string SitecoreFormMode = "sc_formmode";
        public static HtmlString Field(this SitecoreHelper helper, ID fieldID) => helper.Field(fieldID.ToString());
        public static HtmlString Field(this SitecoreHelper helper, ID fieldID, object parameters) => helper.Field(fieldID.ToString(), parameters);
        public static HtmlString Field(this SitecoreHelper helper, ID fieldID, Item item) => helper.Field(fieldID.ToString(), item);
        public static bool IsExperienceFormsEditMode(this SitecoreHelper helper) => Sitecore.Context.Request.QueryString[SitecoreFormMode] != null;
        public static string ItemUrl(this SitecoreHelper helper, Item item) => LinkManager.GetItemUrl(item);
        public static string MediaUrl(this SitecoreHelper helper, ID fieldId) => MediaUrl(helper, fieldId, helper.CurrentItem);
        public static string MediaUrl(this SitecoreHelper helper, ID fieldId, MediaUrlBuilderOptions options) => MediaUrl(helper, fieldId, helper.CurrentItem, options);
        public static string MediaUrl(this SitecoreHelper helper, ID fieldId, Item item) => MediaUrl(helper, fieldId, item, null);
        public static string MediaUrl(this SitecoreHelper helper, ID fieldId, Item item, MediaUrlBuilderOptions options)
        {
            ImageField imageField = item?.Fields[fieldId];
            if (imageField == null || imageField.MediaItem == null)
            {
                return string.Empty;
            }
            var url = options != null ? MediaManager.GetMediaUrl(imageField.MediaItem, options) : MediaManager.GetMediaUrl(imageField.MediaItem);
            return HashingUtils.ProtectAssetUrl(url);
        }
    }
}