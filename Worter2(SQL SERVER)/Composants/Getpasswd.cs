using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace Worter2_SQL_SERVER_

{
    static class Getpasswd
    {
        public static SecureString GetPassword()
        {
            var pwd = new SecureString();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.RemoveAt(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000') // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
                {
                    pwd.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            return pwd;
        }
        //public String SecureStringToString(SecureString value)
        //{
        //    IntPtr valuePtr = IntPtr.Zero;
        //    try
        //    {
        //        valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
        //        return Marshal.PtrToStringUni(valuePtr);
        //    }
        //    finally
        //    {
        //        Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        //    }
        //}
        public static string GetString(this SecureString source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var length = source.Length;
            string result = null;
            char[] chars = new char[length];
            IntPtr pointer = IntPtr.Zero;

            try
            {
                pointer = Marshal.SecureStringToBSTR(source);
                Marshal.Copy(pointer, chars, 0, length);

                result = String.Join("", chars);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Get data error", ex);
            }
            finally
            {
                if (pointer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(pointer);
                }
            }

            return result;
        }
    }
}
