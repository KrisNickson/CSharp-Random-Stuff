using System;
using System.Collections.Generic;



class Program
{
    static void Main()
    {
        string r1=ShuntingYard.ToPolishNotation("4 + 2").ToString();
        string r2 = ShuntingYard.ToPolishNotation("4 + 2 - 17").ToString();
        string r3 = ShuntingYard.ToPolishNotation("4 + 2 - 3 + 5").ToString();
        string r3v2 = ShuntingYard.ToPolishNotation("4.5 + 2 - 3.8 + 5").ToString();
        
        //with operator priority
        string r4 = ShuntingYard.ToPolishNotation("a - 5 * 3").ToString(); //should be: a 5 3 * -
        string r5 = ShuntingYard.ToPolishNotation("a - 5 * 3 + 12").ToString();
        string r6 = ShuntingYard.ToPolishNotation("result = 8 - 5 * 3 + 12").ToString(); //shoould be: "result 8 5 3 * - 12 + ="
        string r7 = ShuntingYard.ToPolishNotation("result = a - 5 * 3 + 4").ToString(); //should be: "result 8 5 3 * - 12 + ="


        //test with ( ) we need to remove them
        string r8 = ShuntingYard.ToPolishNotation("( a * 3 ) + 4").ToString();//should be:  a 3 * 4 +

        //test ( ) changing the operator execution order
        string r9 = ShuntingYard.ToPolishNotation("( 3 + 5 ) / 9").ToString(); //should be: 3 5 + 9 /
        string r10 = ShuntingYard.ToPolishNotation("( ( 3 * 4 ) + 5 )").ToString();//should be: 3 4 * 5 +
        string r11 = ShuntingYard.ToPolishNotation("( ( a = 4 ) + 5 ) * 6").ToString();//should be: a 4 = 5 + 6 * 


        //text function calls
        string r12 = ShuntingYard.ToPolishNotation("sqrt ( 2, 2 )").ToString();//should be: 2 2 sqrt()

        //final test?
        string r13 = ShuntingYard.ToPolishNotation("( 3 + 5.3 ) * 2.7 - ln ( 22 ) / pow ( 2.2 , 1.7 )").ToString();
        string r132 = ShuntingYard.ToPolishNotation("pow ( 2.2 , 1337 + 1.7 )").ToString();


        //THIS SHIT FAILS SUMS 1337 WITH 3 
        //NOW IT SHOULD WORK
        string r133 = ShuntingYard.ToPolishNotation("pow ( 5 + 3 , 1337 + 1.7 )").ToString();


        List<string> zerg = ShuntingYard.AddZeroes("- 5 + - - 1 + 3");
        string r15 = ShuntingYard.ToPolishNotation("( 3 + 5.3 ) * 2.7 - ln ( 22 ) / pow ( 2.2 , - 1.7 )").ToString();
        double res = CalculatePostfixExpression.Calculate(r15);
        string r16 = ShuntingYard.ToPolishNotation("pow ( 2 , 3.14 ) * ( 3 - ( 3 * sqrt ( 2 ) - 3.2 ) + 1.5 * 0.3 )").ToString();
        double res2 = CalculatePostfixExpression.Calculate(r16);
    }
}

