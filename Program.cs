internal class Program
{
    public static void Calculate(string input)
    {
        // 移除空格
        input = input.Trim();

        // 定义操作数和操作符栈
        Stack<double> operands = new Stack<double>();
        Stack<char> operators = new Stack<char>();

        // 遍历输入字符串
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            // 如果是数字，将其解析为操作数
            if (char.IsDigit(c))
            {
                double operand = c - '0';
                while (i + 1 < input.Length && char.IsDigit(input[i + 1]))
                {
                    operand = operand * 10 + (input[i + 1] - '0');
                    i++;
                }
                operands.Push(operand);
            }
            // 如果是操作符，将其压入操作符栈
            else if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                while (operators.Count > 0 && Precedence(c) <= Precedence(operators.Peek()))
                {
                    Evaluate(operands, operators);
                }
                operators.Push(c);
            }
            // 如果是左括号，将其压入操作符栈
            else if (c == '(')
            {
                operators.Push(c);
            }
            // 如果是右括号，计算括号内的表达式
            else if (c == ')')
            {
                while (operators.Peek() != '(')
                {
                    Evaluate(operands, operators);
                }
                operators.Pop();
            }
            // 如果是其他字符，表达式无效
            else
            {
                Console.WriteLine("输入的表达式无效");
                return;
            }
        }

        // 计算剩余的操作符
        while (operators.Count > 0)
        {
            Evaluate(operands, operators);
        }

        // 输出计算结果
        Console.WriteLine(operands.Pop());
    }

    private static int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            default:
                return 0;
        }
    }

    private static void Evaluate(Stack<double> operands, Stack<char> operators)
    {
        double b = operands.Pop();
        double a = operands.Pop();
        char op = operators.Pop();
        switch (op)
        {
            case '+':
                operands.Push(a + b);
                break;
            case '-':
                operands.Push(a - b);
                break;
            case '*':
                operands.Push(a * b);
                break;
            case '/':
                operands.Push(a / b);
                break;
        }
    }

    private static void Main(string[] args)
    {
        Calculate(args[0]);
    }
}