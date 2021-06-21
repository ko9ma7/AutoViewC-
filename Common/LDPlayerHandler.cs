using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using AutoViewWebAdsCSharp.IO;

namespace AutoViewWebAdsCSharp.Common
{
    public class LDPlayerHandler
    {
        private static readonly Lazy<LDPlayerHandler> lazy
                                     = new(() => new LDPlayerHandler());

        // Sử dụng Singleton Pattern
        public static LDPlayerHandler Instance => lazy.Value;
        public static bool CanRun { get; set; } = false;

        // Đặt constructor là private để không khởi tạo được Object thông qua constructor trực tiếp, mà cần sử dụng Singleton Pattern
        private LDPlayerHandler()
        {
            Process.Start(@"D:\LDPlayer\LDPlayer3.0\ldconsole.exe");
            //CommandLineInterface.ExecuteAsAdmin(@"D:\LDPlayer\LDPlayer3.0\dnconsole.exe");
        }

        // Chuyển đổi 1 chuỗi thành mảng
        private static List<string> ConvertStringLinesToList(string data, string delimiter = "\r\n")
        {
            return data
                     .Trim() // Trim để loại bỏ khoảng trắng thừa ở đầu và ở cuối
                     .Split(delimiter) // Cắt theo dấu xuống dòng và thụt dòng
                     .ToList(); // Biến đổi Array thành List
        }

        public void OpenEmulator(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }
            CommandLine.ExecuteCommandLine(string.Format("launch --name {0}", name));
        }

        public void OpenEmulator(int index)
        {
            if (index < 0) throw new ArgumentException($"'{nameof(index)}' cannot less than 0.", nameof(index));
            CommandLine.ExecuteCommandLine(string.Format("launch --index {0}", index));
        }

        public Dictionary<string, Model.LDPlayer> GetRunningLDPlayers(string contains)
        {
            // Thực thi command line "list2" trả về thông tin các giả lập
            string cmdExecResult = CommandLine.ExecuteCommandLine("list2");
            /* 
             * Kết quả thực thi của cmd list2 có nhiều dòng, mỗi dòng đại diện cho 1 máy ảo
             * Ta cần cắt ra theo định dạng mỗi dòng là 1 dữ liệu cho máy ảo
            */
            var resultArray = ConvertStringLinesToList(cmdExecResult);
            Dictionary<string, Model.LDPlayer> ldPlayers = new();
            foreach (var r in resultArray)
            {
                // Định dạng của dữ liệu là index,name,... nên cần cắt ra theo dấu phẩy (",")
                var row = r.Split(",");
                // Khởi tạo đối tượng LDPlayer mới
                var ld = new Model.LDPlayer
                {
                    ID = row[0],
                    Name = row[1],
                    // Nếu pid và pid của vbox đều là -1 thì máy ảo chưa được bật
                    IsRunning = row[4].Equals("1")
                };
                // Lấy nhóm giả lập theo tên
                if (contains != null && contains.Trim().Length > 0 && !row[1].ToLower().Contains(contains.ToLower()))
                {
                    continue;
                }
                // Thêm đối tượng LDPlayer vào mảng
                ldPlayers.Add(ld.ID, ld);
            }
            return ldPlayers;
        }

    }
}
