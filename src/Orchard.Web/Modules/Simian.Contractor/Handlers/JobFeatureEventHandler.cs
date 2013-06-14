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


            //var taxonomyList = new List<string> {
            //    "Remodel",
            //    "Construction",
            //    "Repair"
            //};

            
            
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