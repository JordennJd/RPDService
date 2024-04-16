using System;
namespace MyDataStructures;
public class BinaryTreeNode<T>
{
    public T Data { get; set; }
    public BinaryTreeNode<T> Left { get; set; }
    public BinaryTreeNode<T> Right { get; set; }

    public BinaryTreeNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}

public class BinaryTree<T>
{
    private BinaryTreeNode<T> root;

    public void Insert(T data)
    {
        root = Insert(root, data);
    }

    private BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T data)
    {
        if (node == null)
        {
            return new BinaryTreeNode<T>(data);
        }

        if (Comparer<T>.Default.Compare(data, node.Data) < 0)
        {
            node.Left = Insert(node.Left, data);
        }
        else
        {
            node.Right = Insert(node.Right, data);
        }

        return node;
    }

    public void TraverseInOrder(Action<T> action)
    {
        TraverseInOrder(root, action);
    }

    private void TraverseInOrder(BinaryTreeNode<T> node, Action<T> action)
    {
        if (node != null)
        {
            TraverseInOrder(node.Left, action);
            action(node.Data);
            TraverseInOrder(node.Right, action);
        }
    }
}