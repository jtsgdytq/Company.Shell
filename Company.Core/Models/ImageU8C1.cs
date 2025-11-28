using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Models
{
    public struct ImageU8C1
    {
        public IntPtr IntPtt { get; set; }  // 图像数据指针

        public int width { get; set; }    // 图像宽度

        public int height { get; set; }   // 图像高度


        public ImageU8C1(IntPtr intPtr, int w, int h)
        {
            IntPtt = intPtr;
            width = w;
            height = h;
        }
        /// <summary>
        /// 创建一张8位1通道图像
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static ImageU8C1 Create (int w, int h)
        {
            return new ImageU8C1(Marshal.AllocHGlobal(h*w),w,h);
        }

    }
}
