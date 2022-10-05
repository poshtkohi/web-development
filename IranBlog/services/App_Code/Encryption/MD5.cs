/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;

namespace DotGrid.DotSec
{
	/// <summary>
	///A complete class for MD5 hashing algorithm.
	/// </summary>
	public class MD5
	{
		//**************************************************************************************************************//
		/// <summary>
		/// Initializes a new instance of MD5.
		/// </summary>
		public MD5()
		{
		}
		//**************************************************************************************************************//
		/// <summary>
		/// Hash the input data.
		/// </summary>
		/// <param name="data">Input data.</param>
		/// <returns>Hased data.</returns>
		public byte[] MD5hash (byte[] data)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(data);
			md5.Clear();
			return result;
		}
		//**************************************************************************************************************//
		/// <summary>
		/// Hash the input data.
		/// </summary>
		/// <param name="data">Input data.</param>
		/// <param name="offset">The starting point in the data at which to begin hashing.</param>
		/// <param name="count">The number of characters to hash.</param>
		/// <returns>Hased data.</returns>
		public byte[] MD5hash (byte[] data, int offset , int count)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(data, offset, count);
			md5.Clear();
			return result;
		}
		//**************************************************************************************************************//
	}
}