using AutoViewWebAdsCSharp.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace AutoViewWebAdsCSharp.Common
{
    public abstract class AbstractAction : AbstractADB
    {
        public CancellationToken CancellationToken { get; set; }

        public delegate bool _AutoActionDelegate();

        public delegate bool _AutoActionWithParam(object data);

        //public delegate void _DoClickAction(Point p);

        public AbstractAction(LDPlayer LDPlayer) : base(LDPlayer) { }

        /*
         * Bao hàm callback với chức năng kiểm tra có đang chạy không trong
         * 
         */
        protected bool DoAutoAction(object data, _AutoActionWithParam action, int delayAfterStep = 1000)
        {
            try
            {
                if (!LDPlayerHandler.CanRun)
                {
                    //throw new TaskCanceledException(string.Format("LDPlayer auto {0} stopped! at method {1}", LDPlayer.ID, "DO AUTO ACTION WITH PARAMS"));
                    return false;
                }
                bool status = action(data);
                Thread.Sleep(delayAfterStep);
                return status;
            }
            catch
            {
                return false;
            }
        }

        /*
         * Bao hàm callback với chức năng kiểm tra có đang chạy không trong
         * 
        */
        protected bool DoAutoAction(_AutoActionDelegate action, int delayAfterStep = 1000)
        {
            try
            {
                if (!LDPlayerHandler.CanRun)
                {
                    //throw new TaskCanceledException(string.Format("LDPlayer auto {0} stopped! at method {1}", LDPlayer.ID, "DO AUTO ACTION"));
                    return false;
                }
                var status = action();
                Thread.Sleep(delayAfterStep);
                return status;
            }
            catch
            {
                return false;
            }
        }

        /*
         * Chờ cho đến khi các pixel mong muốn cùng xuất hiện trên màn hình, nếu quá thời gian chờ sẽ ngừng
         * 
         */
        protected bool WaitUntilPixelAppear(List<ColorCompareMeta> equalColors, long maxWaitTime, int refreshRate = 1000)
        {
            if (!LDPlayerHandler.CanRun)
            {
                return false;
                //throw new TaskCanceledException(string.Format("LDPlayer auto {0} stopped! at method {1}", LDPlayer.ID, "WaitUntilPixelAppear"));
            }
            while (DateTimeOffset.Now.ToUnixTimeMilliseconds() <= maxWaitTime)
            {
                // Chụp lại ảnh màn hình
                CaptureScreen();
                using Bitmap bmp = new(".\\tmp\\" + LDPlayer.ID + ".jpg");
                Bitmap bitmap = new(bmp);
                if (!LDPlayer.IsRunning)
                {
                    bmp.Dispose();
                    bitmap.Dispose();
                    System.IO.File.Delete(".\\" + LDPlayer.ID + ".jpg");
                    return false;
                }
                // Nếu không tìm thấy ảnh
                if (!MatchAllPixel(bitmap, equalColors))
                {
                    Thread.Sleep(refreshRate);
                    // Chờ trước khi tiếp tục chụp để đảm bảo vấn đề hiệu năng
                    continue;
                }
                bmp.Dispose();
                System.IO.File.Delete(".\\" + LDPlayer.ID + ".jpg");
                // Tìm thấy ảnh
                return true;
            }
            // TIMEOUT hết thời gian chờ mà vẫn chưa tìm thấy ảnh
            return false;
        }

        protected bool MatchAllPixel(Bitmap bitmap, List<ColorCompareMeta> equalColors)
        {
            foreach (var color in equalColors)
            {
                var toEqual = bitmap.GetPixel(color.X, color.Y);
                var success = color.SuccessColor;
                if (success.R != toEqual.R || success.G != toEqual.G || success.B != toEqual.B)
                {
                    bitmap.Dispose();
                    //var id = LDPlayer.ID;
                    //FormStartup.Show(string.Format("LDPlayer({8}) RGB({0},{1},{2}) != RGB({3},{4}{5}) XY({6},{7})", toEqual.R, toEqual.G, toEqual.B, success.R, success.G, success.B, color.X, color.Y, id));
                    return false;
                }
            }
            bitmap.Dispose();
            // FormStartup.Show("SUCCESS");
            return true;
        }

    }


}
