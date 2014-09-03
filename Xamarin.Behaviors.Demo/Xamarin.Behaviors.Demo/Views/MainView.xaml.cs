
using Xamarin.Behaviors.Demo.ViewModels;

namespace Xamarin.Behaviors.Demo.Views
{
	public partial class MainView
	{
		public MainView()
		{
			InitializeComponent();
			this.BindingContext = new MainViewModel();
		}
	}
}
