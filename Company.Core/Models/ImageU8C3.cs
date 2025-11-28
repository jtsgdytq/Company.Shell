using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Company.Core.Models
{
   public struct ImageU8C3
    {
        public IntPtr IntPtt { get; set; }  // 图像数据指针

        public int width { get; set; }    // 图像宽度

        public int height { get; set; }   // 图像高度


        public ImageU8C3(IntPtr intPtr, int w, int h)
        {
            IntPtt = intPtr;
            width = w;
            height = h;
        }
        /// <summary>
        /// 创建一张8位3通道图像
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static ImageU8C3 Create(int w, int h)
        {
            return new ImageU8C3(Marshal.AllocHGlobal(h * w * 3), w, h);
        }
    }
}
