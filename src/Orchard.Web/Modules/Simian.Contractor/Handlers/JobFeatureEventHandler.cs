using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Contrib.Taxonomies.Models;
using Contrib.Taxonomies.Services;
using Orchard.ContentManagement;
using Orchard.Core.Navigation.Services;
using Orchard.Environment;
using Orchard.Environment.Extensions.Models;
using Orchard.Projections.Models;
using Orchard.Projections.Services;
using Orchard.Widgets.Services;

namespace Simian.Contractor.Handlers
{
    public class JobFeatureEventhandler : IFeatureEventHandler
    {


        private readonly IMenuService _menuService;
        private readonly IProjectionManager _projectionManager;
        private readonly IContentManager _contentManager;
        private readonly IQueryService _queryService;
        private readonly IWidgetsService _widgetsService;
        private readonly ITaxonomyService _taxonomyService;

        public JobFeatureEventhandler(IMenuService menuService,
                          IContentManager contentManager,
                          IQueryService queryService,
                          IWidgetsService widgetsService,
                          ITaxonomyService taxonomyService,
                            IProjectionManager projectionManager)
        {
            _menuService = menuService;
            _contentManager = contentManager;
            _queryService = queryService;
            _widgetsService = widgetsService;
            _taxonomyService = taxonomyService;
            _projectionManager = projectionManager;
        }


        public void Installing(Feature feature)
        {

        }

        public void Installed(Feature feature)
        {

        }

        public void Enabling(Feature feature)
        {

        }

        public void Enabled(Feature feature)
        {


            var taxonomyList = new List<string> {
                "Remodel",
                "Construction",
                "Repair"
            };
            CreateTaxonomy("JobType", taxonomyList);
            GenerateQueries("Job", taxonomyList);
        }


        private void GenerateQueries(string contentType, List<string> taxonomies)
        {
            taxonomies.ForEach(t => _contentManager.Publish(CreateTaxonomyQuery(t, contentType).ContentItem));
        }

        private QueryPart CreateTaxonomyQuery(string taxonomy, string contentType)
        {

            if (_queryService.GetQuery(_taxonomyService.GetTaxonomyByName(taxonomy).Id) == null)
            {

                var query = _queryService.CreateQuery(taxonomy);

                var contentTypeFilter = GenerateXmlFormState(new ContentTypeForm { ContentTypes = contentType });
                var taxName = _taxonomyService.GetTaxonomyByName(taxonomy).Id.ToString();
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
            }
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


        public void Disabling(Feature feature)
        {

        }

        public void Disabled(Feature feature)
        {

        }

        public void Uninstalling(Feature feature)
        {

        }

        public void Uninstalled(Feature feature)
        {

        }
    }
}