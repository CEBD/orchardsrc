using System.Collections.Generic;
using Orchard.Environment.Configuration;
using Orchard.Localization;
using Orchard.UI.Admin.Notification;
using Orchard.UI.Notify;

namespace Contrib.KeepAlive.Services {
    public class DefaultShellBanner: INotificationProvider {
        private readonly ShellSettings _shellSettings;

        public DefaultShellBanner(ShellSettings shellSettings) {
            _shellSettings = shellSettings;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<NotifyEntry> GetNotifications() {
            if ( _shellSettings.Name != "Default") {
                yield return new NotifyEntry { Message = T("The Keep Alive feature should only be enabled on the default tenant. Contact your Orchard administrator for more information." ), Type = NotifyType.Warning };
            }
        }
    }
}
