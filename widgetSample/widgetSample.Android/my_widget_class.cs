using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace widgetSample.Droid
{
    [BroadcastReceiver(Label = "Widget Button Click")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [IntentFilter(new string[] { "com.sTech.teslafobwidget.ACTION_WIDGET_TURNON" })]
    [IntentFilter(new string[] { "com.sTech.teslafobwidget.ACTION_WIDGET_TURNOFF" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/my_widget_provider")]
    public class my_widget_class : AppWidgetProvider
    {
        public static String ACTION_WIDGET_TURNOFF = "TurnOff";
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(my_widget_class)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }

        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.my_widget);

            //SetTextViewText(widgetView);

            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            var intent = new Intent(context, type: typeof(my_widget_class));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            intent.PutExtra(name: AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            widgetView.SetOnClickPendingIntent(Resource.Id.buttonOpenCar, GetPendingSelfIntent(context, ACTION_WIDGET_TURNOFF));
        }

        private PendingIntent GetPendingSelfIntent(Context context, string action)
        {
            var intent = new Intent(context, type: typeof(my_widget_class));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, requestCode: 0, intent, flags: 0);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            if (ACTION_WIDGET_TURNOFF.Equals(intent.Action))
            {
                try
                {
                    loggingRelated.writeALineToLog("Clicked widget: front ");
                    DateTime aDateTime = new DateTime(2021, 12, 23);
                    var euroNok = postingToInterface.getEuroToNokPrice(aDateTime);
                    loggingRelated.writeALineToLog("Clicked widget: " + euroNok);
                }
                catch(Exception e)
                {
                    loggingRelated.writeALineToLog("catched widget: " + e);
                }
            }
        }
    }
}