using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoViewWebAdsCSharp.Model;
using AutoViewWebAdsCSharp.Common;
using System.Threading;

namespace AutoViewWebAdsCSharp.Controller
{
    public class AADSThreadHandler : GameThreadHandler<LDPlayer, Proxy>
    {

        public AADSThreadHandler(List<LDPlayer> lDPlayers, List<Proxy> data)
            : base(lDPlayers, data) { }

        public async Task Run()
        {
            try
            {
                List<Task> tasks = new();
                using var cancellationTokenSource = new CancellationTokenSource();
                foreach (var ld in DistributeDataToAllEmulators())
                {
                    try
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            AADSController tWinHandler = new(ld.Key);
                            tWinHandler.Proxies = ld.Value;
                            tWinHandler.CancellationToken = cancellationTokenSource.Token;
                            tWinHandler.Run();
                        }));
                    }
                    catch
                    {
                        //throw new TaskCanceledException(ex.ToString());
                    }
                }
                //await checkRunningTask;
                await Task.WhenAll(tasks).ContinueWith(_ =>
                {

                });
            }
            catch { }
        }

        public override Dictionary<LDPlayer, List<Proxy>> DistributeDataToAllEmulators()
        {
            Dictionary<LDPlayer, List<Proxy>> jobMap = new();
            int totalData = Data.Count;
            int totalThread = Emulators.Count;
            int mod = totalData % totalThread;
            // Dữ liệu lẻ thì cần cho 1 thread xử lý phần thừa giả 10 data với 3 thread sẽ có 1 thread cần xử lý 4 data
            int dataRemain = (int)Math.Floor((double)totalData / totalThread);
            for (int i = 0; i < Emulators.Count; i++)
            {
                int count = i == 0 ? dataRemain + mod : dataRemain;
                List<Proxy> dataPerThread = Splice(0, count);
                jobMap.Add(Emulators[i], dataPerThread);
            }
            return jobMap;
        }
    }
}
