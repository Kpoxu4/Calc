using System.Text.RegularExpressions;

namespace Calc
{
    public class Cheacking
    {
        public static bool CheckingForCharacters(string expression)
        {
            bool answer = true;
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (c == '/' || c == '-' || c == '+' || c == '*' || c == '(' || c == ')' || Char.IsDigit(c) || c == '.' || c == ',')
                {
                    answer = true;
                }
                else
                {
                    answer = false;
                    break;
                }
            }
            return answer;
        }

        public static bool CheackingBrackets(string expression)
        {
            bool cheackingBrackets = true;
            int countSimvol1 = new Regex("\\(").Matches(expression).Count;
            int countSimvol2 = new Regex("\\)").Matches(expression).Count;

            if (countSimvol1 == countSimvol2 && expression.IndexOf('(') < expression.IndexOf(')'))
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    char c = expression[i];
                    if ((c == '(' || c == ')') && i != 0 && i != expression.Length - 1)
                    {
                        if (c == '(')
                        {
                            if (Char.IsDigit(expression[i - 1]) || expression[i + 1] == '/' || expression[i + 1] == '*')
                            {
                                cheackingBrackets = false;
                                break;
                            }
                            else
                                cheackingBrackets = true;
                        }
                        if (c == ')')
                        {
                            if (Char.IsDigit(expression[i + 1]) || !(Char.IsDigit(expression[i - 1])))
                            {
                                if (expression[i - 1] == ')')
                                    continue;

                                cheackingBrackets = false;
                                break;
                            }
                            else
                                cheackingBrackets = true;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
                cheackingBrackets = false;
            return cheackingBrackets;
        }

        public static bool CheackinDuobleWrongOp(string expression)
        {
            var cheackinDuobleWrongOp = true;

            if (expression.Contains("-/") || expression.Contains("/-") || expression.Contains("+/") || expression.Contains("/+") ||
               expression.Contains("*/") || expression.Contains("/*") || expression.Contains("-*") || expression.Contains("+*") ||
               expression.Contains("*-") || expression.Contains("*+"))
                cheackinDuobleWrongOp = false;

            return cheackinDuobleWrongOp;
        }

        public static bool CheackinOneNumber(string expression)
        {
            var cheackinOneNumber = true;
            if (double.TryParse(expression, out var result))
            {
                cheackinOneNumber = false;
            }
            return cheackinOneNumber;
        }

    }
}
