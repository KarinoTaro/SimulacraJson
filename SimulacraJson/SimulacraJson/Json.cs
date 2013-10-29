
// http://msdn.microsoft.com/ja-jp/library/cc626400(v=vs.95)
namespace SimulacraJson
{
    public class Json
    {
        public enum JsonType
        {
            String,
            Number,
            Object,
            Array,
            Boolean,
        }

        // 0x0021 - 0x007e 以外の文字をエスケープ処理しないで出力する
        public static bool SerializationWithoutEscape { get; set; }

        // JsonObjectの参照でKeyが存在しなかったときにnullを返す
        public static bool ReturnNullIfKeyNotFound { get; set; }
    }
}
