using System;

namespace LexicalAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите 'end' для выхода из программы");
            Console.WriteLine("Введите 'clear' для очистки консоли");
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Введите выражение: ");
                    string inputStr = Console.ReadLine();

                    if (inputStr == "end")
                        break;
                    if (inputStr == "clear")
                    {
                        Console.Clear();
                        continue;
                    }
                    Console.WriteLine();
                    var lexer = new Lexer(inputStr);
                    Console.WriteLine("Lexical Analyzer:");
                    var result = (Token: "", Value: "");
                    while (result.Token != "EOF")
                    {
                        result = lexer.GetNextToken();
                        if(string.IsNullOrEmpty(result.Value))
                            break;
                        Console.WriteLine("(" + result.Token + ":'" + result.Value + "')");
                    }

                    Console.WriteLine("\nПарсер");
                    Parser parser = new Parser(new Lexer(inputStr));
                    var parsResult = parser.S();
                    Console.WriteLine(parsResult.ToString());
                    CalculateValue calculateValue = new CalculateValue();
                    var res = calculateValue.ComputeValue(parsResult);
                    //Console.WriteLine(calculateValue.Tree);
                    var tmp = calculateValue.Operations;
                    
                    var t = calculateValue.Values;
                    var tree = calculateValue.GetTree();
                    Console.WriteLine(tree);
                    // var res = parser.ComputeValue(parsResult);
                    Console.WriteLine($"Результат = {res}");
                }
				catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }    
            }
        } 
    }
}
