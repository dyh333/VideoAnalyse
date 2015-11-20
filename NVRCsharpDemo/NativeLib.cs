using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NVRCsharpDemo
{
    class NativeLib
    {
        [DllImport(@"..\bin\NativeLib.dll")]
        public static extern void PrintMsg(string msg);

        [DllImport(@"..\bin\NativeLib.dll")]
        public static extern int Multiply(int factorA, int factorB);

        //声明一个接收整型为第一参数的函数
        [DllImport("NativeLib.dll", EntryPoint = "PrintMsgByFlag")]
        public static extern void PrintID(ref int id, int flag);
        //声明一个接收字符串为第一参数的函数
        [DllImport("NativeLib.dll", EntryPoint = "PrintMsgByFlag")]
        public static extern void PrintName(string name, int flag);

    }
}
