using System.Reflection;
using System.Reflection.Emit;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Vipl.Base.Extensions;

/// <summary> Extensions for <see cref="IEnumerable{T}"/> </summary>
public static class EnumerableExtensions
{
    /// <summary> Returns elements of <paramref name="target"/> which are not <c>null</c>. </summary>
    /// <typeparam name="T">Type of elements of the <paramref name="target"/></typeparam>
    /// <param name="target"><see cref="IEnumerable{T}"/> of <typeparamref name="T"/> for which nulls should be ignored.</param>
    /// <returns>New <see cref="IEnumerable{T}"/> with all non <c>null</c> elements.</returns>
    public static IEnumerable<T> IgnoreNulls<T>(this IEnumerable<T>? target)
    {
        if (target is null) yield break;
        foreach (var obj in target.Where(item => item is not null))
            yield return obj;
    }
    /// <summary> On each element of <paramref name="values"/> perform action <paramref name="action"/> </summary>
    /// <typeparam name="T">Type of elements of the <paramref name="values"/></typeparam>
    /// <param name="values"><see cref="IEnumerable{T}"/> of <typeparamref name="T"/> for which actions should be performed</param>
    /// <param name="action"><see cref="Delegate"/> to action which should be performed on each element. Argument of this action is element in <paramref name="values"/></param>
    public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
    {
        foreach (var obj in values)
            action(obj);
    }
    /// <summary> On each element of <paramref name="values"/> perform action async <paramref name="action"/> </summary>
    /// <typeparam name="T">Type of elements of the <paramref name="values"/></typeparam>
    /// <param name="values"><see cref="IEnumerable{T}"/> of <typeparamref name="T"/> for which actions should be performed</param>
    /// <param name="action"><see cref="Delegate"/> to action which should be performed on each element. Argument of this action is element in <paramref name="values"/></param>
    public static async Task ForEachAsync<T>(this IEnumerable<T> values, Func<T, Task> action)
    {
        foreach (var obj in values)
            await action(obj).ConfigureAwait(false);
    }

