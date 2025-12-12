using Company.Logger;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Company.Core.Models
{
    public class BItmapGDI:ReactiveUI.ReactiveObject
    {
        [Reactive]
        public ImageSource ImageSource { get; private set; }

        private WriteableBitmap _sourceU8C1;
        private WriteableBitmap _sourceU8C3;

        private int _width;
        private int _height;

        public BItmapGDI(int width,int height)
        {
            _width=width;
            _height=height;

            _sourceU8C1 = new WriteableBitmap(_width, _height, 96d, 96d, PixelFormats.Gray8, null);
            _sourceU8C3= new WriteableBitmap(_width,_height,96d,96d, PixelFormats.Bgr24, null);
            ImageSource = _sourceU8C1;          
        }

        public void WritePixle(ImageU8C1 imageU8C1, int x, int y)
        {
            if (imageU8C1.IntPtt == IntPtr.Zero || imageU8C1.width == 0 ||imageU8C1.height== 0) return;

            Application.Current.Dispatcher.Invoke(new Action(() => {                        
                try
                {
                    var int32Rect = new Int32Rect(x, y, imageU8C1.width, imageU8C1.height);
                    _sourceU8C1.WritePixels(int32Rect, imageU8C1.IntPtt, imageU8C1.width * imageU8C1.height, imageU8C1.width,int32Rect.X,int32Rect.Y);
                }
                catch (Exception ex)
                {

                    Logs.LogError(ex);
                }

                finally
                {
                    ImageSource=null;
                    ImageSource = _sourceU8C1;
                }

            }));
           
        }
    }
}
