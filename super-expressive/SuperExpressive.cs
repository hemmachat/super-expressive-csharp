using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperExpressive
{
    public enum GroupType
    {
        AnyOf,
        Capture,
        Group
    }
    
    public class SuperExpressive
    {
        private const string SpecialChars = @"\.^$|?*+()[]{}-";
        private readonly StringBuilder _pattern = new StringBuilder();
        private readonly Stack _groupStack = new Stack();
        private readonly Stack _patternStack = new Stack();
        private bool _isGroup;

        public static string ReplaceSpecialChars(char charToReplace)
        {
            if (!SpecialChars.Contains(charToReplace))
            {
                return charToReplace.ToString();
            }
            
            var stringToReplace = charToReplace.ToString();
            
            return stringToReplace.Replace(stringToReplace, @"\" + charToReplace);
        }
        
        public string ToRegexString() 
        {
            return _pattern.ToString();
        }

        /// <summary>
        /// Creates a capture group for the proceeding elements. Needs to be finalised with .end(). Can be later referenced with backreference(index).
        /// </summary>
        /// <returns></returns>
        public SuperExpressive Capture()
        {
            _pattern.Append('(');
            return this;
        }

        /// <summary>
        /// Creates a non-capturing group of the proceeding elements. Needs to be finalised with .end().
        /// </summary>
        /// <returns></returns>
        public SuperExpressive Group()
        {
            return this;
        }
        
        /// <summary>
        /// Matches a choice between specified elements. Needs to be finalised with .end().
        /// </summary>
        /// <returns></returns>
        public SuperExpressive AnyOf()
        {
            _groupStack.Push(GroupType.AnyOf);
            _patternStack.Push(new StringBuilder());
            _isGroup = true;

            return this;
        }
        
        /// <summary>
        /// Signifies the end of a SuperExpressive grouping, such as .anyOf, .group, or .capture.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive End()
        {
            if (_groupStack.Count > 0 && _patternStack.Count > 0)
            {
                var groupType = (GroupType) _groupStack.Pop();
            
                switch (groupType)
                {
                    case GroupType.Capture:
                        break;
                
                    case GroupType.AnyOf:
                        _pattern.Append('[');
                        var groupPattern = (StringBuilder) _patternStack.Pop();
                        _pattern.Append(groupPattern);
                        _pattern.Append(']');
                        break;
                
                    case GroupType.Group:
                        break;
                
                    default:
                        throw new ArgumentOutOfRangeException();
                }   
            }

            _isGroup = false;
            
            return this;
        }

        /// <summary>
        /// Assert the start of input, or the start of a line when .lineByLine is used.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive StartOfInput()
        {
            _pattern.Append('^');
            return this;
        }

        /// <summary>
        /// Assert the end of input, or the end of a line when .lineByLine is used.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive EndOfInput()
        {
            _pattern.Append('$');
            return this;
        }

        /// <summary>
        /// Matches any single character.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive AnyChar()
        {
            _pattern.Append('.');
            return this;
        }

        /// <summary>
        /// Matches any whitespace character, including the special whitespace characters: \r\n\t\f\v.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive WhiteSpaceChar()
        {
            _pattern.Append(@"\s");
            return this;
        }     

        /// <summary>
        /// Matches any non-whitespace character, excluding also the special whitespace characters: \r\n\t\f\v.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive NonWhiteSpaceChar()
        {
            _pattern.Append(@"\S");
            return this;
        }   

        /// <summary>
        /// Matches any digit from 0-9.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive Digit()
        {
            _pattern.Append(@"\d");
            return this;
        }     

        /// <summary>
        /// Matches any non-digit.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive NonDigit()
        {
            _pattern.Append(@"\D");
            return this;
        }       

        /// <summary>
        /// Matches any alpha-numeric (a-z, A-Z, 0-9) characters, as well as _.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive Word()
        {
            _pattern.Append(@"\w");
            return this;
        }     

        /// <summary>
        /// Matches any non alpha-numeric (a-z, A-Z, 0-9) characters, excluding _ as well.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive NonWord()
        {
            _pattern.Append(@"\W");
            return this;
        }     

        /// <summary>
        /// Matches (without consuming any characters) immediately between a character matched by .word and a character not matched by .word (in either order).
        /// </summary>
        /// <returns></returns>
        public SuperExpressive WordBoundary()
        {
            _pattern.Append(@"\b");
            return this;
        } 

        /// <summary>
        /// Matches (without consuming any characters) at the position between two characters matched by .word.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive NonWordBoundary()
        {
            _pattern.Append(@"\B");
            return this;
        }   

        /// <summary>
        /// Matches a \n character.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive NewLine()
        {
            _pattern.Append(@"\n");
            return this;
        }     

        /// <summary>
        /// Matches a \r character.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive CarriageReturn()
        {
            _pattern.Append(@"\r");
            return this;
        }  

        /// <summary>
        /// Matches a \t character.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive Tab()
        {
            _pattern.Append(@"\t");
            return this;
        }   

        /// <summary>
        /// Matches a \u0000 character (ASCII 0).
        /// </summary>
        /// <returns></returns>
        public SuperExpressive NullByte()
        {
            _pattern.Append(@"\0");
            return this;
        }     

        /// <summary>
        /// Matches the exact string.
        /// </summary>
        /// <param name="stringToMatch">String to match</param>
        /// <returns></returns>
        public SuperExpressive String(string stringToMatch) 
        {
            UpdatePattern(stringToMatch);
            
            return this;
        }   

        public SuperExpressive Char(char charToMatch)
        {
            var stringPattern = ReplaceSpecialChars(charToMatch);
            UpdatePattern(stringPattern);
            
            return this;
        }

        private void UpdatePattern(string stringPattern)
        {
            if (_isGroup)
            {
                var pattern = (StringBuilder) _patternStack.Peek();
                pattern?.Append(stringPattern);
            }
            else
            {
                _pattern.Append(stringPattern);
            }
        }
        
        public SuperExpressive Range(char beginChar, char endChar)
        {
            var stringPattern = $"{beginChar}-{endChar}";
            UpdatePattern(stringPattern);

            return this;
        }

        /// <summary>
        /// Assert that the proceeding element may or may not be matched.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive Optional()
        {
            _pattern.Append("?");
            return this;
        }               

        /// <summary>
        /// Assert that the proceeding element may not be matched, or may be matched multiple times.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive ZeroOrMore()
        {
            _pattern.Append("*");
            return this;
        }               

        /// <summary>
        /// Assert that the proceeding element may be matched once, or may be matched multiple times.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive OneOrMore()
        {
            _pattern.Append("+");
            return this;
        }            

        /// <summary>
        /// Assert that the proceeding element may not be matched, or may be matched multiple times, but as few times as possible.
        /// </summary>
        /// <returns></returns>
        public SuperExpressive OneOrMoreLazy()
        {
            _pattern.Append("+?");
            return this;
        }

        /// <summary>
        /// Assert that the proceeding element will be matched exactly n times.
        /// </summary>
        /// <param name="times">Number of times to match</param>
        /// <returns></returns>
        public SuperExpressive Exactly(int times)
        {
            _pattern.Append($"{{{times}}}");
            return this;
        }

        /// <summary>
        /// Assert that the proceeding element will be matched at least n times.
        /// </summary>
        /// <param name="times">Number of times to match</param>
        /// <returns></returns>
        public SuperExpressive AtLeast(int times)
        {
            _pattern.Append($"{{{times},}}");
            return this;
        }     
        
        /// <summary>
        /// Assert that the proceeding element will be matched somewhere between x and y times.
        /// </summary>
        /// <param name="numberFrom">Minimum number of times to match</param>
        /// <param name="numberTo">Maximum number of times to match</param>
        /// <returns></returns>
        public SuperExpressive Between(int numberFrom, int numberTo)
        {
            _pattern.Append($"{{{numberFrom},{numberTo}}}");
            return this;
        }   

        /// <summary>
        /// Assert that the proceeding element will be matched somewhere between x and y times, but as few times as possible.
        /// </summary>
        /// <param name="numberFrom">Minimum number of times to match</param>
        /// <param name="numberTo">Maximum number of times to match</param>
        /// <returns></returns>
        public SuperExpressive BetweenLazy(int numberFrom, int numberTo)
        {
            _pattern.Append($"{{{numberFrom},{numberTo}}}?");
            return this;
        }                  
    }
}