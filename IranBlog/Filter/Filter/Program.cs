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

namespace Filter
{
    class Program
    {
        private static ArrayList _filters = new ArrayList();
        //--------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            FilterIni();
            //Console.WriteLine(_filters.Count);
            Int64 _PostID = 1;
            bool _IsUpdatePostContent = false;
            bool _IsUpdateContinuedPostContent = false;
            string _PostContent = null;
            string _ContinuedPostContent = null;
            string _temp = null;
            int _i = 0;
            int _filtered = 0;

            SqlConnection _updaterConnection = new SqlConnection("Data Source=localhost;Initial Catalog=weblogs;Integrated Security=True");
            _updaterConnection.Open();
            SqlCommand _updaterCommand = _updaterConnection.CreateCommand();
            _updaterCommand.Connection = _updaterConnection;
            _updaterCommand.CommandType = CommandType.StoredProcedure;
            _updaterCommand.CommandText = "_updaterConnection_proc";

            _updaterCommand.Parameters.Add("@PostID", SqlDbType.BigInt);
            _updaterCommand.Parameters.Add("@PostContent", SqlDbType.NText);
            _updaterCommand.Parameters.Add("@ContinuedPostContent", SqlDbType.NText);
            _updaterCommand.Parameters.Add("@IsUpdateContinuedPostContent", SqlDbType.Bit);
            _updaterCommand.Parameters.Add("@IsUpdatePostContent", SqlDbType.Bit);



            SqlConnection _readerConnection = new SqlConnection("Data Source=localhost;Initial Catalog=weblogs;Integrated Security=True");
            _readerConnection.Open();
            SqlCommand _readerCommand = _readerConnection.CreateCommand();
            _readerCommand.Connection = _readerConnection;
            _readerCommand.CommandType = CommandType.StoredProcedure;
            _readerCommand.CommandText = "_readerConnection_proc";
            _readerCommand.Parameters.Add("@TopMost", SqlDbType.BigInt);
            _readerCommand.Parameters["@TopMost"].Value = 1;

            SqlDataReader _reader = null;

            First:
            _readerCommand.Parameters["@TopMost"].Value = _PostID;
            _reader = _readerCommand.ExecuteReader();


            int _j = 0;
            while (_reader.Read())
            {
                _j++;
                _PostID = (Int64)_reader["PostID"];
                _i++;
                Console.Write("<CurrentCursorIndex:{0}> <CurrnetPostID:{1}> <Filtered:{2}>", _i, _PostID, _filtered);
                Console.Write("\r");
                if (_reader["PostContent"] != System.DBNull.Value)
                    _PostContent = (string)_reader["PostContent"];
                else
                    _PostContent = null;

                if (_reader["ContinuedPostContent"] != System.DBNull.Value)
                    _ContinuedPostContent = (string)_reader["ContinuedPostContent"];
                else
                    _ContinuedPostContent = null;
                //----------------------------
                if (_PostContent != null)
                {
                    _temp = Filter(_PostContent);
                    if (String.Compare(_temp,  _PostContent) != 0)
                    {
                        _PostContent = _temp;
                        _IsUpdatePostContent = true;
                    }
                    else
                        _IsUpdatePostContent = false;
                }
                else
                    _IsUpdatePostContent = false;



                if (_ContinuedPostContent != null)
                {
                    _temp = Filter(_ContinuedPostContent);
                    if (String.Compare(_temp, _ContinuedPostContent) != 0)
                    {
                        _ContinuedPostContent = _temp;
                        _IsUpdateContinuedPostContent = true;
                    }
                    else
                        _IsUpdateContinuedPostContent = false;
                }
                else
                    _IsUpdateContinuedPostContent = false;
                //----------------------------
                //Console.WriteLine(_PostContent);

                if (_IsUpdatePostContent || _IsUpdateContinuedPostContent)
                {
                    _filtered++;
                    _updaterCommand.Parameters["@PostID"].Value = _PostID;
                    if (_PostContent != null)
                        _updaterCommand.Parameters["@PostContent"].Value = _PostContent;
                    else
                        _updaterCommand.Parameters["@PostContent"].Value = "";
                    if (_ContinuedPostContent != null)
                        _updaterCommand.Parameters["@ContinuedPostContent"].Value = _ContinuedPostContent;
                    else
                        _updaterCommand.Parameters["@ContinuedPostContent"].Value = "";
                    _updaterCommand.Parameters["@IsUpdateContinuedPostContent"].Value = _IsUpdateContinuedPostContent;
                    _updaterCommand.Parameters["@IsUpdatePostContent"].Value = _IsUpdatePostContent;

                    _updaterCommand.ExecuteNonQuery();
                    //Console.WriteLine("hello");
                }

                _PostContent = null;
                _ContinuedPostContent = null;
                _IsUpdatePostContent = false;
                _IsUpdateContinuedPostContent = false;
                if (_j == 1000)
                {
                    _reader.Close();
                    goto First;
                }
            }
            _reader.Close();
            _updaterConnection.Close();
            _readerConnection.Close();
            Console.WriteLine();
            Console.ReadKey();

        }
        //--------------------------------------------------------------------------------------
        private static string Filter(string _buffer)
        {
            string _temp = _buffer;

            for (int i = 0; i < _filters.Count; i++)
            {
                _temp = _temp.Replace((string)_filters[i], " ");
               // Console.WriteLine((string)_filters[i]);
            }

            return _temp;
        }
        //--------------------------------------------------------------------------------------
        private static void FilterIni()
        {
            StreamReader _sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\words.txt");

            while (true)
            {
                string _temp = _sr.ReadLine();
                if (_temp == null)
                    break;
                if(_temp != "")
                    _filters.Add(_temp);
            }

            _sr.Close();
            _sr = null;

            return;
        }
        //--------------------------------------------------------------------------------------
    }
}
