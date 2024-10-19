using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace iConsulting
{
	public class clsCrypto
	{
		byte[] CRYPTO_KEY = { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
		byte[] CRYPTO_IV = { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0xcd };

		public string Encrypt(string PlainText)
		{
			try 
			{	        
				RijndaelManaged RMCrypto = new RijndaelManaged();
				byte[] ByteArray = Encoding.UTF8.GetBytes(PlainText);
				ICryptoTransform enc = RMCrypto.CreateEncryptor(CRYPTO_KEY, CRYPTO_IV);
				byte[] ByteArr = enc.TransformFinalBlock(ByteArray, 0, ByteArray.GetLength(0));
				return Convert.ToBase64String(ByteArr); 
			}
			catch (Exception ex)
			{
				return "Encrypt - " + ex.Message.ToString();
			}
		}

		public string Decrypt(string Base64String)
		{
			try
			{
				RijndaelManaged RMCrypto = new RijndaelManaged();
				ICryptoTransform dec = RMCrypto.CreateDecryptor(CRYPTO_KEY, CRYPTO_IV);
				byte[] ByteArr = Convert.FromBase64String(Base64String);
				return Encoding.UTF8.GetString(dec.TransformFinalBlock(ByteArr, 0, ByteArr.GetLength(0)));
			}
			catch (Exception ex)
			{
				return "Decrypt - " + ex.Message.ToString();
			}
		}
	}
}
