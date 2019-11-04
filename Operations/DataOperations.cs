using System;
using System.Data;
using System.Data.SqlClient;
using BaseConnectionLibrary.ConnectionClasses;

namespace Operations
{
    public class DataOperations : SqlServerConnection
    {

        /// <summary>
        /// Setup the connection string
        /// </summary>
        public DataOperations()
        {

            DatabaseServer = "KARENS-PC";
            DefaultCatalog = "BindingSourceFiltering";

            if (Environment.UserName != "Karens" & DatabaseServer == "KARENS-PC")
            {
                throw new Exception("You need to change DatabaseServer to your server name");
            }
        }

        public DataTable GetProducts()
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {

                    cmd.CommandText =
                        "SELECT ProductID, ProductName FROM  dbo.Products ORDER BY ProductName";

                    cn.Open();

                    dt.Load(cmd.ExecuteReader());

                    dt.Columns["ProductID"].ColumnMapping = MappingType.Hidden;
                  
                }
            }

            return dt;

        }

        /// <summary>
        /// Get all records to show in the CheckedListBox
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomers()
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {

                    cmd.CommandText = 
                        "SELECT CustomerIdentifier,CompanyName,ContactName,ContactTitle,City,PostalCode,Country " + 
                        "FROM dbo.Customers";

                    cn.Open();

                    dt.Load(cmd.ExecuteReader());
                }
            }

            return dt;
        }
    }
}
