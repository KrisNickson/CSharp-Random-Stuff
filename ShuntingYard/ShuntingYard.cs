using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


/// <summary>
/// Converts a standard infix math expression to a 
/// an infix expression (Reverse Polish notation)
/// Example 
/// 2 + 3 => 2 3 +
/// </summary>
class ShuntingYard
{


    private static StringBuilder _result; //the result goes here lol
    //store the elements of the expression:
    //opreators, operands, etc. 
    private static string[] _elements;
    //private static string _prevElement; //here store the previous element
    private static Stack<string> _prevElements; 


    /// <summary>
    /// Initializes the needed variables (fields)
    /// </summary>
    /// <param name="expression">The expression for which we will be initializing the variables</param>
    private static void Initialize(string expression)
    {
        _result = new StringBuilder(expression.Length);
        _elements = AddZeroes(expression).ToArray();
        //_prevElement = "";
        _prevElements = new Stack<string>();
    }


    /// <summary>
    /// Transforms a mathematical expression in infix notation
    /// to mathematical expression in postfix notation - ReversePolishNotation
    /// </summary>
    /// <param name="expression">The math expression in infix notation</param>
    /// <returns>The math expression in Reverse Polish notation</returns>
    public static StringBuilder ToPolishNotation(string expression)
    {
        //variable initializatione
        Initialize(expression);

        int i = 0; //this will be used to indicate the last iteration of the cycle
        foreach (string currentElement in _elements)
        {
            //clean parameter separator if there is any
            string currentElementCleaned = currentElement;//.Trim(',');

            //if the current element is a number or a letter (yes math expressions can have letters
            //append it to the result 
            if (IsFunctionCall(currentElementCleaned) == true)
            {
                HandleFunctionCall(currentElementCleaned);
            }
            else if (currentElementCleaned == ",")
            {
                while(_prevElements.Peek() != "(")
                {
                    AppendElement(_prevElements.Pop());
                }
            }
            else if (IsLeftParentheses(currentElementCleaned) == true)
            {
                HandleLeftParentheses(currentElementCleaned);
            }
            else if (IsRightParentheses(currentElementCleaned) == true)
            {
                HandleRightParentheses(); //if it is the last iteration we add space
            }
            else if (IsLiteral(currentElementCleaned) == true)
            {
                HandleLiteral(currentElementCleaned);
            }
            else //else the element is an operator
            {
                //when we encounter an operator, if we have encountered an operator before
                //we must add that previous operator to its respective operands 
                //which are by now at the result, so we append the previous operator
                //if it exists
                HandleOperator(currentElementCleaned);
            }

            i++;
        }

        //appends the all remaining elements (which are operators most probably)
        while(_prevElements.Count != 0)
        {
            AppendElement(_prevElements.Pop(), _prevElements.Count != 0);
            //_prevElements.Count != 0 this part is AWESOME
            //will add " " spaces to the result only UP TO the last operator
        }

        return _result;
    }

     
    /// <summary>
    /// Appends the passed literal to the result
    /// </summary>
    /// <param name="currentElement">A literal from a math expression (NOT an operator)</param>
    private static void HandleLiteral(string currentElement)
    {
        AppendElement(currentElement);
    }


    /// <summary>
    /// Appends the last found operator to the result
    /// </summary>
    /// <param name="currentElement">An operator from a math expression </param>
    private static void HandleOperator(string currentElement)
    {
        //while the previous operator(s) have higher priority than the current
        //one, we must append them to the result
        while (PreviousBeforeCurrent(currentElement) == true)
        {
            //check if we have previously found an operator (last found operator)
            if (_prevElements.Count != 0 && _prevElements.Peek() != "")
            {
                AppendElement(_prevElements.Pop());//if we have, we append it to the result
            }
        }

        _prevElements.Push(currentElement); //store the current operator to be avaiable for the next pass
    }



    /// <summary>
    /// Handles the occurance of a "(" in the expression.
    /// </summary>
    /// <param name="element">A left/opening prantheses</param>
    private static void HandleLeftParentheses(string element)
    {
        _prevElements.Push(element); //add the parantheses to the stack of operators
    }




