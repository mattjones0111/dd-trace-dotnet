using System.Collections.Generic;
using Datadog.Trace.ClrProfiler.CallTarget.DuckTyping;
using Xunit;

#pragma warning disable SA1201 // Elements must appear in the correct order

namespace Datadog.Trace.ClrProfiler.Managed.Tests.CallTarget.DuckTyping
{
    public class DuckTypeValueTypePropertyTests
    {
        public static IEnumerable<object[]> Data()
        {
            return new[]
            {
                new object[] { ObscureObject.GetPropertyPublicObject() },
                new object[] { ObscureObject.GetPropertyInternalObject() },
                new object[] { ObscureObject.GetPropertyPrivateObject() },
            };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void StaticPropertyOnlySetException(object obscureObject)
        {
            Assert.Throws<DuckTypePropertyCantBeWrittenException>(() =>
            {
                obscureObject.As<IObscureStaticErrorDuckType>();
            });
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void PropertyOnlySetException(object obscureObject)
        {
            Assert.Throws<DuckTypePropertyCantBeWrittenException>(() =>
            {
                obscureObject.As<IObscureErrorDuckType>();
            });
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void StaticOnlyGetProperties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            // *
            Assert.Equal(10, duckInterface.PublicStaticGetValueType);
            Assert.Equal(10, duckAbstract.PublicStaticGetValueType);
            Assert.Equal(10, duckVirtual.PublicStaticGetValueType);

            // *
            Assert.Equal(11, duckInterface.InternalStaticGetValueType);
            Assert.Equal(11, duckAbstract.InternalStaticGetValueType);
            Assert.Equal(11, duckVirtual.InternalStaticGetValueType);

            // *
            Assert.Equal(12, duckInterface.ProtectedStaticGetValueType);
            Assert.Equal(12, duckAbstract.ProtectedStaticGetValueType);
            Assert.Equal(12, duckVirtual.ProtectedStaticGetValueType);

            // *
            Assert.Equal(13, duckInterface.PrivateStaticGetValueType);
            Assert.Equal(13, duckAbstract.PrivateStaticGetValueType);
            Assert.Equal(13, duckVirtual.PrivateStaticGetValueType);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void StaticProperties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            Assert.Equal(20, duckInterface.PublicStaticGetSetValueType);
            Assert.Equal(20, duckAbstract.PublicStaticGetSetValueType);
            Assert.Equal(20, duckVirtual.PublicStaticGetSetValueType);

            duckInterface.PublicStaticGetSetValueType = 42;
            Assert.Equal(42, duckInterface.PublicStaticGetSetValueType);
            Assert.Equal(42, duckAbstract.PublicStaticGetSetValueType);
            Assert.Equal(42, duckVirtual.PublicStaticGetSetValueType);

            duckAbstract.PublicStaticGetSetValueType = 50;
            Assert.Equal(50, duckInterface.PublicStaticGetSetValueType);
            Assert.Equal(50, duckAbstract.PublicStaticGetSetValueType);
            Assert.Equal(50, duckVirtual.PublicStaticGetSetValueType);

            duckVirtual.PublicStaticGetSetValueType = 60;
            Assert.Equal(60, duckInterface.PublicStaticGetSetValueType);
            Assert.Equal(60, duckAbstract.PublicStaticGetSetValueType);
            Assert.Equal(60, duckVirtual.PublicStaticGetSetValueType);

            // *

            Assert.Equal(21, duckInterface.InternalStaticGetSetValueType);
            Assert.Equal(21, duckAbstract.InternalStaticGetSetValueType);
            Assert.Equal(21, duckVirtual.InternalStaticGetSetValueType);

            duckInterface.InternalStaticGetSetValueType = 42;
            Assert.Equal(42, duckInterface.InternalStaticGetSetValueType);
            Assert.Equal(42, duckAbstract.InternalStaticGetSetValueType);
            Assert.Equal(42, duckVirtual.InternalStaticGetSetValueType);

            duckAbstract.InternalStaticGetSetValueType = 50;
            Assert.Equal(50, duckInterface.InternalStaticGetSetValueType);
            Assert.Equal(50, duckAbstract.InternalStaticGetSetValueType);
            Assert.Equal(50, duckVirtual.InternalStaticGetSetValueType);

            duckVirtual.InternalStaticGetSetValueType = 60;
            Assert.Equal(60, duckInterface.InternalStaticGetSetValueType);
            Assert.Equal(60, duckAbstract.InternalStaticGetSetValueType);
            Assert.Equal(60, duckVirtual.InternalStaticGetSetValueType);

            // *

            Assert.Equal(22, duckInterface.ProtectedStaticGetSetValueType);
            Assert.Equal(22, duckAbstract.ProtectedStaticGetSetValueType);
            Assert.Equal(22, duckVirtual.ProtectedStaticGetSetValueType);

            duckInterface.ProtectedStaticGetSetValueType = 42;
            Assert.Equal(42, duckInterface.ProtectedStaticGetSetValueType);
            Assert.Equal(42, duckAbstract.ProtectedStaticGetSetValueType);
            Assert.Equal(42, duckVirtual.ProtectedStaticGetSetValueType);

            duckAbstract.ProtectedStaticGetSetValueType = 50;
            Assert.Equal(50, duckInterface.ProtectedStaticGetSetValueType);
            Assert.Equal(50, duckAbstract.ProtectedStaticGetSetValueType);
            Assert.Equal(50, duckVirtual.ProtectedStaticGetSetValueType);

            duckVirtual.ProtectedStaticGetSetValueType = 60;
            Assert.Equal(60, duckInterface.ProtectedStaticGetSetValueType);
            Assert.Equal(60, duckAbstract.ProtectedStaticGetSetValueType);
            Assert.Equal(60, duckVirtual.ProtectedStaticGetSetValueType);

            // *

            Assert.Equal(23, duckInterface.PrivateStaticGetSetValueType);
            Assert.Equal(23, duckAbstract.PrivateStaticGetSetValueType);
            Assert.Equal(23, duckVirtual.PrivateStaticGetSetValueType);

            duckInterface.PrivateStaticGetSetValueType = 42;
            Assert.Equal(42, duckInterface.PrivateStaticGetSetValueType);
            Assert.Equal(42, duckAbstract.PrivateStaticGetSetValueType);
            Assert.Equal(42, duckVirtual.PrivateStaticGetSetValueType);

            duckAbstract.PrivateStaticGetSetValueType = 50;
            Assert.Equal(50, duckInterface.PrivateStaticGetSetValueType);
            Assert.Equal(50, duckAbstract.PrivateStaticGetSetValueType);
            Assert.Equal(50, duckVirtual.PrivateStaticGetSetValueType);

            duckVirtual.PrivateStaticGetSetValueType = 60;
            Assert.Equal(60, duckInterface.PrivateStaticGetSetValueType);
            Assert.Equal(60, duckAbstract.PrivateStaticGetSetValueType);
            Assert.Equal(60, duckVirtual.PrivateStaticGetSetValueType);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void OnlyGetProperties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            // *
            Assert.Equal(30, duckInterface.PublicGetValueType);
            Assert.Equal(30, duckAbstract.PublicGetValueType);
            Assert.Equal(30, duckVirtual.PublicGetValueType);

            // *
            Assert.Equal(31, duckInterface.InternalGetValueType);
            Assert.Equal(31, duckAbstract.InternalGetValueType);
            Assert.Equal(31, duckVirtual.InternalGetValueType);

            // *
            Assert.Equal(32, duckInterface.ProtectedGetValueType);
            Assert.Equal(32, duckAbstract.ProtectedGetValueType);
            Assert.Equal(32, duckVirtual.ProtectedGetValueType);

            // *
            Assert.Equal(33, duckInterface.PrivateGetValueType);
            Assert.Equal(33, duckAbstract.PrivateGetValueType);
            Assert.Equal(33, duckVirtual.PrivateGetValueType);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Properties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            Assert.Equal(40, duckInterface.PublicGetSetValueType);
            Assert.Equal(40, duckAbstract.PublicGetSetValueType);
            Assert.Equal(40, duckVirtual.PublicGetSetValueType);

            duckInterface.PublicGetSetValueType = 42;
            Assert.Equal(42, duckInterface.PublicGetSetValueType);
            Assert.Equal(42, duckAbstract.PublicGetSetValueType);
            Assert.Equal(42, duckVirtual.PublicGetSetValueType);

            duckAbstract.PublicGetSetValueType = 50;
            Assert.Equal(50, duckInterface.PublicGetSetValueType);
            Assert.Equal(50, duckAbstract.PublicGetSetValueType);
            Assert.Equal(50, duckVirtual.PublicGetSetValueType);

            duckVirtual.PublicGetSetValueType = 60;
            Assert.Equal(60, duckInterface.PublicGetSetValueType);
            Assert.Equal(60, duckAbstract.PublicGetSetValueType);
            Assert.Equal(60, duckVirtual.PublicGetSetValueType);

            // *

            Assert.Equal(41, duckInterface.InternalGetSetValueType);
            Assert.Equal(41, duckAbstract.InternalGetSetValueType);
            Assert.Equal(41, duckVirtual.InternalGetSetValueType);

            duckInterface.InternalGetSetValueType = 42;
            Assert.Equal(42, duckInterface.InternalGetSetValueType);
            Assert.Equal(42, duckAbstract.InternalGetSetValueType);
            Assert.Equal(42, duckVirtual.InternalGetSetValueType);

            duckAbstract.InternalGetSetValueType = 50;
            Assert.Equal(50, duckInterface.InternalGetSetValueType);
            Assert.Equal(50, duckAbstract.InternalGetSetValueType);
            Assert.Equal(50, duckVirtual.InternalGetSetValueType);

            duckVirtual.InternalGetSetValueType = 60;
            Assert.Equal(60, duckInterface.InternalGetSetValueType);
            Assert.Equal(60, duckAbstract.InternalGetSetValueType);
            Assert.Equal(60, duckVirtual.InternalGetSetValueType);

            // *

            Assert.Equal(42, duckInterface.ProtectedGetSetValueType);
            Assert.Equal(42, duckAbstract.ProtectedGetSetValueType);
            Assert.Equal(42, duckVirtual.ProtectedGetSetValueType);

            duckInterface.ProtectedGetSetValueType = 45;
            Assert.Equal(45, duckInterface.ProtectedGetSetValueType);
            Assert.Equal(45, duckAbstract.ProtectedGetSetValueType);
            Assert.Equal(45, duckVirtual.ProtectedGetSetValueType);

            duckAbstract.ProtectedGetSetValueType = 50;
            Assert.Equal(50, duckInterface.ProtectedGetSetValueType);
            Assert.Equal(50, duckAbstract.ProtectedGetSetValueType);
            Assert.Equal(50, duckVirtual.ProtectedGetSetValueType);

            duckVirtual.ProtectedGetSetValueType = 60;
            Assert.Equal(60, duckInterface.ProtectedGetSetValueType);
            Assert.Equal(60, duckAbstract.ProtectedGetSetValueType);
            Assert.Equal(60, duckVirtual.ProtectedGetSetValueType);

            // *

            Assert.Equal(43, duckInterface.PrivateGetSetValueType);
            Assert.Equal(43, duckAbstract.PrivateGetSetValueType);
            Assert.Equal(43, duckVirtual.PrivateGetSetValueType);

            duckInterface.PrivateGetSetValueType = 42;
            Assert.Equal(42, duckInterface.PrivateGetSetValueType);
            Assert.Equal(42, duckAbstract.PrivateGetSetValueType);
            Assert.Equal(42, duckVirtual.PrivateGetSetValueType);

            duckAbstract.PrivateGetSetValueType = 50;
            Assert.Equal(50, duckInterface.PrivateGetSetValueType);
            Assert.Equal(50, duckAbstract.PrivateGetSetValueType);
            Assert.Equal(50, duckVirtual.PrivateGetSetValueType);

            duckVirtual.PrivateGetSetValueType = 60;
            Assert.Equal(60, duckInterface.PrivateGetSetValueType);
            Assert.Equal(60, duckAbstract.PrivateGetSetValueType);
            Assert.Equal(60, duckVirtual.PrivateGetSetValueType);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Indexer(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            duckInterface[1] = 100;
            Assert.Equal(100, duckInterface[1]);
            Assert.Equal(100, duckAbstract[1]);
            Assert.Equal(100, duckVirtual[1]);

            duckAbstract[2] = 200;
            Assert.Equal(200, duckInterface[2]);
            Assert.Equal(200, duckAbstract[2]);
            Assert.Equal(200, duckVirtual[2]);

            duckVirtual[3] = 300;
            Assert.Equal(300, duckInterface[3]);
            Assert.Equal(300, duckAbstract[3]);
            Assert.Equal(300, duckVirtual[3]);
        }

        public interface IObscureDuckType
        {
            int PublicStaticGetValueType { get; }

            int InternalStaticGetValueType { get; }

            int ProtectedStaticGetValueType { get; }

            int PrivateStaticGetValueType { get; }

            // *

            int PublicStaticGetSetValueType { get; set; }

            int InternalStaticGetSetValueType { get; set; }

            int ProtectedStaticGetSetValueType { get; set; }

            int PrivateStaticGetSetValueType { get; set; }

            // *

            int PublicGetValueType { get; }

            int InternalGetValueType { get; }

            int ProtectedGetValueType { get; }

            int PrivateGetValueType { get; }

            // *

            int PublicGetSetValueType { get; set; }

            int InternalGetSetValueType { get; set; }

            int ProtectedGetSetValueType { get; set; }

            int PrivateGetSetValueType { get; set; }

            // *

            int this[int index] { get; set; }
        }

        public interface IObscureStaticErrorDuckType
        {
            int PublicStaticGetValueType { get; set; }
        }

        public interface IObscureErrorDuckType
        {
            int PublicGetValueType { get; set; }
        }

        public abstract class ObscureDuckTypeAbstractClass
        {
            public abstract int PublicStaticGetValueType { get; }

            public abstract int InternalStaticGetValueType { get; }

            public abstract int ProtectedStaticGetValueType { get; }

            public abstract int PrivateStaticGetValueType { get; }

            // *

            public abstract int PublicStaticGetSetValueType { get; set; }

            public abstract int InternalStaticGetSetValueType { get; set; }

            public abstract int ProtectedStaticGetSetValueType { get; set; }

            public abstract int PrivateStaticGetSetValueType { get; set; }

            // *

            public abstract int PublicGetValueType { get; }

            public abstract int InternalGetValueType { get; }

            public abstract int ProtectedGetValueType { get; }

            public abstract int PrivateGetValueType { get; }

            // *

            public abstract int PublicGetSetValueType { get; set; }

            public abstract int InternalGetSetValueType { get; set; }

            public abstract int ProtectedGetSetValueType { get; set; }

            public abstract int PrivateGetSetValueType { get; set; }

            // *

            public abstract int this[int index] { get; set; }
        }

        public class ObscureDuckType
        {
            public virtual int PublicStaticGetValueType { get; }

            public virtual int InternalStaticGetValueType { get; }

            public virtual int ProtectedStaticGetValueType { get; }

            public virtual int PrivateStaticGetValueType { get; }

            // *

            public virtual int PublicStaticGetSetValueType { get; set; }

            public virtual int InternalStaticGetSetValueType { get; set; }

            public virtual int ProtectedStaticGetSetValueType { get; set; }

            public virtual int PrivateStaticGetSetValueType { get; set; }

            // *

            public virtual int PublicGetValueType { get; }

            public virtual int InternalGetValueType { get; }

            public virtual int ProtectedGetValueType { get; }

            public virtual int PrivateGetValueType { get; }

            // *

            public virtual int PublicGetSetValueType { get; set; }

            public virtual int InternalGetSetValueType { get; set; }

            public virtual int ProtectedGetSetValueType { get; set; }

            public virtual int PrivateGetSetValueType { get; set; }

            // *

            public virtual int this[int index]
            {
                get => default;
                set { }
            }
        }
    }
}