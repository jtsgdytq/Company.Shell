using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Helpers
{
    public static class MemeryHelper
    {
        /// <summary>
        /// 内存清零
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="size"></param>
        [DllImport("kernel32.dll", EntryPoint = "RtlZeroMemory", CharSet = CharSet.Ansi)]
        public static extern void ZeroMemory(IntPtr intPtr, long size);


        /// <summary>
        /// 内存复制
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        /// <param name="length"></param>
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, long length);


        /// <summary>
        /// 将指定的值写入内存
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>

        [DllImport("kernel32.dll", EntryPoint = "RtlFillMemory", CharSet = CharSet.Ansi)]
        public static extern void FillMemory(IntPtr dest, long length, byte value);
    }
}
