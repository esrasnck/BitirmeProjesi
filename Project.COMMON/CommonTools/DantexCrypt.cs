using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.COMMON.CommonTools
{
    public static class DantexCrypt
    {
        public static string Crypt(string a)
        {
            try
            {
                Random rnd = new Random();
                char[] charArray = { '*', '_', '?' };
                string hashedCode = "";

                foreach (char item in a)
                {
                    int tempInteger;
                    switch (rnd.Next(1, 4))
                    {
                        case 1:
                            tempInteger = (Convert.ToInt32(item) + 1) * 2;
                            hashedCode += $"{tempInteger.ToString()}{charArray[0]}";
                            break;
                        case 2:
                            tempInteger = (Convert.ToInt32(item) + 2) * 3;
                            hashedCode += $"{tempInteger.ToString()}{charArray[1]}";
                            break;
                        case 3:
                            tempInteger = (Convert.ToInt32(item) + 3) * 4;
                            hashedCode += $"{tempInteger.ToString()}{charArray[2]}";
                            break;
                    }
                }
                return hashedCode;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public static string DeCrypt(string a)
        {
            string decryptedCode = "";

            List<string> parts = Regex.Split(a, @"(?<=[*_?])").ToList(); //split gibi iş yapar anca *_? karakterlerini de alır..

            foreach (string item in parts)
            {
                if (item.Contains("*"))
                {
                    string element = item.TrimEnd('*');
                    int asciiCode = (Convert.ToInt32(element) / 2) - 1;
                    string character = Convert.ToChar(asciiCode).ToString();
                    decryptedCode += character;
                }
                else if (item.Contains("_"))
                {
                    string element2 = item.TrimEnd('_');
                    int asciiCode2 = (Convert.ToInt32(element2) / 3) - 2;
                    string character2 = Convert.ToChar(asciiCode2).ToString();
                    decryptedCode += character2;
                }
                else if (item.Contains("?"))
                {
                    string element3 = item.TrimEnd('?');
                    int asciiCode3 = (Convert.ToInt32(element3) / 4) - 3;
                    string character3 = Convert.ToChar(asciiCode3).ToString();
                    decryptedCode += character3;
                }
            }
            return decryptedCode;

            #region İptal

            //string[] saltArray = a.Split('*','_','?');
            //foreach (string item in saltArray)
            //{
            //    int asciiCode = Convert.ToInt32(item);
            //    int tempChar = (asciiCode / 3) - 3;
            //    string character = Convert.ToChar(tempChar).ToString();
            //    decryptedCode += character;
            //}
            //return decryptedCode; 
            #endregion
        }
    }
}
