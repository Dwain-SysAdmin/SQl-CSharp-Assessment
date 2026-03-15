using System;
using CustomerConsoleApp.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using CustomerConsoleApp.Helpers;

namespace CustomerOrders.Helpers
{
    public class Database
    {
        public static List<CustomerSummary> GetCustomerSummary(decimal? minSpend)
        {
            string encrytionstring = ConfigurationManager.AppSettings["EncryptedConnectionString"];

            string sqlConnection = EncryptionUtility.Decrypt(encrytionstring);

            List<CustomerSummary> list = [];

            using SqlConnection conn = new(sqlConnection);

            conn.Open();

            string sql = @"
            SELECT
                c.CustomerId,
                c.FirstName + ' ' + c.LastName AS CustomerName,
                COUNT(o.OrderId) AS TotalOrders,
                ISNULL(SUM(o.TotalAmount),0) AS TotalSpend
            FROM Customers c
            LEFT JOIN Orders o
                ON c.CustomerId = o.CustomerId
            GROUP BY
                c.CustomerId,
                c.FirstName,
                c.LastName
            HAVING (@minSpend IS NULL OR SUM(o.TotalAmount) >= @minSpend)
            ";

            using SqlCommand cmd = new(sql, conn);

            cmd.Parameters.AddWithValue("@minSpend",
                (object?)minSpend ?? DBNull.Value);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new CustomerSummary
                {
                    CustomerId = reader.GetInt32(0),
                    CustomerName = reader.GetString(1),
                    TotalOrders = reader.GetInt32(2),
                    TotalSpend = reader.GetDecimal(3)
                });
            }

            return list;
        }
    }
}