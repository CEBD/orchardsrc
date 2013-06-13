using Orchard.Data.Migration;

namespace Contrib.KeepAlive {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable("KeepAliveSettingsPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("Url", column => column.WithLength(255))
                    .Column<bool>("Enabled", column => column.NotNull().WithDefault(true))
                );

            return 1;
        }
    }
}
