
using Xamarin.Behaviors.Demo.ViewModels;
using Xamarin.Forms;

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
