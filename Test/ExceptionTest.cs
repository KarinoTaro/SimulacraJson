using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulacraJson;

namespace Test
{
    [TestClass]
    public class ExceptionTest
    {
        //
        // Exception
        //

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidOperationExceptionTestMethod1()
        {
            var json = JsonValue.Parse(@"{""Key"":1}");
            var a = json[0];
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidOperationExceptionTestMethod2()
        {
            var json = JsonValue.Parse(@"[1, 2]");
            var a = json["key"];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ArgumentOutOfRangeExceptionTestMethod()
        {
            var json = JsonValue.Parse(@"[1, 2, 3]");
            var a = json[5];
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void KeyNotFoundExceptionTestMethod()
        {
            var json = JsonValue.Parse(@"{""key1"":1, ""key2"": 2, ""key3"":3]");
            var a = json["key4"];
        }
    }
}
