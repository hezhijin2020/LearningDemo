using HZJ.ORM.Mapping;
using System;
using System.Linq;

namespace HZJ.ORM
{
    public  class BuildSQLString<T>where T:new()
    {

        public BuildSQLString()
        {
            
        }

        public string GetSelectString()
        {
            Type t = typeof(T);
            string columnsString = string.Join(",", t.GetProperties().Select(a => "[" + a.GetMappingName() + "]"));
            string tableString = t.GetMappingName();
            string StrSQL = $"Select {columnsString} from [{tableString}] ";
            return StrSQL;
        }

        public string GetInsertString()
        {
            Type t = typeof(T);
            string columnsString = string.Join(",", t.GetProperties().Select(a => "[" + a.GetMappingName() + "]"));
            string tableString = t.GetMappingName();
            string paraString= string.Join(",", t.GetProperties().Select(a => "@" + a.GetMappingName()));
            string StrSQL = $"Insert Into [{tableString}] ({columnsString}) values({paraString} )";
            return StrSQL;
        }

        public string GetUpdateString()
        {
            Type t = typeof(T);
            string SetString = string.Join(",", t.GetProperties().Where(b=>b.GetMappingName()!="Id").Select(a => "[" + a.GetMappingName() + $"]=@{a.GetMappingName()}"));
            string tableString = t.GetMappingName();
            string StrSQL = $"Update [{tableString}] set {SetString} where Id=@Id";
            return StrSQL;
        }

        public string GetDeleteString(Guid Id)
        {
            Type t = typeof(T);
            string tableString = t.GetMappingName();
            string StrSQL = $"Delete [{tableString}] where [Id]='{Id}' ";
            return StrSQL;
        }
    }
}
