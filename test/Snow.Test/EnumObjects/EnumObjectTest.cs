using System;
using System.ComponentModel;
using Xunit;

namespace Snow.Test.EnumObjects
{

    public class EnumObjectTest
    {
        [Flags]
        public enum MockFlagsEnum
        {
            A = 1,
            B = 1 << 1,
            [Description("答案是C")]
            C = 1 << 2,
            D = 1 << 3,
            All = A | B | C | D
        }


        public enum MockEnum
        {
            A = 1,
            B = 2,
            C = 4,
            D = 8
        }

        [Fact]
        public void Value_property_test()
        {
            EnumObject enumObject = MockFlagsEnum.A;

            Assert.Equal(MockFlagsEnum.A, enumObject.Value);
        }

        [Fact]
        public void Raw_property_test()
        {
            EnumObject enumObject = MockFlagsEnum.A;

            Assert.Equal(1, enumObject.Raw);
        }

        [Fact]
        public void Type_property_test()
        {
            EnumObject enumObject = MockFlagsEnum.A;

            Assert.Same(typeof(MockFlagsEnum), enumObject.Type);
        }

        [Fact]
        public void Name_property_test()
        {
            EnumObject enumObject = MockFlagsEnum.All;

            Assert.Equal("All", enumObject.Name);
        }

        [Fact]
        public void Filed_property_test()
        {
            EnumObject enumObject = MockFlagsEnum.A;
            var aField = typeof(MockFlagsEnum).GetField("A");

            Assert.Same(aField, enumObject.Filed);
        }

        [Fact]
        public void IsFlag_property_test()
        {
            EnumObject enumObject = MockEnum.A;
            EnumObject flagEnumObject = MockFlagsEnum.A;

            Assert.False(enumObject.IsFlag);
            Assert.True(flagEnumObject.IsFlag);
        }

        [Fact]
        public void Description_property_test()
        {
            EnumObject hasDescriptionAttributeEnumObject = MockFlagsEnum.C;
            EnumObject noDescriptionAttributeEnumObject = MockFlagsEnum.All;

            Assert.Null(noDescriptionAttributeEnumObject.Description);
            Assert.Equal("答案是C", hasDescriptionAttributeEnumObject.Description);
        }

        [Fact]
        public void ToString_method_test()
        {
            EnumObject hasDescriptionAttributeEnumObject = MockFlagsEnum.C;
            EnumObject noDescriptionAttributeEnumObject = MockFlagsEnum.All;
            EnumObject enumObject1 = MockFlagsEnum.A | MockFlagsEnum.B | MockFlagsEnum.C | MockFlagsEnum.D;
            EnumObject enumObject2 = MockFlagsEnum.A | MockFlagsEnum.B | MockFlagsEnum.C;

            Assert.Equal("答案是C", hasDescriptionAttributeEnumObject.ToString());
            Assert.Equal("All", noDescriptionAttributeEnumObject.ToString());
            Assert.Equal("All", enumObject1.ToString());
            Assert.Equal("A, B, C", enumObject2.ToString());
        }

        [Fact]
        public void FlagEunms_property_test()
        {
            EnumObject flagEnumObject1 = MockFlagsEnum.A | MockFlagsEnum.C;
            EnumObject flagEnumObject2 = MockFlagsEnum.All | MockFlagsEnum.A;

            Assert.Null(flagEnumObject1.Name);

            Assert.Equal(new Enum[]
            {
                MockFlagsEnum.A,
                MockFlagsEnum.C
            }, flagEnumObject1.FlagEnums);

            Assert.Equal(new Enum[]
            {
                MockFlagsEnum.A,
                MockFlagsEnum.B,
                MockFlagsEnum.C,
                MockFlagsEnum.D,
                MockFlagsEnum.All
            }, flagEnumObject2.FlagEnums);
        }

        [Fact]
        public void GetNullableEnumFromDescription_method_test()
        {
            Assert.Null(EnumObject.GetNullableEnumFromDescription<MockFlagsEnum>(null));
            Assert.Null(EnumObject.GetNullableEnumFromDescription<MockEnum>("答案是C"));
            Assert.Equal(MockFlagsEnum.C, EnumObject.GetNullableEnumFromDescription<MockFlagsEnum>("答案是C"));
        }
        
    }
}
