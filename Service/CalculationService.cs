﻿namespace Debugging.Service;
public interface ICalculationService
{
    int AddNumbers(int num1, int num2);
    int DivideNumbers(int numerator, int denominator);
    int DivideByNumbersNoNo(int numerator, int denominator);
    int DivideByNumbersBetter(int numerator, int denominator);
}

public class CalculationService : ICalculationService
{
    public int AddNumbers(int num1, int num2)
    {
        // Perform the addition (Step into here)
        var sum = num1 + num2;

        // Simulate some additional logic
        if (sum > 100)
        {
            sum = 100;  // Cap the result to 100
        }

        return sum;  // Step out here
    }

    // New method that can result in a divide by zero exception
    public int DivideNumbers(int numerator, int denominator)
    {
        // Step into here to observe the logic
        if (denominator == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero!");  // Exception breakpoint here
        }

        // Perform division
        return numerator / denominator;  // Step into here when denominator is non-zero
    }

    public int DivideByNumbersNoNo(int numerator, int denominator)
    {
        try
        {
            return numerator / denominator;
        }
        catch (Exception e)
        {
            throw new Exception("Uh oh");
        }
    }

    public int DivideByNumbersBetter(int numerator, int denominator)
    {
        try
        {
            return numerator / denominator;
        }
        catch (Exception e)
        {
            throw; 
        }
    }
}
