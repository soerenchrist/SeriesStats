using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeriesStats.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyNavigationPage
    {
        public MyNavigationPage(Page page) : base(page)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; }

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}