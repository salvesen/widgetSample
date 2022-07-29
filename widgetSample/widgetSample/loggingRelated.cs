using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace widgetSample
{
    public class loggingRelated
    {
        public static void writeALineToLog(string line)
        {
            string oldLog = Preferences.Get("log", " ");
            Preferences.Set("log", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + line + System.Environment.NewLine + oldLog);
        }
    }
}
