﻿namespace OmniXaml.Tests
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Classes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Typing;

    [TestClass]
    public class VisualizationTests : GivenAWiringContext
    {
        private readonly XamlNodeBuilder builder;

        public VisualizationTests()
        {
            builder = new XamlNodeBuilder(WiringContext.TypeContext);
        }

        [TestMethod]
        public void ConvertToTags()
        {
            var col = new Collection<XamlNode>()
            {
                builder.NamespacePrefixDeclaration("", "root"),
                builder.StartObject<DummyClass>(),
                builder.StartMember<DummyClass>(@class => @class.Child),
                builder.StartObject<ChildClass>(),
                builder.EndObject(),
                builder.EndMember(),
                builder.EndObject(),
            };

            var result = NodeVisualizer.ToTags(col);
        }

        [TestMethod]
        public void ConvertToNodes()
        {
            var col = new Collection<XamlNode>()
            {
                builder.NamespacePrefixDeclaration("", "root"),
                builder.StartObject<DummyClass>(),
                builder.StartMember<DummyClass>(@class => @class.Child),
                builder.StartObject<ChildClass>(),
                builder.EndObject(),
                builder.EndMember(),
                builder.EndObject(),
            };

            var result = NodeVisualizer.ToTree(col);
        }
    }

    [DebuggerDisplay("{Name}")]
    public class Tag
    {
        public string Name { get; private set; }
        public int Level { get; set; }

        public Tag(string name, int level)
        {
            Name = name;
            Level = level;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}