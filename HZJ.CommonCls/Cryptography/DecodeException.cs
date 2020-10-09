using System;

namespace HZJ.CommonCls.Cryptography
{
    public class DecodeException : ArgumentException
    {
        public DecodeException(string message, string paraName)
            : base(message, paraName)
        {
        }
    }

}
