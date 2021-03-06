﻿namespace OmniXaml.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Attributes;
    using Builder;
    using Classes;
    using Classes.Another;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Typing;
    using Xaml.Tests.Resources;

    [TestClass]
    public class XamlXmlLoaderBuilderTests
    {

        [TestMethod]
        public void AttributeBasedConfigurationForNamespaces()
        {
            var rootType = typeof(DummyClass);
            var sut = new XamlXmlLoaderBuilder();

            sut.WithNamespaces(XamlNamespace.DefinedInAssemblies(new[] { rootType.Assembly }));
            Assert.IsTrue(sut.NamespaceRegistrations.Any());
        }

        [TestMethod]
        public void AttributeBasedConfigurationForContentProperties()
        {
            var rootType = typeof(DummyClass);
            var sut = new XamlXmlLoaderBuilder();

            sut.WithContentProperties(ContentProperties.DefinedInAssemblies(new[] { rootType.Assembly }));
            Assert.IsTrue(sut.ContentPropertyDefinitions.Any());
        }

        [TestMethod]
        public void BasicConfigurationWithPrefixes()
        {
            var rootType = typeof (DummyClass);
            var anotherType = typeof (Foreigner);

            var sut = new XamlXmlLoaderBuilder();

            var definitionForRoot = XamlNamespace
                .Map("root")
                .With(
                    new[]
                    {
                        Route.Assembly(rootType.Assembly)
                            .WithNamespaces(new[] {rootType.Namespace})
                    });

            var definitionForAnother = XamlNamespace
                .Map("another")
                .With(
                    new[]
                    {
                        Route.Assembly(anotherType.Assembly)
                            .WithNamespaces(new[] {anotherType.Namespace})
                    });

            sut.WithNamespaces(new List<XamlNamespace> {definitionForRoot, definitionForAnother})
                .WithNsPrefixes(
                    new List<PrefixRegistration>
                    {
                        new PrefixRegistration(string.Empty, "root"),
                        new PrefixRegistration("x", "another")
                    });

            var loader = sut.Build();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Dummy.DifferentNamespaces)))
            {
                var result = loader.Load(stream);
            }
        }
    }
}
