using System;
using System.Web;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace SquawkerWebApplication
{
    public static class Settings
    {
        public static string GetConnectionString()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionStringSettings == null)
            {
                throw new ConfigurationException("ConnectionStringSettings was null.");
            }
            return connectionStringSettings.ConnectionString;
        }
    }
}