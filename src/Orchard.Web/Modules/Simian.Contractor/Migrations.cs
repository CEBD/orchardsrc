using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Simian.Contractor {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("JobPart", part =>
                                                                    part.Attachable()
                                                                        .WithField("JobType", f =>
                                                                                              f.OfType("TaxonomyField")
                                                                                               .WithDisplayName("JobType")
                                                                                               .WithSetting("TaxonomyFieldSettings.Taxonomy", "JobType")
                                                                                               .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                                                                                               .WithSetting("TaxonomyFieldSettings.SingleChoice", "True")
                                                                                               .WithSetting("TaxonomyFieldSettings.Hint", "Please select what kind of job you are annotating.")
                                                                        )
                );


            ContentDefinitionManager.AlterTypeDefinition("Job", type => type
                                                                            .Creatable()
                                                                            .Draftable()
                                                                            .WithPart("CommonPart")
                                                                            .WithPart("TitlePart")
                                                                            .WithPart("BodyPart")
                                                                            .WithPart("PublishLaterPart")
                                                                            .WithPart("GalleriaPart")
                                                                            .WithPart("JobPart")
                                                                            .WithPart("AutoroutePart", settings =>
                                                                                                       settings
                                                                                                           .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                                                                                                           .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                                                                                                           .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name: 'Job Title', Pattern: 'jobs/{Content.Slug}', Description: 'jobs/job-title'}]")
                                                                                                           .WithSetting("AutorouteSettings.DefaultPatternIndex", "0")
                                                                            ));


            return 1;
        }



    }
}