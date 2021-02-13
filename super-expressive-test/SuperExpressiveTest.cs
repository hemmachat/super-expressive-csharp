using System;
using Xunit;
using SuperExpressive;

namespace SuperExpressive.Test
{
    public class SuperExpressiveTest
    {
        public SuperExpressiveTest () {
            
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
    }
}