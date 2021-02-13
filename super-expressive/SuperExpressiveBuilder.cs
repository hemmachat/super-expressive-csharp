using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperExpressive
{
    public class SuperExpressiveBuilder
    {
        StringBuilder pattern = new StringBuilder();
           
        public string ToRegexString() {
            return pattern.ToString();
        }

        public SuperExpressiveBuilder StartOfInput()
        {
            pattern.Append("^");
            return this;
        }

        public SuperExpressiveBuilder EndOfInput()
        {
            pattern.Append("$");
            return this;
        }

        public SuperExpressiveBuilder Capture()
        {
            pattern.Append("(");
            return this;
        }

        public SuperExpressiveBuilder End()
        {
            pattern.Append(")");
            return this;
        }
    }
}