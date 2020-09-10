using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HZJ.CommonCls.IO
{
    /// <summary>
    /// 文件操作帮助类
    /// </summary>
    public  class FileHelper
    {

        #region 文件目录操作

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static bool FileExists(string FileName)
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
                FileHelper.CheckFolderExists(fileInfo.DirectoryName);
            }
        }

        /// <summary>
        /// 检件文件夹是否存在，不存在则创建
        /// </summary>
        public static void CheckFolderExists(string BaseDir, string SubDir)
        {
            string folder = string.Format("{0}\\{1}", BaseDir, SubDir);
            FileHelper.CheckFolderExists(folder);
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
            if (!FileHelper.FileExists(FileName))
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
            return FileHelper.GetSaveFileName("EXCEL File(*.xls)|*.xls", "", DefaultFileName);
        }

        /// <summary>
        /// 获取打开的XLS文件名称
        /// </summary>
        /// <param name="DefaultFileName">默认文件名</param>
        /// <returns></returns>
        public static string GetOpenXlsFileName(string DefaultFileName)
        {
            return FileHelper.GetOpenFileName("EXCEL File(*.xls)|*.xls", DefaultFileName);
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
    }
}
