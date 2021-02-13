using System;
using Xunit;
using SuperExpressive;

namespace SuperExpressive.Test
{
    public class SuperExpressiveBuilderTest
    {
        public SuperExpressiveBuilderTest () {
            
        }

        [Fact]
        public void Start_Of_Input()
        {
            var builder = new SuperExpressiveBuilder();
            builder.StartOfInput();

            Assert.Equal("^", builder.ToRegexString());
        }

        [Fact]
        public void End_Of_Input()
        {
            var builder = new SuperExpressiveBuilder();
            builder.EndOfInput();

            Assert.Equal("$", builder.ToRegexString());
        }

        [Fact]
        public void Capture()
        {
            var builder = new SuperExpressiveBuilder();
            builder.Capture();

            Assert.Equal("(", builder.ToRegexString());
        }   

        [Fact]
        public void End()
        {
            var builder = new SuperExpressiveBuilder();
            builder.End();

            Assert.Equal(")", builder.ToRegexString());
        }              
    }
}