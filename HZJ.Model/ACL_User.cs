using HZJ.ORM.Mapping;
using System;

namespace HZJ.Model
{
    [TableMapping("ACL_User")]
    public class ACL_User:BaseEntity
    {
        [ColumnMapping("LoginName")]
        public string LoginName { get; set; }

        [ColumnMapping("LoginPwd")]
        public string LoginPwd { get; set; }

        [ColumnMapping("UserName")]
        public string UserName { get; set; }

        [ColumnMapping("DepartmentId")]
        public Guid DepartmentId { get; set; }


    }
}
