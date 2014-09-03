using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Behaviors.Demo.Views;
using Xamarin.Forms;

namespace Xamarin.Behaviors.Demo
{
	public class App
	{
		public static Page GetMainPage()
		{
			return new MainView();
		}
	}
}
