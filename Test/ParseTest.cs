using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulacraJson;

namespace Test
{
    [TestClass]
    public class ParseTest
    {
        //
        // Parse
        //

        [TestMethod]
        public void NumberArrayParseTestMethod()
        {
            var json = JsonValue.Parse(@"[1, 2, 3, 4]");
            int array0 = json[0];
            Assert.AreEqual(array0, 1);
            int array3 = json[3];
            Assert.AreEqual(array3, 4);
        }

        [TestMethod]
        public void StringArrayParseTestMethod()
        {
            var json = JsonValue.Parse(@"[""one"", ""two"", ""three"", ""four""]");
            string array0 = json[0];
            Assert.AreEqual(array0, "one");
            string array3 = json[3];
            Assert.AreEqual(array3, "four");
        }

        [TestMethod]
        public void MultiByteStringArrayParseTestMethod()
        {
            var json = JsonValue.Parse(@"[""１"", ""２"", ""３"", ""４""]");
            string array0 = json[0];
            Assert.AreEqual(array0, "１");
            string array3 = json[3];
            Assert.AreEqual(array3, "４");
        }

        [TestMethod]
        public void ArrayUnderArrayParseTestMethod()
        {
            var json = JsonValue.Parse(@"[1, 2, 3, [11, 12, 13]]");
            int array0 = json[3][0];
            Assert.AreEqual(array0, 11);
            int array3 = json[3][2];
            Assert.AreEqual(array3, 13);
        }

        [TestMethod]
        public void ObjectUnderArrayParseTestMethod()
        {
            var json = JsonValue.Parse(@"[1, 2, 3, {""key1"":11,""key2"":12,""key3"":13}]");
            int array0 = json[3]["key1"];
            Assert.AreEqual(array0, 11);
            int array3 = json[3]["key3"];
            Assert.AreEqual(array3, 13);
        }

        [TestMethod]
        public void BooleanAndNullArrayParseTestMethod()
        {
            var json = JsonValue.Parse(@"[true, false, null]");
            bool array0 = json[0];
            Assert.IsTrue(array0);
            bool array1 = json[1];
            Assert.IsFalse(array1);
            var array2 = json[2];
            Assert.IsNull(array2);
        }
        
        [TestMethod]
        public void NumberObjectParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""Key1"":1, ""Key2"":2, ""Key3"":3, ""Key4"":4}");
            int objectKey1 = json["Key1"];
            Assert.AreEqual(objectKey1, 1);
            int objectKey4 = json["Key4"];
            Assert.AreEqual(objectKey4, 4);
            JsonValue value;
            var getOk = ((JsonObject)json).TryGetValue("Key1", out value);
            Assert.IsTrue(getOk);
            Assert.AreEqual((int)value, 1);
            var getNg = ((JsonObject)json).TryGetValue("NothingKey", out value);
            Assert.IsFalse(getNg);
        }

        [TestMethod]
        public void StringObjectParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""Key1"":""one"", ""Key2"":""two"", ""Key3"":""three"", ""Key4"":""four""}");
            string objectKey1 = json["Key1"];
            Assert.AreEqual(objectKey1, "one");
            string objectKey4 = json["Key4"];
            Assert.AreEqual(objectKey4, "four");
        }

        [TestMethod]
        public void MultiByteStringObjectParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""Key1"":""一"", ""Key2"":""二"", ""Key3"":""三"", ""Key4"":""四""}");
            string objectKey1 = json["Key1"];
            Assert.AreEqual(objectKey1, "一");
            string objectKey4 = json["Key4"];
            Assert.AreEqual(objectKey4, "四");
        }

        [TestMethod]
        public void ArrayUnderObjectParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""Key1"":1, ""Key2"":2, ""Key3"":3, ""Key4"":[11,12,13]}");
            int objectKey1 = json["Key4"][0];
            Assert.AreEqual(objectKey1, 11);
            int objectKey4 = json["Key4"][2];
            Assert.AreEqual(objectKey4, 13);
        }

        [TestMethod]
        public void ObjectUnderObjectParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""Key1"":1, ""Key2"":2, ""Key3"":3, ""Key4"":{""Key11"":11, ""Key12"":12, ""Key13"":13, ""Key14"":14}}");
            int objectKey1 = json["Key4"]["Key11"];
            Assert.AreEqual(objectKey1, 11);
            int objectKey4 = json["Key4"]["Key14"];
            Assert.AreEqual(objectKey4, 14);
        }
        
        [TestMethod]
        public void BooleanAndNullArrayObjectParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""TRUE"":true, ""FALSE"":false, ""NULL"":null}");
            bool array0 = json["TRUE"];
            Assert.IsTrue(array0);
            bool array1 = json["FALSE"];
            Assert.IsFalse(array1);
            var array2 = json["NULL"];
            Assert.IsNull(array2);
        }

        [TestMethod]
        public void NumberMinMaxParseTestMethod()
        {
            var json = JsonValue.Parse(@"{""MIN"":" + Int64.MinValue + @", ""MAX"":" + Int64.MaxValue + @"}");
            Int64 min = json["MIN"];
            Assert.AreEqual(min, Int64.MinValue);
            Int64 max = json["MAX"];
            Assert.AreEqual(max, Int64.MaxValue);
        }

        [TestMethod]
        public void EscapeCharactersParseTestMethod()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append("\"\\uff11\"");       // \uff11
            sb.Append(",");
            sb.Append("\"\\\"\"");          // \"
            sb.Append(",");
            sb.Append("\"\\\\\"");          // \\
            sb.Append(",");
            sb.Append("\"\\/\"");           // \/
            sb.Append(",");
            sb.Append("\"\\b\"");           // \b
            sb.Append(",");
            sb.Append("\"\\f\"");           // \f
            sb.Append(",");
            sb.Append("\"\\n\"");           // \n
            sb.Append(",");
            sb.Append("\"\\r\"");           // \r
            sb.Append(",");
            sb.Append("\"\\t\"");           // \t
            sb.Append("]");
            var json = JsonValue.Parse(sb.ToString());
            string escapeCharacters0 = json[0];
            Assert.AreEqual(escapeCharacters0, "１");
            string escapeCharacters1 = json[1];
            Assert.AreEqual(escapeCharacters1, "\"");
            string escapeCharacters2 = json[2];
            Assert.AreEqual(escapeCharacters2, "\\");
            string escapeCharacters3 = json[3];
            Assert.AreEqual(escapeCharacters3, "/");
            string escapeCharacters4 = json[4];
            Assert.AreEqual(escapeCharacters4, "\b");
            string escapeCharacters5 = json[5];
            Assert.AreEqual(escapeCharacters5, "\f");
            string escapeCharacters6 = json[6];
            Assert.AreEqual(escapeCharacters6, "\n");
            string escapeCharacters7 = json[7];
            Assert.AreEqual(escapeCharacters7, "\r");
            string escapeCharacters8 = json[8];
            Assert.AreEqual(escapeCharacters8, "\t");
        }

        //
        // Exception
        //

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullExceptionParseTestMethod()
        {
            var json = JsonValue.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentExceptionParseTestMethod()
        {
            var json = JsonValue.Parse(@"");
        }

    }
}
