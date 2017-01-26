using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net;

namespace SemesterSoda101
{
   public interface ISQLite
    {
        SQLiteConnection getConnection();
    }
}
