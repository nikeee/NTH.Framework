using System;
using System.Linq;
using System.Reflection;
using NTH.Diagnostics;

namespace NTH.Framework
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal abstract class ParameterBaseAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }

        protected ParameterBaseAttribute(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal class ParameterAttribute : ParameterBaseAttribute
    {
        public bool IsRequired { get; private set; }

        public ParameterAttribute(string name, bool isRequired, string alias)
            : base(name, alias)
        {
            IsRequired = isRequired;
        }

        public ParameterAttribute(string name, bool isRequired)
            : this(name, isRequired, null)
        { }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal class ParameterSwitchAttribute : ParameterBaseAttribute
    {

        public ParameterSwitchAttribute(string name, string alias)
            : base(name, alias)
        {
        }

    }

    abstract class ArgumentsBase
    {
        public bool IsValid { get; private set; }

        public static T Parse<T>(string arguments) where T : ArgumentsBase, new()
        {
            var args = ArgumentList.Parse(arguments);
            return FromArgumentList<T>(args);
        }

        public static T FromArgumentList<T>(ArgumentList arguments) where T : ArgumentsBase, new()
        {
            var targetType = typeof(T);

            var fields = targetType.GetFields(BindingFlags.Instance);
            var props = targetType.GetProperties(BindingFlags.Instance);

            var relevantFields = fields.Where(f => f.GetCustomAttribute<ParameterBaseAttribute>() != null);
            var relevantProps = props.Where(p => p.GetCustomAttribute<ParameterBaseAttribute>() != null);

            for (int i = 0; i < arguments.Count; ++i)
            {
                var s = arguments[i];

                var currentType = ArgType.Data;

                if (s.StartsWith("--"))
                {
                    currentType = ArgType.Name;
                    s = s.Substring("--".Length - 1);
                }
                else if (s.StartsWith("-"))
                {
                    currentType = ArgType.Alias;
                    s = s.Substring("-".Length - 1);
                }

                var matchedFields = relevantFields.Where(f =>
                                     {
                                         var attr = f.GetCustomAttribute<ParameterAttribute>();
                                         switch (currentType)
                                         {
                                             case ArgType.Name:
                                                 return s.Equals(attr.Name, StringComparison.InvariantCultureIgnoreCase);
                                             case ArgType.Alias:
                                                 return attr.Alias != null && s.Equals(attr.Alias, StringComparison.InvariantCultureIgnoreCase);
                                             default:
                                                 return false;
                                         }
                                     });

                // TODO
            }


            throw new NotImplementedException();
        }

        private enum ArgType
        {
            Name,
            Alias,
            Data
        }
    }

    class TestArguments : ArgumentsBase
    {
        [Parameter("filename", true, "f")]
        public string FileName { get; set; }
    }
}
