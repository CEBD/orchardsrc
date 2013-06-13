using System;
using System.Net;
using System.Web;
using Contrib.KeepAlive.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;

namespace Contrib.KeepAlive.Drivers {
    public class KeepAliveSettingsPartDriver : ContentPartDriver<KeepAliveSettingsPart> {
        public KeepAliveSettingsPartDriver() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix { get { return "KeepAliveSettings"; } }

        protected override DriverResult Editor(KeepAliveSettingsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_KeepAlive_SiteSettings",
                               () => shapeHelper.EditorTemplate(TemplateName: "Parts.KeepAlive.SiteSettings", Model: part.Record, Prefix: Prefix));
        }

        protected override DriverResult Editor(KeepAliveSettingsPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part.Record, Prefix, null, null);

            // ensure the url is well formed
            if(part.Record.Enabled && !part.Record.Url.StartsWith("http", StringComparison.OrdinalIgnoreCase)) {
                updater.AddModelError("Url", T("The Keep Alive url must begin with http://"));
            }
            else {
                // try a request
                try {
                    var url = VirtualPathUtility.AppendTrailingSlash(part.Record.Url) + "keepalive"; 
                    
                    var request = WebRequest.Create(url) as HttpWebRequest;
                    if (request != null) {
                        using (request.GetResponse() as HttpWebResponse) { }
                    }
                }
                catch (Exception e) {
                    updater.AddModelError("Url", T("There was an error when querying the Keep Alive url: {0}", e.Message));
                }
            }

            return Editor(part, shapeHelper);
        }
    }
}