using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Simian.Construction
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {


            ContentDefinitionManager.AlterPartDefinition("JobPart", part =>
                                                                    part
                                                                            .Attachable()
                                                                             .WithField("Subtitle", f => f.OfType("TextField")
                                                                                                          .WithDisplayName("Subtitle")
                                                                             )
                                                                             .WithField("PropertyType", f =>
                                                                                                        f.OfType("TaxonomyField")
                                                                                                         .WithDisplayName("PropertyType")
                                                                                                         .WithSetting("TaxonomyFieldSettings.Taxonomy", "PropertyType")
                                                                                                         .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                                                                                                         .WithSetting("TaxonomyFieldSettings.SingleChoice", "True")
                                                                                                         .WithSetting("TaxonomyFieldSettings.Hint", "Please select what kind of property you are posting.")
                                                                             )
                                                                             .WithField("PropertyFeatures", f =>
                                                                                                      f.OfType("TaxonomyField")
                                                                                                       .WithDisplayName("Ammenities")
                                                                                                       .WithSetting("TaxonomyFieldSettings.Taxonomy", "PropertyFeatures")
                                                                                                       .WithSetting("TaxonomyFieldSettings.LeavesOnly", "False")
                                                                                                       .WithSetting("TaxonomyFieldSettings.SingleChoice", "False")
                                                                                                       .WithSetting("TaxonomyFieldSettings.Hint", "Please select the property features added to this property")
                                                                             )
                                                                             .WithField("Bedrooms", f =>
                                                                                                    f.OfType("TextField")
                                                                                                     .WithDisplayName("Bedrooms")
                                                                             )
                                                                             .WithField("Bathrooms", f => f.OfType("TextField")
                                                                                                           .WithDisplayName("Bathrooms")
                                                                             )
                                                                             .WithField("HouseSqFeet", f => f.OfType("TextField")
                                                                                                             .WithDisplayName("Sq. Feet")
                                                                             )
                                                                             .WithField("LotSize", f => f.OfType("TextField")
                                                                                                         .WithDisplayName("Lot Size")
                                                                             )
                                                                             .WithField("YearBuilt", f => f.OfType("TextField")
                                                                                                           .WithDisplayName("Year Built")
                                                                             )
                                                                             .WithField("DateRemodeled", f => f.OfType("TextField")
                                                                                                            .WithDisplayName("DateRemodeled")
                                                                             
                                                                             )
                                                                             .WithField("FloorPlanImageGallery", f =>
                                                                                                                 f.OfType("ImageMultiPickerField")
                                                                                                                  .WithDisplayName("Property Floor Plan Image Gallery")
                                                                                                                  .WithSetting("ImageMultiPickerFieldSettings.CustomFields", "[{name: 'title', displayName: 'Title', type:'text'}]")
                                                                             )
                );

            ContentDefinitionManager.AlterTypeDefinition("Job", builder =>
                                                                     builder
                                                                         .WithPart("CommonPart")
                                                                         .WithPart("TitlePart")
                                                                         .WithPart("BodyPart")                                                                         
                                                                         .WithPart("PublishLaterPart")
                                                                         .WithPart("GalleriaPart")
                                                                         .WithPart("JobPart")
                                                                         .WithPart("AutoroutePart", partBuilder =>
                                                                                                    partBuilder
                                                                                                        .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                                                                                                        .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                                                                                                        .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name: 'Property Title', Pattern: 'properties/{Content.Slug}', Description: 'properties/property-title'}]")
                                                                                                        .WithSetting("AutorouteSettings.DefaultPatternIndex", "0")
                                                                         )
                                                                         .Creatable()
                                                                         .Draftable()
                );

            return 1;
        }





    }
}