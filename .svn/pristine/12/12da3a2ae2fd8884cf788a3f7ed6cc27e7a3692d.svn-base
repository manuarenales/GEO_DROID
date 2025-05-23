using Android.Webkit;
using Microsoft.AspNetCore.Components.WebView.Maui;
 

namespace GEO_DROID.Platforms.Android.Handlers
{
    internal class MyWebChromeClient : WebChromeClient
    {
        public override void OnPermissionRequest(PermissionRequest request)
        {
            // dirty code, fix this yourself
            try
            {
                request.Grant(request.GetResources());
                base.OnPermissionRequest(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public class MauiBlazorWebViewHandler : BlazorWebViewHandler
    {
        protected override global::Android.Webkit.WebView CreatePlatformView()
        {
            var view = base.CreatePlatformView();

            view.SetWebChromeClient(new MyWebChromeClient());

            return view;
        }
    }
}
