using System;
using System.Linq;
using System.Collections.Generic;

namespace Inf2Postf
{
    class Program
    {
        static char[] operators = new char[] { '+', '-', '*', '/', '^' };
        static char[] pref = new char[] {'*', '/', '^' };
        static List<string> inf2postf(string infix)
        {
            Console.WriteLine(infix);
            Stack<char> stack = new Stack<char>();
            List<string> postfix = new List<string>();
            bool added = true;
            double aux = 0, dec = 0;
            for (int i = 0; i < infix.Length; i++) //Iterates thru every character in the string
            {
                if(Char.IsDigit(infix[i])) //Number (Digit)
                {
                    if(dec == 0)
                    {
                        aux *= 10;
                        aux += Int32.Parse(infix[i].ToString());
                        added = false;
                    }
                    else
                    {
                        aux += Int32.Parse(infix[i].ToString())*Math.Pow(10, dec);
                        dec -= 1;
                        added = false;
                    }
                }
                else if(infix[i] == '.')
                {
                    dec -= 1;
                }
                else //Not a digit
                {
                    dec = 0;
                    if (!added)
                    {
                        postfix.Add(aux.ToString());
                        aux = 0;
                        added = true;
                    }

                    if (infix[i] == '(') //Open parenthesis
                    {
                        stack.Push('(');

                    }
                    else if (infix[i] == ')') //Close parenthesis
                    {
                        if (operators.Contains(stack.Peek()))
                        {
                            postfix.Add(stack.Pop().ToString());
                            while (stack.Count > 0)
                                if (stack.Peek() == '(')
                                    stack.Pop();
                                else
                                    break;
                        }
                    }
                    else if (operators.Contains(infix[i])) //Operator
                    {
                        if (stack.Count > 0 && pref.Contains(stack.Peek()))
                            postfix.Add(stack.Pop().ToString());
                        stack.Push(infix[i]);
                    }
                }

                if(i == infix.Length-1)  //Last character
                {
                    if(!added)
                        postfix.Add(aux.ToString());
                    while (stack.Count > 0)
                    {
                        if (operators.Contains(stack.Peek()))
                        {
                            postfix.Add(stack.Pop().ToString());
                        }
                        else
                            stack.Pop();
                    }
                }
                Console.Write("Postfix: ");
                foreach (string s in postfix)
                    Console.Write(s + " ");
                Console.Write("\n");

                Console.Write("Stack: ");
                foreach (char c in stack)
                    Console.Write(c + " ");
                Console.Write("\n");
                Console.WriteLine("Aux: " + aux);
            }

            return postfix;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> postfix = new List<string>();
            //postfix = inf2postf("((1-(2+3))*4)^(5+6)");
            //postfix = inf2postf("(1+(2*3");
            postfix = inf2postf("1.5*2-6*5");
            foreach (string s in postfix)
                Console.Write(s+" ");
            Console.ReadLine();
        }
    }
}
