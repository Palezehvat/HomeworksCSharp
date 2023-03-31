using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParsingTree;

// The container is a "tree" with a hierarchical structure
public class Tree
{
    private Node Root { get; set; }

    private void AddToTree(Node root, ref bool isAdded, double value = 0, char symbol = '\0')
    {
        if (root != null)
        {
            if (root.IsEmpty && !isAdded)
            {
                if (symbol != '\0')
                {
                    switch (symbol)
                    {
                        case '-':
                            root.Symbol = new Minus(symbol);
                            break;
                        case '+':
                            root.Symbol = new Plus(symbol);
                            break;
                        case '*':
                            root.Symbol = new Multiplication(symbol);
                            break;
                        case '/':
                            root.Symbol = new Divider(symbol);
                            break;
                    }
                    root.IsEmpty = false;
                    root.Left = new Node();
                    root.Right = new Node();
                    root.Left.Value = new Operand(0);
                    root.Right.Value = new Operand(0);
                    ++Root.Size;
                }
                else
                {
                    root.Value.Number = value;
                    root.IsEmpty = false;
                    --Root.Size;

                }
                isAdded = true;
            }
            AddToTree(root.Left, ref isAdded, value, symbol);
            AddToTree(root.Right, ref isAdded, value, symbol);
        }
    }

    private void AddToTreeNumber(double value)
    {
        if (Root == null)
        {
            throw new InvalidExpressionException();
        }
        bool isAdded = false;
        AddToTree(Root, ref isAdded, value);
        if (!isAdded)
        {
            throw new InvalidExpressionException();
        }
    }

    private void AddToTreeSymbol(char symbol)
    {
        if (Root == null)
        {
            Root = new Node();
            switch (symbol)
            {
                case '-':
                    Root.Symbol = new Minus(symbol);
                    break;
                case '+':
                    Root.Symbol = new Plus(symbol);
                    break;
                case '*':
                    Root.Symbol = new Multiplication(symbol);
                    break;
                case '/':
                    Root.Symbol = new Divider(symbol);
                    break;
            }
            Root.Symbol.Symbol = symbol;
            Root.Value = new Operand(0);
            Root.Left = new Node();
            Root.Right = new Node();
            Root.Right.Value = new Operand(0);
            Root.Left.Value = new Operand(0);
            Root.IsEmpty = false;
            Root.Size += 2;
            return;
        }
        bool isAdded = false;
        AddToTree(Root, ref isAdded, 0, symbol);
        if (!isAdded)
        {
            throw new InvalidExpressionException();
        }
    }

    private bool isSymbolOperation(char symbol)
    {
        return symbol == '+'
            || symbol == '-'
            || symbol == '*'
            || symbol == '/';
    }

    // Builds an expression tree by expression
    public void TreeExpression(string stringExpression)
    {
        for (int i = 0; i < stringExpression.Length; i++)
        {
            if(Char.IsNumber(stringExpression[i]) || (i < stringExpression.Length - 1 && stringExpression[i] == '-' && Char.IsNumber(stringExpression[i + 1])))
            {
                double number = 0;
                bool isMinus = false;
                if (stringExpression[i] == '-')
                {
                    ++i;
                    isMinus = true;
                } 
                while (i < stringExpression.Length && Char.IsNumber(stringExpression[i]))
                {
                    number += stringExpression[i] - 48;
                    number *= 10;
                    ++i;
                }
                number /= 10;
                if (isMinus)
                {
                    number *= -1;
                }
                AddToTreeNumber(number);
            }
            else if (isSymbolOperation(stringExpression[i]))
            {
                AddToTreeSymbol(stringExpression[i]);
            }
            else if (stringExpression[i] != ' ' && stringExpression[i] != ')' && stringExpression[i] != '(')
            {
                throw new InvalidExpressionException();
            }
        }
        if (Root.Size != 0 || Root == null)
        {
            throw new InvalidExpressionException();
        }
    }

    private void Order(Node root)
    {
        if (root.Symbol != null)
        {
            Order(root.Left);
            if (root.Left.Symbol == null || root.Left.IsEmpty)
            {
                root.Value.Number = root.Symbol.Calcuate(root.Left.Value.Number, root.Right.Value.Number);
                if (root.Left.Symbol != null)
                {
                    root.Left.IsEmpty = false;
                }
                root.IsEmpty = true;
            }
        }
    }

    // Counts an expression in the tree
    public double Calcuate()
    {
        if (Root == null)
        {
            throw new NullReferenceException();
        }
        Order(Root);
        Root.IsEmpty = false;
        return Root.Value.Number;
    }

    private void PostOrderPrint(Node root, ref bool isFirstNumber)
    {
        if (root != null)
        {
            if (root.Symbol == null)
            {
                root.Value.Print();
                if (!isFirstNumber)
                {
                    Console.Write(") ");
                }
                isFirstNumber = false;
            }
            else
            {
                Console.Write('(');
                Root.Symbol.Print();
            }
            PostOrderPrint(root.Left, ref isFirstNumber);
            PostOrderPrint(root.Right, ref isFirstNumber);
        }
    }

    // Outputs the expression stored in the tree to the screen
    public void PrintExpression()
    {
        bool isFirstNumber = true;
        PostOrderPrint(Root, ref isFirstNumber);
    }

    private class Node
    {
        public Node()
        {
            IsEmpty = true;
        }

        public Operand  Value { get; set; }

        public Operator Symbol { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public bool IsEmpty { get; set; }

        public int Size { get; set; }
    }
}
