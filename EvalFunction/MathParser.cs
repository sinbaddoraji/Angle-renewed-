using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalFunction
{
    public class Node
    {
        public Node Left;
        public Node Right;

        public string Symbol;
        public double Value
        {
            get
            {
                switch (Symbol)
                {
                    case "+":
                        return Left.Value + Right.Value;
                    case "-":
                        return Left.Value - Right.Value;
                    case "*":
                        return Left.Value * Right.Value;
                    case "/":
                        return Left.Value / Right.Value;
                    case "^":
                        return Math.Pow(Left.Value , Right.Value);
                }

                return MathCore.Evauluate(Symbol);
            }
        }
    }

    public class MathParser
    {
        public static Node Parse(PostgixTokenObject postgixTokenObject)
        {
            if (postgixTokenObject.Symbols.Count == 0 && postgixTokenObject.Numbers.Count == 1)
            {
                return new Node()
                {
                    Symbol = postgixTokenObject.Numbers[0].Value,
                };
            }
            var node  = new Node()
            {
                Symbol = postgixTokenObject.Symbols[0].Value,
                Left = new Node()
                {
                    Symbol = postgixTokenObject.Numbers[0].Value
                },
                Right = new Node()
                {
                    Symbol = postgixTokenObject.Numbers[1].Value
                }
            };


            for (var i = 1; i < postgixTokenObject.Symbols.Count; i++)
            {
                node = new Node()
                {
                    Symbol = postgixTokenObject.Symbols[i].Value,
                    Left = node,
                    Right = new Node()
                    {
                        Symbol = postgixTokenObject.Numbers[i + 1].Value
                    }
                };
            }
            return node;
        }
    }

    public class MathCore
    {
        public static double Evauluate(string mathProblem)
        {

            if (double.TryParse(mathProblem, out double output))
            {
                return output;
            }

            MathLexer mathLexer = new MathLexer();

            var postfixTokens = mathLexer.TokenizeToPostFix(mathProblem);
            var tokenTree = MathParser.Parse(postfixTokens);
            return tokenTree.Value;
        }
    }
}
