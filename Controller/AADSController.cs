using AutoViewWebAdsCSharp.Model;
using System;
using System.Collections.Generic;
using AutoViewWebAdsCSharp.Common;
using System.Threading;
using System.Drawing;

namespace AutoViewWebAdsCSharp.Controller
{

    public class AADSController : AbstractAction
    {
        private static readonly Random rd = new Random();
        public List<Proxy> Proxies { get; set; }

        public AADSController(LDPlayer _ldPlayer) : base(_ldPlayer) { }

        public void Run()
        {
            foreach (var proxy in Proxies)
            {
                if (!LDPlayerHandler.CanRun)
                {
                    DoAutoAction(AADSStatic.FirefoxMeta, CloseApp);
                    return;
                }
                //proxy.Reset = true;
                DoAutoAction(proxy, ConnectProxy, 2000);
                // Mở firefox
                OpenFirefox(AADSStatic.FirefoxMeta);
                // Chờ tải
                bool loadFirefoxDone = DoAutoAction(CheckFirefoxLoadDone, rd.Next(500) + 1000);
                if (loadFirefoxDone)
                {
                    // Bấm nút addressbar
                    DoAutoAction(ClickAddressBar, 1200 + rd.Next(500));
                    // Nhập địa chỉ web và bấm enter
                    DoAutoAction(GoWeb, 5000 + rd.Next(1000));
                    DoAutoAction(CheckStatus, 1000);
                    DoAutoAction(ScrollRandomOrClick, rd.Next(500) + 500 + rd.Next(300));
                    DoAutoAction(AADSStatic.FirefoxMeta, CloseApp);
                }
                else
                {
                    DoAutoAction(AADSStatic.FirefoxMeta, CloseApp);
                }
            }
        }

        private bool ScrollRandomOrClick()
        {
            int randomScroll = rd.Next(2);
            for (int i = 0; i < randomScroll; i++)
            {
                ScrollPoint scrollPoint = new()
                {
                    X = 36 + rd.Next(10),
                    Y = 245 + rd.Next(10),
                    TargetX = 58 + rd.Next(11),
                    TargetY = 99 + rd.Next(8),
                };
                DoAutoAction(scrollPoint, Scroll, rd.Next(500) + 100);
            }
            int rdClickTime = rd.Next(6);
            for (int i = 0; i < rdClickTime; i++)
            {
                int rdX = rd.Next(360) + 5;
                int rdY = rd.Next(180) + 36;
                DoAutoAction(new SimplePoint() { X = rdX, Y = rdY }, Click, 600 + rd.Next(500));
            }
            return true;
        }


        private bool CheckStatus()
        {
            Thread.Sleep(2000 + rd.Next(1000));
            List<ColorCompareMeta> pixelError1 = new()
            {
                new()
                {
                    X = 104,
                    Y = 62,
                    SuccessColor = Color.FromArgb(255, 228, 100),
                },
                new() { X = 126, Y = 43, SuccessColor = Color.FromArgb(255, 57, 129) }
            };
            CaptureScreen();
            using Bitmap bmp = new(".\\tmp\\" + LDPlayer.ID + ".jpg");
            Bitmap bitmap = new(bmp);
            // Nếu tìm thấy ảnh
            if (MatchAllPixel(bitmap, pixelError1))
            {
                bmp.Dispose();
                System.IO.File.Delete(".\\" + LDPlayer.ID + ".jpg");
                return false;
            }
            // Chờ tối thiểu 2 giây
            DoAutoAction(AADSStatic.CloseSecurityTipPoint, Click, 500);
            Thread.Sleep(2000);
            Thread.Sleep(rd.Next(10000));
            bmp.Dispose();
            System.IO.File.Delete(".\\" + LDPlayer.ID + ".jpg");
            DoAutoAction(AADSStatic.FirefoxMeta, CloseApp);
            return true;
        }

        private bool GoWeb()
        {
            DoAutoAction(AADSStatic.Website, InputText, 1200);
            string enter = "66";
            DoAutoAction(enter, PressKey, 10000 + rd.Next(3600));
            DoAutoAction(AADSStatic.CloseSecurityTipPoint, Click, 1500);
            return true;
        }

        private bool ClickAddressBar()
        {
            DoAutoAction(AADSStatic.AddressBarPoint, Click, 300);
            return true;
        }

        private bool CheckFirefoxLoadDone()
        {
            return WaitUntilPixelAppear(AADSStatic.ColorFirefoxInitSuccess, DateTimeOffset.Now.ToUnixTimeMilliseconds() + 10000, 2500);
        }

        private void OpenFirefox(object meta)
        {
            // Xóa dữ liệu game 
            DoAutoAction(meta, ClearAppData);
            // Mở game
            DoAutoAction(meta, OpenApp, 5000);
            // Close Crash
            DoAutoAction(new SimplePoint() { X = 410, Y = 220 }, Click, rd.Next(1000) + 300);
        }

        public void TesCapture()
        {
            CaptureScreen();
        }

    }
}
