using System;
using System.Collections;
using System.Collections.Generic;

// http://msdn.microsoft.com/ja-jp/library/system.json.jsonobject(v=vs.95).aspx
namespace SimulacraJson
{
    public class JsonObject : JsonValue, IDictionary<string, JsonValue>, ICollection<KeyValuePair<string, JsonValue>>, IEnumerable<KeyValuePair<string, JsonValue>>, IEnumerable
    {
        private IDictionary<string, JsonValue> _items = new Dictionary<string, JsonValue>();

        public JsonObject()
        {
        }

        public JsonObject(IEnumerable<KeyValuePair<String, JsonValue>> values)
        {
            foreach (var value in values)
            {
                this._items.Add(value);
            }
        }

        public JsonObject(KeyValuePair<String, JsonValue>[] values)
        {
            foreach (var value in values)
            {
                this._items.Add(value);
            }
        }

        // Property
        public override int Count
        {
            get { return _items.Count; }
        }

        public override Json.JsonType JsonType
        {
            get { return Json.JsonType.Object; }
        }

        public bool IsReadOnly
        {
            get
            {
                return _items.IsReadOnly;
            }
        }

        // Method
        public void Add(string key, JsonValue value)
        {
            _items.Add(key, value);
        }

        public void Add(KeyValuePair<string, JsonValue> value)
        {
            _items.Add(value);
        }

        public override bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public bool Contains(KeyValuePair<string, JsonValue> value)
        {
            return _items.Contains(value);
        }

        public void CopyTo(KeyValuePair<string, JsonValue>[] values, int index )
        {
            _items.CopyTo(values, index);
        }

        public bool Remove(KeyValuePair<string, JsonValue> value)
        {
            return _items.Remove(value);
        }
        
        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool TryGetValue(string key, out JsonValue value)
        {
            return _items.TryGetValue(key, out value);
        }

        public ICollection<string> Keys
        {
            get
            {
                return _items.Keys;
            }
        }

        public ICollection<JsonValue> Values
        {
            get
            {
                return _items.Values;
            }
        }

        public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public override JsonValue this[string key]
        {
            get
            {
                if (_items.ContainsKey(key))
                {
                    return _items[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _items[key] = value;
            }
        }

    }
}
