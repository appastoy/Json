using System.Collections;
using System.Collections.Generic;

namespace AppAsToy.Json.Documents;

public struct JObject : IJElement, IEnumerable<JProperty>
{
    public struct Enumerator : IEnumerator<JProperty>
    {
        private SequentialList<JPropertyData> _list;
        private JDocument _document;
        private int _index;

        internal Enumerator(SequentialList<JPropertyData> list, JDocument document)
        {
            _list = list;
            _document = document;
            _index = -1;
        }

        public JProperty Current
        {
            get => new JProperty(ref _list[_index], _document);
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_index >= _list.Count)
                return false;

            _index += 1;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public void Dispose()
        {

        }
    }

    private readonly JDocument _document;
    private readonly int _index;

    private ref JObjectData data
    {
        get => ref _document._objectPool[_index];
    }

    public JType Type => JType.Object;

    public int Count => data.Count;

    public JElement this[string key]
    {
        get => new JElement(_document, data[key]);
        set => data[key] = value._ref;
    }

    internal JObject(JDocument document, int index)
    {
        _document = document;
        _index = index;
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator(data._propertyList, _document);
    }

    IEnumerator<JProperty> IEnumerable<JProperty>.GetEnumerator()
    {
        var list = data._propertyList;
        for (int i = 0; i < list.Count; i++)
            yield return new JProperty(ref list[i], _document);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<JProperty>)this).GetEnumerator();
    }
}
