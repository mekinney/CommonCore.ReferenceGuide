using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class WebViewEffect : BasePages
    {
        public WebViewEffect()
        {
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body>
  <h1>Xamarin.Forms</h1>
  <p>Prosciutto biltong tenderloin shankle salami t-bone pig pork belly corned beef. Meatloaf pig boudin t-bone, bacon pastrami kevin filet mignon biltong shank turducken corned beef beef ribs prosciutto. Ribeye landjaeger shank beef sirloin bresaola fatback. Corned beef chuck tongue porchetta salami pork belly tail pig meatball</p>
<p>Morbi sizzle. Dawg potenti. Its fo rizzle fo. Gangster elizzle shizzlin dizzle, ullamcorpizzle quis, ullamcorpizzle phat, scelerisque brizzle, fo shizzle my nizzle. Crunk fo shizzle dang. Break yo neck, yall felizzle.</p>
  </body></html>";

            var webView = new WebView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Source = htmlSource
            };
#if __IOS__
            webView.Effects.Add(new DisableWebViewScrollEffect());
#endif

            Content = new StackLayout()
            {
                Children = { webView }
            };

        }
    }
}