    /// <summary> On each element of <paramref name="values"/> perform action <paramref name="action"/> </summary>
    /// <typeparam name="T">Type of elements of the <paramref name="values"/></typeparam>
    /// <param name="values"><see cref="IEnumerable{T}"/> of <typeparamref name="T"/> for which actions should be performed</param>
    /// <param name="action"><see cref="Delegate"/> to action which should be performed on each element.
    /// First argument of this action is element in <paramref name="values"/>, second argument is index inside of <see cref="Enumerable"/></param>
    public static void ForEach<T>(this IEnumerable<T> values, Action<T, int> action)
    {
        var index = 0;
        foreach (var obj in values)
            action(obj, index++);
    }
    /// <summary> On each element of <paramref name="values"/> perform async action <paramref name="action"/> </summary>
    /// <typeparam name="T">Type of elements of the <paramref name="values"/></typeparam>
    /// <param name="values"><see cref="IEnumerable{T}"/> of <typeparamref name="T"/> for which actions should be performed</param>
    /// <param name="action"><see cref="Delegate"/> to action which should be performed on each element.
    /// First argument of this action is element in <paramref name="values"/>, second argument is index inside of <see cref="Enumerable"/></param>
    public static async Task ForEachAsync<T>(this IEnumerable<T> values, Func<T, int, Task> action)
    {
        var index = 0;
        foreach (var obj in values)
            await action(obj, index++).ConfigureAwait(false);
    }
    /// <summary> Group elements of <paramref name="source"/> into batches sized at <paramref name="batchSize"/> </summary>
    /// <param name="source"><see cref="IEnumerable{T}"/> which needs to be spited.</param>
    /// <param name="batchSize">Size of each batch.</param>
    /// <typeparam name="T">Type of elements of the <paramref name="source"/></typeparam>
    /// <returns><see cref="IEnumerable{IEnumerable}"/> of <typeparamref name="T"/> where every <see cref="IEnumerable{T}"/> is of size of <paramref name="batchSize"/> or less for last one.</returns>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
            yield return YieldBatchElements(enumerator, batchSize - 1).ToArray();
    }
    private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
    {
        yield return source.Current;
        for (var i = 0; i < batchSize && source.MoveNext(); i++)
            yield return source.Current;
    }
        
    /// <summary> Async copy elements from <paramref name="arrayIndex"/> to <paramref name="array"/>. </summary>
    /// <param name="enumerator">Input enumerator.</param>
    /// <param name="array">Output array. Array must have enough space to hold all values.</param>
    /// <param name="arrayIndex">Index from which elements should be copied.</param>
    /// <typeparam name="T">Type of elements.</typeparam>
    public static async ValueTask CopyToAsync<T>(this IAsyncEnumerator<T> enumerator, T[] array, int arrayIndex)
    {
        try
        {
            var currentIndex = 0;
                 
            while (await enumerator.MoveNextAsync().ConfigureAwait(false))
            {
                if(currentIndex++ < arrayIndex)
                    continue;
                array[currentIndex - arrayIndex - 1] = enumerator.Current;
            }
        }
        finally
        {
            await enumerator.DisposeAsync().ConfigureAwait(false);
        }
    }
        
    /// <summary> Copy elements from <paramref name="arrayIndex"/> to <paramref name="array"/>. </summary>
    /// <param name="enumerator">Input enumerator.</param>
    /// <param name="array">Output array. Array must have enough space to hold all values.</param>
    /// <param name="arrayIndex">Index from which elements should be copied.</param>
    /// <typeparam name="T">Type of elements.</typeparam>
    public static void CopyTo<T>(this IEnumerator<T> enumerator, T[] array, int arrayIndex)
    {
        try
        {
            var currentIndex = 0;
                 
            while (enumerator.MoveNext())
            {
                if(currentIndex++ < arrayIndex)
                    continue;
                array[currentIndex - arrayIndex - 1] = enumerator.Current;
            }
        }
        finally
        {
            enumerator.Dispose();
        }
    }
        
    /// <summary> Async copy elements from <paramref name="arrayIndex"/> to <paramref name="array"/>. </summary>
    /// <param name="enumerable">Input enumerable.</param>
    /// <param name="array">Output array. Array must have enough space to hold all values.</param>
    /// <param name="arrayIndex">Index from which elements should be copied.</param>
    /// <typeparam name="T">Type of elements.</typeparam>
    public static async ValueTask CopyToAsync<T>(this IAsyncEnumerable<T> enumerable, T[] array, int arrayIndex)
    {
        await enumerable.GetAsyncEnumerator().CopyToAsync(array, arrayIndex).ConfigureAwait(false);
    }
        
    /// <summary> Copy elements from <paramref name="arrayIndex"/> to <paramref name="array"/>. </summary>
    /// <param name="enumerable">Input enumerable.</param>
    /// <param name="array">Output array. Array must have enough space to hold all values.</param>
    /// <param name="arrayIndex">Index from which elements should be copied.</param>
    /// <typeparam name="T">Type of elements.</typeparam>
    public static void CopyTo<T>(this IEnumerable<T> enumerable, T[] array, int arrayIndex)
    {
        if (enumerable is ICollection<T> collection)
        {
            collection.CopyTo(array, arrayIndex);
            return;
        }
        enumerable.GetEnumerator().CopyTo(array, arrayIndex);
    }
    /// <summary> Searches for first element of the sequence which is not equal to default value for <typeparamref name="T"/> </summary>
    /// <param name="values">Sequence to search</param>
    /// <typeparam name="T">Type of sequence elements.</typeparam>
    /// <returns> First element of the sequence which is not equal to default value for <typeparamref name="T"/></returns>
    public static T? FirstNonDefault<T>(this IEnumerable<T> values)
    {
        return values.FirstOrDefault(obj => !obj.IsDefault());
    }

    /// <summary> Find first element that match the predicate or default if such element is not found. </summary>
    /// <param name="values">Sequence to search</param>
    /// <param name="predicateAsync">Async predicate to match.</param>
    /// <typeparam name="T">Type of sequence elements.</typeparam>
    /// <returns> First element that match the predicate or default if such element is not found</returns>
    public static async Task<T?> FirstOrDefaultAwait<T>(this IEnumerable<T> values, Func<T, Task<bool>> predicateAsync)
    {
        foreach (var value in values)
        {
            if (await predicateAsync(value).ConfigureAwait(false))
            {
                return value;
            }
        }

        return default!;
    }
        
    /// <summary> Searches for first element of the sequence which is not equal to default value for <typeparamref name="T"/> </summary>
    /// <param name="values">Sequence to search</param>
    /// <typeparam name="T">Type of sequence elements.</typeparam>
    /// <returns> First element of the sequence which is not equal to default value for <typeparamref name="T"/></returns>
    public static async Task<T> FirstNonDefaultAwait<T>(this IEnumerable<Task<T>> values)
    {
        var firstNonDefault = await values.FirstOrDefaultAwait(async task =>
            !(await task.ConfigureAwait(false)).IsDefault()).ConfigureAwait(false);
        if (firstNonDefault is  not null)
        {
            return await firstNonDefault.ConfigureAwait(false);
        }

        return default!;
    }
        
    /// <summary> Execute function <paramref name="function"/> on every element of <paramref name="enumerable"/> in parallel with level of parallelism of <paramref name="levelOfParallelism"/> </summary>
    /// <param name="enumerable">Enumeration of elements function <paramref name="function"/> should be executed</param>
    /// <param name="levelOfParallelism">How many task can be executed in parallel.  If set to 0 it will default to number of logical cores on machine. Default value is 0.</param>
    /// <param name="function">Function which should be executed on every element</param>
    /// <param name="token">Token to cancel operation</param>
    /// <typeparam name="T">Type of elements</typeparam>
    /// <typeparam name="TResult">Type of resulting elements</typeparam>
    /// <returns>Array of elements produced by function <paramref name="function"/></returns>
    public static async Task<TResult[]> ForEachParallelAsync<T, TResult>(this IEnumerable<T> enumerable, Func<T, CancellationToken, Task<TResult>> function, int levelOfParallelism = 0,  CancellationToken token = default)
    {
        var elements = enumerable.Select((e, i) => (e, i)).ToArray();
        var result = new TResult[elements.Length];
        if(levelOfParallelism > 0)
            await Parallel.ForEachAsync(elements, new ParallelOptions{MaxDegreeOfParallelism = levelOfParallelism, CancellationToken = token }, async (arg1, _) => result[arg1.i] = await function(arg1.e, token).ConfigureAwait(false)).ConfigureAwait(false);
        else
            await Parallel.ForEachAsync(elements, token, async (arg1, _) => result[arg1.i] = await function(arg1.e, token).ConfigureAwait(false)).ConfigureAwait(false);
        
        return result;
    }
        
    /// <summary> Execute action <paramref name="action"/> on every element of <paramref name="enumerable"/> in parallel with level of parallelism of <paramref name="levelOfParallelism"/> </summary>
    /// <param name="enumerable">Enumeration of elements action <paramref name="action"/> should be executed</param>
    /// <param name="levelOfParallelism">How many task can be executed in parallel.  If set to 0 it will default to number of logical cores on machine. Default value is 0.</param>
    /// <param name="action">Action which should be executed on every element</param>
    /// <param name="token">Token to cancel operation</param>
    /// <typeparam name="T">Type of elements</typeparam>
    public static async Task ForEachParallelAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action, int levelOfParallelism = 0,  CancellationToken token = default)
    {
        if(levelOfParallelism > 0)
            await Parallel.ForEachAsync(enumerable, new ParallelOptions{MaxDegreeOfParallelism = levelOfParallelism, CancellationToken = token }, async (arg1, _) => await action(arg1));
        else
            await Parallel.ForEachAsync(enumerable, token , async (arg1, _) => await action(arg1));
    }
    
    static class ListHelpers<T>
    {
        public static readonly Func<List<T>, T[]> GetInternalArrayMethod;
        
        private static readonly Action<object?, object?>? SetInternalSize = typeof(List<T>).GetField("_size", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue;
        private static readonly Action<object?, object?>? SetInternalVersion = typeof(List<T>).GetField("_version", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue;
        private static readonly Func<object?, object?>? GetInternalVersion = typeof(List<T>).GetField("_version", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue;
        public static readonly Action<List<T>, int> ResizeInternalMethod = (list, newSize) =>
        {
            if (list.Capacity < newSize)
            {
                list.Capacity =  newSize;
            }

            SetInternalSize(list, newSize);
            SetInternalVersion(list, (int)GetInternalVersion(list)! + 1);
        };

        static ListHelpers()
        {
            var dm = new DynamicMethod("get", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(T[]), new [] { typeof(List<T>) }, typeof(ListHelpers<T>), true);
            var il = dm.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // Load List<T> argument
            il.Emit(OpCodes.Ldfld, typeof(List<T>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)!); // Replace argument by field
            il.Emit(OpCodes.Ret); // Return field
            GetInternalArrayMethod = (Func<List<T>, T[]>)dm.CreateDelegate(typeof(Func<List<T>, T[]>));
        }
        
    }

    /// <summary> Get internal array of the list. </summary>
    /// <param name="list">List from which to get internal array</param>
    /// <typeparam name="T">Type of array element</typeparam>
    /// <returns>Internal array of the given list.</returns>
    public static T[] GetInternalArray<T>(this List<T> list)
    {
        return ListHelpers<T>.GetInternalArrayMethod(list);
    }

    /// <summary> Resize list by changing the position of internal _size property.
    /// This will allocate new size in needed, but it will not populate new entries with any data. </summary>
    /// <param name="list">List to resize.</param>
    /// <param name="newSize">New size of the list.</param>
    /// <typeparam name="T">Type of list elements.</typeparam>
    public static void ResizeWithJunkInternal<T>(this List<T> list, int newSize)
    {
        ListHelpers<T>.ResizeInternalMethod(list, newSize);
    }
}