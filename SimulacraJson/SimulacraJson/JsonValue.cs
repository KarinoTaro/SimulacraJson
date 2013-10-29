using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;

// http://msdn.microsoft.com/ja-jp/library/system.json.jsonvalue(v=vs.95).aspx
namespace SimulacraJson
{
    public abstract class JsonValue : IEnumerable
    {
        public virtual bool ContainsKey(string key)
        {
            throw new InvalidOperationException();
        }

        public virtual int Count
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public virtual JsonValue this[int index]
        {
            get
            {
                throw new InvalidOperationException();
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        public virtual JsonValue this[string key]
        {
            get
            {
                throw new InvalidOperationException();
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        public abstract Json.JsonType JsonType { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }

        #region Boolean
        public static implicit operator JsonValue(Boolean value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Boolean(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Byte
        public static implicit operator JsonValue(Byte value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Byte(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Char
        public static implicit operator JsonValue(Char value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Char(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region DateTime
        public static implicit operator JsonValue(DateTime value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator DateTime(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region DateTimeOffset
        //public static implicit operator JsonValue(DateTimeOffset value)
        //{
        //    return new JsonPrimitive(value);
        //}

        //public static implicit operator DateTimeOffset(JsonValue value)
        //{
        //    return (JsonPrimitive)value;
        //}
        #endregion

        #region Decimal
        public static implicit operator JsonValue(Decimal value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Decimal(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Double
        public static implicit operator JsonValue(Double value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Double(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Guid
        public static implicit operator JsonValue(Guid value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Guid(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Int16
        public static implicit operator JsonValue(Int16 value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Int16(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Int32
        public static implicit operator JsonValue(Int32 value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Int32(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Int64
        public static implicit operator JsonValue(Int64 value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Int64(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region SByte
        public static implicit operator JsonValue(SByte value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator SByte(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Single
        public static implicit operator JsonValue(Single value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Single(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region String
        public static implicit operator JsonValue(String value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator String(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region TimeSpan
        public static implicit operator JsonValue(TimeSpan value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator TimeSpan(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region UInt16
        public static implicit operator JsonValue(UInt16 value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator UInt16(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region UInt32
        public static implicit operator JsonValue(UInt32 value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator UInt32(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region UInt64
        public static implicit operator JsonValue(UInt64 value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator UInt64(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Uri
        public static implicit operator JsonValue(Uri value)
        {
            return new JsonPrimitive(value);
        }

        public static implicit operator Uri(JsonValue value)
        {
            return (JsonPrimitive)value;
        }
        #endregion

        #region Parser
        public static JsonValue Parse(string jsonString)
        {
            if (jsonString == null)
            {
                throw new ArgumentNullException();
            }
            if (jsonString == "")
            {
                throw  new ArgumentException();
            }

            return Load(new StringReader(jsonString));
        }

        public static JsonValue Load(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException();
            }
            return Load(new StreamReader(stream, Encoding.UTF8));
        }

        public static JsonValue Load(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException();
            }
            return Parser(textReader);
        }

        // True: OK : False: Error
        //   None   BeObj  EnObj  BeAry  EnAry  Str    Num    Colon  Comma  True   False  Null   EOF    <- new token   | last token
        private static readonly bool[,] SyntaxCheckTable = {                                                        // v
            {true,  true,  false, true,  false, true,  true,  false, false, true,  true,  true,  true},             // None
            {true,  false, true,  false, false, true,  false, false, false, false, false, false, true},             // BeginObject
            {true,  false, true,  false, true,  false, false, false, true,  false, false, false, true},             // EndObject
            {true,  true,  false, true,  true,  true,  true,  false, true,  true,  true,  true,  true},             // BeginArray
            {true,  false, true,  false, true,  false, false, false, true,  false, false, false, true},             // EndArray
            {true,  false, true,  false, true,  false, false, true,  true,  false, false, false, true},             // String
            {true,  false, true,  false, true,  false, false, false, true,  false, false, false, true},             // Number
            {true,  true,  false, true,  false, true,  true,  false, false, true,  true,  true,  true},             // Colon
            {true,  true,  true,  true,  true,  true,  true,  false, false, true,  true,  true,  true},             // Comma
            {true,  false, true,  false, true,  false, false, false, true,  false, false, false, true},             // True
            {true,  false, true,  false, true,  false, false, false, true,  false, false, false, true},             // False
            {true,  false, true,  false, true,  false, false, false, true,  false, false, false, true}              // Null
        };

        private static JsonValue Parser(TextReader reader)
        {
            JsonValue root = null;

            var tree = new List<JsonValue>();

            JsonValue parent = null;
            var lastReadToken = TokenKind.None;
            var key = "";

            foreach (var token in JsonToken.Lexical(reader))
            {
                // 簡易構文チェック
                if (!SyntaxCheckTable[(int)lastReadToken, (int)token.TokenKind])
                {
                    throw new Exception("ParseException");
                }

                JsonValue jobj = null;
                switch (token.TokenKind)
                {
                    case TokenKind.Eof:
                        break;
                    case TokenKind.BeginArray:
                        jobj = new JsonArray();
                        break;
                    case TokenKind.BeginObject:
                        jobj = new JsonObject();
                        break;
                    case TokenKind.String:
                        if (lastReadToken == TokenKind.BeginObject || (parent.JsonType == Json.JsonType.Object && lastReadToken == TokenKind.Comma))
                        {
                            key = token.Value;
                        }
                        else
                        {
                            jobj = new JsonPrimitive(token.Value);
                        }
                        break;
                    case TokenKind.Number:
                        jobj = token.Value.IndexOf('.') > -1 ? new JsonPrimitive(double.Parse(token.Value)) : new JsonPrimitive(Int64.Parse(token.Value));
                        break;
                    case TokenKind.True:
                        jobj = new JsonPrimitive(true);
                        break;
                    case TokenKind.False:
                        jobj = new JsonPrimitive(false);
                        break;
                    case TokenKind.Null:
                        jobj = null;
                        break;
                }

                if (jobj != null || token.TokenKind == TokenKind.Null)
                {
                    if (root == null)
                    {
                        root = jobj;
                    }
                    else
                    {
                        if (parent.JsonType == Json.JsonType.Array)
                        {
                            ((JsonArray)parent).Add(jobj);

                        }
                        else if (parent.JsonType == Json.JsonType.Object)
                        {
                            ((JsonObject)parent).Add(key, jobj);
                        }
                    }
                }

                switch (token.TokenKind)
                {
                    case TokenKind.BeginArray:
                    case TokenKind.BeginObject:
                        tree.Add(parent);
                        parent = jobj;
                        break;
                    case TokenKind.EndArray:
                    case TokenKind.EndObject:
                        int last = tree.Count - 1;
                        parent = tree[last];
                        tree.RemoveAt(last);
                        break;
                }

                lastReadToken = token.TokenKind;
            }

            return root;
        }
        #endregion

        #region Serialize
        public void Save(Stream stream)
        {
            Save(new StreamWriter(stream, Encoding.UTF8));
        }

        public void Save(TextWriter textWriter)
        {
            textWriter.Write(ToString());
        }

        public override string ToString()
        {
            var result = "";
            StringBuilder buil;

            switch (JsonType)
            {
                case Json.JsonType.Array:
                    var jsonArray = this as JsonArray;
                    buil = new StringBuilder();
                    buil.Append("[");

                    if (jsonArray != null && jsonArray.Count > 0)
                    {
                        var i = 0;
                        foreach (var obj in jsonArray)
                        {
                            if (i > 0)
                            {
                                buil.Append(",");
                            }
                            buil.Append(obj != null ? obj.ToString() : "null");
                            i++;
                        }
                    }
                    
                    buil.Append("]");
                    
                    result = buil.ToString();
                    break;
                case Json.JsonType.Object:
                    var jsonObject = this as JsonObject;
                    buil = new StringBuilder();
                    buil.Append("{");
                    if (jsonObject != null && jsonObject.Count > 0)
                    {
                        var i = 0;
                        foreach (var key in jsonObject.Keys)
                        {
                            if (i > 0)
                            {
                                buil.Append(",");
                            }
                            buil.Append(string.Format("\"{0}\"", key));
                            buil.Append(":");
                            buil.Append(jsonObject[key] != null ? jsonObject[key].ToString() : "null");
                            i++;
                        }
                    }
                    buil.Append("}");
                    result = buil.ToString();
                    break;
                case Json.JsonType.String:
                case Json.JsonType.Number:
                case Json.JsonType.Boolean:
                    result = ToString();
                    break;
            }

            return result;
        }
        #endregion
    }

    #region JsonToken
    enum TokenKind
    {
        None,
        BeginObject,
        EndObject,
        BeginArray,
        EndArray,
        String,
        Number,
        Colon,
        Comma,
        True,
        False,
        Null,
        Eof,
    }

    class JsonToken
    {
        private const string HexChars = "0123456789abcdefABCDEF";
        static readonly int[] CharCode = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 10, 11, 12, 13, 14, 15 };

        static JsonToken()
        {
        }

        public TokenKind TokenKind { get; private set; }
        public string Value { get; private set; }

        JsonToken(TokenKind kind)
        {
            TokenKind = kind;
        }

        public static IEnumerable<JsonToken> Lexical(TextReader stream)
        {
            var sb = new StringBuilder();

            while (true)
            {
                if (stream.Peek() == -1)
                {
                    yield return new JsonToken(TokenKind.Eof);
                    yield break;
                }

                char code = Convert.ToChar(stream.Read());

                switch (code)
                {
                    case '{':
                        yield return new JsonToken(TokenKind.BeginObject);
                        break;
                    case '}':
                        yield return new JsonToken(TokenKind.EndObject);
                        break;
                    case '[':
                        yield return new JsonToken(TokenKind.BeginArray);
                        break;
                    case ']':
                        yield return new JsonToken(TokenKind.EndArray);
                        break;
                    case '"':
                        sb = new StringBuilder();

                        while (true)
                        {
                            char scode = Convert.ToChar(stream.Read());

                            if (scode == '\\')
                            {
                                scode = Convert.ToChar(stream.Read());
                                switch (scode)
                                {
                                    case '"':
                                        sb.Append('"');
                                        break;
                                    case '\\':
                                        sb.Append('\\');
                                        break;
                                    case '/':
                                        sb.Append('/');
                                        break;
                                    case 'b':
                                        sb.Append('\b');
                                        break;
                                    case 'f':
                                        sb.Append('\f');
                                        break;
                                    case 'n':
                                        sb.Append('\n');
                                        break;
                                    case 'r':
                                        sb.Append('\r');
                                        break;
                                    case 't':
                                        sb.Append('\t');
                                        break;
                                    case 'u':
                                        // 以下の４行は高速化のためチェックせず行っているがチェックが必要
                                        int ucode = CharCode[HexChars.IndexOf((char)stream.Read())] << 4;
                                        ucode = (ucode + CharCode[HexChars.IndexOf((char)stream.Read())]) << 4;
                                        ucode = (ucode + CharCode[HexChars.IndexOf((char)stream.Read())]) << 4;
                                        ucode = (ucode + CharCode[HexChars.IndexOf((char)stream.Read())]);
                                        sb.Append((char)ucode);
                                        break;
                                    default:
                                        sb.Append('\\');
                                        sb.Append(scode);
                                        break;
                                }
                            }
                            else
                            {

                                if (scode == '"') 
                                {
                                    yield return new JsonToken(TokenKind.String) { Value = sb.ToString() };
                                    sb = new StringBuilder();
                                    break;
                                }

                                sb.Append(scode);
                            }
                        }
                        break;
                    case ':':
                        yield return new JsonToken(TokenKind.Colon);
                        break;
                    case ',':
                        yield return new JsonToken(TokenKind.Comma);
                        break;
                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        sb = new StringBuilder();
                        sb.Append(code);
                        while (stream.Peek() != -1)
                        {
                            switch (stream.Peek())
                            {
                                case '-':
                                case '0':
                                case '1':
                                case '2':
                                case '3':
                                case '4':
                                case '5':
                                case '6':
                                case '7':
                                case '8':
                                case '9':
                                case '.':
                                    sb.Append(Convert.ToChar(stream.Read()));
                                    break;
                                default:
                                    goto LoopOut_Number;
                            }
                        }
LoopOut_Number:
                        yield return new JsonToken(TokenKind.Number) { Value = sb.ToString() };
                        sb = new StringBuilder();
                        break;
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':

                        break;
                    default:
                        sb.Append(code);
                        var len = sb.Length;
                        if (len == 4 || len == 5)
                        {
                            switch (sb.ToString().ToUpper())
                            {
                                case "TRUE":
                                    yield return new JsonToken(TokenKind.True);
                                    sb = new StringBuilder();
                                    break;
                                case "FALSE":
                                    yield return new JsonToken(TokenKind.False);
                                    sb = new StringBuilder();
                                    break;
                                case "NULL":
                                    yield return new JsonToken(TokenKind.Null);
                                    sb = new StringBuilder();
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
    #endregion
}
