using System;
using System.ComponentModel;
using Xunit;
using UOKOFramework.Extensions;

namespace UOKOFramework.Test.Extensions.EnumExtension
{
    public class GetEnumsTest
    {
        [Flags]
        public enum MockEnum
        {
            A = 1,
            B = 1 << 1,
            C = 1 << 2,
            D = 1 << 3,
            All = A | B | C | D
        }

        [Fact]
        public void when_this_type_is_not_match_generic_type_whould_throw_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => MockEnum.A.GetEnums<ConsoleKey>());
        }

        [Fact]
        public void when_enum_is_not_FlagsAttribute_whould_throw_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Constellation.Aquarius.GetEnums<Constellation>());
        }

        [Fact]
        public void when_enum_is_valid()
        {
            Assert.Equal(new[]
           {
                MockEnum.A,
                MockEnum.B
            }, (MockEnum.A | MockEnum.B).GetEnums<MockEnum>());

            Assert.Equal(new[]
            {
                MockEnum.A,
                MockEnum.B,
                MockEnum.C,
                MockEnum.D,
                MockEnum.All
            }, MockEnum.All.GetEnums<MockEnum>());
        }
    }
}
