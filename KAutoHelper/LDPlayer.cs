using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace KAutoHelper
{
    // Token: 0x02000017 RID: 23
    public class LDPlayer
    {
        // Token: 0x060000A6 RID: 166 RVA: 0x00005B1C File Offset: 0x00003D1C
        public void Open(string param, string NameOrId)
        {
            this.ExecuteLD(string.Format("launch --{0} {1}", param, NameOrId));
        }

        // Token: 0x060000A7 RID: 167 RVA: 0x00005B32 File Offset: 0x00003D32
        public void Open_App(string param, string NameOrId, string Package_Name)
        {
            this.ExecuteLD(string.Format("launchex --{0} {1} --packagename {2}", param, NameOrId, Package_Name));
        }

        // Token: 0x060000A8 RID: 168 RVA: 0x00005B49 File Offset: 0x00003D49
        public void Close(string param, string NameOrId)
        {
            this.ExecuteLD(string.Format("quit --{0} {1}", param, NameOrId));
        }

        // Token: 0x060000A9 RID: 169 RVA: 0x00005B5F File Offset: 0x00003D5F
        public void CloseAll()
        {
            this.ExecuteLD("quitall");
        }

        // Token: 0x060000AA RID: 170 RVA: 0x00005B6E File Offset: 0x00003D6E
        public void ReBoot(string param, string NameOrId)
        {
            this.ExecuteLD(string.Format("reboot --{0} {1}", param, NameOrId));
        }

        // Token: 0x060000AB RID: 171 RVA: 0x00005B84 File Offset: 0x00003D84
        public void Create(string Name)
        {
            this.ExecuteLD("add --name " + Name);
        }

        // Token: 0x060000AC RID: 172 RVA: 0x00005B99 File Offset: 0x00003D99
        public void Copy(string Name, string From_NameOrId)
        {
            this.ExecuteLD(string.Format("copy --name {0} --from {1}", Name, From_NameOrId));
        }

        // Token: 0x060000AD RID: 173 RVA: 0x00005BAF File Offset: 0x00003DAF
        public void Delete(string param, string NameOrId)
        {
            this.ExecuteLD(string.Format("remove --{0} {1}", param, NameOrId));
        }

        // Token: 0x060000AE RID: 174 RVA: 0x00005BC5 File Offset: 0x00003DC5
        public void ReName(string param, string NameOrId, string title_new)
        {
            this.ExecuteLD(string.Format("rename --{0} {1} --title {2}", param, NameOrId, title_new));
        }

        // Token: 0x060000AF RID: 175 RVA: 0x00005BDC File Offset: 0x00003DDC
        public void InstallApp_File(string param, string NameOrId, string File_Name)
        {
            this.ExecuteLD(string.Format("installapp --{0} {1} --filename \"{2}\"", param, NameOrId, File_Name));
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x00005BF3 File Offset: 0x00003DF3
        public void InstallApp_Package(string param, string NameOrId, string Package_Name)
        {
            this.ExecuteLD(string.Format("installapp --{0} {1} --packagename {2}", param, NameOrId, Package_Name));
        }

        // Token: 0x060000B1 RID: 177 RVA: 0x00005C0A File Offset: 0x00003E0A
        public void UnInstallApp(string param, string NameOrId, string Package_Name)
        {
            this.ExecuteLD(string.Format("uninstallapp --{0} {1} --packagename {2}", param, NameOrId, Package_Name));
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00005C21 File Offset: 0x00003E21
        public void RunApp(string param, string NameOrId, string Package_Name)
        {
            this.ExecuteLD(string.Format("runapp --{0} {1} --packagename {2}", param, NameOrId, Package_Name));
        }

        // Token: 0x060000B3 RID: 179 RVA: 0x00005C38 File Offset: 0x00003E38
        public void KillApp(string param, string NameOrId, string Package_Name)
        {
            this.ExecuteLD(string.Format("killapp --{0} {1} --packagename {2}", param, NameOrId, Package_Name));
        }

        // Token: 0x060000B4 RID: 180 RVA: 0x00005C4F File Offset: 0x00003E4F
        public void Locate(string param, string NameOrId, string Lng, string Lat)
        {
            this.ExecuteLD(string.Format("locate --{0} {1} --LLI {2},{3}", new object[]
            {
                param,
                NameOrId,
                Lng,
                Lat
            }));
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x00005C7A File Offset: 0x00003E7A
        public void Change_Property(string param, string NameOrId, string cmd)
        {
            this.ExecuteLD(string.Format("modify --{0} {1} {2}", param, NameOrId, cmd));
        }

        // Token: 0x060000B6 RID: 182 RVA: 0x00005C91 File Offset: 0x00003E91
        public void SetProp(string param, string NameOrId, string key, string value)
        {
            this.ExecuteLD(string.Format("setprop --{0} {1} --key {2} --value {3}", new object[]
            {
                param,
                NameOrId,
                key,
                value
            }));
        }

        // Token: 0x060000B7 RID: 183 RVA: 0x00005CBC File Offset: 0x00003EBC
        public string GetProp(string param, string NameOrId, string key)
        {
            return this.ExecuteLD_Result(string.Format("getprop --{0} {1} --key {2}", param, NameOrId, key));
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x00005CE4 File Offset: 0x00003EE4
        public string ADB(string param, string NameOrId, string cmd)
        {
            return this.ExecuteLD_Result(string.Format("adb --{0} {1} --command {2}", param, NameOrId, cmd));
        }

        // Token: 0x060000B9 RID: 185 RVA: 0x00005D09 File Offset: 0x00003F09
        public void DownCPU(string param, string NameOrId, string rate)
        {
            this.ExecuteLD(string.Format("downcpu --{0} {1} --rate {2}", param, NameOrId, rate));
        }

        // Token: 0x060000BA RID: 186 RVA: 0x00005D20 File Offset: 0x00003F20
        public void Backup(string param, string NameOrId, string file_path)
        {
            this.ExecuteLD(string.Format("backup --{0} {1} --file \"{2}\"", param, NameOrId, file_path));
        }

        // Token: 0x060000BB RID: 187 RVA: 0x00005D37 File Offset: 0x00003F37
        public void Restore(string param, string NameOrId, string file_path)
        {
            this.ExecuteLD(string.Format("restore --{0} {1} --file \"{2}\"", param, NameOrId, file_path));
        }

        // Token: 0x060000BC RID: 188 RVA: 0x00005D4E File Offset: 0x00003F4E
        public void Action(string param, string NameOrId, string key, string value)
        {
            this.ExecuteLD(string.Format("action --{0} {1} --key {2} --value {3}", new object[]
            {
                param,
                NameOrId,
                key,
                value
            }));
        }

        // Token: 0x060000BD RID: 189 RVA: 0x00005D79 File Offset: 0x00003F79
        public void Scan(string param, string NameOrId, string file_path)
        {
            this.ExecuteLD(string.Format("scan --{0} {1} --file {2}", param, NameOrId, file_path));
        }

        // Token: 0x060000BE RID: 190 RVA: 0x00005D90 File Offset: 0x00003F90
        public void SortWnd()
        {
            this.ExecuteLD("sortWnd");
        }

        // Token: 0x060000BF RID: 191 RVA: 0x00005D9F File Offset: 0x00003F9F
        public void zoomIn(string param, string NameOrId)
        {
            this.ExecuteLD(string.Format("zoomIn --{0} {1}", param, NameOrId));
        }

        // Token: 0x060000C0 RID: 192 RVA: 0x00005DB5 File Offset: 0x00003FB5
        public void zoomOut(string param, string NameOrId)
        {
            this.ExecuteLD(string.Format("zoomOut --{0} {1}", param, NameOrId));
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x00005DCB File Offset: 0x00003FCB
        public void Pull(string param, string NameOrId, string remote_file_path, string local_file_path)
        {
            this.ExecuteLD(string.Format("pull --{0} {1} --remote \"{2}\" --local \"{3}\"", new object[]
            {
                param,
                NameOrId,
                remote_file_path,
                local_file_path
            }));
        }

        // Token: 0x060000C2 RID: 194 RVA: 0x00005DF6 File Offset: 0x00003FF6
        public void Push(string param, string NameOrId, string remote_file_path, string local_file_path)
        {
            this.ExecuteLD(string.Format("push --{0} {1} --remote \"{2}\" --local \"{3}\"", new object[]
            {
                param,
                NameOrId,
                remote_file_path,
                local_file_path
            }));
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x00005E21 File Offset: 0x00004021
        public void BackupApp(string param, string NameOrId, string Package_Name, string file_path)
        {
            this.ExecuteLD(string.Format("backupapp --{0} {1} --packagename {2} --file \"{3}\"", new object[]
            {
                param,
                NameOrId,
                Package_Name,
                file_path
            }));
        }

        // Token: 0x060000C4 RID: 196 RVA: 0x00005E4C File Offset: 0x0000404C
        public void RestoreApp(string param, string NameOrId, string Package_Name, string file_path)
        {
            this.ExecuteLD(string.Format("restoreapp --{0} {1} --packagename {2} --file \"{3}\"", new object[]
            {
                param,
                NameOrId,
                Package_Name,
                file_path
            }));
        }

        // Token: 0x060000C5 RID: 197 RVA: 0x00005E77 File Offset: 0x00004077
        public void Golabal_Config(string param, string NameOrId, string fps, string audio, string fast_play, string clean_mode)
        {
            this.ExecuteLD(string.Format("globalsetting --{0} {1} --audio {2} --fastplay {3} --cleanmode {4}", new object[]
            {
                param,
                NameOrId,
                audio,
                fast_play,
                clean_mode
            }));
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x00005EA8 File Offset: 0x000040A8
        public List<string> GetDevices()
        {
            string[] array = this.ExecuteLD_Result("list").Trim().Split(new char[]
            {
                '\n'
            });
            for (int i = 0; i < array.Length; i++)
            {
                bool flag = array[i] == "";
                if (flag)
                {
                    return new List<string>();
                }
                array[i] = array[i].Trim();
            }
            return array.ToList<string>();
        }

        // Token: 0x060000C7 RID: 199 RVA: 0x00005F1C File Offset: 0x0000411C
        public List<string> GetDevices_Running()
        {
            string[] array = this.ExecuteLD_Result("runninglist").Trim().Split(new char[]
            {
                '\n'
            });
            for (int i = 0; i < array.Length; i++)
            {
                bool flag = array[i] == "";
                if (flag)
                {
                    return new List<string>();
                }
                array[i] = array[i].Trim();
            }
            return array.ToList<string>();
        }

        // Token: 0x060000C8 RID: 200 RVA: 0x00005F90 File Offset: 0x00004190
        public bool IsDevice_Running(string param, string NameOrId)
        {
            string a = this.ExecuteLD_Result(string.Format("isrunning --{0} {1}", param, NameOrId)).Trim();
            return a == "running";
        }

        // Token: 0x060000C9 RID: 201 RVA: 0x00005FD0 File Offset: 0x000041D0
        public List<Info_Devices> GetDevices2()
        {
            List<Info_Devices> result;
            try
            {
                List<Info_Devices> list = new List<Info_Devices>();
                string[] array = this.ExecuteLD_Result("list2").Trim().Split(new char[]
                {
                    '\n'
                });
                for (int i = 0; i < array.Length; i++)
                {
                    Info_Devices item = default(Info_Devices);
                    string[] array2 = array[i].Trim().Split(new char[]
                    {
                        ','
                    });
                    item.index = int.Parse(array2[0]);
                    item.name = array2[1];
                    item.adb_id = "-1";
                    list.Add(item);
                }
                result = list;
            }
            catch
            {
                result = new List<Info_Devices>();
            }
            return result;
        }

        // Token: 0x060000CA RID: 202 RVA: 0x00006090 File Offset: 0x00004290
        public List<Info_Devices> GetDevices2_Running()
        {
            List<Info_Devices> result;
            try
            {
                int num = 0;
                List<string> devices = ADBHelper.GetDevices();
                List<Info_Devices> list = new List<Info_Devices>();
                List<string> devices_Running = this.GetDevices_Running();
                string[] array = this.ExecuteLD_Result("list2").Trim().Split(new char[]
                {
                    '\n'
                });
                for (int i = 0; i < array.Length; i++)
                {
                    Info_Devices info_Devices = default(Info_Devices);
                    string[] array2 = array[i].Trim().Split(new char[]
                    {
                        ','
                    });
                    info_Devices.index = int.Parse(array2[0]);
                    info_Devices.name = array2[1];
                    bool flag = devices_Running.Contains(info_Devices.name);
                    if (flag)
                    {
                        info_Devices.adb_id = devices[num];
                        list.Add(info_Devices);
                        num++;
                    }
                }
                result = list;
            }
            catch
            {
                result = new List<Info_Devices>();
            }
            return result;
        }

        // Token: 0x060000CB RID: 203 RVA: 0x00006188 File Offset: 0x00004388
        public void ExecuteLD(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = LDPlayer.pathLD;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        // Token: 0x060000CC RID: 204 RVA: 0x00006200 File Offset: 0x00004400
        public string ExecuteLD_Result(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = LDPlayer.pathLD,
                    Arguments = cmdCommand,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000CD RID: 205 RVA: 0x00006298 File Offset: 0x00004498
        public void Back(string deviceID)
        {
            ADBHelper.Key(deviceID, ADBKeyEvent.KEYCODE_BACK);
        }

        // Token: 0x060000CE RID: 206 RVA: 0x000062A3 File Offset: 0x000044A3
        public void Home(string deviceID)
        {
            ADBHelper.Key(deviceID, ADBKeyEvent.KEYCODE_HOME);
        }

        // Token: 0x060000CF RID: 207 RVA: 0x000062AE File Offset: 0x000044AE
        public void Menu(string deviceID)
        {
            ADBHelper.Key(deviceID, ADBKeyEvent.KEYCODE_APP_SWITCH);
        }

        // Token: 0x060000D0 RID: 208 RVA: 0x000062C0 File Offset: 0x000044C0
        public void Tap_Img(string deviceID, Bitmap ImgFind)
        {
            //vkl
            //Bitmap subBitmap = (Bitmap)ImgFind.Clone();
            //Bitmap mainBitmap = ADBHelper.ScreenShoot(deviceID, true, "screenShoot.png");
            //Point? point = ImageScanOpenCV.FindOutPoint(mainBitmap, subBitmap, 0.9);
            //bool flag = point != null;
            //if (flag)
            //{
            //	ADBHelper.Tap(deviceID, point.Value.X, point.Value.Y, 1);
            //}
        }

        // Token: 0x060000D1 RID: 209 RVA: 0x0000632E File Offset: 0x0000452E
        public void Change_Proxy(string deviceID, string ip_proxy, string port_proxy)
        {
            ADBHelper.ExecuteCMD(string.Format("adb -s {0} shell settings put global http_proxy {1}:{2}", deviceID, ip_proxy, port_proxy));
        }

        // Token: 0x060000D2 RID: 210 RVA: 0x00006344 File Offset: 0x00004544
        public void Remove_Proxy(string deviceID)
        {
            ADBHelper.ExecuteCMD(string.Format("adb -s {0} shell settings put global http_proxy :0", deviceID));
        }

        // Token: 0x040001A3 RID: 419
        public static string pathLD = "C:\\LDPlayer\\LDPlayer4.0\\ldconsole.exe";
    }
}
