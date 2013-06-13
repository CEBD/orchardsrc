using Contrib.KeepAlive.Models;
using JetBrains.Annotations;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;

namespace Contrib.KeepAlive.Handlers {
    [UsedImplicitly]
    public class KeepAliveSettingsPartHandler : ContentHandler {
        public KeepAliveSettingsPartHandler(IRepository<KeepAliveSettingsPartRecord> repository) {
            Filters.Add(new ActivatingFilter<KeepAliveSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
        }
    }
}