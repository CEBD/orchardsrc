using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simian.Construction.Handlers
{
    public class JobDescriptionEventHandler : IFeatureEventHandler
    {

        private readonly ITaxonomyService _taxonomyService;
        private readonly IContentManager _contentManager;

        public JobDescriptionEventHandler(ITaxonomyService taxonomyService, IContentManager contentManager)
        {
            _taxonomyService = taxonomyService;
            _contentManager = contentManager;
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
            CreateTaxonomy("JobType", newList<string> {
                    "New Home Construction",
                    "New Commercial Construction",
                    "Addition",
                    "Home Remodel",
                    "Bathroom Remodel",
                    "Interior Remodel",
                    "Kitchen Remodel",
                    "Exterior Remodel"                
                });
        
            CreateTaxonomy("PropertyFeatures", new List<string> {
                    "New Sink",
                    "New Ceiling",
                    "New Pool",
                    "New Cabinets",
                    "New Hardwood Floors",
                    "New Reclaimed Wood Exposed Beams",
                    "New Shower",
                    "New Bathtub"
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