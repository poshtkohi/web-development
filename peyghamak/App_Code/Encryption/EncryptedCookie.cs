/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotGrid.DotSec;
using DotGrid.Serialization;

namespace Peyghamak
{
    public class EncryptedCookie
    {
        private RijndaelEncryption rijndael;
        //--------------------------------------------------------------------------------
        [Serializable]
        private class ObjInfo
        {
            public byte[] obj;
            public byte[] hash;
            public ObjInfo(byte[] obj)
            {
                this.obj = obj;
                this.hash = new MD5().MD5hash(obj);
            }
        }
        //--------------------------------------------------------------------------------
        public EncryptedCookie(string key, string iv)
        {
            this.rijndael = new RijndaelEncryption(RijndaelEncryption.Base64StringToBinary(key), RijndaelEncryption.Base64StringToBinary(iv));
        }
        //--------------------------------------------------------------------------------
        public string EncryptWithMd5Hash(object obj)
        {
            ObjInfo info = new ObjInfo(SerializeDeserialize.Serialize(obj));
            byte[] buffer = this.rijndael.encrypt(SerializeDeserialize.Serialize(info));
            return RijndaelEncryption.BinaryToBase64String(buffer, buffer.Length);
        }
        //--------------------------------------------------------------------------------
        public object DecryptWithMd5Hash(string data)
        {
            ObjInfo info = (ObjInfo)SerializeDeserialize.DeSerialize(rijndael.decrypt(RijndaelEncryption.Base64StringToBinary(data)));
            byte[] hash = new MD5().MD5hash(info.obj);
            for (int i = 0; i < hash.Length; i++)
                if (hash[i] != info.hash[i])
                    throw new System.Security.SecurityException("Invalid hash.");
            hash = null;
            return SerializeDeserialize.DeSerialize(info.obj);
        }
        //--------------------------------------------------------------------------------
    }
}
