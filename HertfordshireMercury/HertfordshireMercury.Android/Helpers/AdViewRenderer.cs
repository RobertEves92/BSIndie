using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using BSIndie.Droid.Helpers;
using Android.Gms.Ads;
using BSIndie.Controls;

[assembly: ExportRenderer(typeof(AdControlView),typeof(AdViewRenderer))]
namespace BSIndie.Droid.Helpers
{
    public class AdViewRenderer : ViewRenderer<AdControlView, AdView>
    {
        string adUnitId = "ca-app-pub-4100821384102775/1679854589";
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;

        AdView CreateAdView()
        {
            if (adView != null)
                return adView;

            adView = new AdView(Forms.Context);

            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;
            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest.Builder().Build());

            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {
                CreateAdView();
                SetNativeControl(adView);
            }
        }
    }
}