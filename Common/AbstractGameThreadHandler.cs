using AutoViewWebAdsCSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoViewWebAdsCSharp.Common
{
    public abstract class GameThreadHandler<Emulator, Model>
        where Emulator : AndroidEmulator
    {
        /*
         * Danh sách dữ liệu tool tương tác
         *
         */
        protected List<Model> Data { get; set; }

        /*
         * Danh sách chứa giả lập
         * 
         */
        protected List<Emulator> Emulators { get; set; }

        public GameThreadHandler(List<Emulator> emulators, List<Model> init)
        {
            Emulators = emulators;
            Data = init;
        }

        /*
         * Chia danh sách dữ liệu cho các thread xử lý 
         * 
         */
        public abstract Dictionary<LDPlayer, List<Proxy>> DistributeDataToAllEmulators();

        /*
         * Cắt mảng và trả về mảng con
         * 
         */
        protected List<Model> Splice(int index, int count)
        {
            List<Model> splicedData = Data.GetRange(index, count);
            Data.RemoveRange(index, count);
            return splicedData;
        }

    }
}
