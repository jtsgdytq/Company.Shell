using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Company.Core.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// 根据文件路径读取图片数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static  Bitmap Load(string filePath)
        {
            if(!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist", nameof(filePath));
            }
            byte[] imageData = System.IO.File.ReadAllBytes(filePath);

            using (var ms = new System.IO.MemoryStream(imageData))
            {            
                return new Bitmap(ms);
            }
        }
    }
}
