using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexicalAnalyzer
{
    public class CalculateValue
    {
        List<string> Operations { get; set; } = new List<string>();
        List<float> Values { get; set; } = new List<float>();
        public float ComputeValue(dynamic tree) 
        {
            if (tree.Item1 == "Plus")
            {
                Operations.Add("+");
                return ComputeValue(tree.Item2.f1) + ComputeValue(tree.Item2.f2);
            }
            if (tree.Item1 == "Minus")
            {
                Operations.Add("-");
                return ComputeValue(tree.Item2.f1) - ComputeValue(tree.Item2.f2);
            }
            if (tree.Item1 == "Mul") {
                Operations.Add("*");
                return ComputeValue(tree.Item2.f1) * ComputeValue(tree.Item2.f2);
            }
            if (tree.Item1 == "Div") {
                Operations.Add("/");
                return ComputeValue(tree.Item2.f1) / ComputeValue(tree.Item2.f2);
            }
            if (tree.Item1 == "Exp") {
                Operations.Add("^");
                return Math.Pow(ComputeValue(tree.Item2.f1), ComputeValue(tree.Item2.f2));
            }
            if (tree.Item1 == "Neg") {
                Operations.Add("neg");
                return -ComputeValue(tree.Item2);
            }
            if (tree.Item1 == "Number") {
                var value = float.Parse(tree.Item2);
                Values.Add(value);
                return value;
            }
            throw new Exception("Ошибка при вычислении");
        }
        public string GetTree()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Operations.Count == 0 && Values.Count == 0)
                return string.Empty;

            if (Values.Count == 1 && Operations.Count == 0) 
                return Values[0].ToString();
            if (Operations.Count >= 1)
            {
                stringBuilder.Append(Operations[0]);
                for (int i = 1; i < Operations.Count; i++)
                    stringBuilder.Append("(").Append(Operations[i]);
                stringBuilder.Append("(").Append(Values[0]).Append(",");

                for (int i = 1; i < Values.Count; i++)
                {
                    stringBuilder.Append(Values[i]);
                    if (i != Values.Count - 1) 
                        stringBuilder.Append("),");
                    if (i == Values.Count - 1)
                        stringBuilder.Append(")");
                }
                
            }
            
            return stringBuilder.ToString();

        }
    }
}