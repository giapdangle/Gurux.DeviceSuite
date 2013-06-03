//
// --------------------------------------------------------------------------
//  Gurux Ltd
// 
//
//
// Filename:        $HeadURL$
//
// Version:         $Revision$,
//                  $Date$
//                  $Author$
//
// Copyright (c) Gurux Ltd
//
//---------------------------------------------------------------------------
//
//  DESCRIPTION
//
// This file is a part of Gurux Device Framework.
//
// Gurux Device Framework is Open Source software; you can redistribute it
// and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation; version 2 of the License.
// Gurux Device Framework is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
//
// This code is licensed under the GNU General Public License v2. 
// Full text may be retrieved at http://www.gnu.org/licenses/gpl-2.0.txt
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Gurux.DeviceSuite.Publisher
{
    class CryptHelper
    {
        private static string md5(string text)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(new System.Text.ASCIIEncoding().GetBytes(text))).Replace("-", "").ToLower();
        }

        static public string GetCryptedPassword(string name, string password)
        {
            // form the string for encrypting
            // and put into byte array
            string textToEncrypt = name + md5(password);
            byte[] plainTextBytes = new System.Text.ASCIIEncoding().GetBytes(textToEncrypt);

            // set up encrytion object
            RijndaelManaged aes128 = new RijndaelManaged();
            aes128.KeySize = 128;
            aes128.BlockSize = 128;
            aes128.Padding = PaddingMode.PKCS7;
            aes128.Mode = CipherMode.CBC;
            byte[] key = new byte[16];
            Array.Copy(new System.Text.ASCIIEncoding().GetBytes(textToEncrypt), key, 16);
            aes128.Key = key;
            aes128.IV = new byte[] { 0x67, 0x40, 0x38, 0x2b, 0x47, 0x5e, 0x13, 0x49, 0x7b, 0x56, 0x34, 0x78, 0x31, 0x54, 0x5a, 0x65 };
            // encrypt the data            
            ICryptoTransform encryptor = aes128.CreateEncryptor();
            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(plainTextBytes, 0, plainTextBytes.Length);

            // convert our encrypted data from a memory stream into a byte array.
            cs.FlushFinalBlock();

            byte[] cypherTextBytes = ms.ToArray();

            // close memory stream
            ms.Close();
            return System.Uri.EscapeDataString(Convert.ToBase64String(cypherTextBytes));
        }
    }
}
