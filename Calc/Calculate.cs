namespace Calc
{
    public class Calculate
    {

        public static double Solution(string expression)
        {
            string doubleOpertIntoSingleOpert = DoubleOpertIntoSingleOpert(expression);
            string preparingString = PreparingString(doubleOpertIntoSingleOpert);
            string rpn = DoRpn(preparingString);
            return Calculate.Answer(rpn);
        }

        private static  string DoubleOpertIntoSingleOpert(string expression)
        {
            while (expression.Contains("++") || expression.Contains("--") || expression.Contains("-+") || expression.Contains("+-"))
            {
                expression = expression.Replace("--", "+");
                expression = expression.Replace("-+", "-");
                expression = expression.Replace("+-", "-");
                expression = expression.Replace("++", "+");
            }
            if (expression[0] == '+')
                expression = expression.TrimStart('+');

            return expression;
        }

        private static string PreparingString(string expression)
        {
            string preparingString = "";
            for (int i = 0; i < expression.Length; i++)
            {
                char simbol = expression[i];
                if (simbol == '-')
                {
                    if (i == 0)
                        preparingString += '0';
                    else if (expression[i - 1] == '(')
                        preparingString += '0';
                }
                preparingString += simbol;

            }

            return preparingString;
        }

        private static string DoRpn(string expression)
        {

            string current = "";
            Stack<string> stack = new Stack<string>();
            int priority;
            for (int i = 0; i < expression.Length; i++)
            {
                priority = GetPriority(expression[i]);

                if (priority == 0)
                    current += expression[i];
                if (priority == 1)
                    stack.Push(expression[i].ToString());
                if (priority > 1)
                {
                    current += ' ';
                    while (stack.Count != 0)
                    {
                        if (GetPriority(char.Parse(stack.Peek())) >= priority)
                            current += stack.Pop();
                        else
                            break;
                    }
                    stack.Push(expression[i].ToString());
                }
                if (priority == -1)
                {
                    current += ' ';
                    while (GetPriority(char.Parse(stack.Peek())) != 1)
                    {
                        current += stack.Pop();
                    }
                    stack.Pop();
                }
            }
            while (stack.Count != 0)
            {
                current += stack.Pop();

            }
            return current;
        }

        private static double Answer(string rpn)
        {
            string operand = "";
            Stack<Double> stack = new Stack<Double>();

            for (int i = 0; i < rpn.Length; i++)
            {
                if (rpn[i] == ' ')
                    continue;

                if (GetPriority(rpn[i]) == 0)
                {
                    while (rpn[i] != ' ' && GetPriority(rpn[i]) == 0)
                    {
                        operand += rpn[i++];
                        if (i == rpn.Length)
                        {
                            break;
                        }
                    }
                    stack.Push(double.Parse(operand));
                    operand = "";
                }

                if (GetPriority(rpn[i]) > 1)
                {
                    double a = stack.Pop();
                    double b = stack.Pop();
                    switch (rpn[i])
                    {
                        case '+':
                            stack.Push(b + a);
                            break;
                        case '-':
                            stack.Push(b - a);
                            break;
                        case '*':
                            stack.Push(b * a);
                            break;
                        case '/':
                            try
                            {
                                if (a == 0)
                                    throw new DivideByZeroException("попытка деления на ноль");
                                else
                                {
                                    stack.Push(b / a);
                                    break;
                                }

                            }
                            catch (DivideByZeroException)
                            {
                                stack.Clear();
                                Console.WriteLine("делить на 0 нельзя, но вам можно");
                                return 0;
                            }
                    }
                }
            }
            return stack.Pop();
        }

        private static int GetPriority(char tokin)
        {
            switch (tokin)
            {
                case '*':
                case '/':
                    return 3;
                case '+':
                case '-':
                    return 2;
                case '(':
                    return 1;
                case ')':
                    return -1;
                default: return 0;
            }
        }
    }

}
