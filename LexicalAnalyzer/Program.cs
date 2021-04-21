using System;
using Newtonsoft.Json;
namespace LexicalAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Введите выражение: ");
                    string inputStr = Console.ReadLine();
                    Console.WriteLine();
                    var lexer = new Lexer(inputStr);
                    Console.WriteLine("Лексический анализатор:");
                    var result = (Token: "", Value: "");
					while (result.Token != "EOF")
					{
						result = lexer.GetNextToken();
						if (string.IsNullOrEmpty(result.Value))
							break;
						Console.WriteLine("(" + result.Token + ":'" + result.Value + "')");
					}
					Parser parser = new Parser(new Lexer(inputStr));
                    var parsResult = parser.S();
                    Console.WriteLine("Результат работы парсера");
                    Console.WriteLine(parsResult.ToString());
                    CalculateValue calculateValue = new CalculateValue();
                    var res = calculateValue.ComputeValue(parsResult);                    
                    var tree = calculateValue.GetTree();
                    string json = JsonConvert.SerializeObject(parsResult, Formatting.Indented);
                    Console.WriteLine(json);
                    //Console.WriteLine(tree);
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
