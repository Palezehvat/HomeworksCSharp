namespace ParsingTree;

// The container is a "tree" with a hierarchical structure
public class Tree
{
    private Node Root { get; set; }

    private void AddSymbolToTree(char symbol)
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
                    Root.Symbol = new Divisioncs(symbol);
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
        var walker = Root;
        while (walker.Left != null)
        {
            walker = walker.Left;
        }
        switch (symbol)
        {
            case '-':
                walker.Symbol = new Minus(symbol);
                break;
            case '+':
                walker.Symbol = new Plus(symbol);
                break;
            case '*':
                walker.Symbol = new Multiplication(symbol);
                break;
            case '/':
                walker.Symbol = new Divisioncs(symbol);
                break;
        }
        walker.Symbol.Symbol = symbol;
        walker.IsEmpty = false;
        walker.Left = new Node();
        walker.Right = new Node();
        walker.Left.Value = new Operand(0);
        walker.Right.Value = new Operand(0);
        ++Root.Size;
    }

    private void InfixOrder(Node root, double number, ref bool isAdded)
    {
        if (root != null)
        {
            InfixOrder(root.Left, number, ref isAdded);
            if (root.IsEmpty && !isAdded)
            {
                root.Value.Number = number;
                root.IsEmpty = false;
                isAdded = true;
            } 
            InfixOrder(root.Right, number, ref isAdded);
        }
    }

    private void AddNumberToTree(double number)
    {
        if (Root == null)
        {
            throw new InvalidExpressionException();
        }
        bool isAdded = false;
        InfixOrder(Root, number, ref isAdded);
        if (isAdded == false)
        {
            throw new InvalidExpressionException();
        }
        --Root.Size;
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
                AddNumberToTree(number);
            }
            else if (isSymbolOperation(stringExpression[i]))
            {
                AddSymbolToTree(stringExpression[i]);
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
