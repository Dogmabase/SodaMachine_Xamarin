using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SQLite.Net;
using System.Text;
using Xamarin.Forms;
using SemesterSoda101.Droid;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly:Dependency(typeof(SQLite_Droid))]
namespace SemesterSoda101.Droid
{
    class SQLite_Droid : ISQLite
    {
        public SQLite_Droid()        {        }


        #region Implement getConnection() from ISQLite

        public SQLiteConnection getConnection()
        {
            var fileName = "SodasFile.db3";
            var docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);

            if (!File.Exists(path)) {
                File.Create(path);
            }

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);
            return connection;

        }


        #endregion


    }
}