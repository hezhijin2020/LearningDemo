using System;
using System.Collections.Generic;

namespace HZJ.DxWinComm.CommCls
{
    public class Session
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid _UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string _LoginName { get; set; }=string.Empty;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string _LoginPwd { get; set; } = string.Empty;

        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool _IsRemPwd { get; set; } = false;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string _FullName { get; set; } = string.Empty;

        /// <summary>
        /// 问题ID
        /// </summary>
        public Guid _DepartmentId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string _DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 角色ID
        /// </summary>
        public List<Guid> _RoleIds { get; set; } = null;

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<Guid> _RoleNames { get; set; } = null;

        /// <summary>
        /// 系统ID
        /// </summary>
        public Guid _SystemId { get; set; } = Guid.Empty;

        /// <summary>
        /// 系统名称
        /// </summary>
        public string _SystemName { get; set; } = string.Empty;

        /// <summary>
        /// 登录电脑IP
        /// </summary>
        public string _IPAddress { get; set; } = string.Empty;

        /// <summary>
        /// 登录电脑的Mac地址
        /// </summary>
        public string _MACAddress { get; set; } = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Session(
            string IPAddress,
            string MACAddress,
            Guid SystemId,
            string SystemName)
        {
            _SystemId = SystemId;
            _SystemName = SystemName;
            _IPAddress = IPAddress;
            _MACAddress = MACAddress;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Session(
            Guid UserId,
            string LoginName,
            string LoginPwd,
            bool IsRemPwd,
            string FullName,
            Guid DepartmentId,
            string DepartmentName,
            string IPAddress,
            string MACAddress,
            Guid SystemId,
            string SystemName,
            List<Guid> RoleIds,
            List<Guid> RoleNames)
        {
            _UserId = UserId;
            _LoginName = LoginName;
            _LoginPwd = LoginPwd;
            _IsRemPwd = IsRemPwd;
            _FullName = FullName;
            _DepartmentId = DepartmentId;
            _DepartmentName = DepartmentName;
            _RoleIds = RoleIds;
            _RoleNames = RoleNames;
            _SystemId = SystemId;
            _SystemName = SystemName;
            _IPAddress = IPAddress;
            _MACAddress = MACAddress;
        }
        /// <summary>
        /// 构造函数
        /// </summary>

        public Session(
            Guid UserId,
            string LoginName,
            string LoginPwd,
            bool IsRemPwd,
            string FullName,
            Guid DepartmentId,
            string DepartmentName,
            string IPAddress,
            string MACAddress,
            Guid SystemId,
            string SystemName)
        {
            _UserId = UserId;
            _LoginName = LoginName;
            _LoginPwd = LoginPwd;
            _IsRemPwd = IsRemPwd;
            _FullName = FullName;
            _DepartmentId = DepartmentId;
            _DepartmentName = DepartmentName;
            _SystemId = SystemId;
            _SystemName = SystemName;
            _IPAddress = IPAddress;
            _MACAddress = MACAddress;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public Session(
            Guid UserId,
            string LoginName,
            string LoginPwd,
            bool IsRemPwd,
            string FullName,
            Guid DepartmentId,
            string DepartmentName,
            string IPAddress,
            string MACAddress)
        {
            _UserId = UserId;
            _LoginName = LoginName;
            _LoginPwd = LoginPwd;
            _IsRemPwd = IsRemPwd;
            _FullName = FullName;
            _DepartmentId = DepartmentId;
            _DepartmentName = DepartmentName;
            _IPAddress = IPAddress;
            _MACAddress = MACAddress;
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public void SessionIntial()
        {
            _UserId = Guid.Empty;
            _LoginPwd = "";
            _IsRemPwd = false;
            _LoginName = "";
            _FullName = "";
            _DepartmentId = Guid.Empty;
            _DepartmentName = "";
            _RoleIds = null;
            _RoleNames = null;
        }
    }
}
