namespace Exercises.Exercise1;

/// <summary>
/// A deliberately simple calculator. This is the "existing code" for exercise 1:
/// you are not asked to change it, you are asked to test it.
/// </summary>
public class Calculator
{
    /// <summary>Returns the sum of the two numbers.</summary>
    public double Add(double num1, double num2)
    {
        return num1 + num2;
    }

    /// <summary>Returns the first number minus the second.</summary>
    public double Subtract(double num1, double num2)
    {
        return num1 - num2;
    }

    /// <summary>Returns the product of the two numbers.</summary>
    public double Multiply(double num1, double num2)
    {
        return num1 * num2;
    }

    /// <summary>
    /// Returns the first number divided by the second.
    /// Throws when the divisor is zero.
    /// </summary>
    /// <remarks>
    /// MAPPING NOTE: the Java original throws IllegalArgumentException here.
    /// The .NET equivalent is ArgumentException: both mean "a caller passed an
    /// argument value this method cannot accept". We do NOT use DivideByZeroException,
    /// because that is reserved by the runtime for integer division by zero, and this
    /// method deals in doubles (where 1.0 / 0.0 would otherwise quietly give Infinity).
    /// Guarding the argument, and reporting it as a bad argument, is the faithful mapping.
    /// </remarks>
    public double Divide(double num1, double num2)
    {
        if (num2 == 0) throw new ArgumentException("Division by zero: divisor must not be 0");
        return num1 / num2;
    }
}
