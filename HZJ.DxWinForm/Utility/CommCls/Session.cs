using System;
using System.Collections.Generic;

namespace HZJ.DxWinForm.Utility.CommCls
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
        public bool _IsRberPwd { get; set; } = false;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string _FullName { get; set; } = string.Empty;

        /// <summary>
        /// 问题ID
        /// </summary>
        public Guid _DepartmentId { get; set; } = Guid.Empty;

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
        public Guid _SystemId { get; set; } = AppSetingHelper.GetSystemId();

        /// <summary>
        /// 系统名称
        /// </summary>
        public string _SystemName { get; set; } = AppSetingHelper.GetSystemName();

        /// <summary>
        /// 登录电脑IP
        /// </summary>
        public string _IPAddress { get; set; } = DxPublic.GetLocalIP();

        /// <summary>
        /// 登录电脑的Mac地址
        /// </summary>
        public string _MACAddress { get; set; } = DxPublic.GetLocalMac();

        /// <summary>
        /// 初始化方法
        /// </summary>
        public void SessionIntial()
        {
            _UserId = Guid.Empty;
            _LoginPwd = "";
            _IsRberPwd = false;
            _LoginName = "";
            _FullName = "";
            _DepartmentId = Guid.Empty;
            _DepartmentName = "";
            _RoleIds = null;
            _RoleNames = null;
        }
    }
}
