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
        private object _value;

        public JsonPrimitive(Boolean value)
        {
            _jsonType = Json.JsonType.Boolean;
            _type = PrimitiveType.Boolean;
            _value = value;
        }
        public static implicit operator Boolean(JsonPrimitive value)
        {
            return (Boolean)value._value;
        }

        public JsonPrimitive(Byte value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.Byte;
            _value = value;
        }
        public static implicit operator Byte(JsonPrimitive value)
        {
            return (Byte)value._value;
        }
        
        public JsonPrimitive(Char value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Char;
            _value = value;
        }
        public static implicit operator Char(JsonPrimitive value)
        {
            return (Char)value._value;
        }

        public JsonPrimitive(DateTime value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.DateTime;
            _value = value;
        }
        public static implicit operator DateTime(JsonPrimitive value)
        {
            return (DateTime)value._value;
        }

        public JsonPrimitive(Decimal value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Decimal;
            _value = value;
        }
        public static implicit operator Decimal(JsonPrimitive value)
        {
            return (Decimal)value._value;
        }

        public JsonPrimitive(Double value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Double;
            _value = value;
        }
        public static implicit operator Double(JsonPrimitive value)
        {
            return (Double)value._value;
        }

        public JsonPrimitive(Guid value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.Guid;
            _value = value;
        }
        public static implicit operator Guid(JsonPrimitive value)
        {
            return (Guid)value._value;
        }

        public JsonPrimitive(Int16 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Int16;
            _value = value;
        }
        public static implicit operator Int16(JsonPrimitive value)
        {
            return (Int16)value._value;
        }
        
        public JsonPrimitive(Int32 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Int32;
            _value = value;
        }
        public static implicit operator Int32(JsonPrimitive value)
        {
            return (Int32)value._value;
        }
        
        public JsonPrimitive(Int64 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Int64;
            _value = value;
        }
        public static implicit operator Int64(JsonPrimitive value)
        {
            return (Int64)value._value;
        }

        public JsonPrimitive(SByte value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.SByte;
            _value = value;
        }
        public static implicit operator SByte(JsonPrimitive value)
        {
            return (SByte)value._value;
        }
        
        public JsonPrimitive(Single value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.Single;
            _value = value;
        }
        public static implicit operator Single(JsonPrimitive value)
        {
            return (Single)value._value;
        }

        public JsonPrimitive(String value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.String;
            _value = value;
        }
        public static implicit operator String(JsonPrimitive value)
        {
            return value._value as string;
        }

        public JsonPrimitive(TimeSpan value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.TimeSpan;
            _value = value;
        }
        public static implicit operator TimeSpan(JsonPrimitive value)
        {
            return (TimeSpan)value._value;
        }

        public JsonPrimitive(UInt16 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.UInt16;
            _value = value;
        }
        public static implicit operator UInt16(JsonPrimitive value)
        {
            return (UInt16)value._value;
        }

        public JsonPrimitive(UInt32 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.UInt32;
            _value = value;
        }
        public static implicit operator UInt32(JsonPrimitive value)
        {
            return (UInt32)value._value;
        }

        public JsonPrimitive(UInt64 value)
        {
            _jsonType = Json.JsonType.Number;
            _type = PrimitiveType.UInt64;
            _value = value;
        }
        public static implicit operator UInt64(JsonPrimitive value)
        {
            return (UInt64)value._value;
        }

        public JsonPrimitive(Uri value)
        {
            _jsonType = Json.JsonType.String;
            _type = PrimitiveType.Uri;
            _value = value;
        }
        public static implicit operator Uri(JsonPrimitive value)
        {
            return (Uri)value._value;
        }

        public JsonPrimitive()
        {
        }

        public override string ToString()
        {
            string serialized = "";

            switch (this.JsonType)
            {
                case Json.JsonType.String:
                    StringBuilder sb = new StringBuilder();
                    sb.Append("\"");
                    foreach (char c in ((string)_value).ToCharArray())
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
                    serialized = _value.ToString();
                    break;
                case Json.JsonType.Boolean:
                    serialized = ((bool)_value) ? "true" : "false";
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
