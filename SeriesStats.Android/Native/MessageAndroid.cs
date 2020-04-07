using Android.App;
using Android.Widget;
using SeriesStats.Droid.Native;
using SeriesStats.Util;
using SeriesStats.Util.Abstractions;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace SeriesStats.Droid.Native
{
    public class MessageAndroid : IMessages
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}