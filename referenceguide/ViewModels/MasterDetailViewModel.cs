using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class MasterDetailViewModel : ObservableViewModel
	{
		private Dictionary<string, NavigationPage> navPages { get; set; } = new Dictionary<string, NavigationPage>();
	
		public bool IsPresented{ get; set; }
		public ObservableCollection<SlidingPageItem> MasterPageItems{ get; set; }

		public ICommand NavClicked { get; set; }

		public MasterDetailViewModel()
		{
			SetNavigation();
			NavClicked = new RelayCommand((obj) =>
			{
				var item = (SlidingPageItem)obj;
				var page = (MasterDetailPage)Application.Current.MainPage;

				if (!navPages.ContainsKey(item.TargetType.Name))
				{
					var np = new NavigationPage((Page)Activator.CreateInstance(item.TargetType))
					{
						BarBackgroundColor = Color.FromHex("#b85921"),
						BarTextColor = Color.White
					};
                    AppSettings.AppNav = np.Navigation;
					navPages.Add(item.TargetType.Name, np);
				}
				page.Detail = navPages[item.TargetType.Name];

				page.IsPresented = false;
			});
		}

		private void SetNavigation()
		{
			var lst = new List<SlidingPageItem>();
			lst.Add(new SlidingPageItem
			{
				Title = "Animations",
				IconSource = "index24.png",
				TargetType = typeof(AnimationsMain)
			});
			lst.Add(new SlidingPageItem
			{
				Title = "Behaviors",
				IconSource = "index24.png",
				TargetType = typeof(BehaviorsMain)
			});
			lst.Add(new SlidingPageItem
			{
				Title = "Dependencies",
				IconSource = "index24.png",
				TargetType = typeof(DependeciesMain)
			});
			lst.Add(new SlidingPageItem
			{
				Title = "Effects",
				IconSource = "index24.png",
				TargetType = typeof(EffectsMain)
			});
			lst.Add(new SlidingPageItem
			{
				Title = "Services",
				IconSource = "index24.png",
				TargetType = typeof(ServicesMain)
			});
			lst.Add(new SlidingPageItem
			{
				Title = "Controls",
				IconSource = "index24.png",
				TargetType = typeof(ControlsMain)
			});
			lst.Add(new SlidingPageItem
			{
				Title = "Authentication",
				IconSource = "index24.png",
				TargetType = typeof(AuthenticationMain)
			});
            lst.Add(new SlidingPageItem
            {
                Title = "Multiple ViewModels",
                IconSource = "index24.png",
                TargetType = typeof(BindingViewMain)
            });

			MasterPageItems = lst.ToObservable();
		}

		public override void OnViewMessageReceived(string key, object obj)
		{
			if (key == CoreSettings.MasterDetailIsPresented)
			{
				IsPresented = !IsPresented;
			}
		}
	}
}
