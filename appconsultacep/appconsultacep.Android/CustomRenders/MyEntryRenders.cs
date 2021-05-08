using Android.Content;
using Android.Graphics.Drawables;
using appconsultacep.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(EntryRenderer), typeof(MyEntryRenders))]
namespace appconsultacep.Droid.CustomRenders
{
    public class MyEntryRenders : EntryRenderer
    {
        public MyEntryRenders(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control !=null)
            {

                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(5f);
                gradientDrawable.SetStroke(2, Android.Graphics.Color.White);
                gradientDrawable.SetColor(Color.FromHex("#FFF").ToAndroid());
                Control.SetCursorVisible(false);
                Control.SetBackground(gradientDrawable);
            }
        }
    }
}