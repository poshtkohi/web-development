/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DotGrid.Serialization
{
	/// <summary>
	/// A class for binary data serialization and deserialization.
	/// </summary>
	/*internal*/public class SerializeDeserialize
	{
		//**************************************************************************************************************//
		/// <summary>
		/// Serialize the obj.
		/// </summary>
		/// <param name="obj">The object for serialization.</param>
		/// <returns>Binary serialized object data.</returns>
		public static byte[] Serialize(object obj)
		{
			MemoryStream ms = new MemoryStream();
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(ms, obj);
			return ms.GetBuffer();
		}
		//**************************************************************************************************************//
		/// <summary>
		/// Deserialize the obj binary format.
		/// </summary>
		/// <param name="obj">Binary representation of the object.</param>
		/// <returns>Deserialized object.</returns>
		public static object DeSerialize(byte[] obj)
		{
			IFormatter formatter = new BinaryFormatter();
			return formatter.Deserialize(new MemoryStream(obj)); 
		}
		//**************************************************************************************************************//
	}
}