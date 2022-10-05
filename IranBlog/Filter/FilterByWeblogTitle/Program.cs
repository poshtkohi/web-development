/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace FilterByBlogTitle
{
    class Program
    {
        private static ArrayList _filters = new ArrayList();
        //--------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            FilterIni();
            //Console.WriteLine(_filters.Count);
            /*Int64 _BlogID = (Int64)1;
            string _BlogTitle = null;
            int _i = 0;
            int _filtered = 0;

            //StreamWriter _sw = new StreamWriter(@"c:\ss.txt");

            SqlConnection _updaterConnection = new SqlConnection("Data Source=localhost;Initial Catalog=general;Integrated Security=True");
            _updaterConnection.Open();
            SqlCommand _updaterCommand = _updaterConnection.CreateCommand();
            _updaterCommand.Connection = _updaterConnection;
            _updaterCommand.CommandType = CommandType.StoredProcedure;
            _updaterCommand.CommandText = "_updaterConnection_proc";

            _updaterCommand.Parameters.Add("@BlogID", SqlDbType.BigInt);


            SqlConnection _readerConnection = new SqlConnection("Data Source=localhost;Initial Catalog=general;Integrated Security=True");
            _readerConnection.Open();
            SqlCommand _readerCommand = _readerConnection.CreateCommand();
            _readerCommand.Connection = _readerConnection;
            _readerCommand.CommandType = CommandType.StoredProcedure;
            _readerCommand.CommandText = "_readerConnection_proc";
            _readerCommand.Parameters.Add("@TopMost", SqlDbType.BigInt);
            _readerCommand.Parameters["@TopMost"].Value = 1;

            SqlDataReader _reader = null;

        First:
            _readerCommand.Parameters["@TopMost"].Value = _BlogID;
            _reader = _readerCommand.ExecuteReader();


            int _j = 0;
            while (_reader.Read())
            {
                _j++;
                _BlogID = (Int64)_reader["BlogID"];
                _i++;
                Console.Write("<CurrentCursorIndex:{0}> <CurrnetBlogID:{1}> <Filtered:{2}>", _i, _BlogID, _filtered);
                Console.Write("\r");
                if (_reader["BlogTitle"] != System.DBNull.Value)
                    _BlogTitle = (string)_reader["BlogTitle"];
                else
                    _BlogTitle = null;

                
                //----------------------------
                //Console.WriteLine(_BlogTitle);
                if (_BlogTitle != null)
                {
                    if (Filter(_BlogTitle))
                    {
                        //Console.WriteLine(_BlogID);
                        _filtered++;
                        //_sw.WriteLine(_BlogTitle);
                        //_sw.Flush();
                        _updaterCommand.Parameters["@BlogID"].Value = _BlogID;
                        _updaterCommand.ExecuteNonQuery();
                        //Console.WriteLine("hello");
                    }
                }

                _BlogTitle = null;

                if (_j == 1000)
                {
                    _reader.Close();
                    goto First;
                }
            }
            _reader.Close();
            //_updaterConnection.Close();
            _readerConnection.Close();
            Console.WriteLine();
            Console.ReadKey();*/

            Console.WriteLine("Start...");

            FilterIni();

            SqlConnection _updaterConnection = new SqlConnection("Data Source=localhost;Initial Catalog=general;Integrated Security=True");
            _updaterConnection.Open();
            SqlCommand _updaterCommand = _updaterConnection.CreateCommand();
            _updaterCommand.Connection = _updaterConnection;
            _updaterCommand.CommandText = QueryBulider();

            _updaterCommand.ExecuteNonQuery();
            _updaterConnection.Close();

            Console.WriteLine("End...");
            Console.ReadKey();


            return;
        }
        //--------------------------------------------------------------------------------------
        private static bool Filter(string _buffer)
        {
            for (int i = 0; i < _filters.Count; i++)
            {
                if (_buffer.IndexOf((string)_filters[i]) >= 0)
                {
                    //Console.WriteLine((string)_filters[i]);
                    return true;
                }
            }

            return false;
        }
        //--------------------------------------------------------------------------------------
        static private string QueryBulider()
        {
            string _query = "UPDATE usersInfo SET f='n' WHERE ";

            for (int i = 0; i < _filters.Count; i++)
            {
                if(i == 0)
                    _query += String.Format("title LIKE N'%{0}%'", (string)_filters[i]);
                else
                    _query += String.Format(" OR title LIKE N'%{0}%'", (string)_filters[i]);
            }

            return _query;
        }
        //--------------------------------------------------------------------------------------
        private static void FilterIni()
        {
            StreamReader _sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\FilterByWeblogTitle.txt");

            while (true)
            {
                string _temp = _sr.ReadLine();
                if (_temp == null)
                    break;
                if (_temp != "")
                {
                    _filters.Add(_temp);
                }
            }

            _sr.Close();
            _sr = null;

            return;
        }
        //--------------------------------------------------------------------------------------
    }
}
