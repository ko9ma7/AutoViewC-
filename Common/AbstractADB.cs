using AutoViewWebAdsCSharp.IO;
using AutoViewWebAdsCSharp.Model;

namespace AutoViewWebAdsCSharp.Common
{
    public class AbstractADB
    {

        public LDPlayer LDPlayer { get; set; }

        private string BaseCommand { get; }

        public AbstractADB(LDPlayer lDPlayer)
        {
            LDPlayer = lDPlayer;
            BaseCommand = string.Format("adb --index {0} --command ", lDPlayer.ID);
        }

        /*
         * Hàm Click Nhận Vào Simple Point
         */
        protected bool Click(object meta)
        {
            var p = (SimplePoint)meta;
            CommandLine.ExecuteCommandLine(BaseCommand + "\"shell input touchscreen tap " + p.X + " " + p.Y + "\"");
            return true;
        }

        protected bool ClearAppData(object meta)
        {
            AppMeta appMeta = (AppMeta)meta;
            CommandLine.ExecuteCommandLine(BaseCommand + "\"shell pm clear " + appMeta.PackageName + "\"");
            return true;
        }
        protected bool CaptureScreen()
        {
            // Capture Screen
            string commandCapture = BaseCommand + "\"shell screencap -p /sdcard/" + LDPlayer.ID + ".jpg\"";
            CommandLine.ExecuteCommandLine(commandCapture);
            // Copy capture image to tool
            string commandGet = BaseCommand + "\"pull /sdcard/" + LDPlayer.ID + ".jpg \".\\tmp\\" + LDPlayer.ID + ".jpg\"\"";
            CommandLine.ExecuteCommandLine(commandGet);
            return true;
        }

        protected bool OpenApp(object meta)
        {
            AppMeta appMeta = (AppMeta)meta;
            string query = string.Format("runapp --index {0} --packagename {1}", LDPlayer.ID, appMeta.PackageName);
            CommandLine.ExecuteCommandLine(query);
            return true;
        }

        protected bool CloseApp(object meta)
        {
            AppMeta appMeta = (AppMeta)meta;
            string query = string.Format("killapp --index {0} --packagename {1}", LDPlayer.ID, appMeta.PackageName);
            CommandLine.ExecuteCommandLine(query);
            return true;
        }

        protected bool InputText(object meta)
        {
            string text = (string)meta;
            string command = BaseCommand + "\"shell input text \"" + text.ToString() + "\"\"";
            CommandLine.ExecuteCommandLine(command);
            return true;
        }

        protected bool ClearTextBox(int length = 10)
        {
            string command = BaseCommand + "\"shell input keyevent KEYCODE_DEL\"";
            for (int i = 0; i < length; i++)
            {
                CommandLine.ExecuteCommandLine(command);
            }
            return true;
        }

        protected bool Scroll(object meta)
        {
            ScrollPoint scrollPoint = (ScrollPoint)meta;
            string command = string
                .Format("{0} \" shell input swipe {1} {2} {3} {4} {5} \"", BaseCommand, scrollPoint.X, scrollPoint.Y, scrollPoint.TargetX, scrollPoint.TargetY, scrollPoint.Duration);
            CommandLine.ExecuteCommandLine(command);
            return true;
        }

        protected bool PressKey(object meta)
        {
            string key = (string)meta;
            string command = string
                .Format("{0} \" shell input keyevent {1}\"", BaseCommand, key);
            CommandLine.ExecuteCommandLine(command);
            return true;
        }

        protected bool ConnectProxy(object meta)
        {
            Proxy proxy = (Proxy)meta;
            string command = string.Format("{0} \" shell settings put global http_proxy {1}:{2}\"", BaseCommand, proxy.IP, proxy.Port);
            if (proxy.RequireAuthenticate)
            {
                //command = string.Format("{0} \" shell settings put global http_proxy {1}:{2}\"", BaseCommand, proxy.IP, proxy.Port);
            }
            if (proxy.Reset)
            {
                command = string.Format("{0} \" shell settings put global http_proxy :0 \"", BaseCommand);
            }
            CommandLine.ExecuteCommandLine(command);
            return true;
        }


    }
}
