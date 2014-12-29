
namespace Xamarin.Behaviors.Demo.WinPhone
{
	using Forms.Platform.WinPhone;

	public partial class MainPage : FormsApplicationPage
	{
		public MainPage()
		{
			InitializeComponent();

			Xamarin.Forms.Forms.Init();
			LoadApplication(new Xamarin.Behaviors.Demo.App());
		}
	}
}
