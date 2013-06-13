using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement.Records;

namespace Contrib.KeepAlive.Models {
    public class KeepAliveSettingsPartRecord : ContentPartRecord {
        [StringLength(255)]
        public virtual string Url { get; set; }

        public virtual bool Enabled { get; set; }
    }
}