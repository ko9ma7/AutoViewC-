using AutoViewWebAdsCSharp.Common;
using AutoViewWebAdsCSharp.Controller;
using AutoViewWebAdsCSharp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoViewWebAdsCSharp
{
    public partial class FormStartup : Form
    {
        private readonly LDPlayerHandler ldHandler = null;

        public FormStartup()
        {
            InitializeComponent();
            ldHandler = LDPlayerHandler.Instance;
            UpdateLDUIState();
            richTextProxyList.AppendText("140.227.65.129:58888\n");
            richTextProxyList.AppendText("178.63.17.151:3128\n");
            richTextProxyList.AppendText("140.227.67.141:58888\n");
            richTextProxyList.AppendText("203.75.190.21:80\n");
        }

        // Method để cập nhật lại danh sách trạng tahis các máy ảo LDPlayer trong hệ thống
        private void UpdateLDUIState()
        {
            checkedListBoxRunning.Items.Clear();
            var listRunning = ldHandler.GetRunningLDPlayers("AADS");
            foreach (var ld in listRunning)
            {
                checkedListBoxRunning.Items.Add(ld.Value, ld.Value.IsRunning);
            }
        }

        private async void ButtonStartAuto_Click(object sender, EventArgs e)
        {
            // Cho phép chạy
            LDPlayerHandler.CanRun = true;
            var listRunning = ldHandler.GetRunningLDPlayers("AADS");
            // Lấy dữ liệu các máy ảo cần chạy
            var runners = checkedListBoxRunning.CheckedItems;
            // Trường hợp không chọn máy ảo nào để chạy
            if (runners.Count == 0)
            {
                MessageBox.Show(this, "Chưa chọn máy ảo để chạy", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<LDPlayer> toRun = new();
            // Kiểm tra xem máy ảo đã chọn có thực sự đang chạy hay không, nếu có bất kỳ sai xót nào thì ngưng chạy và update lại trạng thái
            foreach (var item in runners)
            {
                var ldPlayer = (LDPlayer)item;
                var actual = listRunning[ldPlayer.ID];
                if (!actual.IsRunning)
                {
                    UpdateLDUIState();
                    MessageBox.Show(this, "Dữ liệu máy ảo đang chạy không khớp\n\n Kiểm tra lại xem máy ảo đã được bật!", "Lỗi Trạng Thái", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                toRun.Add(ldPlayer);
            }
            buttonStartAuto.Enabled = false;
            buttonStopAuto.Enabled = true;
            try
            {
                AADSThreadHandler aadsHandler = new(toRun, GetListProxy());
                await aadsHandler.Run()
                        .ContinueWith(_ =>
                {
                    LDPlayerHandler.CanRun = false;
                });
                //MessageBox.Show(this, "Đã Hoàn Thành", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                buttonStartAuto.Enabled = true;
                buttonStopAuto.Enabled = false;
            }
        }

        private List<Proxy> GetListProxy()
        {
            var proxyRawList = new List<string>(
                richTextProxyList.Text
                .Trim()
                .Split("\n")
                );
            List<Proxy> proxies = new();
            foreach (var proxy in proxyRawList)
            {
                try
                {
                    var parse = proxy.Split(":");
                    proxies.Add(new Proxy() { IP = parse[0], Port = parse[1] });
                }
                catch
                {
                    continue;
                }
            }
            return proxies;
        }

        private void ButtonUpdateVMs_Click(object sender, EventArgs e)
        {
            UpdateLDUIState();
        }

        private void ButtonStopAuto_Click(object sender, EventArgs e)
        {
            LDPlayerHandler.CanRun = false;
            buttonStartAuto.Enabled = true;
            buttonStopAuto.Enabled = false;
        }

        private void CheckedListBoxRunning_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Kiểm tra là trạng thái đã bấm
            if (e.NewValue.ToString().ToLower().Equals("checked"))
            {
                var current = (LDPlayer)checkedListBoxRunning.Items[e.Index];
                // Nếu máy ảo đang chọn chưa chạy thì báo lỗi
                if (!current.IsRunning)
                {
                    MessageBox.Show(this, "Máy Ảo Này Chưa Được Bật\nBạn có muốn bật lên không?", "Lỗi Trạng Thái", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Muốn bật máy ảo lên
                    LDPlayerHandler.Instance.OpenEmulator(int.Parse(current.ID));
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LDPlayer l2 = new LDPlayer();
            l2.ID = "7";
            l2.IsRunning = true;
            AADSController hand = new(l2);
            hand.TesCapture();
        }
    }

}
