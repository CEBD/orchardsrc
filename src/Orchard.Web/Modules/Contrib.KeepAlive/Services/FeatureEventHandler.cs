using System.Web;
using Contrib.KeepAlive.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Utility.Extensions;
using Orchard.UI.Notify;

namespace Contrib.KeepAlive.Services {
    public class FeatureEventHandler : IFeatureEventHandler {
        private readonly IOrchardServices _orchardServices;

        public FeatureEventHandler(IOrchardServices orchardServices) {
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Installing(Feature feature) {
        }

        public void Installed(Feature feature) { }
        public void Enabling(Feature feature) { }
        public void Enabled(Feature feature) {
            _orchardServices.WorkContext.CurrentSite.As<KeepAliveSettingsPart>().Record.Enabled = true;
            _orchardServices.WorkContext.CurrentSite.As<KeepAliveSettingsPart>().Record.Url = _orchardServices.WorkContext.HttpContext.Request.ToApplicationRootUrlString();
        }

        public void Disabling(Feature feature) { }
        public void Disabled(Feature feature) { }
        public void Uninstalling(Feature feature) { }
        public void Uninstalled(Feature feature) { }
    }
}