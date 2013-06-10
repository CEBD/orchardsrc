using Orchard.UI.Resources;

namespace Simian.Framework {
    public class ResourceManifest :IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {

            var manifest = builder.Add();
            //manifest.DefineStyle("GalleriaImageGallery").SetUrl("GalleriaImageGallery.css");
            //manifest.DefineScript("GalleriaImageGallery").SetUrl("GalleriaImageGallery.js");

            manifest.DefineScript("ImageSlider").SetUrl("jQuery.Slides.js");
            manifest.DefineScript("ImageSliderCaptions").SetUrl("jQuery.Slides.Simian.js");
        }
    }
}