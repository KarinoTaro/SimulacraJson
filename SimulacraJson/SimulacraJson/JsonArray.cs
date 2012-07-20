using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SimulacraJson
{
    public class JsonArray : JsonValue, IList<JsonValue>, ICollection<JsonValue>, IEnumerable<JsonValue>, IEnumerable
    {
        // http://msdn.microsoft.com/ja-jp/library/system.json.jsonarray(v=vs.95).aspx
        private IList<JsonValue> _items = new List<JsonValue>();

        public JsonArray()
        {
        }

        public JsonArray(IEnumerable<JsonValue> values)
        {
            foreach (var value in values)
            {
                this._items.Add(value);
            }
        }

        public JsonArray(JsonValue[] values)
        {
            foreach (var value in values)
            {
                this._items.Add(value);
            }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override Json.JsonType JsonType
        {
            get { return Json.JsonType.Array; }
        }

        public bool IsReadOnly
        {
            get
            {
                return _items.IsReadOnly;
            }
        }
        
        public bool Remove(JsonValue value)
        {
            return _items.Remove(value);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public void Add(JsonValue value)
        {
            _items.Add(value);
        }

        public void Insert(int index, JsonValue value)
        {
            _items.Insert(index, value);
        }

        public int IndexOf(JsonValue value)
        {
            return _items.IndexOf(value);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(JsonValue value)
        {
            return _items.Contains(value);
        }

        public void CopyTo(JsonValue[] values, int arrayIndex)
        {
            _items.CopyTo(values, arrayIndex);
        }

        public IEnumerator<JsonValue> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public override JsonValue this[int i]
        {
            get
            {
                return _items[i];
            }
            set
            {
                _items[i] = value;
            }
        }
    }
}
