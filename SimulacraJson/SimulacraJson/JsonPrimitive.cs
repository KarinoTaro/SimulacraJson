using System;
using System.Text;

namespace SimulacraJson
{
    // http://msdn.microsoft.com/ja-jp/library/system.json.jsonprimitive(v=vs.95).aspx
    public class JsonPrimitive : JsonValue
    {
        enum PrimitiveType
        {
            Boolean,
            Byte,
            Char,
            DateTime,
            Decimal,
            Double,
            Guid,
            Int16,
            Int32,
            Int64,
            SByte,
            Single,
            String,
            TimeSpan,
            UInt16,
            UInt32,
            UInt64,
            Uri,
        };

        private Json.JsonType _jsonType = Json.JsonType.String;
        private PrimitiveType _type;
        private string _value;

        public JsonPrimitive(Boolean value)
        {
            _jsonType = Json.JsonType.Boolean;
            _type = PrimitiveType.Boolean;
            _value = value ? "true" : "false";
        }

        public JsonPrimitive(Byte value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.Byte;
            _value = value.ToString();
        }
        
        public JsonPrimitive(Char value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Char;
            _value = value.ToString();
        }

        public JsonPrimitive(DateTime value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.DateTime;
            _value = value.ToString();
        }

        public JsonPrimitive(Decimal value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Decimal;
            _value = value.ToString();
        }

        public JsonPrimitive(Double value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Double;
            _value = value.ToString();
        }

        public JsonPrimitive(Guid value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.Guid;
            _value = value.ToString();
        }

        public JsonPrimitive(Int16 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Int16;
            _value = value.ToString();
        }
        
        public JsonPrimitive(Int32 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Int32;
            _value = value.ToString();
        }
        
        public JsonPrimitive(Int64 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Int64;
            _value = value.ToString();
        }

        public JsonPrimitive(SByte value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.SByte;
            _value = value.ToString();
        }
        
        public JsonPrimitive(Single value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Single;
            _value = value.ToString();
        }

        public JsonPrimitive(String value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.String;
            _value = value.ToString();
        }
        
        public JsonPrimitive(TimeSpan value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.TimeSpan;
            _value = value.ToString();
        }

        public JsonPrimitive(UInt16 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.UInt16;
            _value = value.ToString();
        }

        public JsonPrimitive(UInt32 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.UInt32;
            _value = value.ToString();
        }

        public JsonPrimitive(UInt64 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.UInt64;
            _value = value.ToString();
        }

        public JsonPrimitive(Uri value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.Uri;
            _value = value.ToString();
        }

        public JsonPrimitive()
        {
        }

        //public static explicit operator Boolean(JsonPrimitive json)
        //{
        //    return Boolean.Parse(json._value);
        //}

        //public static explicit operator String(JsonPrimitive json)
        //{
        //    return json._value;
        //}


        public override string ToString()
        {
            string serialized = "";

            switch (this.JsonType)
            {
                case Json.JsonType.String:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("\"");
                    foreach (char c in _value.ToCharArray())
                    {
                        switch (c)
                        {
                            case '"':
                                sb.Append("\\\"");
                                break;
                            case '\\':
                                sb.Append("\\\\");
                                break;
                            case '/':
                                sb.Append("\\/");
                                break;
                            case '\b':
                                sb.Append("\\b");
                                break;
                            case '\f':
                                sb.Append("\\f");
                                break;
                            case '\n':
                                sb.Append("\\n");
                                break;
                            case '\r':
                                sb.Append("\\r");
                                break;
                            case '\t':
                                sb.Append("\\t");
                                break;
                            default:
                                if (0x0021 <= c && c <= 0x007e || Json.SerializationWithoutEscape)
                                {
                                    sb.Append(c);
                                }
                                else
                                {
                                    sb.Append("\\u" + string.Format("{0:x04}", (int)c));
                                }
                                break;
                        }
                    }
                    sb.Append("\"");
                    serialized = sb.ToString();

                    break;
                case Json.JsonType.Number:
                    serialized = _value;
                    break;
                case Json.JsonType.Boolean:
                    serialized = _value;
                    break;
            }

            return serialized;
        }

        public override Json.JsonType JsonType
        {
            get { return _jsonType; }
        }
    }
}
