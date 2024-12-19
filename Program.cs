using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    using System;
    using System.Collections.Generic;

    class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    class BinaryTree
    {
        private TreeNode root;

        public void AddNode(int value)
        {
            root = AddNodeRecursive(root, value);
        }

        private TreeNode AddNodeRecursive(TreeNode node, int value)
        {
            if (node == null)
                return new TreeNode(value);

            if (value < node.Value)
                node.Left = AddNodeRecursive(node.Left, value);
            else if (value > node.Value)
                node.Right = AddNodeRecursive(node.Right, value);

            return node;
        }

        public void RemoveNode(int value)
        {
            root = RemoveNodeRecursive(root, value);
        }

        private TreeNode RemoveNodeRecursive(TreeNode node, int value)
        {
            if (node == null) return null;

            if (value < node.Value)
                node.Left = RemoveNodeRecursive(node.Left, value);
            else if (value > node.Value)
                node.Right = RemoveNodeRecursive(node.Right, value);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                TreeNode minNode = FindMinNode(node.Right);
                node.Value = minNode.Value;
                node.Right = RemoveNodeRecursive(node.Right, minNode.Value);
            }

            return node;
        }

        public TreeNode FindNode(int value)
        {
            return FindNodeRecursive(root, value);
        }

        private TreeNode FindNodeRecursive(TreeNode node, int value)
        {
            if (node == null || node.Value == value)
                return node;

            if (value < node.Value)
                return FindNodeRecursive(node.Left, value);

            return FindNodeRecursive(node.Right, value);
        }

        public int CountNodes()
        {
            return CountNodesRecursive(root);
        }

        private int CountNodesRecursive(TreeNode node)
        {
            if (node == null) return 0;
            return 1 + CountNodesRecursive(node.Left) + CountNodesRecursive(node.Right);
        }

        public int GetHeight()
        {
            return GetHeightRecursive(root);
        }

        private int GetHeightRecursive(TreeNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(GetHeightRecursive(node.Left), GetHeightRecursive(node.Right));
        }

        public int FindMin()
        {
            TreeNode node = FindMinNode(root);
            if (node == null) throw new InvalidOperationException("Tree is empty");
            return node.Value;
        }

        private TreeNode FindMinNode(TreeNode node)
        {
            while (node?.Left != null)
                node = node.Left;
            return node;
        }

        public int FindMax()
        {
            TreeNode node = root;
            while (node?.Right != null)
                node = node.Right;
            if (node == null) throw new InvalidOperationException("Tree is empty");
            return node.Value;
        }

        public bool IsBalanced()
        {
            return IsBalancedRecursive(root) != -1;
        }

        private int IsBalancedRecursive(TreeNode node)
        {
            if (node == null) return 0;

            int leftHeight = IsBalancedRecursive(node.Left);
            int rightHeight = IsBalancedRecursive(node.Right);

            if (leftHeight == -1 || rightHeight == -1 || Math.Abs(leftHeight - rightHeight) > 1)
                return -1;

            return 1 + Math.Max(leftHeight, rightHeight);
        }

        public List<int> ToList()
        {
            List<int> result = new List<int>();
            InOrderTraversal(root, result);
            return result;
        }

        private void InOrderTraversal(TreeNode node, List<int> result)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, result);
            result.Add(node.Value);
            InOrderTraversal(node.Right, result);
        }

        public TreeNode FindParent(int value)
        {
            return FindParentRecursive(root, null, value);
        }

        private TreeNode FindParentRecursive(TreeNode node, TreeNode parent, int value)
        {
            if (node == null || node.Value == value)
                return parent;

            if (value < node.Value)
                return FindParentRecursive(node.Left, node, value);

            return FindParentRecursive(node.Right, node, value);
        }

        public BinaryTree CopyTree()
        {
            BinaryTree copy = new BinaryTree();
            copy.root = CopyTreeRecursive(root);
            return copy;
        }

        private TreeNode CopyTreeRecursive(TreeNode node)
        {
            if (node == null) return null;

            TreeNode newNode = new TreeNode(node.Value)
            {
                Left = CopyTreeRecursive(node.Left),
                Right = CopyTreeRecursive(node.Right)
            };
            return newNode;
        }

        public void MergeTree(BinaryTree other)
        {
            foreach (var value in other.ToList())
            {
                AddNode(value);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            BinaryTree tree = new BinaryTree();
            while (true)
            {
                Console.WriteLine("Выберите задание (1-12) или 0 для выхода:");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 12)
                {
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    continue;
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите значение для добавления:");
                        if (int.TryParse(Console.ReadLine(), out int valueToAdd))
                        {
                            tree.AddNode(valueToAdd);
                            Console.WriteLine("Узел добавлен.");
                        }
                        else
                            Console.WriteLine("Неверный ввод.");
                        break;

                    case 2:
                        Console.WriteLine("Введите значение для удаления:");
                        if (int.TryParse(Console.ReadLine(), out int valueToRemove))
                        {
                            tree.RemoveNode(valueToRemove);
                            Console.WriteLine("Узел удален.");
                        }
                        else
                            Console.WriteLine("Неверный ввод.");
                        break;

                    case 3:
                        Console.WriteLine("Введите значение для поиска:");
                        if (int.TryParse(Console.ReadLine(), out int valueToFind))
                        {
                            var foundNode = tree.FindNode(valueToFind);
                            Console.WriteLine(foundNode != null ? $"Узел найден: {foundNode.Value}" : "Узел не найден.");
                        }
                        else
                            Console.WriteLine("Неверный ввод.");
                        break;

                    case 4:
                        Console.WriteLine($"Количество узлов в дереве: {tree.CountNodes()}");
                        break;

                    case 5:
                        Console.WriteLine($"Высота дерева: {tree.GetHeight()}");
                        break;

                    case 6:
                        try
                        {
                            Console.WriteLine($"Минимальное значение в дереве: {tree.FindMin()}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 7:
                        try
                        {
                            Console.WriteLine($"Максимальное значение в дереве: {tree.FindMax()}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 8:
                        Console.WriteLine(tree.IsBalanced() ? "Дерево сбалансировано." : "Дерево не сбалансировано.");
                        break;

                    case 9:
                        Console.WriteLine("Содержимое дерева (в виде списка):");
                        Console.WriteLine(string.Join(", ", tree.ToList()));
                        break;

                    case 10:
                        Console.WriteLine("Введите значение узла для поиска родителя:");
                        if (int.TryParse(Console.ReadLine(), out int childValue))
                        {
                            var parent = tree.FindParent(childValue);
                            Console.WriteLine(parent != null ? $"Родитель узла: {parent.Value}" : "Родитель не найден.");
                        }
                        else
                            Console.WriteLine("Неверный ввод.");
                        break;

                    case 11:
                        BinaryTree copy = tree.CopyTree();
                        Console.WriteLine("Дерево скопировано.");
                        break;

                    case 12:
                        BinaryTree otherTree = new BinaryTree();
                        Console.WriteLine("Введите значения для второго дерева (через пробел):");
                        string[] inputs = Console.ReadLine().Split();
                        foreach (var input in inputs)
                        {
                            if (int.TryParse(input, out int val))
                                otherTree.AddNode(val);
                        }
                        tree.MergeTree(otherTree);
                        Console.WriteLine("Деревья объединены.");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

}
