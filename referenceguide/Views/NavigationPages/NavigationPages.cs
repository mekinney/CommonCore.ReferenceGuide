using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Threading.Tasks;

namespace referenceguide
{
    public class Nav1 : BasePages
    {
        public Nav1()
        {
            this.Title = "Nav1";

#if __IOS__
			this.OverrideBackButton = true;
#endif

            var btn = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Navigate",
                AutomationId = "btn",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PushAsync(new Nav2());

                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btn }
            };
        }
    }
    public class Nav2ViewModel : ObservableViewModel
    {
        public override bool OnBackButtonPressed()
        {
            return true;
        }

        public override void ReleaseResources(string parameter = null)
        {
            var x = 10;
        }
    }
    public class Nav2 : BoundPage<Nav2ViewModel>
    {
        public Nav2()
        {
            this.NeedOverrideSoftBackButton = false;
#if __IOS__
			this.OverrideBackButton = true;
#endif
            this.Title = "Nav2";
            var btn = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Navigate",
                AutomationId = "btn",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PushAsync(new Nav3());
                })
            };

            var btnBack = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Back",
                AutomationId = "btnBack",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PopAsync(true);
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btn, btnBack }
            };
        }
    }
    public class Nav3ViewModel : ObservableViewModel
    {
        public override bool OnBackButtonPressed()
        {
            Navigation.PopTo<Nav1>(true).ContinueOn();
            return false;

        }
        public async override void OnSoftBackButtonPressed()
        {
            await Navigation.PopTo<Nav1>(true);
        }

        public override void ReleaseResources(string parameter = null)
        {
            var x = 10;

        }
    }
    public class Nav3 : BoundPage<Nav3ViewModel>
    {
        public Nav3()
        {
            this.NeedOverrideSoftBackButton = true;
#if __IOS__
			this.OverrideBackButton = true;
#endif
            this.Title = "Nav3";
            var btn = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Navigate",
                AutomationId = "btn",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PushAsync(new Nav4());
                })
            };

            var btnBack = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Back",
                AutomationId = "btnBack",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PopTo<Nav1>(true);
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btn, btnBack }
            };
        }
    }
	public class Animal
	{
		public string Description { get; set; }
	}
    public class Nav4ViewModel : ObservableViewModel
    {

        public string AnimalDescription { get; set; }

        public override bool OnBackButtonPressed()
        {
            Navigation.PopTo<Nav2>(false).ContinueOn();
            return false;

        }
        public override void OnSoftBackButtonPressed()
        {
            Navigation.PopTo<Nav2>(false).ContinueOn();
        }

        public override void LoadResources(string parameter = null)
        {
            Task.Run(async () =>
            {
                var result = await DataBLL.GetFileData<Animal>("animal");
                if(result.Error==null)  
                    AnimalDescription = result.Response?.Description;
            });
        }

        public override void ReleaseResources(string parameter = null)
        {
            Task.Run(async()=>{
                await DataBLL.SaveFileData<Animal>("animal", new Animal() { Description = "Dog" });
            });

        }
    }
    public class Nav4 : BoundPage<Nav4ViewModel>
    {
        public Nav4()
        {
            this.NeedOverrideSoftBackButton = true;
#if __IOS__
            this.OverrideBackButton = true;
#endif

            this.Title = "Nav4";

            var btnBack = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Back",
                AutomationId = "btnBack",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PopTo<Nav2>(true);
                })
            };

            var btnRelease = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Release Resources",
                AutomationId = "btnRelease",
                Command = new Command((obj) =>
                {
                    InjectionManager.ReleaseResources<Nav4ViewModel>();
                })
            };

            var lbl = new Label()
            {
                Margin=5
            };
            lbl.SetBinding(Label.TextProperty, "AnimalDescription");

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btnBack, btnRelease,lbl }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.LoadResources();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VM.ReleaseResources();
        }
    }
}
