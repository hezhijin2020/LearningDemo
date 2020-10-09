using HZJ.DxWinForm.Utility.CommCls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HZJ.DxWinForm.Utility.CommCls
{
    /// <summary>
    /// 权限管理通用类
    /// </summary>
    public class RightingSysManager
    {
        #region SQL数据库操作

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <returns></returns>
        public bool IsConnectionTest()
        {
            return Global._SqlDb.IsConnectionTest();
        }
    
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
            return DxPublic.DataTableToList<Models.ACL_Role_Function>(dt);
        }

        /// <summary>
        /// 根据用户和密码获取用户信息
        /// </summary>
        /// <param name="LoginName">用户名</param>
        /// <param name="LoginPwd">密码</param>
        /// <returns>用户信息</returns>
        public DataTable GetUserInfo(string LoginName, string LoginPwd)
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

        /// <summary>
        /// 检查版本是否更新
        /// </summary>
        /// <param name="systemId">系统Id</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        public bool IsUpdateVersion(Guid systemId,string version)
        {
           
            bool Result = false;
            string sqlText = @"Select Version from ACL_System Where Id=@Id";
            SqlParameter s1 = new SqlParameter("@Id", systemId);
            object objVersion = Global._SqlDb.ExecuteScalar(sqlText, new SqlParameter[] { s1});
            if (objVersion == DBNull.Value)
            {
                Result= false;
            }
            if (objVersion.ToString().CompareTo(version) > 0)
                    Result = true;
            return Result;
        }
        #endregion
    }
}
