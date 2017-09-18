using System;
using System.Collections.Generic;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.GuidExtension
{
    public class ToStringArrayTest
    {
        [Fact]
        public void when_guids_is_null()
        {
            var guids = (IEnumerable<Guid>)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var stringArray = guids.ToStringArray();

            Assert.Equal(0, stringArray.Length);
        }

        [Fact]
        public void when_guids_is_empty()
        {
            var guids = new Guid[0];

            var stringArray = guids.ToStringArray();

            Assert.Equal(0, stringArray.Length);
        }

        [Fact]
        public void when_guids_is_vaild()
        {
            var guids = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            var stringArray = guids.ToStringArray();

            for (var i = 0; i < guids.Count; i++)
            {
                Assert.Equal(guids[i].ToString("N"), (stringArray[i]));
            }

        }
    }
}
