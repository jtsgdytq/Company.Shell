using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Company.Application.UI.Controls
{
    public class RectEventArgs:RoutedEventArgs
    {
        public Rect SelectRect { get;  }

        public double X { get; }

        public double Y { get; }


        public double Zoom { get; }

        public RectEventArgs(RoutedEvent routedEvent,object source,Rect selectRect, double x,double y,double zoom):base(routedEvent,source)
        {
            SelectRect = selectRect;
            X = x;
            Y = y;
            Zoom = zoom;       
        }
    }


    public partial class CameraBox : UserControl
    {

        /// <summary>
        /// 图像源
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CameraBox), new PropertyMetadata(callback));
        /// <summary>
        /// 如果ImageSource属性变化，则更新显示的图像
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is CameraBox box)
            {
                var source = e.NewValue as System.Windows.Media.ImageSource;
                if (source != null)
                {
                    box.image.Source = source;
                }
            }
        }


        /// <summary>
        /// 矩形框选择
        /// </summary>
        public bool IsRectSelect
        {
            get { return (bool)GetValue(IsRectSelectProperty); }
            set { SetValue(IsRectSelectProperty, value); }
        }

        
        public static readonly DependencyProperty IsRectSelectProperty =
            DependencyProperty.Register("IsRectSelect", typeof(bool), typeof(CameraBox), new PropertyMetadata(false));

        public static readonly RoutedEvent RectChangeEvent
            =EventManager.RegisterRoutedEvent("RectChange",RoutingStrategy.Bubble,typeof(RoutedEventHandler),typeof(CameraBox));

        /// <summary>
        /// 矩形改变事件
        /// </summary>
        public event RoutedEventHandler RectChange
        {
            add { AddHandler(RectChangeEvent, value); }
            remove { RemoveHandler(RectChangeEvent, value); }
        }
        /// <summary>
        /// 重写OnRectChange方法以提供对RectChange事件的处理
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRectChange(RectEventArgs e)
        {
          
            RaiseEvent(e);
        }

        private ScaleTransform _scaleTransform=new ScaleTransform();

        private TranslateTransform _translateTransform = new TranslateTransform();
        public TransformGroup TransformGroup { get; } = new TransformGroup();
        public Rect SelectRect { get; private set; } = new Rect(0,0,0,0);


        private int _viewportWidth = 0;
        private int _viewportHeight = 0;
        private int imageboxWidth = 0;
        private int imageboxHeight = 0;

        private double _scale = 1.0;

        private bool _isMouseDown = false;
        private bool _isImageMove = false;

        private Point StartPoint =new Point(0,0);
        private Point MousePoint=new Point(-1,-1);
        private Point MouseDownPos=new Point(0,0);
        public CameraBox()
        {
            InitializeComponent();
            Loaded += CameraBox_Loaded;
        }

        private void CameraBox_Loaded(object sender, RoutedEventArgs e)
        {
            SetTransform();
            Loaded-= CameraBox_Loaded;
        }

        private void SetTransform()
        {
            TransformGroup.Children.Add(_scaleTransform);
            TransformGroup.Children.Add(_translateTransform);
            imagebox.RenderTransform = TransformGroup;
            _viewportWidth=(int)viewport.ActualWidth;
            _viewportHeight=(int)viewport.ActualHeight;
            imageboxWidth=(int)imagebox.ActualWidth;
            imageboxHeight=(int)imagebox.ActualHeight;
            //缩放比例计算
            _scale = Math.Min((double)_viewportWidth / imageboxWidth, (double)_viewportHeight / imageboxHeight);
            _scaleTransform.ScaleX = _scale;
            _scaleTransform.ScaleY = _scale;
            //平移计算
            var translateX = (_viewportWidth - imageboxWidth * _scale) / 2;
            var translateY = (_viewportHeight - imageboxHeight * _scale) / 2;
            _translateTransform.X = translateX;
            _translateTransform.Y = translateY;

        }

        private void viewport_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseDownPos= new Point(-1, -1);
        }

        private void viewport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _isMouseDown = true;
                MousePoint = e.GetPosition(viewport);
                StartPoint = e.GetPosition(viewport);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                _isImageMove = true;
                MouseDownPos = e.GetPosition(viewport);
            }

            rect.Width = 0;
            rect.Height = 0;

            Canvas.SetLeft(rect, StartPoint.X);
            Canvas.SetTop(rect, StartPoint.Y);
            viewport.CaptureMouse();
        }

        private void viewport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown=false;
            MouseDownPos=e.GetPosition(viewport);
            viewport.ReleaseMouseCapture();
        }

        private void viewport_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(viewport);
            MousePoint = pos;
            if (_isMouseDown)
            {
                rect.Width = pos.X - StartPoint.X < 0 ? 0 : pos.X - StartPoint.X;
                rect.Height = pos.Y - StartPoint.Y < 0 ? 0 : pos.Y - StartPoint.Y;
                SelectRect = new Rect(StartPoint.X, StartPoint.Y, rect.Width, rect.Height);
                rectLabel.Text = $"{rect.Width},{rect.Height}";
                Canvas.SetLeft(rectLabel, StartPoint.X + rect.Width + 5);
                Canvas.SetTop(rectLabel, StartPoint.Y + rect.Height + 5);
            }
            else if(_isImageMove)
            {
                _translateTransform.X = pos.X - StartPoint.X;
                _translateTransform.Y = pos.Y - StartPoint.Y;
                MouseDownPos = pos;           
            }

        }
        private void viewport_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isImageMove = false;
            viewport.ReleaseMouseCapture();
        }

        private void viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            _scale = e.Delta > 0 ? _scale * 1.1 : _scale / 1.1;
            _scaleTransform.ScaleX = _scale;
            _scaleTransform.ScaleY = _scale;
        }
    }
}
