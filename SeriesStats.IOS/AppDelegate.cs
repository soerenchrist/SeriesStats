using Foundation;
using SegmentedControl.FormsPlugin.iOS;
using Sharpnado.Presentation.Forms.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace SeriesStats.IOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Rg.Plugins.Popup.Popup.Init();
            Xamarin.Forms.Forms.Init();
            SharpnadoInitializer.Initialize();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            LoadApplication(new App());
            Plugin.InputKit.Platforms.iOS.Config.Init();
            SegmentedControlRenderer.Init();

            return base.FinishedLaunching(application, launchOptions);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (Xamarin.Essentials.Platform.OpenUrl(app, url, options))
                return true;

            return base.OpenUrl(app, url, options);
        }
    }
}