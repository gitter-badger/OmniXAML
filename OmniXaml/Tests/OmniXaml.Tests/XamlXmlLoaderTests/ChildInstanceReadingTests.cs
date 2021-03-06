﻿namespace OmniXaml.Tests.XamlXmlLoaderTests
{
    using System.Diagnostics;
    using Classes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OmniXaml.Reader.Tests.Wpf;
    using Xaml.Tests.Resources;

    [TestClass]
    public class ChildInstanceReadingTests : GivenAXamlXmlLoader
    {
        [TestMethod]
        public void ReadInstanceWithChild()
        {
            var actualInstance = Loader.Load(Dummy.InstanceWithChild);

            Assert.IsInstanceOfType(actualInstance, typeof(DummyClass), "The retrieved instance should be of type DummyClass");
            var dummyClass = actualInstance as DummyClass;
            Debug.Assert(dummyClass != null, "dummyClass != null");            
            Assert.IsInstanceOfType(dummyClass.Child, typeof(ChildClass));
        }


        [TestMethod]
        public void ReadInstanceWithThreeLevelsOfNesting()
        {
            var root = Loader.Load(Dummy.ThreeLevelsOfNesting);
            
            var dummy = root as DummyClass;
            Assert.IsInstanceOfType(root, typeof(DummyClass), "The retrieved instance should be of type DummyClass");

            Debug.Assert(dummy != null, "dummy != null");
            var level2Instance = dummy.Child;
            Assert.IsNotNull(level2Instance);

            var level3Instance = level2Instance.Child;
            Assert.IsNotNull(level3Instance);
        }
    }
}