    private static void HandleRightParentheses()
    {
        //add the operators to the result
        while (_prevElements.Count!=0 && _prevElements.Peek() != "(")
        {
            AppendElement(_prevElements.Pop());
        }
        //when we reach the "(" we just remove it from the stack
        if (_prevElements.Count != 0)
        {
            _prevElements.Pop();
        }
        
    }




    private static void HandleFunctionCall(string function)
    {
        _prevElements.Push(function + "()");
    }


    /// <summary> 
    /// Appends the passed element to the result (_result) and adds empty space if needed.
    /// By default adds an empty space after each element.
    /// </summary>
    /// <param name="element">element to append</param>
    /// <param name="addSpace">Indicates wether to add or not an empty space ""
    /// at after the element</param>
    private static void AppendElement(string element, bool addSpace=true)
    {
        _result.Append(element);
        if (addSpace == true)
        {
            _result.Append(" ");
        }
    }



    /// <summary>
    /// Checks if the passed element of an math expression is 
    /// a literal like number or a letter
    /// </summary>
    /// <param name="element">Eelement of a match expression to be evaluated</param>
    /// <returns>True if the element is [0, 9] or [a, z] or [A, Z]</returns>
    private static bool IsLiteral(string element)
    {
        //the regular expression checks if the element is a "word thing"
        //or a "number thing" rofl
        return Regex.IsMatch(element, @"(\w+|\d+)");
    }



    /// <summary>
    /// Checks if the passed element is a left/opening parentheses "("
    /// </summary>
    /// <param name="element">a non literal element (operator of some kind)</param>
    /// <returns>True if the element is "(" or False if it is not</returns>
    private static bool IsLeftParentheses(string element)
    {
        return "(" == element;
    }


    /// <summary>
    /// Checks if the passed element is a right/ closing parentheses ")"
    /// </summary>
    /// <param name="element">a non literal element (operator of some kind)</param>
    /// <returns>True if the element is ")" or False if it is not</returns>
    private static bool IsRightParentheses(string element)
    {
        return ")" == element;
    }



    private static bool IsFunctionCall(string element)
    {
        switch (element)
        {
            case "pow":
            case "ln":
            case "sqrt":
                return true;
                break;
            default:
                return false;
                break;
        }
    }



    /// <summary>
    /// Decides wether the previous operator should execute before the current operator.
    /// </summary>
    /// <param name="currentOperator">The current operator to be evaluated agains the previous one</param>
    /// <returns>True if the previous operator should execute before the current one.
    /// False if the other way around</returns>
    private static bool PreviousBeforeCurrent(string currentOperator)
    {
        if (_prevElements.Count == 0)
        {
            //no previous operator, so it cant have higher priority than the current one
            return false; 
        }
        int prevPriority = CalculatePriority(_prevElements.Peek());
        int currentPriority = CalculatePriority(currentOperator);
        return prevPriority >= currentPriority;
    }



    /// <summary>
    /// Callculates the priority (precedence of the passed) math operator
    /// </summary>
    /// <param name="operat">The operator for which priority we want to callculate</param>
    /// <returns>The priority of the passed operator as an integer number</returns>
    private static int CalculatePriority(string currentOperator)
    {
        int priority = 0;
        switch (currentOperator)
        {
            case "*":
            case "/":
                priority = 100;
                break;
            case "+":
            case "-":
                priority = 10;
                break;
            case "(":
            case ")":
                priority = -1;
                break;
            case "pow()":
            case "ln()":
            case "sqrt()":
                priority = 110;
                break;
            default:
                priority = 1;
                break;
        }
        return priority;
    }


    public static List<string> AddZeroes(string inputExpression)
    {
        string[] temp = inputExpression.Split(' ');
        List<string> result = new List<string>();
        for (int i = 0; i < temp.Length; i++)
        {
            // if an operator - is preceeded by  any of the signs in the if statement =>
            //its a binary - and we add zeroes
            if (temp[i] == "-" && (i - 1 < 0 || temp[i-1]=="," || temp[i-1] =="+" || temp[i-1] == "-" ||
                temp[i-1] == "*" || temp[i-1]=="/" || temp[i-1]=="("))
            {
                result.Add("0");
                result.Add(temp[i]);
            }
            else
            {
                result.Add(temp[i]);
            }
        }
        return result;
    }

}

