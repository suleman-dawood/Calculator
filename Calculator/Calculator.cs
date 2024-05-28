
namespace Calculator;

public class Calculator
{
    public static string Calculate(string calculationString)
    {
        try
        {
            var parsedList = Parse(calculationString);
            var rootNode = ConstructTree(parsedList);
            return "" + SolveTree(rootNode);
        }
        catch (InvalidOperationException ex) 
        {
            return "Invalid Expression";
        }    
    }

    private static List<string> Parse(string calculationString)
    {
        Stack<string> stack = new Stack<string>();
        List<string> output = new List<string>();
        int i = 0;
        int size = calculationString.Length;
        bool operatorBehindBracket = false;

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
                // Check for implicit multiplication with π, e, or (
                if (i < size && (calculationString[i] == 'π' || calculationString[i] == 'e' || calculationString[i] == '%'))
                {
                    if (calculationString[i] == 'π')
                        output.Add(Math.PI.ToString());

                    else if (calculationString[i] == 'e')
                        output.Add(Math.E.ToString());

                    else if (calculationString[i] == '%')
                        output.Add(""+0.01);

                    i++;
                    output.Add("x");

                }
            } 
            else if (calculationString[i] == '(')
            {
                if (i > 0 && (char.IsDigit(calculationString[i - 1]) || calculationString[i - 1] == 'π' || calculationString[i] == '%' || calculationString[i - 1] == 'e'))
                {
                    operatorBehindBracket = true;
                }
                stack.Push(calculationString[i].ToString());
                i++;
            }
            else if (calculationString[i] == ')')
            {
                while (stack.Peek() != "(")
                {
                    output.Add(stack.Pop());
                }

                if (operatorBehindBracket)
                    output.Add("x");

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
        if (op == "^")
            return 3;
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
            case "^":
                return Math.Pow(left, right);
            default:
                throw new InvalidOperationException("Invalid operator");
        }
    }
}