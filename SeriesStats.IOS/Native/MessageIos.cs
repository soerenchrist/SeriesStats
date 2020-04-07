using Foundation;
using SeriesStats.IOS.Native;
using SeriesStats.Util;
using SeriesStats.Util.Abstractions;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIos))]
namespace SeriesStats.IOS.Native
{
    public class MessageIos : IMessages
    {
        private const double _longDelay = 3.5;
        private const double _shortDelay = 1;

        public void LongAlert(string message)
        {
            ShowAlert(message, _longDelay);
        }

        public void ShortAlert(string message)
        {
            ShowAlert(message, _shortDelay);
        }

        private void ShowAlert(string message, double seconds)
        {
            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) => { DismissMessage(alert, obj); });
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage(UIAlertController alert, NSTimer alertDelay)
        {
            alert?.DismissViewController(true, null);
            alertDelay?.Dispose();
        }

    }
}