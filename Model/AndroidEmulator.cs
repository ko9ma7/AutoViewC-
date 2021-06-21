using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoViewWebAdsCSharp.Model
{
    public class AndroidEmulator
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public bool IsRunning { get; set; } = false;

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, IsRunning ? "Đang Chạy" : "Đã Tắt");
        }
    }
}
