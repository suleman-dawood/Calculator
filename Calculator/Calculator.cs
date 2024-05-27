
namespace Calculator;

public class Calculator
{
    public static double Calculate(string calculationString)
    {
        var parsedList = Parse(calculationString);
        var rootNode = ConstructTree(parsedList);
        return SolveTree(rootNode);
    }

    private static List<string> Parse(string calculationString)
    {
        Stack<string> stack = new Stack<string>();
        List<string> output = new List<string>();
        int i = 0;
        int size = calculationString.Length;

        while (i < size)
        {
            if (char.IsDigit(calculationString[i]) || calculationString[i] == '.') 
            {
                string numString = "";

                while (i < size && (char.IsDigit(calculationString[i]) || calculationString[i] == '.'))
                {
                    numString += calculationString[i];
                    i++;
                }
                output.Add(numString);
            }
            else if (calculationString[i] == '(')
            {
                stack.Push(calculationString[i].ToString());
                i++;
            }
            else if (calculationString[i] == ')')
            {
                while (stack.Peek() != "(")
                {
                    output.Add(stack.Pop());
                }
                stack.Pop();
                i++;
            }
            else
            {
                while (stack.Count > 0 && OperatorPrecedence(calculationString[i].ToString()) <= OperatorPrecedence(stack.Peek()))
                {
                    output.Add(stack.Pop());
                }
                stack.Push(calculationString[i].ToString());
                i++;
            }
        }
        while (stack.Count > 0)
        {
            output.Add(stack.Pop());
        }

        return output;
    }

    private static int OperatorPrecedence(string op)
    {
        if (op == "+" || op == "-")
            return 1;
        if (op == "x" || op == "÷")
            return 2;
        return 0;
    }

    private static BinaryTree ConstructTree(List<string> parsedList)
    {
        Stack<BinaryTree> stack = new Stack<BinaryTree>();

        foreach (string item in parsedList) {
            if (double.TryParse(item, out _))
            {
                stack.Push(new BinaryTree(item));
            }
            else 
            {
                var node = new BinaryTree(item);
                node.Right = stack.Pop();
                node.Left = stack.Pop();
                stack.Push(node);
            }
        }

        return stack.Pop();
    }

    private static double SolveTree(BinaryTree node)
    {
        if (node == null) 
            return 0;
        if (double.TryParse(node.Value, out double num)) 
            { return num; }

        double left = SolveTree(node.Left);
        double right = SolveTree(node.Right);

        switch (node.Value)
        {
            case "+":
                return left + right;
            case "-":
                return left - right;
            case "x":
                return left * right;
            case "÷":
                return left / right;
            default:
                throw new InvalidOperationException("Invalid operator");
        }
    }
}