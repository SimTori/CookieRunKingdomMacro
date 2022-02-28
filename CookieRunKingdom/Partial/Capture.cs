using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CookieRunKingdom
{
    public partial class MainWindow
    {
        public Image<Bgr, byte> captureImage;

        public void capture()
        {
            Rectangle rc = Rectangle.Empty;
            Graphics gfxWin = Graphics.FromHwnd(_hwnd);
            rc = Rectangle.Round(gfxWin.VisibleClipBounds);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics gfxbmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxbmp.GetHdc();

            bool succeeded = Dll.PrintWindow(_hwnd, hdcBitmap, 1);
            gfxbmp.ReleaseHdc(hdcBitmap);
            if (!succeeded)
            {
                gfxbmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(System.Drawing.Point.Empty, bmp.Size));
            }

            IntPtr hRgn = Dll.CreateRectRgn(0, 0, 0, 0);

            Dll.GetWindowRgn(_hwnd, hRgn);

            Region region = Region.FromHrgn(hRgn);
            if (!region.IsEmpty(gfxbmp))
            {
                gfxbmp.ExcludeClip(region);
                gfxbmp.Clear(Color.Transparent);
            }
            gfxbmp.Dispose();
            //bmp.Save("C:\\Users\\cjh01\\Desktop\\text\\b.bmp");
            captureImage = new Image<Bgr, byte>(bmp);

            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
            {
                IntPtr hBitmap = bmp.GetHbitmap();
                imgCapture.Source = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                Dll.DeleteObject(hBitmap);
            });


        }
    }
}
