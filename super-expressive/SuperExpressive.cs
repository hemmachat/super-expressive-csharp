using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperExpressive
{
    public class SuperExpressive
    {
        StringBuilder pattern = new StringBuilder();
           
        public string ToRegexString() {
            return pattern.ToString();
        }

        public SuperExpressive StartOfInput()
        {
            pattern.Append("^");
            return this;
        }

        public SuperExpressive EndOfInput()
        {
            pattern.Append("$");
            return this;
        }

        public SuperExpressive Capture()
        {
            pattern.Append("(");
            return this;
        }

        public SuperExpressive End()
        {
            pattern.Append(")");
            return this;
        }
    }
}