using System;
using System.Collections.Generic;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CryptoBusinessLayer :BusinessBase 
    {
		public (string Response, Exception Error) EncryptText(string text)
		{
			try
			{
				var str = this.EncryptionService.AesEncrypt(text, this.AESEncryptionKey);
				return (str, null);
			}
			catch (Exception ex)
			{
				return (null, ex);
			}
		}
		public (string Response, Exception Error) DecryptText(string text)
		{
			try
			{
				var str = this.EncryptionService.AesDecrypt(text, this.AESEncryptionKey);
                return (str, null);
			}
			catch (Exception ex)
			{
				return (null, ex);
			}
		}

        public (bool Success, Exception Error) HashValueMatch(string hash1, string hash2)
        {
            try
            {
                var h1 = this.EncryptionService.GetHashString(hash1);
                var h2 = this.EncryptionService.GetHashString(hash2);
                var isMatch = h1.Equals(h2);
                return (isMatch, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

    }
}
