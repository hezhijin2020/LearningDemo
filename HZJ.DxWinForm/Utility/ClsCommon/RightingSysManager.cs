using HZJ.CommonCls;
using HZJ.CommonCls.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HZJ.DxWinForm.Utility.ClsCommon
{
    /// <summary>
    /// 权限管理通用类
    /// </summary>
    public class RightingSysManager
    {
        #region SQL数据库操作
    
        /// <summary>
        /// 根据用户ID获取用户权限列表
        /// </summary>
        /// <param name="guidUserId">用户ID</param>
        /// <returns>用户列表</returns>
        public  DataTable GetUserFunction(Guid sysetemId ,Guid userId,Guid departmentId)
        {
            string sqlText = string.Format(@"select DISTINCT  a.RoleId,a.FunctionId,a.OpCode,b.FuncName 
             from ACL_Role_Function as a inner join ACL_Function as b  on a.FunctionId=b.Id  
			 where  b.SystemId='{0}' and RoleId in ( select RoleId from ACL_Role_User where UserId='{1}' 
                union select RoleId from ACL_Role_Department where DepartmentId='{2}')",sysetemId, userId,departmentId);
            return Global._SqlDb.ExecuteDataTable(sqlText);
        }

        /// <summary>
        /// 根据用户ID获取用户权限列表
        /// </summary>
        /// <param name="guidUserId">用户ID</param>
        /// <returns>用户列表</returns>
        public IList<Models.ACL_Role_Function> GetUserFunctionList(Guid sysetemId, Guid userId, Guid departmentId)
        {
            string sqlText = string.Format(@"select DISTINCT  a.RoleId,a.FunctionId,a.OpCode,b.FuncName 
             from ACL_Role_Function as a inner join ACL_Function as b  on a.FunctionId=b.Id  
			                             inner join ACL_Role as c on a.RoleId=c.Id
			 where  b.SystemId='{0}' and RoleId in ( select distinct  RoleId from ACL_Role_User where UserId='{1}' 
                union select RoleId from ACL_Role_Department where DepartmentId='{2}')", sysetemId, userId, departmentId);
             DataTable dt= Global._SqlDb.ExecuteDataTable(sqlText);
            return CommonCls.clsPublic.DataTableToList<Models.ACL_Role_Function>(dt);
        }

        /// <summary>
        /// 根据用户和密码获取用户信息
        /// </summary>
        /// <param name="LoginName">用户名</param>
        /// <param name="LoginPwd">密码</param>
        /// <returns>用户信息</returns>
        public  DataTable GetUserInfo(string LoginName, string LoginPwd)
        {
            string sqlText = string.Format(@"SELECT a.Id,a.[LoginName],a.[LoginPwd],a.[FullName],c.DepartmentName,c.Id DepartmentId
            FROM [dbo].[ACL_User] as a left join ACL_Department as c on a.DepartmentId=c.Id
            where LoginName='{0}' and LoginPwd='{1}' and a.IsRemoved=0 ", LoginName, LoginPwd);
            return Global._SqlDb.ExecuteDataTable(sqlText);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="oldPwd">原密码</param>
        /// <param name="newPwd">新密码</param>
        public  bool ModifyUserPwd(Guid userId, String oldPwd, string newPwd)
        {
            string sqlText = @"UPDATE [dbo].[ACL_User]SET [LoginPwd] = @newPwd
                               WHERE [Id] = @UserID and [LoginPwd]=@oldPwd ";
            SqlParameter s1 = new SqlParameter("@UserID", userId);
            SqlParameter s2 = new SqlParameter("@newPwd", newPwd);
            SqlParameter s3 = new SqlParameter("@oldPwd", oldPwd);
            int i = Global._SqlDb.ExecuteNoQuery(sqlText, new SqlParameter[] { s1, s2, s3 });
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion


        #region  用户主题配置、读取写入


        #region 不加密 读写信息
        /// <summary>
        /// 字段信息写入IniFile配置文件
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void IniWriteValue(string section, string key, string value)
        {
           IniFileHelper.IniWriteValue(section, key, value, Global._IniConfigFile);
        }

        /// <summary>
        /// 读取IniFile配置文件的配置信息
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="retval">默认初始值</param>
        /// <returns></returns>
        public  string IniReadValue(string section, string key, string retval)
        {
          return IniFileHelper.IniReadValue(section, key, retval, Global._IniConfigFile);
        }
        #endregion

        #region 加密 读写信息
        /// <summary>
        /// 字段信息加密后写入IniFile配置文件
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public  void IniWriteValueEncrypt(string section, string key, string value)
        {
            IniFileHelper.IniWriteValueEncrypt(section, key, clsPublic.EncryptString(value),Global._IniConfigFile);
        }
       
        /// <summary>
        /// 读取解密后IniFile配置文件的配置信息
        /// </summary>
        /// <param name="section">会话</param>
        /// <param name="key">Key</param>
        /// <param name="retval">默认初始值</param>
        /// <returns></returns>
        public  string IniReadValueDecrypt(string section, string key, string retval)
        {
            return IniFileHelper.IniReadValueDecrypt(section, key, retval, Global._IniConfigFile);
        }

        #endregion

        /// <summary>
        /// 从INIFile配置文件中读取默认主题
        /// </summary>
        /// <returns></returns>
        public   string ReadDefaultSkinName()
        {
            try
            {
                return HZJ.CommonCls.IO.IniFileHelper.IniReadValue("UserSkin", "DefaultSkin", "Springtime",Global._IniConfigFile);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将用户默认主题写入的IniFile配置文件
        /// </summary>
        /// <param name="SkinName">是题名称</param>
        public  void WriteDefaultSkinName(string SkinName)
        {
            IniFileHelper.IniWriteValue("UserSkin", "DefaultSkin", SkinName,Global._IniConfigFile);
        }

        #endregion

        #region 用户信息 读取与写入

        /// <summary>
        /// 用户信息写入配置文件
        /// </summary>
        public void SaveLoginConfig()
        {
            try
            {
                if (Global._Session._IsRemPwd)
                {
                    IniFileHelper.IniWriteValue("UserConfig", "RemPwd", "1", Global._IniConfigFile);
                    IniFileHelper.IniWriteValue("UserConfig", "LoginName", Global._Session._LoginName,Global._IniConfigFile);
                    IniFileHelper.IniWriteValueEncrypt("UserConfig", "LoginPwd", Global._Session._LoginPwd, Global._IniConfigFile);
                }
                else
                {
                    IniFileHelper.IniWriteValue("UserConfig", "RemPwd", "0", Global._IniConfigFile);
                    IniFileHelper.IniWriteValue("UserConfig", "LoginName", Global._Session._LoginName,Global._IniConfigFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("用户配置信息入写出错", ex);
            }
        }

        /// <summary>
        /// 从配置文件读取用户信息
        /// </summary>
        public  void ReadLoginConfig()
        {
            try
            {
                Global._Session._IsRemPwd = IniFileHelper.IniReadValue("UserConfig", "RemPwd", "0",Global._IniConfigFile) == "1";
                Global._Session._LoginName = IniFileHelper.IniReadValue("UserConfig", "LoginName", "", Global._IniConfigFile);
                if (Global._Session._IsRemPwd)
                {
                    Global._Session._LoginPwd = IniFileHelper.IniReadValueDecrypt("UserConfig", "LoginPwd", "",Global._IniConfigFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("用户配置信息读取出错", ex);
            }

        }
        #endregion
    }
}
