using HZJ.CommonCls.IO;

namespace HZJ.CommonCls.Reflection
{
    /// <summary>
    /// 反射常用方法
    /// </summary>
    public class ReflectionHelper
    {
        #region  反射
        /// <summary>
        /// 反射实列化对象
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static object ReflectionMakeObj(string FileName, string Class)
        {
            if (!FileHelper.FileExists(FileName))
            {
                return null;
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(FileName);
            System.Type type = assembly.GetType(Class, true, true);
            return System.Activator.CreateInstance(type);
        }

        /// <summary>
        /// 反射实列化对象
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="className">类名</param>
        /// <param name="Args">参数</param>
        /// <returns></returns>
        public static object ReflectionMakeObj(string FileName, string Class, object[] Args)
        {
            if (!FileHelper.FileExists(FileName))
            {
                return null;
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(FileName);
            System.Type type = assembly.GetType(Class, true, true);
            return System.Activator.CreateInstance(type, Args);
        }

        /// <summary>
        /// 反射实列化方法
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="className">类名</param>
        /// <param name="Method">方法</param>
        /// <param name="Args">参数</param>
        /// <returns></returns>
        public static object ReflectionInvokeMethods(string fileName, string className, string Method, object[] Args)
        {
            if (!FileHelper.FileExists(fileName))
            {
                return null;
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(fileName);
            System.Type type = assembly.GetType(className, true, true);
            return type.InvokeMember(Method, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.InvokeMethod, null, null, Args);
        }
        #endregion

        #region  获取和设置对象属性
        /// <summary>
        /// 获取对象的属性值
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="t">对象</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns></returns>
        public static object GetObjectPropertyValue<T>(T t, string propertyname)
        {
            System.Type typeFromHandle = typeof(T);
            System.Reflection.PropertyInfo property = typeFromHandle.GetProperty(propertyname);
            if (property == null)
            {
                return null;
            }
            return property.GetValue(t, null);
        }

        /// <summary>
        /// 获取对象的属性值
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="t">对象</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns></returns>
        public static object GetObjectFieldValue<T>(T t, string Fieldname)
        {
            System.Type typeFromHandle = typeof(T);
            System.Reflection.FieldInfo[] fields = typeFromHandle.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            System.Reflection.FieldInfo[] array = fields;
            for (int i = 0; i < array.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = array[i];
                if (fieldInfo.Name == Fieldname)
                {
                    return fieldInfo.GetValue(t);
                }
            }
            return null;
        }

        /// <summary>
        /// 设置对象的属性值
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="t">对象</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns></returns>
        public static void SetObjectPropertyValue<T>(T t, string propertyname, object value)
        {
            System.Type typeFromHandle = typeof(T);
            System.Reflection.PropertyInfo property = typeFromHandle.GetProperty(propertyname);
            if (null != property && property.CanWrite)
            {
                property.SetValue(t, value, null);
            }
        }

        /// <summary>
        /// 设置对象的属性值
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="t">对象</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns></returns>
        public static void SetObjectFieldValue<T>(T t, string FieldName, object value)
        {
            System.Type typeFromHandle = typeof(T);
            System.Reflection.FieldInfo[] fields = typeFromHandle.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            System.Reflection.FieldInfo[] array = fields;
            for (int i = 0; i < array.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = array[i];
                if (fieldInfo.Name == FieldName)
                {
                    fieldInfo.SetValue(t, value);
                    break;
                }
            }
        }
        #endregion
    }
}
