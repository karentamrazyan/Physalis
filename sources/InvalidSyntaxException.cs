using System;

namespace Physalis.Specs.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidSyntaxException : Exception
    {
        private string filter;

        public InvalidSyntaxException(string msg, string filter) : base(msg)
        {
            this.filter = filter;
        }

        public string Filter
        {
            get
            {
                return filter;
            }
        }
    }
}
