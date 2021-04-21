using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalyzer
{
	public class Lexer
	{
		private string input;

		public Lexer(string input)
		{
			this.input = input;
		}
		public (string, string) GetNextToken()
		{
			while (input != "")
			{
				int state = 0;
				//Последние принимающее состояние
				string lastAccess = "False";
				//Последнее положение во входной строке
				int lastPosition = -1;
				
				int position = 0;
				while (state != -1) 
				{
					string token = StateToToken(state);
					if (token != "False")
					{
						lastAccess = token;
						lastPosition = position;
					}
					if(position>=input.Length) break;
					char symbol = input[position];
					position++;
					state = TransitionTable(symbol, state);
				}
				if (lastAccess == "Space")
					input = input.Remove(0, lastPosition);
				else if (lastAccess != "False") 
				{
					string tokStr =  input.Substring(0, lastPosition);
					input = input.Remove(0, lastPosition);
					return (lastAccess, tokStr);
				}
				else 
					throw new Exception("Неизвестный оператор");
			}
			return ("EOF", string.Empty);
		}
		/// <summary>
		/// Для принимающего состояния возращаем токен
		/// </summary>
		/// <param name="state"> состояние</param>
		/// <returns></returns>
		private string StateToToken(int state)
		{
			switch (state)
			{
				case 1:
					return "Number";
				case 2:
					return "Number";
				case 5:
					return "Number";
				case 6:
					return "Id";
				case 7:
					return "Plus";
				case 8:
					return "Minus";
				case 9:
					return "Mul";
				case 10:
					return "Div";
				case 11:
					return "Exp";
				case 12:
					return "LPar";
				case 13:
					return "RPar";
				case 14:
					return "Comma";
				case 15:
					return "Space";
				default:
					return "False";
			}
		}
		/// <summary>
		/// Определим таблицу переходов
		/// </summary>
		/// <param name="symbol"> символ</param>
		/// <param name="state"> состояние</param>
		/// <returns></returns>
		private int TransitionTable(char symbol, int state)
		{
			switch (state)
			{
				case 0:
					if (symbol >= '0' && symbol <= '9')
						return 1;
					else if (symbol >= 'a' && symbol <= 'z' || symbol == '_')
						return 6;
					else if (symbol == '+')
						return 7;
					else if (symbol == '-')
						return 8;
					else if (symbol == '*')
						return 9;
					else if (symbol == '/')
						return 10;
					else if (symbol == '^')
						return 11;
					else if (symbol == '(')
						return 12;
					else if (symbol == ')')
						return 13;
					else if (symbol == ',')
						return 14;
					else if (symbol == ' ')
						return 15;
					break;
				case 1:
					if (symbol >= '0' && symbol <= '9')
						return 1;
					else if (symbol == '.')
						return 2;
					else if (symbol == 'e' || symbol == 'E')
						return 3;
					break;
				case 2:
					if (symbol >= '0' && symbol <= '9')
						return 2;
					else if (symbol == 'e' || symbol == 'E')
						return 3;
					break;
				case 3:
					if (symbol == '+' || symbol == '-')
						return 4;
					else if (symbol >= '0' && symbol <= '9')
						return 5;
					break;
				case 4:
					if (symbol >= '0' && symbol <= '9')
						return 5;
					break;
				case 5:
					if (symbol >= '0' && symbol <= '9')
						return 5;
					break;
				case 6:
					if ((symbol >= 'a' && symbol <= 'z') || symbol == '_' || (symbol >= '0' && symbol <= '9'))
						return 6;
					break;
				case 15:
					if (symbol == ' ')
						return 15;
					break;
			}
			return -1;
		}
		
	}
}
