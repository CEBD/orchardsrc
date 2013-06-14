using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Contrib.Taxonomies.Models;
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


            var taxonomyList = new List<string> {
                "Remodel",
                "Construction",
                "Repair"
            };

            CreateTaxonomy("JobType", taxonomyList);
            //GenerateQueries("Job", taxonomyList);

            return 1;
        }

        private void CreateTaxonomy(string nameOfTaxonomy, List<string> terms)
        {
            if (_taxonomyService.GetTaxonomyByName(nameOfTaxonomy) == null)
            {
                var taxonomy = _contentManager.New<TaxonomyPart>("Taxonomy");
                taxonomy.Name = nameOfTaxonomy;
                _contentManager.Create(taxonomy, VersionOptions.Published);
                terms.ForEach(t => CreateTerm(taxonomy, t));
            }
        }

        private void CreateTerm(TaxonomyPart taxonomyPart, string name)
        {
            TermPart term = _taxonomyService.NewTerm(taxonomyPart);
            term.Name = name;
            _contentManager.Create(term, VersionOptions.Published);
        }
        private void GenerateQueries(string contentType, List<string> taxonomies)
        {
            taxonomies.ForEach(t => CreateTaxonomyQuery(t, contentType));
        }

        private void CreateTaxonomyQuery(string taxonomy, string contentType)
        {

            var query = _queryService.CreateQuery(taxonomy);

            var contentTypeFilter = GenerateXmlFormState(new ContentTypeForm { ContentTypes = contentType });
            var tax = _taxonomyService.GetTaxonomyByName(taxonomy);

            var taxName = tax.Id.ToString();
            var taxonomyFilter = GenerateXmlFormState(new TaxonomyForm
            {
                TermIds = taxName
            });

            query.FilterGroups[0].Filters.Add(new FilterRecord
            {
                Category = "Content",
                Description = contentTypeFilter,
                Position = 0,
                State = contentTypeFilter,
                Type = "ContentTypes"
            });

            query.FilterGroups[0].Filters.Add(new FilterRecord
            {
                Category = "Taxonomy",
                Description = taxonomyFilter,
                Position = 1,
                State = taxonomyFilter,
                Type = "HasTerms"
            });

            _contentManager.Publish(query.ContentItem);

        }

        private string GenerateXmlFormState(ContentTypeForm form)
        {
            var xmlSerializer = new XmlSerializer(form.GetType());
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xmlSerializer.Serialize(writer, form);
            var state = sww.ToString();
            return state;
        }

        private string GenerateXmlFormState(TaxonomyForm form)
        {
            var xmlSerializer = new XmlSerializer(form.GetType());
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xmlSerializer.Serialize(writer, form);
            var state = sww.ToString();
            return state;
        }

        [Serializable]
        public class ContentTypeForm
        {
            public string Description { get; set; }
            public string ContentTypes { get; set; }
        }

        [Serializable]
        public class TaxonomyForm
        {
            public string Description { get; set; }
            public string TermIds { get; set; }
        }

    }
}