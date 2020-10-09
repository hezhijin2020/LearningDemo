#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：f2fb390e-e628-46c5-a38a-e7dc1ef5bab2
// 文件名：DxPublic
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-26 14:29:27
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-26 14:29:27
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace HZJ.DxWinForm.Utility.CommCls
{
    /// <summary>
    /// 公用帮助类
    /// </summary>
    public class DxPublic
    {
        #region  提示消息处理方法


        /// <summary>
        /// 询问提示方法
        /// </summary>
        /// <param name="Question">询问内容</param>
        /// <param name="Caption">提示框名称</param>
        /// <returns></returns>
        public static bool GetMessageBoxYesNoResult(string Question, string Caption = "系统提示")
        {
            return XtraMessageBox.Show(Question, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// 异常信息提示
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="Caption">提示框名称</param>
        public static void ShowException(System.Exception ex, string Caption = "系统提示")
        {
            XtraMessageBox.Show(ex.Message, Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        /// <summary>
        /// 异常提示方法
        /// </summary>
        /// <param name="Content">内容</param>
        /// <param name="Caption">提示框名称</param>
        /// <returns></returns>
        public static void ShowException(string Content, string Caption = "系统提示")
        {
            XtraMessageBox.Show(Content, Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        /// <summary>
        /// 异常信息提示
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="Caption">提示框名称</param>
        public static void ShowExceptionFormat(Exception ex, string Caption = "系统提示")
        {
            string content = string.Format("Message:{0} {1} Source:{2} {1} StackTrace:{3}", new object[]
            {
                ex.Message,
                System.Environment.NewLine,
                ex.Source,
                ex.StackTrace
            });
            DxPublic.ShowMessage(MessageBoxIcon.Hand, content, Caption);
        }

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="Content">内容</param>
        /// <param name="Caption">提示框名称</param>
        public static void ShowMessage(string Content, string Caption = "系统提示")
        {
            MessageBox.Show(Content, Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="Content">内容</param>
        /// <param name="Caption">提示框名称</param>
        public static void ShowMessage(MessageBoxIcon type, string Content, string Caption = "系统提示")
        {
            MessageBoxIcon icon;
            if (type == MessageBoxIcon.Exclamation)
            {
                icon = MessageBoxIcon.Exclamation;
            }
            else if (type == MessageBoxIcon.Hand)
            {
                icon = MessageBoxIcon.Hand;
            }
            else
            {
                icon = MessageBoxIcon.Asterisk;
            }
            MessageBox.Show(Content, Caption, MessageBoxButtons.OK, icon);
        }

        #endregion

        #region 获取网络信息
        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetLocalMac()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "unknown";
            }
        }

        /// <summary>  
        /// 获取当前使用的IP
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetLocalIP()
        {
            string result = RunCmd("route", "print", true);
            Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
            if (m.Success)
            {
                return m.Groups[2].Value;
            }
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                    c.Connect("www.baidu.com", 80);
                    string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                    c.Close();
                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取本机主DNS
        /// </summary>
        /// <returns></returns>
        public static string GetPrimaryDNS()
        {
            string result = RunCmd("nslookup", "", true);
            Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 运行一个控制台程序并返回其输出参数。
        /// </summary>
        /// <param name="filename">程序名</param>
        /// <param name="arguments">输入参数</param>
        /// <returns></returns>
        public static string RunCmd(string filename, string arguments, bool recordLog)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    Thread.Sleep(100);          //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应
                    if (!proc.HasExited)         //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行
                    {                            //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行
                        proc.Kill();
                    }
                    string txt = sr.ReadToEnd();
                    sr.Close();
                    if (recordLog)
                        Trace.WriteLine(txt);
                    return txt;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }


        #endregion

        #region 格式转换

       /// <summary>
       /// 检查类型是否为空
       /// </summary>
       /// <param name="theType">类型</param>
       /// <returns></returns>
        public static bool IsNullableType(System.Type theType)
        {
            return theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(System.Nullable<>));
        }

        /// <summary>
        /// 检查对象是否为空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsObjNull(object obj)
        {
            return obj == null || obj == System.DBNull.Value;
        }

       /// <summary>
       /// 对象转成String数型
       /// </summary>
       /// <param name="obj">对象</param>
       /// <returns></returns>
        public static string GetObjString(object obj)
        {
            if (obj != null && obj != System.DBNull.Value && !(obj.ToString() == ""))
            {
                return obj.ToString();
            }
            return "";
        }

        /// <summary>
        /// Obj转成大定的String类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string GetObjStrUpperTrim(object obj)
        {
            return DxPublic.GetObjString(obj).Trim().ToUpper();
        }

        /// <summary>
        /// Obj转成GUID
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static System.Guid GetObjGUID(object obj)
        {
            string objectString = DxPublic.GetObjString(obj);
            System.Guid result;
            if (!System.Guid.TryParse(objectString, out result))
            {
                result = System.Guid.Parse("00000000-0000-0000-0000-000000000000");
            }
            return result;
        }

        /// <summary>
        /// 字节数组转泛型对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="Bytes">字节数组</param>
        /// <returns></returns>
        public static T GetBytesToObj<T>(byte[] Bytes) where T : class
        {
            T result;
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(Bytes))
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                result = (T)((object)formatter.Deserialize(memoryStream));
            }
            return result;
        }

        /// <summary>
        /// 对象转字节数组
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static byte[] GetObjToBytes(object obj)
        {
            byte[] buffer;
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                buffer = memoryStream.GetBuffer();
            }
            return buffer;
        }

        /// <summary>
        /// OBJECT类型转时间格式
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns></returns>
        public static DateTime GetObjDateTime(object obj)
        {
            if (DxPublic.IsObjNull(obj))
            {
                return new System.DateTime(2020, 1, 1);
            }
            return System.Convert.ToDateTime(obj);
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static bool GetFileExists(string FileName)
        {
            return System.IO.File.Exists(FileName);
        }

        /// <summary>
        /// 获取路径的上级路径
        /// </summary>
        /// <param name="Dir">路径</param>
        /// <returns></returns>
        public static string GetParentDir(string Dir)
        {
            string text = Dir;
            if (!text.EndsWith("\\"))
            {
                text += "\\";
            }
            text += "..\\";
            return System.IO.Path.GetFullPath(text);
        }

        /// <summary>
        /// 获取文件所在的文件夹
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string GetFolder(string FileName)
        {
            System.IO.FileInfo fileInfo = null;
            string directoryName;
            try
            {
                fileInfo = new System.IO.FileInfo(FileName);
                directoryName = fileInfo.DirectoryName;
            }
            finally
            {
                if (fileInfo != null)
                {
                    fileInfo = null;
                }
            }
            return directoryName;
        }

        /// <summary>
        /// 检件文件是否存在，不存在则创建
        /// </summary>
        /// <param name="Folder"></param>
        public static void CheckFileExists(string FileName)
        {
            if (!GetFileExists(FileName))
            {
                System.IO.File.Create(FileName);
            }
        }

        /// <summary>
        /// 检件文件夹是否存在，不存在则创建
        /// </summary>
        /// <param name="Folder"></param>
        public static void CheckFolderExists(string Folder)
        {
            if (!System.IO.Directory.Exists(Folder))
            {
                System.IO.Directory.CreateDirectory(Folder);
            }
        }

        /// <summary>
        /// 检件文件夹是否存在，不存在则创建
        /// </summary>
        /// <param name="FileName">文件名</param>
        public static void CheckFolderExistsViaFileName(string FileName)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(FileName);
            if (fileInfo != null)
            {
                DxPublic.CheckFolderExists(fileInfo.DirectoryName);
            }
        }

        /// <summary>
        /// 检件文件夹是否存在，不存在则创建
        /// </summary>
        public static void CheckFolderExists(string BaseDir, string SubDir)
        {
            string folder = string.Format("{0}\\{1}", BaseDir, SubDir);
            CheckFolderExists(folder);
        }

        /// <summary>
        /// 下载URL转文件路径
        /// </summary>
        /// <param name="Url">下载URL</param>
        /// <param name="ToDir">存储路径</param>
        public static void CheckDownUrl(ref string Url, ref string ToDir)
        {
            if (Url.StartsWith("\\\\"))
            {
                Url = Url.Replace('/', '\\');
            }
            else
            {
                Url = Url.Replace('\\', '/');
            }
            ToDir = ToDir.Replace('/', '\\');
        }

        /// <summary>
        /// CMD命令方式打开文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        public static void OpenFile(string FileName)
        {
            FileName = FileName.ToLower();
            FileName = FileName.Replace("documents and settings", "docume~1");
            Process process = null;
            try
            {
                process = new Process();
                process.StartInfo.FileName = "Cmd.exe";
                process.StartInfo.Arguments = string.Format(" /c start {0}", FileName);
                process.Start();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (process != null)
                {
                    process.Dispose();
                }
            }
        }

        /// <summary>
        /// 读取文件文本信息
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string ReadTextFile(string FileName)
        {
            if (!DxPublic.GetFileExists(FileName))
            {
                throw new System.Exception("文件不存在！!");
            }
            System.IO.StreamReader streamReader = null;
            string result;
            try
            {
                streamReader = new System.IO.StreamReader(FileName, System.Text.Encoding.UTF8);
                result = streamReader.ReadToEnd();
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        public static void DeleteFile(string FileName)
        {
            try
            {
                if (System.IO.File.Exists(FileName))
                {
                    System.IO.File.Delete(FileName);
                }
            }
            catch
            {
                throw new Exception("删除文件出错文件名:" + FileName);
            }
        }

        /// <summary>
        /// 将文件信息写入文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="ConTent">文本信息</param>
        public static void WriteTextFile(string FileName, string ConTent)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                streamWriter = new System.IO.StreamWriter(FileName, false);
                streamWriter.Write(ConTent);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// 将文件信息写入文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="ConTent">文本信息</param>
        /// <param name="Append">是否是追加</param>
        public static void WriteTextFile(string FileName, string ConTent, bool Append)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                streamWriter = new System.IO.StreamWriter(FileName, Append);
                streamWriter.Write(ConTent);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// 将文件信息以新行的方式写入文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="ConTent">文本信息</param>
        /// <param name="Append">是否是追加</param>
        public static void WriteTextFileLine(string FileName, string ConTent, bool Append)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(FileName);
                if (fileInfo.Exists && (double)((float)fileInfo.Length / 1048576f) > 1.0)
                {
                    streamWriter = new System.IO.StreamWriter(FileName, false);
                }
                else
                {
                    streamWriter = new System.IO.StreamWriter(FileName, Append);
                }
                streamWriter.WriteLine(ConTent);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取文件所在的目录
        /// </summary>
        /// <param name="FullFileName">文件名</param>
        /// <returns></returns>
        public static string GetPathOnly(string FullFileName)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(FullFileName);
            return fileInfo.DirectoryName;
        }

        /// <summary>
        /// 获取应用程序根目录
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationBaseDir()
        {
            string text = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!text.EndsWith("\\"))
            {
                text += "\\";
            }
            return text;
        }

        /// <summary>
        /// 获取文件名称（简称）
        /// </summary>
        /// <param name="FileName"> 文件名全称（带路径）</param>
        /// <returns></returns>
        public static string GetShortFileName(string FileName)
        {
            int num = FileName.LastIndexOf('\\');
            if (num < 0)
            {
                return FileName;
            }
            return FileName.Substring(num + 1, FileName.Length - num - 1);
        }

        /// <summary>
        /// 将字符串数组组合成一个路径。
        /// </summary>
        /// <param name="Para">字符串数组组合</param>
        /// <returns></returns>
        public static string CombineDir(params string[] Para)
        {
            return System.IO.Path.Combine(Para);
        }




        #region 文件打开与保存对话框
        /// <summary>
        /// 获取保存的XLS文件名称
        /// </summary>
        /// <param name="DefaultFileName">默认文件名</param>
        /// <returns></returns>
        public static string GetSaveXlsFileName(string DefaultFileName)
        {
            return DxPublic.GetSaveFileName("EXCEL File(*.xls)|*.xls", "", DefaultFileName);
        }

        /// <summary>
        /// 获取打开的XLS文件名称
        /// </summary>
        /// <param name="DefaultFileName">默认文件名</param>
        /// <returns></returns>
        public static string GetOpenXlsFileName(string DefaultFileName)
        {
            return DxPublic.GetOpenFileName("EXCEL File(*.xls)|*.xls", DefaultFileName);
        }

        /// <summary>
        /// 获取打开文件名称
        /// </summary>
        /// <param name="Filter">文件类型</param>
        /// <param name="FileName">文件名称</param>
        /// <returns></returns>
        public static string GetOpenFileName(string Filter, string FileName = "")
        {
            OpenFileDialog openFileDialog = null;
            string result;
            try
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = Filter;
                if (!string.Empty.Equals(FileName))
                {
                    openFileDialog.FileName = FileName;
                }
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = openFileDialog.FileName;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (openFileDialog != null)
                {
                    openFileDialog.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取打开文件名称
        /// </summary>
        /// <param name="Filter">文件类型</param>
        /// <param name="InitDir">保存的默认目录</param>
        /// <param name="FileName">文件名称</param>
        /// <returns></returns>
        public static string GetOpenFileName(string Filter, string InitDir, string FileName = "")
        {
            OpenFileDialog openFileDialog = null;
            string result;
            try
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = Filter;
                openFileDialog.InitialDirectory = InitDir;
                if (!string.Empty.Equals(FileName))
                {
                    openFileDialog.FileName = FileName;
                }
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = openFileDialog.FileName;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (openFileDialog != null)
                {
                    openFileDialog.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取保存文件名称
        /// </summary>
        /// <param name="Filter">文件类型</param>
        /// <param name="InitDir">保存的默认目录</param>
        /// <returns></returns>
        public static string GetSaveFileName(string Filter, string InitDir)
        {
            SaveFileDialog saveFileDialog = null;
            string result;
            try
            {
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = Filter;
                saveFileDialog.InitialDirectory = InitDir;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = saveFileDialog.FileName;
                }
                else
                {
                    result = "";
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (saveFileDialog != null)
                {
                    saveFileDialog.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取保存文件名称
        /// </summary>
        /// <param name="Filter">文件类型</param>
        /// <param name="InitDir">保存的默认目录</param>
        /// <param name="InitFileName">默认文件名称</param>
        /// <returns></returns>
        public static string GetSaveFileName(string Filter, string InitDir, string InitFileName)
        {
            SaveFileDialog saveFileDialog = null;
            string result;
            try
            {
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = Filter;
                if (InitDir != "")
                {
                    saveFileDialog.InitialDirectory = InitDir;
                }
                if (InitFileName != "")
                {
                    saveFileDialog.FileName = InitFileName;
                }
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = saveFileDialog.FileName;
                }
                else
                {
                    result = "";
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (saveFileDialog != null)
                {
                    saveFileDialog.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取保存文件名称
        /// </summary>
        /// <param name="Filter">文件类型</param>
        /// <returns></returns>
        public static string GetSaveFileName(string Filter)
        {
            SaveFileDialog saveFileDialog = null;
            string result;
            try
            {
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = Filter;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = saveFileDialog.FileName;
                }
                else
                {
                    result = "";
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (saveFileDialog != null)
                {
                    saveFileDialog.Dispose();
                }
            }
            return result;
        }
        #endregion




        #endregion

        #region 字符串简单加密

        private static readonly string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/=";

        public static string EncryptString(string str)
        {
            byte b = 0;
            ArrayList arrayList = new ArrayList(Encoding.UTF8.GetBytes(str));
            int count = arrayList.Count;
            int num = count / 3;
            int num2 = 0;
            if ((num2 = count % 3) > 0)
            {
                for (int i = 0; i < 3 - num2; i++)
                {
                    arrayList.Add(b);
                }
                num++;
            }
            StringBuilder stringBuilder = new StringBuilder(num * 4);
            for (int j = 0; j < num; j++)
            {
                byte[] array = new byte[3]
                {
                (byte)arrayList[j * 3],
                (byte)arrayList[j * 3 + 1],
                (byte)arrayList[j * 3 + 2]
                };
                int[] array2 = new int[4]
                {
                array[0] >> 2,
                ((array[0] & 3) << 4) ^ (array[1] >> 4),
                0,
                0
                };
                if (!array[1].Equals(b))
                {
                    array2[2] = (((array[1] & 0xF) << 2) ^ (array[2] >> 6));
                }
                else
                {
                    array2[2] = 64;
                }
                if (!array[2].Equals(b))
                {
                    array2[3] = (array[2] & 0x3F);
                }
                else
                {
                    array2[3] = 64;
                }
                stringBuilder.Append(Base64Code[array2[0]]);
                stringBuilder.Append(Base64Code[array2[1]]);
                stringBuilder.Append(Base64Code[array2[2]]);
                stringBuilder.Append(Base64Code[array2[3]]);
            }
            return stringBuilder.ToString();
        }
        public static string DecryptString(string str)
        {
            if (str.Length % 4 != 0)
            {
                throw new ArgumentException("encode error!", "str");
            }
            if (!Regex.IsMatch(str, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("encode error!", "str");
            }
            int num = str.Length / 4;
            ArrayList arrayList = new ArrayList(num * 3);
            char[] array = str.ToCharArray();
            for (int i = 0; i < num; i++)
            {
                byte[] array2 = new byte[4]
                {
                (byte)Base64Code.IndexOf(array[i * 4]),
                (byte)Base64Code.IndexOf(array[i * 4 + 1]),
                (byte)Base64Code.IndexOf(array[i * 4 + 2]),
                (byte)Base64Code.IndexOf(array[i * 4 + 3])
                };
                byte[] array3 = new byte[3]
                {
                (byte)((array2[0] << 2) ^ ((array2[1] & 0x30) >> 4)),
                0,
                0
                };
                if (array2[2] != 64)
                {
                    array3[1] = (byte)((array2[1] << 4) ^ ((array2[2] & 0x3C) >> 2));
                }
                else
                {
                    array3[2] = 0;
                }
                if (array2[3] != 64)
                {
                    array3[2] = (byte)((array2[2] << 6) ^ array2[3]);
                }
                else
                {
                    array3[2] = 0;
                }
                arrayList.Add(array3[0]);
                if (array3[1] != 0)
                {
                    arrayList.Add(array3[1]);
                }
                if (array3[2] != 0)
                {
                    arrayList.Add(array3[2]);
                }
            }
            byte[] bytes = (byte[])arrayList.ToArray(Type.GetType("System.Byte"));
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion 

        #region 实体列表与DataTable互换

        /// <summary>
        /// DataTable转换成实体列表
        /// </summary>
        /// <typeparam name="T">实体 T </typeparam>
        /// <param name="table">datatable</param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable table) where T : class
        {
            // if (!IsHaveRows(table))
            if (table == null || table.Rows.Count == 0)
                return new List<T>();

            IList<T> list = new List<T>();
            T model = default(T);
            foreach (DataRow dr in table.Rows)
            {
                model = Activator.CreateInstance<T>();

                foreach (DataColumn dc in dr.Table.Columns)
                {
                    object drValue = dr[dc.ColumnName];
                    PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);

                    if (pi != null && pi.CanWrite && (drValue != null && !Convert.IsDBNull(drValue)))
                    {
                        pi.SetValue(model, drValue, null);
                    }
                }

                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 实体列表转换成DataTable
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="list"> 实体列表</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(IList<T> list) where T : class
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int length = myPropertyInfo.Length;
            bool createColumn = true;

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();
                for (int i = 0; i < length; i++)
                {
                    PropertyInfo pi = myPropertyInfo[i];
                    string name = pi.Name;
                    if (createColumn)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                if (createColumn)
                {
                    createColumn = false;
                }

                dt.Rows.Add(row);
            }
            return dt;

        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns></returns>
        public static DataTable ToDataTable(System.Collections.IList list)
        {
            DataTable dataTable = new DataTable();
            if (list.Count > 0)
            {
                System.Reflection.PropertyInfo[] properties = list[0].GetType().GetProperties();
                System.Reflection.PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    System.Reflection.PropertyInfo propertyInfo = array[i];
                    if (!DxPublic.IsNullableType(propertyInfo.PropertyType))
                    {
                        dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                    }
                }
                for (int j = 0; j < list.Count; j++)
                {
                    System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
                    System.Reflection.PropertyInfo[] array2 = properties;
                    for (int k = 0; k < array2.Length; k++)
                    {
                        System.Reflection.PropertyInfo propertyInfo2 = array2[k];
                        if (!DxPublic.IsNullableType(propertyInfo2.PropertyType))
                        {
                            object value = propertyInfo2.GetValue(list[j], null);
                            arrayList.Add(value);
                        }
                    }
                    object[] values = arrayList.ToArray();
                    dataTable.LoadDataRow(values, true);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="ValidFields">转换的字段</param>
        /// <returns></returns>
        public static DataTable ToDataTable(System.Collections.IList list, System.Collections.Generic.List<string> ValidFields)
        {
            DataTable dataTable = new DataTable();
            bool flag = ValidFields != null && 0 < ValidFields.Count;
            if (list.Count > 0)
            {
                System.Reflection.PropertyInfo[] properties = list[0].GetType().GetProperties();
                System.Reflection.PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    System.Reflection.PropertyInfo propertyInfo = array[i];
                    if (!DxPublic.IsNullableType(propertyInfo.PropertyType) && (!flag || ValidFields.Contains(propertyInfo.Name)))
                    {
                        dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                    }
                }
                for (int j = 0; j < list.Count; j++)
                {
                    System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
                    System.Reflection.PropertyInfo[] array2 = properties;
                    for (int k = 0; k < array2.Length; k++)
                    {
                        System.Reflection.PropertyInfo propertyInfo2 = array2[k];
                        if (!DxPublic.IsNullableType(propertyInfo2.PropertyType) && (!flag || ValidFields.Contains(propertyInfo2.Name)))
                        {
                            object value = propertyInfo2.GetValue(list[j], null);
                            arrayList.Add(value);
                        }
                    }
                    object[] values = arrayList.ToArray();
                    dataTable.LoadDataRow(values, true);
                }
            }
            return dataTable;
        }
        #endregion
    }
}
