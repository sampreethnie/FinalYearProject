using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace FinalYearProject
{
    public sealed class SQLHelper
    {
        public string ConnString()
        {
            string connstr = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            return connstr;
        }
    }
}