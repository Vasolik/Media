using System.Collections;
using System.Diagnostics;

namespace Vipl.Media.Core;
/// <summary> List implementation which allows conversion between 2 types of data.
/// Original <typeparamref name="TFrom"/> list list is wrapped so it can be used <typeparamref name="TTo"/> list.</summary>
/// <typeparam name="TFrom">Type from which data will be converted</typeparam>
/// <typeparam name="TTo">Type to which date will be converted.</typeparam>
[DebuggerDisplay("{System.Linq.ToList(this)}")]
public class ListTypeConversionWrapper<TFrom, TTo> : IList<TTo>
{
    private readonly IList<TFrom> _listImplementation;
    private readonly Func<TFrom, TTo> _fromToConvertor;
    private readonly Func<TTo, TFrom> _toFromConvertor;

    /// <summary>
    /// Created wrapper from original list and two conversion methods.
    /// </summary>
    /// <param name="listImplementation">Original list.</param>
    /// <param name="fromToConvertor">Conversion delegate for converting from <typeparamref name="TFrom"/> element to <typeparamref name="TTo"/> element.</param>
    /// <param name="toFromConvertor">Conversion delegate for converting from <typeparamref name="TTo"/> element to <typeparamref name="TFrom"/> element.</param>
    public ListTypeConversionWrapper(IList<TFrom> listImplementation, Func<TFrom, TTo> fromToConvertor, Func<TTo, TFrom> toFromConvertor)
    {
        _listImplementation = listImplementation;
        _fromToConvertor = fromToConvertor;
        _toFromConvertor = toFromConvertor;
    
    }
    /// <inheritdoc/>
    public IEnumerator<TTo> GetEnumerator()
    {
        return _listImplementation.Select(_fromToConvertor).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _listImplementation).GetEnumerator();
    }
    /// <inheritdoc/>
    public void Add(TTo item)
    {
        _listImplementation.Add(_toFromConvertor( item));
    }
    /// <inheritdoc/>
    public void Clear()
    {
        _listImplementation.Clear();
    }
    /// <inheritdoc/>
    public bool Contains(TTo item)
    {
        return _listImplementation.Contains(_toFromConvertor( item));
    }
    /// <inheritdoc/>
    public void CopyTo(TTo[] array, int arrayIndex)
    {
        _listImplementation.Select(_fromToConvertor).ToList().CopyTo(array, arrayIndex);
    }
    /// <inheritdoc/>
    public bool Remove(TTo item)
    {
        return _listImplementation.Remove(_toFromConvertor(item));
    }
    /// <inheritdoc/>
    public int Count => _listImplementation.Count;
    /// <inheritdoc/>
    public bool IsReadOnly => _listImplementation.IsReadOnly;
    /// <inheritdoc/>
    public int IndexOf(TTo item)
    {
        return _listImplementation.IndexOf(_toFromConvertor(item));
    }
    /// <inheritdoc/>
    public void Insert(int index, TTo item)
    {
        _listImplementation.Insert(index, _toFromConvertor(item));
    }
    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        _listImplementation.RemoveAt(index);
    }
    /// <inheritdoc/>
    public TTo this[int index]
    {
        get => _fromToConvertor( _listImplementation[index]);
        set => _listImplementation[index] = _toFromConvertor(value);
    }
}