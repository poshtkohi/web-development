/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=general;Integrated Security=True");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "SELECT subdomain,f FROM usersInfo WHERE IsWeblogExpired=0";

            StreamWriter _sr1 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\filtered.txt");
            StreamWriter _sr2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\total.txt");

            SqlDataReader _reader = command.ExecuteReader();
            while (_reader.Read())
            {
                if (_reader["subdomain"] == DBNull.Value)
                    continue;
                if ((string)_reader["f"] == "n")
                {
                    _sr1.WriteLine(String.Format("http://{0}.iranblog.com", (string)_reader["subdomain"]));
                    _sr1.Flush();
                }
                else
                {
                    _sr2.WriteLine(String.Format("http://{0}.iranblog.com", (string)_reader["subdomain"]));
                    _sr2.Flush();

                }
            }
            _sr1.Close();
            _sr2.Close();
            connection.Close();


        }
    }
}
