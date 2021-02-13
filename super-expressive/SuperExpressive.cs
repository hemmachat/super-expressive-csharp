using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperExpressive
{
    public class SuperExpressive
    {
        StringBuilder pattern = new StringBuilder();
           
        public string ToRegexString() 
        {
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

        public SuperExpressive AnyChar()
        {
            pattern.Append(".");
            return this;
        }

        public SuperExpressive WhiteSpaceChar()
        {
            pattern.Append(@"\s");
            return this;
        }     

        public SuperExpressive NonWhiteSpaceChar()
        {
            pattern.Append(@"\S");
            return this;
        }   

        public SuperExpressive Digit()
        {
            pattern.Append(@"\d");
            return this;
        }     

        public SuperExpressive NonDigit()
        {
            pattern.Append(@"\D");
            return this;
        }       

        public SuperExpressive Word()
        {
            pattern.Append(@"\w");
            return this;
        }     

        public SuperExpressive NonWord()
        {
            pattern.Append(@"\W");
            return this;
        }     

        public SuperExpressive WordBoundary()
        {
            pattern.Append(@"\b");
            return this;
        } 

        public SuperExpressive NonWordBoundary()
        {
            pattern.Append(@"\B");
            return this;
        }   

        public SuperExpressive NewLine()
        {
            pattern.Append(@"\n");
            return this;
        }     

        public SuperExpressive CarriageReturn()
        {
            pattern.Append(@"\r");
            return this;
        }  

        public SuperExpressive Tab()
        {
            pattern.Append(@"\t");
            return this;
        }   

        public SuperExpressive NullByte()
        {
            pattern.Append(@"\0");
            return this;
        }     

        public SuperExpressive String(string stringToMatch) 
        {
            pattern.Append(stringToMatch);
            return this;
        }   

        public SuperExpressive Char(char charToMatch)
        {
            pattern.Append(charToMatch);
            return this;
        }     

        public SuperExpressive Range(char beginChar, char endChar)
        {
            pattern.Append($"{beginChar}-{endChar}");
            return this;
        }

        public SuperExpressive Optional()
        {
            pattern.Append("?");
            return this;
        }               

        public SuperExpressive ZeroOrMore()
        {
            pattern.Append("*");
            return this;
        }               

        public SuperExpressive OneOrMore()
        {
            pattern.Append("+");
            return this;
        }            

        public SuperExpressive OneOrMoreLazy()
        {
            pattern.Append("+?");
            return this;
        }

        public SuperExpressive Exactly(int numberToMatch)
        {
            pattern.Append($"{{{numberToMatch}}}");
            return this;
        }

        public SuperExpressive AtLeast(int numberToMatch)
        {
            pattern.Append($"{{{numberToMatch},}}");
            return this;
        }     


        public SuperExpressive Between(int numberFrom, int numberTo)
        {
            pattern.Append($"{{{numberFrom},{numberTo}}}");
            return this;
        }   

        public SuperExpressive BetweenLazy(int numberFrom, int numberTo)
        {
            pattern.Append($"{{{numberFrom},{numberTo}}}?");
            return this;
        }                  
    }
}