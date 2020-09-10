using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace HZJ.CommonCls.IO
{
    /// <summary>
    /// ini文件帮助类
    /// </summary>
    public class IniFileHelper
    {
        private string _filename = string.Empty;

        /// <summary>
        /// 生成Ini文件帮助类，当提供的文件不存在时，生成文件
        /// </summary>
        /// <param name="filename">文件名（带路径名称）</param>
        public IniFileHelper(string  filename)
        {
            if (File.Exists(filename))
            {
                File.Create(filename);
            }
            _filename = filename;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        private string FileName { get; }

        #region 引入Kerne132 操作Ini文件

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="section">要写入的段落名</param>
        /// <param name="key">要读取的键</param>
        /// <param name="val">key所对应的值</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns>0、1带表成功和失败</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="section">要读取的段落名</param>
        /// <param name="key">要读取的键</param>
        /// <param name="def">读取异常的情况下的缺省值</param>
        /// <param name="retVal">key所对应的值，如果该key不存在则返回空值</param>
        /// <param name="size">值允许的大小</param>
        /// <param name="filePath">INI文件的完整路径和文件名</param>
        /// <returns>key所对应的值的长度</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion

        #region 不加密 读写信息
        /// <summary>
        /// 字段信息写入IniFile配置文件
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void IniWriteValue(string section, string key, string value,string filename)
        {
            WritePrivateProfileString(section, key, value, filename);
        }

        /// <summary>
        /// 读取IniFile配置文件的配置信息
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="retval">默认初始值</param>
        /// <returns></returns>
        public static string IniReadValue(string section, string key, string retval,string filename)
        {
            StringBuilder temp = new StringBuilder(500);
            int length = GetPrivateProfileString(section, key, retval, temp, 500, filename);
            return temp.ToString();
        }
        #endregion

        #region 加密 读写信息
        /// <summary>
        /// 字段信息加密后写入IniFile配置文件
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void IniWriteValueEncrypt(string section, string key, string value, string filename)
        {
            WritePrivateProfileString(section, key, clsPublic.EncryptString(value), filename);
        }
        /// <summary>
        /// 读取解密后IniFile配置文件的配置信息
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="retval">默认初始值</param>
        /// <returns></returns>
        public static string IniReadValueDecrypt(string section, string key, string retval, string filename)
        {
            StringBuilder temp = new StringBuilder(500);
            int length = GetPrivateProfileString(section, key, retval, temp, 500, filename);
            return clsPublic.DecryptString(temp.ToString());
        }

        #endregion

    }
}
