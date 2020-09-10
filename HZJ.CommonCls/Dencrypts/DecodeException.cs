using System;

namespace HZJ.CommonCls.Dencrypts
{
    public class DecodeException : ArgumentException
    {
        public DecodeException(string message, string paraName)
            : base(message, paraName)
        {
        }
    }

}
