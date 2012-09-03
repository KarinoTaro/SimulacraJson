using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

// http://msdn.microsoft.com/ja-jp/library/system.json.jsonvalue(v=vs.95).aspx
namespace SimulacraJson
{
    public abstract class JsonValue : IEnumerable
    {
        public JsonValue()
        {
        }

        public virtual bool ContainsKey(string key)
        {
            throw new Exception("InvalidOperationException");
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
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public virtual JsonValue this[string key]
        {
            get
            {
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public abstract Json.JsonType JsonType { get; }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
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

        #region DateTimeOffset
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
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new Exception("ArgumentException");
            }

            return Load(new StringReader(jsonString));
        }

        public static JsonValue Load(Stream stream)
        {
            if (stream == null)
            {
                throw new Exception("ArgumentNullException");
            }
            return Load(new StreamReader(stream, Encoding.UTF8));
        }

        public static JsonValue Load(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new Exception("ArgumentNullException");
            }
            return Parser(textReader);
        }

        private static JsonValue Parser(TextReader reader)
        {
            JsonValue root = null;
            //root.IsRoot = true;

            List<JsonValue> tree = new List<JsonValue>();

            JsonValue parent = null;
            TokenKind lastReadToken = TokenKind.None;
            string key = "";

            bool done = false;

            while (!done)
            {
                JsonToken token = JsonToken.Lexical(reader);

                // 簡易構文チェック
                switch (lastReadToken)
                {
                    case TokenKind.None:
                        switch (token.TokenKind)
                        {
                            case TokenKind.EndObject:
                            case TokenKind.EndArray:
                            case TokenKind.Colon:
                            case TokenKind.Comma:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.BeginObject:
                        switch (token.TokenKind)
                        {
                            case TokenKind.BeginObject:
                            case TokenKind.BeginArray:
                            case TokenKind.EndArray:
                            case TokenKind.Number:
                            case TokenKind.True:
                            case TokenKind.False:
                            case TokenKind.Null:
                            case TokenKind.Colon:
                            case TokenKind.Comma:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.EndObject:
                        switch (token.TokenKind)
                        {
                            case TokenKind.BeginObject:
                            case TokenKind.BeginArray:
                            case TokenKind.String:
                            case TokenKind.Number:
                            case TokenKind.True:
                            case TokenKind.False:
                            case TokenKind.Null:
                            case TokenKind.Colon:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.BeginArray:
                        switch (token.TokenKind)
                        {
                            case TokenKind.EndObject:
                            case TokenKind.Colon:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.EndArray:
                        switch (token.TokenKind)
                        {
                            case TokenKind.BeginObject:
                            case TokenKind.BeginArray:
                            case TokenKind.String:
                            case TokenKind.Number:
                            case TokenKind.True:
                            case TokenKind.False:
                            case TokenKind.Null:
                            case TokenKind.Colon:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.String:
                        switch (token.TokenKind)
                        {
                            case TokenKind.BeginObject:
                            case TokenKind.BeginArray:
                            case TokenKind.String:
                            case TokenKind.Number:
                            case TokenKind.True:
                            case TokenKind.False:
                            case TokenKind.Null:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.Number:
                    case TokenKind.True:
                    case TokenKind.False:
                    case TokenKind.Null:
                        switch (token.TokenKind)
                        {
                            case TokenKind.BeginObject:
                            case TokenKind.BeginArray:
                            case TokenKind.String:
                            case TokenKind.Number:
                            case TokenKind.True:
                            case TokenKind.False:
                            case TokenKind.Null:
                            case TokenKind.Colon:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.Colon:
                        switch (token.TokenKind)
                        {
                            case TokenKind.EndObject:
                            case TokenKind.EndArray:
                            case TokenKind.Colon:
                            case TokenKind.Comma:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                    case TokenKind.Comma:
                        switch (token.TokenKind)
                        {
                            case TokenKind.Colon:
                            case TokenKind.Comma:
                                // error
                                done = true;
                                continue;
                        }
                        break;
                }

                JsonValue jobj = null;
                switch (token.TokenKind)
                {
                    case TokenKind.EOF:
                        done = true;
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
                        if (token.Value.IndexOf('.') > -1)
                        {
                            jobj = new JsonPrimitive(double.Parse(token.Value));
                        }
                        else
                        {
                            jobj = new JsonPrimitive(Int64.Parse(token.Value));
                        }
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
            this.Save(new StreamWriter(stream, Encoding.UTF8));
        }

        public void Save(TextWriter textWriter)
        {
            textWriter.Write(this.ToString());
        }

        public override string ToString()
        {
            string result = "";
            StringBuilder buil;

            switch (this.JsonType)
            {
                case Json.JsonType.Array:
                    var jsonArray = this as JsonArray;
                    buil = new StringBuilder();
                    buil.Append("[");

                    if (jsonArray.Count > 0)
                    {
                        int i = 0;
                        foreach (JsonValue obj in jsonArray)
                        {
                            if (i > 0)
                            {
                                buil.Append(",");
                            }
                            if (obj != null)
                            {
                                buil.Append(obj.ToString());
                            }
                            else
                            {
                                buil.Append("null");
                            }
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
                    if (jsonObject.Count > 0)
                    {
                        int i = 0;
                        foreach (string key in jsonObject.Keys)
                        {
                            if (i > 0)
                            {
                                buil.Append(",");
                            }
                            buil.Append(string.Format("\"{0}\"", key));
                            buil.Append(":");
                            if (jsonObject[key] != null)
                            {
                                buil.Append(jsonObject[key].ToString());
                            }
                            else
                            {
                                buil.Append("null");
                            }
                            i++;
                        }
                    }
                    buil.Append("}");
                    result = buil.ToString();
                    break;
                case Json.JsonType.String:
                case Json.JsonType.Number:
                case Json.JsonType.Boolean:
                    result = ((JsonPrimitive)this).ToString();
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
        EOF,
    }

    class JsonToken
    {
        static string hexchars = "0123456789abcdefABCDEF";
        static int[] charcode = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 10, 11, 12, 13, 14, 15 };

        static JsonToken()
        {
        }

        public TokenKind TokenKind { get; private set; }
        public string Value { get; private set; }

        JsonToken()
            : this(TokenKind.None)
        {

        }

        JsonToken(TokenKind kind)
        {
            TokenKind = kind;
        }

        public static JsonToken Lexical(TextReader stream)
        {
            StringBuilder sb = new StringBuilder();
            bool isString = false;

            while (true)
            {
                if (stream.Peek() == -1)
                {
                    return new JsonToken(TokenKind.EOF);
                }

                char code = Convert.ToChar(stream.Read());

                switch (code)
                {
                    case '{':
                        return new JsonToken(TokenKind.BeginObject);
                    case '}':
                        return new JsonToken(TokenKind.EndObject);
                    case '[':
                        return new JsonToken(TokenKind.BeginArray);
                    case ']':
                        return new JsonToken(TokenKind.EndArray);
                    case '"':
                        sb = new StringBuilder();

                        char scode;
                        char lastCode = '\0';
                        while (true)
                        {
                            scode = Convert.ToChar(stream.Read());

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
                                        int ucode = 0;
                                        // 以下の４行は高速化のためチェックせず行っているがチェックが必要
                                        ucode = charcode[hexchars.IndexOf((char)stream.Read())] << 4;
                                        ucode = (ucode + charcode[hexchars.IndexOf((char)stream.Read())]) << 4;
                                        ucode = (ucode + charcode[hexchars.IndexOf((char)stream.Read())]) << 4;
                                        ucode = (ucode + charcode[hexchars.IndexOf((char)stream.Read())]);
                                        sb.Append((char)ucode);
                                        break;
                                    default:
                                        sb.Append('\\' + (char)scode);
                                        break;
                                }
                            }
                            else
                            {

                                if (scode == '"')   // && lastCode != '\\')
                                {
                                    return new JsonToken(TokenKind.String) { Value = sb.ToString() };
                                }

                                sb.Append(scode);
                            }
                            lastCode = scode;
                        }
                    case ':':
                        return new JsonToken(TokenKind.Colon);
                    case ',':
                        return new JsonToken(TokenKind.Comma);
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
                        if (!isString)
                        {
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
                            return new JsonToken(TokenKind.Number) { Value = sb.ToString() };
                        }
                        break;
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':

                        break;
                    default:
                        sb.Append(code);
                        if (!isString)
                        {
                            int len = sb.Length;
                            if (len == 4 || len == 5)
                            {
                                string check = sb.ToString();
                                switch (check)
                                {
                                    case "true":
                                    case "True":
                                    case "TRUE":
                                        return new JsonToken(TokenKind.True);
                                    case "false":
                                    case "False":
                                    case "FALSE":
                                        return new JsonToken(TokenKind.False);
                                    case "null":
                                    case "Null":
                                    case "NULL":
                                        return new JsonToken(TokenKind.Null);
                                }
                            }
                        }
                        break;
                }
            }

            //return new JsonToken();
        }
    }
    #endregion
}
