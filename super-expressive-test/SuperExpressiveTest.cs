using System;
using Xunit;
using SuperExpressive;

namespace SuperExpressive.Test
{
    public class SuperExpressiveTest
    {
        public SuperExpressiveTest () 
        {
            
        }

        [Fact]
        public void Replace_Special_Chars_Dot()
        {
            var newText = SuperExpressive.ReplaceSpecialChars('.');
            
            Assert.Equal(@"\.", newText);
        }
        
        [Fact]
        public void Replace_Special_Chars_Back_Slash()
        {
            var newText = SuperExpressive.ReplaceSpecialChars('\\');
            
            Assert.Equal(@"\\", newText);
        }
        
        [Fact]
        public void Replace_Special_Chars_Back_Pipe()
        {
            var newText = SuperExpressive.ReplaceSpecialChars('|');
            
            Assert.Equal(@"\|", newText);
        }
        
        [Fact]
        public void Empty() 
        {
            var builder = new SuperExpressive();

            Assert.Equal("", builder.ToRegexString());
        }

        [Fact]
        public void Start_Of_Input()
        {
            var builder = new SuperExpressive();
            builder.StartOfInput();

            Assert.Equal("^", builder.ToRegexString());
        }

        [Fact]
        public void End_Of_Input()
        {
            var builder = new SuperExpressive();
            builder.EndOfInput();

            Assert.Equal("$", builder.ToRegexString());
        }

        [Fact]
        public void Capture()
        {
            var builder = new SuperExpressive();
            builder.Capture();

            Assert.Equal("(", builder.ToRegexString());
        }   

        [Fact]
        public void End()
        {
            var builder = new SuperExpressive();
            builder.End();

            Assert.Equal(")", builder.ToRegexString());
        }     

        [Fact]
        public void Start_Of_Input_End_Of_Input()
        {
            var builder = new SuperExpressive();
            builder
                .StartOfInput()
                .EndOfInput();

            Assert.Equal("^$", builder.ToRegexString());
        }   

        [Fact]
        public void Any_Char() 
        {
            var builder = new SuperExpressive();
            builder.AnyChar();

            Assert.Equal(".", builder.ToRegexString());
        }    

        [Fact]
        public void White_Space_Char() 
        {
            var builder = new SuperExpressive();
            builder.WhiteSpaceChar();

            Assert.Equal(@"\s", builder.ToRegexString());
        }       

        [Fact]
        public void NMon_White_Space_Char() 
        {
            var builder = new SuperExpressive();
            builder.NonWhiteSpaceChar();

            Assert.Equal(@"\S", builder.ToRegexString());
        }  

        [Fact]
        public void Digit() 
        {
            var builder = new SuperExpressive();
            builder.Digit();

            Assert.Equal(@"\d", builder.ToRegexString());
        }        

        [Fact]
        public void Non_Digit() 
        {
            var builder = new SuperExpressive();
            builder.NonDigit();

            Assert.Equal(@"\D", builder.ToRegexString());
        }  

        [Fact]
        public void Word() 
        {
            var builder = new SuperExpressive();
            builder.Word();

            Assert.Equal(@"\w", builder.ToRegexString());
        }     

        [Fact]
        public void Non_Word() 
        {
            var builder = new SuperExpressive();
            builder.NonWord();

            Assert.Equal(@"\W", builder.ToRegexString());
        }    

        [Fact]
        public void Word_Boundary() 
        {
            var builder = new SuperExpressive();
            builder.WordBoundary();

            Assert.Equal(@"\b", builder.ToRegexString());
        }    

        [Fact]
        public void Non_Word_Boundary() 
        {
            var builder = new SuperExpressive();
            builder.NonWordBoundary();

            Assert.Equal(@"\B", builder.ToRegexString());
        } 

        [Fact]
        public void New_Line() 
        {
            var builder = new SuperExpressive();
            builder.NewLine();

            Assert.Equal(@"\n", builder.ToRegexString());
        }      

        [Fact]
        public void Carriage_Return() 
        {
            var builder = new SuperExpressive();
            builder.CarriageReturn();

            Assert.Equal(@"\r", builder.ToRegexString());
        }                                                                     
        [Fact]
        public void Tab() 
        {
            var builder = new SuperExpressive();
            builder.Tab();

            Assert.Equal(@"\t", builder.ToRegexString());
        }    

        [Fact]
        public void Null_Byte() 
        {
            var builder = new SuperExpressive();
            builder.NullByte();

            Assert.Equal(@"\0", builder.ToRegexString());
        }         

        [Fact]
        public void String() 
        {
            var builder = new SuperExpressive();
            builder.String("test");

            Assert.Equal("test", builder.ToRegexString());
        }    

        [Fact]
        public void Char() 
        {
            var builder = new SuperExpressive();
            builder.Char('a');

            Assert.Equal("a", builder.ToRegexString());
        }  

        [Fact]
        public void Char_Dot() 
        {
            var builder = new SuperExpressive();
            builder.Char('.');

            Assert.Equal(@"\.", builder.ToRegexString());
        }         

        [Fact]
        public void Range_Alphabet() 
        {
            var builder = new SuperExpressive();
            builder.Range('a', 'z');

            Assert.Equal("a-z", builder.ToRegexString());
        }    

        [Fact]
        public void Range_Number() 
        {
            var builder = new SuperExpressive();
            builder.Range('0', '9');

            Assert.Equal("0-9", builder.ToRegexString());
        }          

        [Fact]
        public void Optional() 
        {
            var builder = new SuperExpressive();
            builder.Optional();

            Assert.Equal("?", builder.ToRegexString());
        }     

        [Fact]
        public void Zero_Or_More() 
        {
            var builder = new SuperExpressive();
            builder.ZeroOrMore();

            Assert.Equal("*", builder.ToRegexString());
        }          

        [Fact]
        public void One_Or_More() 
        {
            var builder = new SuperExpressive();
            builder.OneOrMore();

            Assert.Equal("+", builder.ToRegexString());
        }   

        [Fact]
        public void One_Or_More_Lazy() 
        {
            var builder = new SuperExpressive();
            builder.OneOrMoreLazy();

            Assert.Equal("+?", builder.ToRegexString());
        }  

        [Fact]
        public void Exactly() 
        {
            var builder = new SuperExpressive();
            builder.Exactly(4);

            Assert.Equal("{4}", builder.ToRegexString());
        }      

        [Fact]
        public void At_Least() 
        {
            var builder = new SuperExpressive();
            builder.AtLeast(4);

            Assert.Equal("{4,}", builder.ToRegexString());
        }  

        [Fact]
        public void Between() 
        {
            var builder = new SuperExpressive();
            builder.Between(4,6);

            Assert.Equal("{4,6}", builder.ToRegexString());
        }   

        [Fact]
        public void Between_Lazy() 
        {
            var builder = new SuperExpressive();
            builder.BetweenLazy(4,6);

            Assert.Equal("{4,6}?", builder.ToRegexString());
        }
    }
}