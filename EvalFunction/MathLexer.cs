using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalFunction
{
    public class Token
    {
        public string Value { get; set; }
    }

    public class PostgixTokenObject
    {
        public List<Token> Symbols { get; set; }
        public List<Token> Numbers { get; set; }
    }


    public class MathLexer
    {
        public List<Token> GenerateTokens(string expression)
        {
            var output = new List<Token>();

            var symbols = new[] { '*', '/', '+', '-', '^' };

            var sb = new StringBuilder();


            int bracketOpen = 0;

            foreach (var cur in expression)
            {
                    if (cur == '(')
                    {
                        bracketOpen++;
                        if(bracketOpen == 1)
                            continue;;
                    }
                   if (cur == ')')
                   {
                       bracketOpen--;

                       if (bracketOpen == 0)
                       {
                           output.Add(new Token()
                           {
                               Value = sb.ToString()
                           });
                           sb.Clear();
                           continue;
                       }
                   }

                   if (bracketOpen > 0)
                   {
                        if (!char.IsWhiteSpace(cur))
                            sb.Append(cur);
                        continue;
                   }

                   if (!symbols.Contains(cur))
                   {
                       if(!char.IsWhiteSpace(cur))
                           sb.Append(cur);
                   }
                   else
                   {
                       if(sb.Length > 0)
                           output.Add(new Token()
                           {
                               Value = sb.ToString()
                           });
                       output.Add(new Token()
                       {
                           Value = cur.ToString()
                       });
                       sb.Clear();
                   }
            }

            if(sb.ToString() != string.Empty)
                output.Add(new Token()
                {
                    Value = sb.ToString()
                });

            return output;
        }

        public PostgixTokenObject ConvertToPostfix(List<Token> tokens)
        {
            var symbols = new List<Token>();
            var numbers = new List<Token>();

            var s = new[] { "*", "/", "+", "-", "^" };

            foreach (var token in tokens)
            {
                if (s.Contains(token.Value))
                {
                    symbols.Add(new Token()
                    {
                        Value = token.Value
                    });
                }
                else
                {
                    numbers.Add(new Token()
                    {
                        Value = token.Value
                    });
                }
            }
            

            return new PostgixTokenObject()
            {
                Symbols = symbols,
                Numbers = numbers
                
            };
        }

        public PostgixTokenObject TokenizeToPostFix(string expression)
        {
            return ConvertToPostfix(GenerateTokens(expression));
        }

    }
}
