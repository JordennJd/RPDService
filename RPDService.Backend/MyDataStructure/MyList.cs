using System;
using System.Collections;
using System.Collections.Generic;

public class MyList<T> : IEnumerable<T>
{
    private T[] items;
    private int count;

    public MyList()
    {
        items = new T[4]; // Начальная емкость списка
        count = 0;
    }

    public void Add(T item)
    {
        if (count == items.Length)
        {
            // Увеличиваем емкость вдвое, если достигнут предел
            Array.Resize(ref items, items.Length * 2);
        }
        items[count++] = item;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
            items[index] = value;
        }
    }

    public int Count => count;

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}