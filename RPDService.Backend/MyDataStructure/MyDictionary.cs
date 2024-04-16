namespace MyDataStructures;

public class KeyValuePair<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public KeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public class Dictionary<TKey, TValue>
{
    private BinaryTreeNode<KeyValuePair<TKey, TValue>> root;

    public void Add(TKey key, TValue value)
    {
        var kvp = new KeyValuePair<TKey, TValue>(key, value);
        root = Add(root, kvp);
    }

    private BinaryTreeNode<KeyValuePair<TKey, TValue>> Add(BinaryTreeNode<KeyValuePair<TKey, TValue>> node, KeyValuePair<TKey, TValue> kvp)
    {
        if (node == null)
        {
            return new BinaryTreeNode<KeyValuePair<TKey, TValue>>(kvp);
        }

        if (Comparer<TKey>.Default.Compare(kvp.Key, node.Data.Key) < 0)
        {
            node.Left = Add(node.Left, kvp);
        }
        else
        {
            node.Right = Add(node.Right, kvp);
        }

        return node;
    }

    public TValue Get(TKey key)
    {
        return Get(root, key);
    }

    private TValue Get(BinaryTreeNode<KeyValuePair<TKey, TValue>> node, TKey key)
    {
        if (node == null)
        {
            throw new KeyNotFoundException("Key not found");
        }

        int comparison = Comparer<TKey>.Default.Compare(key, node.Data.Key);
        if (comparison == 0)
        {
            return node.Data.Value;
        }
        else if (comparison < 0)
        {
            return Get(node.Left, key);
        }
        else
        {
            return Get(node.Right, key);
        }
    }
}