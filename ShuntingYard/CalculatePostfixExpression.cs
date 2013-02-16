using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;



/// <summary>
/// Calculate a math expression which is in reverse polish notation (postfix operators)
/// </summary>
class CalculatePostfixExpression
{

    private static Stack<double> _operands;
    private static string[] _expressionElements;
    //private static int result;

    private static void Initialize(string expression)
    {
        _expressionElements = CleanExpression(expression).Split(' ');
        _operands = new Stack<double>();
    }


    /// <summary>
    /// There is a chance that the expression resulting from
    /// ShuntingYard Class may have an empty space ' ' at the very end
    /// So we remove this space if it exists
    /// </summary>
    /// <param name="expression">Math expression in reverse polish notation (result from the Shunting Yard Class</param>
    /// <returns>A cleaned version of the expression without ' ' at the end</returns>
    private static string CleanExpression(string expression)
    {
        if (expression[expression.Length - 1] == ' ')
        {
            return expression.TrimEnd(' ');
        }

        return expression;
    }


    public static double Calculate(string expression)
    {
        Initialize(expression);
        double result = 0;
        foreach (string element in _expressionElements)
        {
            if (IsNumber(element) == true)//found an operand
            {
                //push it to the stack
                //NOTE !!!! : May it be possible that this parse fails ? Is my regular expression good ?
                _operands.Push(double.Parse(element));
            }
            else //if the element is not a number (operand), then it is an operator (or function)
            {
                double temp = HandleOperations(element);
                result = temp;
                _operands.Push(temp);
            }
        }

        return result;
    }



    private static double HandleOperations(string element)
    {
        double a= 0;
        double b= 0;
        switch (element)
        {
            case "+":
                b = _operands.Pop(); //first operand should be on the right of the operator (thats for binary operators)
                a = _operands.Pop(); //second operand should be on the left of the operator (thats for binary operators)
                return a + b;
                break;
            case "-":
                b = _operands.Pop(); 
                a = _operands.Pop(); 
                return a - b;
                break;
            case "*":
                b = _operands.Pop(); 
                a = _operands.Pop();
                return a * b;
                break;
            case "/":
                b = _operands.Pop(); 
                a = _operands.Pop();
                return a / b;
                break;
            case "ln()":
                a = _operands.Pop();
                return Math.Log(a);
                break;
            case "sqrt()":
                a = _operands.Pop();
                return Math.Sqrt(a);
                break;
            case "pow()":
                b = _operands.Pop(); 
                a = _operands.Pop();
                return Math.Pow(a, b);
                break;
            default:
                return 0;
        }

    }


    /// <summary>
    /// Checks if the passed element is a number
    /// </summary>
    /// <param name="element">Element of math expression to evaluate as a number or not</param>
    /// <returns>True if the element is a number, False if not</returns>
    private static bool IsNumber(string element)
    {
        return Regex.IsMatch(element, @"(\d+)");
    }

}

