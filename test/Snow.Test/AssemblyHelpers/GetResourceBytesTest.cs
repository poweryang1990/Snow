﻿using System;
using System.Reflection;
using Xunit;
using Snow.Extensions;

namespace Snow.Test.AssemblyHelpers
{
    public class GetResourceBytesTest
    {
        [Fact]
        public void when_assembly_is_null_should_throw_ArgumentNullException()
        {
            var assemblyHelper = AssemblyHelper.New();

            Assert.Throws<ArgumentNullException>(() => assemblyHelper.GetResourceBytes(null, "resource.txt", true));
            Assert.Throws<ArgumentNullException>(() => assemblyHelper.GetResourceBytes(null, _ => true));
        }

        [Fact]
        public void when_resourcename_is_null_should_throw_ArgumentNullException()
        {
            var assemblyHelper = AssemblyHelper.New();
            var assembly = Assembly.GetExecutingAssembly();

            Assert.Throws<ArgumentNullException>(() => assemblyHelper.GetResourceBytes(assembly, (string)null, false));
            Assert.Throws<ArgumentNullException>(() => assemblyHelper.GetResourceBytes(assembly, " ", false));
        }

        [Fact]
        public void when_predicate_is_null_should_throw_ArgumentNullException()
        {
            var assemblyHelper = AssemblyHelper.New();
            var assembly = Assembly.GetExecutingAssembly();

            Assert.Throws<ArgumentNullException>(() => assemblyHelper.GetResourceBytes(assembly, (Func<string, bool>)null));
        }

        [Fact]
        public void GetResouceBytes()
        {
            var assemblyHelper = AssemblyHelper.New();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceContent = "MOCK程序集中的资源文件,不要删除。";
            var resourceBytes = assemblyHelper.GetResourceBytes(
                assembly, "resource.txt", false);

            Assert.Equal(resourceContent, resourceBytes.GetString());
        }
    }
}