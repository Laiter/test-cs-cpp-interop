using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.NativeAPI;

namespace ConsoleApp1
{
    class Program
    {
        static void PrintFundamentalTypes(FundamentalTypes myObj)
        {
            foreach (var field in myObj.GetType().GetFields())
            {
                Console.WriteLine(field.Name + ":" + field.GetValue(myObj));
            }
        }

        static void RunPassStructCase()
        {
            FundamentalTypes fundamentalTypes = new FundamentalTypes();
            FundamentalTypes fundamentalTypesRef = new FundamentalTypes();
            FundamentalTypes fundamentalTypesPtr = new FundamentalTypes();
            Console.WriteLine("\ncs init fundamentalTypes:");
            PrintFundamentalTypes(fundamentalTypes);
            Console.WriteLine("\ncs init fundamentalTypesRef:");
            PrintFundamentalTypes(fundamentalTypesRef);
            Console.WriteLine("\ncs init fundamentalTypesPtr:");
            PrintFundamentalTypes(fundamentalTypesPtr);
            NativeAPI.PassStructCase(fundamentalTypes, ref fundamentalTypesRef, ref fundamentalTypesPtr);
            Console.WriteLine("\ncs received fundamentalTypes:");
            PrintFundamentalTypes(fundamentalTypes);
            Console.WriteLine("\ncs received fundamentalTypesRef:");
            PrintFundamentalTypes(fundamentalTypesRef);
            Console.WriteLine("\ncs received fundamentalTypesPtr:");
            PrintFundamentalTypes(fundamentalTypesPtr);
        }

        static void RunPassBufferCase()
        {
            byte[] buffer = new byte[1024];
            string str = "message from cs";
            ASCIIEncoding.ASCII.GetBytes(str, 0, str.Length, buffer, 0);
            Console.WriteLine("cs init buffer:" + Encoding.ASCII.GetString(buffer).TrimEnd('\0'));
            NativeAPI.PassBufferCase(buffer);
            str = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
            Console.WriteLine("cs received buffer:" + str);
        }

        static void RunPassBufferAndReallocMemory()
        {
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes("message from cs");
            Console.WriteLine("cs init buffer:" + Encoding.ASCII.GetString(buffer).TrimEnd('\0'));
            IntPtr outBufferPtr = IntPtr.Zero;
            int outBufferLen = 0;
            NativeAPI.PassBufferAndReallocMemoryCase(buffer, (ref IntPtr bufferPtr, int len) =>
            {
                if (len < 0) return -1;
                try
                {
                    outBufferLen = len;
                    outBufferPtr = bufferPtr = Marshal.AllocHGlobal(len);
                }
                catch
                {
                    return -1;
                }
                return 0;
            });
            byte[] outBuffer = new byte[outBufferLen];
            if (outBufferPtr != IntPtr.Zero)
            {
                Marshal.Copy(outBufferPtr, outBuffer, 0, outBufferLen);
                Marshal.FreeHGlobal(outBufferPtr);
                outBufferPtr = IntPtr.Zero;
            }
            Console.WriteLine("cs received out buffer:" + Encoding.ASCII.GetString(outBuffer).TrimEnd('\0'));
        }

        static void Main(string[] args)
        {
            RunPassBufferCase();
            RunPassBufferAndReallocMemory();
            RunPassStructCase();
        }
    }
}
