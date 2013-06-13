using System;
using System.Net;
using System.Web;
using Contrib.KeepAlive.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Logging;
using Orchard.Settings;
using Orchard.Tasks;

namespace Contrib.KeepAlive.Services {
    public class KeepAliveExecutor : IBackgroundTask {
        private readonly IOrchardServices _orchardServices;

        public KeepAliveExecutor(IOrchardServices orchardServices) {
            _orchardServices = orchardServices;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Sweep() {
            var part = _orchardServices.WorkContext.CurrentSite.As<KeepAliveSettingsPart>();
            if(!part.Record.Enabled) {
                return;
            }

            var url = VirtualPathUtility.AppendTrailingSlash(part.Record.Url) + "keepalive";

            try {
                if(!url.StartsWith("http", StringComparison.OrdinalIgnoreCase)) {
                    Logger.Error("Keep Alive Url must be absolute (e.g. http://) {0}", url);
                    return;
                }

                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request != null) {
                    using (request.GetResponse() as HttpWebResponse) {}
                }
            }
            catch(Exception e) {
                Logger.Error(e, "Could not ping keep alive service at url [{0}]", url);
            }
        }
    }
}
