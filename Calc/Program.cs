using Calc;

while (true)
{
    Console.Clear();
    Information.Info();
    string stringOperation = Console.ReadLine();
    if (stringOperation.Contains(' '))
        stringOperation = string.Join("", stringOperation.Split(' ').ToArray());

    if (stringOperation.Contains('.'))
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    else
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("Ru");

    if (stringOperation != "" && stringOperation.Length >= 3 && Cheacking.CheackinOneNumber(stringOperation))
    {

        var lastSimbol = stringOperation[stringOperation.Length - 1];
        var firstSimbol = stringOperation[0];

        if (Cheacking.CheckingForCharacters(stringOperation) && firstSimbol != '/' && firstSimbol != '*' && Cheacking.CheackinDuobleWrongOp(stringOperation))
        {

            while (lastSimbol == '+' || lastSimbol == '-' || lastSimbol == '/' || lastSimbol == '*')
            {
                stringOperation = stringOperation.TrimEnd(lastSimbol);
                lastSimbol = stringOperation[stringOperation.Length - 1];
            }

            if (stringOperation.Contains('('))
            {

                if (Cheacking.CheackingBrackets(stringOperation))
                {

                    Information.Info($"{stringOperation} = {Math.Round(Calculate.Solution(stringOperation), 3, MidpointRounding.ToEven)}");
                }
                else
                {
                    Information.Info("формат записи не верный");
                }
            }
            else
            {
                Information.Info($"{stringOperation} = {Math.Round(Calculate.Solution(stringOperation), 3, MidpointRounding.ToEven)}");
            }
        }
        else
        {
            Information.Info("неверное выражение, внимательней");
        }
    }
    else
        Information.Info($"надо ввести выражение");

    if (Console.ReadKey().Key == ConsoleKey.Escape)
        break;
}





