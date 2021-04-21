using System;

namespace LexicalAnalyzer
{
    // E -> T  E_
    // E_ -> + T E_ | - T E_ | ε
    // T -> F T_
    // T_ -> * F T_ | / F T_ | ε
    // F -> V ^ F | V
    // V -> id | num | ( E ) | - V

    public class Parser
    {
        private readonly Lexer _lexer;
        private (string, string) _currentToken;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = lexer.GetNextToken();
        }

        public dynamic S() 
        {
            var e = E();
            if (_currentToken.Item1 != "EOF") 
            {
                throw new Exception("Ожидался EOF");
            }
            return e;
        }
        public dynamic E()
        {
            // E -> T  E_
            var t = T();
            return E_(t);
        }

        //E_ -> + T E_ | - T E_ | ε
        public dynamic E_(dynamic f1)
        {
            //E_ -> + T E_ | - T E_ 
            if (_currentToken.Item1 == "Plus" || _currentToken.Item1 == "Minus")
            {
                var ct = _currentToken;
                _currentToken = _lexer.GetNextToken();
                var f2 = T();//
                return E_((ct.Item1, new {f1, f2}));
            }
            //E_ -> ε
            return f1;
        }

        public dynamic T()
        {
            //T -> F T_
            var t = F();
            return T_(t);
        }
        
        // T_ -> * F T_ | / F T_ | ε
        public dynamic T_(dynamic f1)
        {
            //T_ -> * F T_ | / F T_ 
            if (_currentToken.Item1 == "Mul" || _currentToken.Item1 == "Div")
            {
                var ct = _currentToken;
                _currentToken = _lexer.GetNextToken();
                var f2 = F();//
                return T_((ct.Item1, new {f1, f2}));
            }
            //T_ -> ε
            return f1;
        }
        // F -> V ^ F | V
        public dynamic F()
        {
            var f1 = V();
            // F -> V ^ F 
            if (_currentToken.Item1 == "Exp")
            {
                var exp = _currentToken;
                _currentToken = _lexer.GetNextToken();
                var f2 = F();
                return (exp.Item1, new {f1, f2});
            }
            //F -> V
            return f1;
        }

        public dynamic V()
        {
            switch (_currentToken.Item1)
            {
                //V -> id
                case "Id":
                {
                    var t = _currentToken;
                    _currentToken = _lexer.GetNextToken();
                    return t;
                }
                //V -> Num
                case "Number":
                {
                    var num = _currentToken;
                    _currentToken = _lexer.GetNextToken();
                    return num;
                }
                //V -> ( E )
                case "LPar":
                {
                    var lpar = _currentToken;
                    _currentToken = _lexer.GetNextToken();
                    var e = E(); //разбор нетерминала E
                    //проверка на закрывающую скобку
                    if (_currentToken.Item1 == "RPar")
                    {
                        var rPar = _currentToken;
                        _currentToken = _lexer.GetNextToken();
                        return e;
                    }
                    else
                    {
                        throw new Exception("Ожидалась закрвыющая скобка )");
                    }
                }
                //V -> -V
                case "Minus":
                {
                    var minus = _currentToken;
                    _currentToken = _lexer.GetNextToken();
                    var v = V();
                    return ("Neg", v);
                }
                default:
                   throw new ArgumentException("Ошибка при разборе терминала");
            }
        }
    }
}