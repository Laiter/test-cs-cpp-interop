using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class NativeAPI
    {
        public struct FundamentalTypes
        {
            public byte boolVal;
            public byte ucharVal;
            public sbyte charVal;
            public short shortVal;
            public ushort ushortVal;
            public int intVal;
            public uint uintVal;
            public long longlongVal;
            public ulong ulonglongVal;
            public float floatVal;
            public double doubleVal;
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public delegate int CallBackMemoryAlloc(ref IntPtr bufferPtr, int value);

        [DllImport("Dll1.dll", EntryPoint = "PassStructCase", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int PassStructCase([In] FundamentalTypes fundamentalTypes, [In, Out] ref FundamentalTypes fundamentalTypesRef, [In, Out] ref FundamentalTypes fundamentalTypesPtr);

        [DllImport("Dll1.dll", EntryPoint = "PassBufferCase", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int PassBufferCase([In, Out] byte[] buffer);

        [DllImport("Dll1.dll", EntryPoint = "PassBufferAndReallocMemoryCase", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int PassBufferAndReallocMemoryCase([In] byte[] buffer, [In] CallBackMemoryAlloc memAllocFunc);
    }
}
