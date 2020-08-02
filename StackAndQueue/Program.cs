using System;
using System.Collections.Generic;
using System.Linq;
using SeqList;
using StackAndQueue;
using PolishCalcExpressionNamespace;
using CalcEasyExpNamespace;
using LinkStackNamespace;
namespace LinkStackNamespace
{
    static class TestLinkStack
    {
        static bool MatchBracket(string str)
        {
            LinkStack<char> myCharList = new LinkStack<char>();
            char[] charlist = str.ToCharArray();
            char temp;
            for (int i = 0; i < charlist.Length; i++)
            {
                if (myCharList.IsEmpty())
                {
                    myCharList.Push(charlist[i]);
                }
                else
                {
                    temp = myCharList.GetTop();
                    if ((temp == '(' && charlist[i] == ')') || (temp == '[' && charlist[i] == ']')
                        || (temp == '{' && charlist[i] == '}'))
                    {
                        myCharList.Pop();
                    }
                    else
                    {
                        myCharList.Push(charlist[i]);
                    }
                }
            }
            if (myCharList.IsEmpty())
            {
                return true;
            }
            return false;
        }
        static bool JudgePalindrome(string str)
        {
            LinkStack<char> newLinkStack = new LinkStack<char>(str.ToCharArray());
            LinkQueue<char> newLinkQueue = new LinkQueue<char>(str.ToCharArray());
            for (int i = 0; i < str.Length; i++)
            {
                if (newLinkQueue.Out() != newLinkStack.Pop())
                {
                    return false;
                }
                break;
            }
            return true;
        }
        public static void TestReverseStack()
        {
            int[] nums = { 1, 3, 3, 5, 7, 9 };
            LinkStack<int> myStack = new LinkStack<int>(nums);
            LinkStack<int>.ShowList(myStack.Top);
            LinkStack<int>.ReverseStack(ref myStack);
            LinkStack<int>.ShowList(myStack.Top);
        }
        public static void TestReverseQueue()
        {
            int[] nums = { 1, 3, 5, 7, 9 };
            LinkQueue<int> myQueue = new LinkQueue<int>(nums);
            LinkQueue<int>.ShowList(myQueue);
            LinkQueue<int>.ReverseQueue(ref myQueue);
            LinkQueue<int>.ShowList(myQueue);
        }
        public static void TestBracketPalindome()
        {
            string str = "cab][cddc[]bac";
            Console.WriteLine(JudgePalindrome(str));
            string strTest = "[()[()][]]";
            //strTest = "([()]){(([()]))[]}";
            Console.WriteLine(MatchBracket(strTest));
        }
    }
}
namespace PolishCalcExpressionNamespace
{
    class PolishCalcExpression
    {
        static bool IsOpChar(char op)
        {
            if (op != '+' && op != '-' && op != '*' && op != '/'
                && op != '(' && op != ')' && op != '[' && op != ']' && op != '{' && op != '}')
            {
                return false;
            }
            return true;
        }
        static string[] MulDivStr = { "*", "/" };
        static string[] AddSubStr = { "+", "-" };
        static bool IsOpeartor(string op)
        {
            if (op != "+" && op != "-" && op != "*" && op != "/")
            {
                return false;
            }
            return true;
        }
        static byte IsBracket(string op)
        {
            switch (op)
            {
                case "(":
                    return 1;
                case ")":
                    return 2;
                case "[":
                    return 3;
                case "]":
                    return 4;
                case "{":
                    return 5;
                case "}":
                    return 6;
                default:
                    return 0;
            }
        }
        static bool IsNumber(char op)
        {
            if (op >= '0' && op <= '9')
            {
                return true;
            }
            return false;
        }
        static char PrecedeOperator(string op1, string op2)
        {
            if ((MulDivStr.Contains(op1) && MulDivStr.Contains(op2)) ||
                (AddSubStr.Contains(op1) && AddSubStr.Contains(op2)))
            {
                return '=';
            }
            else if (MulDivStr.Contains(op1) && AddSubStr.Contains(op2))
            {
                return '>';
            }
            else // 包含如果括号与运算符比较时，运算符大 
            {
                return '<';
            }
        }
        static double CalcTwoNums(double p, double q, string op)
        {
            switch (op)
            {
                case "+":
                    return p + q;
                case "-":
                    return p - q;
                case "*":
                    return p * q;
                case "/":
                    if (q <= double.Epsilon)
                    {
                        return double.NaN;
                    }
                    return p / q;
                default:
                    return double.NaN;
            }
        }
        static void SplitStr(string str, out HeadLinkList<string> output)
        {
            output = new HeadLinkList<string>();
            char[] charArray = str.ToCharArray();
            string tempStr = "";
            for (int i = 0; i < charArray.Length;)
            {
                if (IsOpChar(charArray[i]))
                {
                    output.AddElement(charArray[i].ToString());
                    i++;
                }
                else
                {
                    tempStr = "";
                    while (i < charArray.Length && !IsOpChar(charArray[i]))
                    {
                        tempStr += charArray[i].ToString();
                        i++;
                    }
                    output.AddElement(tempStr);
                }
            }
        }
        /// <summary>
        /// 将输入的表达式标准化，主要针对负数，将负数前面加上0，如：“-1.5”存储为 “0-1.5”，
        /// 否则后缀表达式中得到相邻的两个“-”，无法计算。
        /// 当然也可以在后面计算中添加逻辑判断
        /// </summary>
        /// <param name="strlist"></param>
        static void NormalizeStrList(ref HeadLinkList<string> strlist)
        {
            if (strlist.Head.Next.Data == "-")
            {
                strlist.Insert("0", 0);
            }
            List<int> indexList = strlist.FullMatch("(");
            int index = 0;
            foreach (var item in indexList)
            {
                if (strlist.GetElement(item + 1 + index) == "-")
                {
                    strlist.Insert("0", item + 1 + index);
                    index++;
                }
            }
        }
        /// <summary>
        /// 将中缀表达式转换为后缀表达式，转换规则如下：
        /// 一个带头结点链表存储转换结果，一个栈存储中间过程。
        /// 遇到数字直接进链表
        /// 栈为空时，遇到符号直接进栈
        /// 遇到左括号“([{"直接进栈
        /// 遇到右括号")]}",直到遇到对应的左括号，将中间的运算符加入链表
        /// 遇到操作符，与栈顶的运算符比较，如果栈顶为括号，直接加入链表
        /// 如果栈顶优先级小于该操作符，该操作符直接进栈
        /// 如果栈顶符号不小于该操作符，则将栈顶元素弹出加入链表，继续判断下一个栈顶元素，直到满足上述各条件
        /// 最后将该操作符进栈
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        #region 使用自己编写的带头结点链表存储操作数据，实现逆波兰法
        static void GetPostFixPolishExpression(HeadLinkList<string> input, out HeadLinkList<string> output)
        {
            LinkStack<string> myStack = new LinkStack<string>();
            output = new HeadLinkList<string>();
            SeqList.Node<string> head = input.Head;
            string tempStr = "";
            while (head.Next != null)
            {
                head = head.Next;
                tempStr = head.Data;
                if (!IsOpeartor(tempStr) && IsBracket(tempStr) == 0)
                {
                    output.AddElement(tempStr);
                }
                else
                {
                    if (myStack.IsEmpty())
                    {
                        myStack.Push(tempStr);
                    }
                    else
                    {
                        if (IsBracket(tempStr) % 2 == 1)
                        {
                            myStack.Push(tempStr);
                        }
                        else if (IsBracket(tempStr) == 2)
                        {
                            while (myStack.GetTop() != "(")
                            {
                                output.AddElement(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else if (IsBracket(tempStr) == 4)
                        {
                            while (myStack.GetTop() != "[")
                            {
                                output.AddElement(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else if (IsBracket(tempStr) == 6)
                        {
                            while (myStack.GetTop() != "{")
                            {
                                output.AddElement(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else
                        {
                            if (PrecedeOperator(myStack.GetTop(), tempStr) == '<')//如果栈顶为括号，小于任意运算符，运算符直接进栈
                            {
                                myStack.Push(tempStr);
                            }
                            else
                            {
                                while (PrecedeOperator(myStack.GetTop(), tempStr) != '<' && !myStack.IsEmpty())
                                {
                                    output.AddElement(myStack.Pop());
                                }
                                myStack.Push(tempStr);
                            }
                        }

                    }
                }
            }
            while (!myStack.IsEmpty())
            {
                output.AddElement(myStack.Pop());
            }
        }
        static void CalcPostFixPolishExpression(HeadLinkList<string> input)
        {
            if (input.GetLength() == 1)
            {
                Console.WriteLine(input.Head.Next.Data);
                return;
            }
            SeqList.Node<string> tempNode = input.Head.Next;
            int num = -1;
            while (tempNode.Next.Next != null)
            {
                num++;
                if (IsOpeartor(tempNode.Data) && !IsOpeartor(tempNode.Next.Data) && !IsOpeartor(tempNode.Next.Next.Data))
                {
                    string result = CalcTwoNums(Convert.ToDouble(tempNode.Next.Next.Data), Convert.ToDouble(tempNode.Next.Data), tempNode.Data).ToString();
                    input.RemoveElementAt(num);
                    input.RemoveElementAt(num);
                    input.RemoveElementAt(num);
                    input.Insert(result, num);
                    break;
                }
                tempNode = tempNode.Next;
            }
            //在此处增加显示列表，查看是否进行正确运算
            //HeadLinkList<string>.ShowList(input.Head);
            CalcPostFixPolishExpression(input);
            return;
        }
        #endregion
        static void ReverseString(ref string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            str = new string(charArray);
        }
        /// <summary>
        /// 计算任意不带括号的表达式，按照这个思路，如果计算待括号的表达式，逻辑将会是很复杂的。
        /// 所以针对带括号的表达式决定使用波兰法和逆波兰法
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static string CalcExpressionAnyNum(List<string> list)
        {
            LinkStack<double> operand = new LinkStack<double>();
            LinkStack<string> opStr = new LinkStack<string>();
            string op = default(string);
            double firstOp = double.Epsilon;
            double secOp = double.Epsilon;
            string temp = default(string);
            for (int i = 0; i < list.Count;)
            {
                temp = list[i];
                if (!IsOpeartor(temp))
                {
                    operand.Push(Convert.ToDouble(temp));
                    i++;
                }
                else
                {
                    if (opStr.IsEmpty())
                    {
                        opStr.Push(temp);
                        i++;
                    }
                    else
                    {
                        switch (PrecedeOperator(opStr.GetTop(), temp))
                        {
                            case '<':
                                firstOp = operand.Pop();
                                if (++i < list.Count)
                                {
                                    secOp = Convert.ToDouble(list[i]);
                                }
                                operand.Push(CalcTwoNums(firstOp, secOp, temp));
                                break;
                            case '=':
                                secOp = operand.Pop();
                                firstOp = operand.Pop();
                                op = opStr.Pop();
                                operand.Push(CalcTwoNums(firstOp, secOp, op));
                                opStr.Push(temp);
                                break;
                            case '>':
                                secOp = operand.Pop();
                                firstOp = operand.Pop();
                                op = opStr.Pop();
                                operand.Push(CalcTwoNums(firstOp, secOp, op));
                                opStr.Push(temp);
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                }
            }
            if (opStr.GetLength() == 1)
            {
                secOp = operand.Pop();
                firstOp = operand.Pop();
                op = opStr.Pop();
                return CalcTwoNums(firstOp, secOp, op).ToString();
            }
            else
            {
                //经过上述计算，已经将表达式转换为同一级别的运算，只需要再进行一次运算即可
                //但是因为栈是从顶部出来，所以需要将出栈的表达式翻转一下。
                string strResult = operand.Pop().ToString();
                while (!opStr.IsEmpty())
                {
                    strResult += opStr.Pop();
                    strResult += operand.Pop().ToString();
                }
                //将表达式翻转。
                ReverseString(ref strResult);
                return strResult;
            }
        }
        static List<string> SplitStr2List(string str)
        {
            List<string> myList = new List<string>();
            char[] charArray = str.ToCharArray();
            string tempStr = "";
            for (int i = 0; i < charArray.Length;)
            {
                if (IsOpChar(charArray[i]))
                {
                    myList.Add(charArray[i].ToString());
                    i++;
                }
                else
                {
                    tempStr = "";
                    while (i < charArray.Length && !IsOpChar(charArray[i]))
                    {
                        tempStr += charArray[i].ToString();
                        i++;
                    }
                    myList.Add(tempStr);
                }
            }
            return myList;
        }
        static void NormalizeStr2List(ref List<string> strlist)
        {
            if (strlist[0] == "-")
            {
                strlist.Insert(0, "0");
            }
            for (int i = 0; i < strlist.Count - 1; i++)
            {
                if (strlist[i] == "(" && strlist[i + 1] == "-")
                {
                    strlist.Insert(i + 1, "0");
                }
            }
        }
        #region 用C#自带的List存储数据，实现逆波兰法
        static void PostFixCalc(List<string> list)
        {
            if (list.Count == 1)
            {
                Console.WriteLine(list[0]);
                return;
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (IsOpeartor(list[i]) && !IsOpeartor(list[i - 1]) && !IsOpeartor(list[i - 2]))
                {
                    string result = CalcTwoNums(Convert.ToDouble(list[i - 2]), Convert.ToDouble(list[i - 1]), list[i]).ToString();
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    list.RemoveAt(i - 2);
                    list.Insert(i - 2, result);
                    break;
                }
            }
            PostFixCalc(list);
            return;
        }
        static void PostFixPolish(List<string> list, out List<string> output)
        {
            LinkStack<string> myStack = new LinkStack<string>();
            output = new List<string>();
            string tempStr = "";
            for (int i = 0; i < list.Count; i++)
            {
                tempStr = list[i];
                if (!IsOpeartor(tempStr) && IsBracket(tempStr) == 0)
                {
                    output.Add(tempStr);
                }
                else
                {
                    if (myStack.IsEmpty())
                    {
                        myStack.Push(tempStr);
                    }
                    else
                    {
                        if (IsBracket(tempStr) % 2 == 1)
                        {
                            myStack.Push(tempStr);
                        }
                        else if (IsBracket(tempStr) == 2)
                        {
                            while (myStack.GetTop() != "(")
                            {
                                output.Add(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else if (IsBracket(tempStr) == 4)
                        {
                            while (myStack.GetTop() != "[")
                            {
                                output.Add(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else if (IsBracket(tempStr) == 6)
                        {
                            while (myStack.GetTop() != "{")
                            {
                                output.Add(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else
                        {
                            if (PrecedeOperator(myStack.GetTop(), tempStr) == '<')
                            {
                                myStack.Push(tempStr);
                            }
                            else
                            {
                                while (PrecedeOperator(myStack.GetTop(), tempStr) != '<' && !myStack.IsEmpty())
                                {
                                    output.Add(myStack.Pop());
                                }
                                myStack.Push(tempStr);
                            }
                        }

                    }
                }
            }
            while (!myStack.IsEmpty())
            {
                output.Add(myStack.Pop());
            }
        }
        #endregion
        #region 系统自带的List存储数据，实现波兰法
        static void GetPreFixPolishExpression(List<string> list, out List<string> output)
        {
            LinkStack<string> myStack = new LinkStack<string>();
            output = new List<string>();
            string tempStr = "";
            //从后向前判断
            for (int i = list.Count - 1; i >= 0; i--)
            {
                tempStr = list[i];
                if (!IsOpeartor(tempStr) && IsBracket(tempStr) == 0)
                {
                    output.Add(tempStr);
                }
                else
                {
                    if (myStack.IsEmpty())
                    {
                        myStack.Push(tempStr);
                    }
                    else
                    {
                        if (IsBracket(tempStr) == 0)
                        {
                            if (PrecedeOperator(myStack.GetTop(), tempStr) != '>')
                            {
                                myStack.Push(tempStr);
                            }
                            else
                            {
                                while (PrecedeOperator(myStack.GetTop(), tempStr) == '>' && !myStack.IsEmpty())
                                {
                                    output.Add(myStack.Pop());
                                }
                                myStack.Push(tempStr);
                            }
                        }
                        else if (IsBracket(tempStr) == 1)
                        {
                            while (myStack.GetTop() != ")")
                            {
                                output.Add(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else if (IsBracket(tempStr) == 3)
                        {
                            while (myStack.GetTop() != "]")
                            {
                                output.Add(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else if (IsBracket(tempStr) == 5)
                        {
                            while (myStack.GetTop() != "}")
                            {
                                output.Add(myStack.Pop());
                            }
                            myStack.Pop();
                        }
                        else
                        {
                            myStack.Push(tempStr);
                        }

                    }
                }
            }
            while (!myStack.IsEmpty())
            {
                output.Add(myStack.Pop());
            }
        }
        static void CalcPreFixPolishExpression(List<string> list)
        {
            if (list.Count == 1)
            {
                Console.WriteLine(list[0]);
                return;
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (IsOpeartor(list[i]) && !IsOpeartor(list[i - 1]) && !IsOpeartor(list[i - 2]))
                {
                    string result = CalcTwoNums(Convert.ToDouble(list[i - 1]), Convert.ToDouble(list[i - 2]), list[i]).ToString();
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    list.RemoveAt(i - 2);
                    list.Insert(i - 2, result);
                    break;
                }
            }
            CalcPreFixPolishExpression(list);
            return;
        }
        #endregion
        static void TestViolentSolution()
        {
            string test = "1+2*3-4-5*6";
            Console.WriteLine(Convert.ToDouble(CalcExpressionAnyNum(SplitStr2List(test))));
            string input = "";
            string select;
            while (true)
            {
                Console.WriteLine("请输入表达式(不含括号)");
                input = "";
                do
                {
                    input += Console.ReadLine();
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
                Console.WriteLine(Convert.ToDouble(CalcExpressionAnyNum(SplitStr2List(input))));
                Console.WriteLine("是否继续[y/n]:");
                select = Console.ReadLine();
                if (select.ToUpper() != "Y")
                {
                    Console.WriteLine("退出成功！");
                    break;
                }
            }
        }
        static void TestSelf()
        {
            string expression = "1.8+3.6*1.5+{[2*(3-4)-5]*6-3}/9+10*(10-33/66)-(-0.4)*7";
            HeadLinkList<string> strList = new HeadLinkList<string>();
            HeadLinkList<string> expressionResult = new HeadLinkList<string>();
            SplitStr(expression, out strList);
            NormalizeStrList(ref strList);
            HeadLinkList<string>.ShowList(strList.Head);
            GetPostFixPolishExpression(strList, out expressionResult);
            HeadLinkList<string>.ShowList(expressionResult.Head);
            Console.WriteLine();
            expressionResult.Head = HeadLinkList<string>.Reverse(expressionResult.Head);
            HeadLinkList<string>.ShowList(expressionResult.Head);
            Console.WriteLine();
            CalcPostFixPolishExpression(expressionResult);
        }
        static void TestPostPolish()
        {
            Console.WriteLine("逆波兰法计算表达式：");
            string expression = "1.8+3.6*1.5+{[2*(3-4)-5]*6-3}/9+10*(10-33/66)-(-0.4)*7";
            List<string> strlist = SplitStr2List(expression);
            List<string> result = new List<string>();
            NormalizeStr2List(ref strlist);
            PostFixPolish(strlist, out result);
            PostFixCalc(result);
        }
        static void ShowList(List<string> strlist)
        {
            foreach (var item in strlist)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        static void TestPrePolish()
        {
            Console.WriteLine("波兰法计算表达式：");
            string expression = "1.8+3.6*1.5+{[2*(3-4)-5]*6-3}/9+10*(10-33/66)-(-0.4)*7";
            List<string> strlist = SplitStr2List(expression);
            List<string> result = new List<string>();
            NormalizeStr2List(ref strlist);
            ShowList(strlist);
            GetPreFixPolishExpression(strlist, out result);
            ShowList(result);
            CalcPreFixPolishExpression(result);
        }
        public static void Action()
        {
            TestSelf();
            TestPrePolish();
            TestPostPolish();
            TestViolentSolution();
        }
    }
}
namespace CalcEasyExpNamespace
{
    class CalcExp
    {
        static char[] MulDiv = { '*', '/' };
        static char[] AddSub = { '+', '-' };
        static bool IsOpChar(char op)
        {
            if (op != '+' && op != '-' && op != '*' && op != '/'
                && op != '(' && op != ')' && op != '[' && op != ']' && op != '{' && op != '}')
            {
                return false;
            }
            return true;
        }
        static char[] WholeChar = { '+', '-', '*', '/', '(', ')' };
        static double Calc(double p, double q, char op)
        {
            switch (op)
            {
                case '+':
                    return p + q;
                case '-':
                    return p - q;
                case '*':
                    return p * q;
                case '/':
                    if (q <= double.Epsilon)
                    {
                        return double.NaN;
                    }
                    return p / q;
                default:
                    return double.NaN;
            }
        }
        static char Precede(char op1, char op2)
        {
            if ((MulDiv.Contains(op1) && MulDiv.Contains(op2)) ||
                (AddSub.Contains(op1) && AddSub.Contains(op2)))
            {
                return '=';
            }
            else if (MulDiv.Contains(op1) && AddSub.Contains(op2))
            {
                return '>';
            }
            else
            {
                return '<';
            }
        }
        static void ReverseString(ref string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            str = new string(charArray);
        }
        /// <summary>
        /// 一位数的表达式四则远算（不带括号）
        /// 不断判断操作符与栈顶操作符的优先级，选择是否取数计算，将结果入栈
        /// 如果强迫机器按照人类的认知计算，就是这么繁琐，这还没有加括号
        /// 对比之下，更突显波兰法的精妙
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string CalcExpressionSingleNum(string str)
        {
            LinkStack<double> operand = new LinkStack<double>();
            LinkStack<char> opChar = new LinkStack<char>();
            int index = 0;
            char temp = str[index];
            char op = default(char);
            double firstOp = double.Epsilon;
            double secOp = double.Epsilon;
            while (temp != '#')
            {
                if (!IsOpChar(temp))
                {
                    operand.Push(Convert.ToDouble(temp.ToString()));
                }
                else
                {
                    if (opChar.IsEmpty())
                    {
                        opChar.Push(temp);
                    }
                    else
                    {
                        switch (Precede(opChar.GetTop(), temp))
                        {
                            case '<':
                                firstOp = operand.Pop();
                                secOp = Convert.ToDouble(str[++index].ToString());
                                operand.Push(Calc(firstOp, secOp, temp));
                                break;
                            case '=':
                                secOp = operand.Pop();
                                firstOp = operand.Pop();
                                op = opChar.Pop();
                                operand.Push(Calc(firstOp, secOp, op));
                                opChar.Push(temp);
                                break;
                            case '>':
                                secOp = operand.Pop();
                                firstOp = operand.Pop();
                                op = opChar.Pop();
                                operand.Push(Calc(firstOp, secOp, op));
                                opChar.Push(temp);
                                break;
                            default:
                                break;
                        }

                    }
                }
                temp = str[++index];
            }
            if (opChar.GetLength() == 1)
            {
                secOp = operand.Pop();
                firstOp = operand.Pop();
                op = opChar.Pop();
                return Calc(firstOp, secOp, op).ToString();
            }
            else
            {
                string strResult = "#" + operand.Pop().ToString();
                while (!opChar.IsEmpty())
                {
                    strResult += opChar.Pop().ToString();
                    strResult += operand.Pop().ToString();
                }
                ReverseString(ref strResult);
                return strResult;
            }
        }
        public static void Action()
        {
            string select;
            string expression;
            while (true)
            {
                Console.WriteLine("请输入表达式(不含括号),以#结束：");
                expression = "";
                do
                {
                    expression += Console.ReadLine();
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
                try
                {
                    Console.WriteLine(Convert.ToDouble(CalcExpressionSingleNum(expression)));
                }
                catch (Exception)
                {
                    Console.WriteLine(Convert.ToDouble(CalcExpressionSingleNum(CalcExpressionSingleNum(expression))));
                }
                Console.WriteLine("是否继续[y/n]:");
                select = Console.ReadLine();
                if (select.ToUpper() != "Y")
                {
                    Console.WriteLine("退出成功！");
                    break;
                }
            }
        }
    }
}
namespace StackAndQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLinkStack.TestReverseQueue();
            TestLinkStack.TestReverseStack();
            //PolishCalcExpression.Action();
            //CalcExp.Action();
            Console.Read();
        }
    }
}
