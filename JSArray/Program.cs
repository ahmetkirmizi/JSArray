

using System.Collections;

var array = new JSArray();
array.Push("ahmet");
array.Push("mehmet");
array.Push("fatih");
array.Push(10_000, "ayse");


array["ahmet"] = "AHMET";

array[10_000] = "bilge";


array = new string[] { "ahmet", "mehmet" };//implicit operator

/*foreach (var value in array)
{
    Console.WriteLine("value -> {0}", value);
}*/
  
array.Print();


var intArray = new JSArray<int>();

intArray = new int[] { 23,3213,3 };

intArray.Print();

class JSArray : JSArray<string>
{
    public static implicit operator JSArray(string[] array)
    {
        var arr = new JSArray();
        foreach (var item in array)
        {
            arr.Push(item);
        }

        return arr;
    }
}

class JSArray<T> : IEnumerable<T>
{
    private Dictionary<int, T> items = new Dictionary<int, T>();


    public T this[int index]//array[10_000] = "bilge";
    {
        get => items[index];
        set => items[index] = value;
    }

    public T this[T item]
    {
        get
        {
            //O(n)
            return items.Values.FirstOrDefault(m => m.Equals(item));
        }

        set
        { 
            //O(n)
            foreach (var kv in items)
            {
                if (kv.Value.Equals(item))
                    items[kv.Key] = value;
            }
        }
    }

    public void Push(T item)
    {
        Push(nextIndex, item);
       // items.Add(nextIndex, item);
    }

    public void Push(int index, T item) => items.Add(index, item);


    private int nextIndex => items.Count == 0 ? 0 : items.Count;

    public IEnumerable<T> Values => items?.Values ?? Enumerable.Empty<T>();

    public IEnumerable<int> Keys => items?.Keys ?? Enumerable.Empty<int>();

    public void Print()
    {
        foreach (var item in items)
        {
            Console.WriteLine("{0} {1}", item.Key, item.Value);
        }
    }

    public IEnumerator<T> GetEnumerator()//dışarıdan foreach ile JSArray de kullanabilmek için
    {
        return items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator JSArray<T>(T[] array)// new int[] { 23,3213,3 };
    {
        var arr = new JSArray<T>();
        foreach (var item in array)
        {
            arr.Push(item);
        }

        return arr;
    }
}