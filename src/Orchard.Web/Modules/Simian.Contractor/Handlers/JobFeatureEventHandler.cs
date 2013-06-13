using System.Collections.Generic;
using Contrib.Taxonomies.Models;
using Contrib.Taxonomies.Services;
using Orchard.ContentManagement;
using Orchard.Environment;
using Orchard.Environment.Extensions.Models;

namespace Simian.Contractor.Handlers {
    public class JobFeatureEventhandler : IFeatureEventHandler
    {


        private readonly ITaxonomyService _taxonomyService;
        private readonly IContentManager _contentManager;

        public JobFeatureEventhandler(ITaxonomyService taxonomyService, IContentManager contentManager)
        {
            _taxonomyService = taxonomyService;
            _contentManager = contentManager;
        }


        public void Installing(Feature feature) {
         
        }

        public void Installed(Feature feature) {
        
        }

        public void Enabling(Feature feature) {
         
        }

        public void Enabled(Feature feature) {
            CreateTaxonomy("JobType", new List<string> {
                "Remodel",
                "Construction",
                "Repair"
            });
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
            var term = _taxonomyService.NewTerm(taxonomyPart);
            term.Name = name;
            _contentManager.Create(term, VersionOptions.Published);
        }


        public void Disabling(Feature feature) {
          
        }

        public void Disabled(Feature feature) {
     
        }

        public void Uninstalling(Feature feature) {
  
        }

        public void Uninstalled(Feature feature) {
           
        }
    }
}