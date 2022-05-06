using System;

namespace SimpleOPC.Utilities
{
    public class Ex
    {
        public struct Msg
        {
            public const string NotinDic = "Key Not Existed in Dictionary. Name: {0}";
            public const string TagListEmpty = "Tags list haven't initialzied, Please call the {0} method";
            public const string ControllerNull = "Ingear Controller haven't initialzied, Please call the {0} method or {1} method";
        }

        public class CustomException : Exception
        {
            public CustomException(string Message) : base(Message) { }
        }
    }
}
