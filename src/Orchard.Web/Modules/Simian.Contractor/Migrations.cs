using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Contrib.Taxonomies.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Core.Navigation.Services;
using Orchard.Data.Migration;
using Orchard.Projections.Models;
using Orchard.Projections.Services;

using Orchard.Widgets.Services;

namespace Simian.Contractor
{

    public class Migrations : DataMigrationImpl
    {
        private readonly IMenuService _menuService;
        private readonly IProjectionManager _projectionManager;
        private readonly IContentManager _contentManager;
        private readonly IQueryService _queryService;
        private readonly IWidgetsService _widgetsService;
        private readonly ITaxonomyService _taxonomyService;


        public Migrations(IMenuService menuService,
                          IContentManager contentManager,
                          IQueryService queryService,
                          IWidgetsService widgetsService,
                          ITaxonomyService taxonomyService,
                            IProjectionManager projectionManager) {

            _menuService = menuService;
            _contentManager = contentManager;
            _queryService = queryService;
            _widgetsService = widgetsService;
            _taxonomyService = taxonomyService;
            _projectionManager = projectionManager;
        }

        public int Create()
        {
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