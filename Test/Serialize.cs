using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulacraJson;

namespace Test
{
    [TestClass]
    public class Serialize
    {
        [TestMethod]
        public void NumberArraySerializeTestMethod()
        {
            var json = new JsonArray() {1, 2, 3, 4};
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"[1,2,3,4]");
        }

        [TestMethod]
        public void StringArraySerializeTestMethod()
        {
            var json = new JsonArray() { "one", "two", "three", "four" };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"[""one"",""two"",""three"",""four""]");
        }

        [TestMethod]
        public void NumberAndStringArraySerializeTestMethod()
        {
            var json = new JsonArray() { 1, 2, 3, 4, "one", "two", "three", "four" };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"[1,2,3,4,""one"",""two"",""three"",""four""]");
        }

        [TestMethod]
        public void BooleanAndNullArraySerializeTestMethod()
        {
            var json = new JsonArray() { true, false, null };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"[true,false,null]");
        }

        [TestMethod]
        public void StringObjectSerializeTestMethod()
        {
            var json = new JsonObject() { { "key1", "one" }, { "key2", "two" }, { "key3", "three" }, { "key4", "four" } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":""one"",""key2"":""two"",""key3"":""three"",""key4"":""four""}");
        }

        [TestMethod]
        public void NumberAndStringObjectSerializeTestMethod()
        {
            var json = new JsonObject() { { "key1", 1 }, { "key2", 2 }, { "key3", "three" }, { "key4", "four" } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":1,""key2"":2,""key3"":""three"",""key4"":""four""}");
        }

        [TestMethod]
        public void BooleanAndNullObjectSerializeTestMethod()
        {
            var json = new JsonObject() { { "key1", true }, { "key2", false }, { "key3", null }, { "key4", 4 } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":true,""key2"":false,""key3"":null,""key4"":4}");
        }

        [TestMethod]
        public void ArrayUnderArraySerializeTestMethod()
        {
            var json = new JsonArray() { 1, 2, 3, new JsonArray() { 11, 12, 13 } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"[1,2,3,[11,12,13]]");
        }

        [TestMethod]
        public void ObjectUnderArraySerializeTestMethod()
        {
            var json = new JsonArray() { 1, 2, 3, new JsonObject() { { "key11", 11 }, { "key12", 12 }, { "key13", 13 } } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"[1,2,3,{""key11"":11,""key12"":12,""key13"":13}]");
        }

        [TestMethod]
        public void ArrayUnderObjectSerializeTestMethod()
        {
            var json = new JsonObject() { { "key1", 1 }, { "key2", 2 }, { "key3", 3}, { "key4", new JsonArray() { 11, 12, 13 } } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":1,""key2"":2,""key3"":3,""key4"":[11,12,13]}");
        }

        [TestMethod]
        public void ObjectUnderObjectSerializeTestMethod()
        {
            var json = new JsonObject() { { "key1", 1 }, { "key2", 2 }, { "key3", 3 }, { "key4", new JsonObject() { { "key11", 11 }, { "key12", 12 }, { "key13", 13 } } } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":1,""key2"":2,""key3"":3,""key4"":{""key11"":11,""key12"":12,""key13"":13}}");
        }

        [TestMethod]
        public void MultibyteStringSerializeTestMethod()
        {
            Json.SerializationWithoutEscape = false;        // default
            var json = new JsonObject() { { "key1", "漢字" }, { "key2", "あい" }, { "key3", "１" } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":""\u6f22\u5b57"",""key2"":""\u3042\u3044"",""key3"":""\uff11""}");
        }

        [TestMethod]
        public void MultibyteStringWithoutEscapeSerializeTestMethod()
        {
            Json.SerializationWithoutEscape = true;
            var json = new JsonObject() { { "key1", "漢字" }, { "key2", "あい" }, { "key3", "１" } };
            var serialize = json.ToString();
            Assert.AreEqual(serialize, @"{""key1"":""漢字"",""key2"":""あい"",""key3"":""１""}");
        }
    }
}
