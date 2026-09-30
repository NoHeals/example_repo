using Exercises.Exercise1;
using NUnit.Framework;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 1: testing existing code.
///
/// The Calculator in src/exercise1/Calculator.cs already exists and already works.
/// This exercise is about writing tests for code you did not write: reading it, deciding
/// what is worth checking, and proving it behaves as documented.
///
/// WHAT YOU DO HERE. Fill in the twelve stubs below with tests for Add, Subtract, Multiply
/// and Divide: at least three per method, one ordinary case and the borderline values at
/// the edges of what a double can hold. You do not edit src/, only this file.
///
/// TWO PARTS, IN THIS ORDER.
///   1. Write the test plan FIRST. Copy tasks/TEST_PLAN_TEMPLATE.md to TEST_PLAN.md at the
///      root of this repository and fill in the exercise 1 table: ID, method, description,
///      inputs, expected output, actual output. Writing the plan is the exercise.
///   2. Then implement it. Each row becomes one test down here. The tests are just the plan
///      turned into C#.
///
/// HOW THE TODOs WORK. Every unwritten test carries an [Ignore("TODO ...")] attribute, so
/// NUnit reports it as SKIPPED rather than failed and a fresh clone is green. To activate
/// one, delete its [Ignore] line, leaving [Test] alone. It will now fail, which is correct.
/// Then replace the comment and the Assert.Fail with arrange, act and assert.
///
/// TO RUN, from the csharp folder at the root of this repository:
///   dotnet build                                                      compile
///   dotnet test                                                       the whole suite
///   dotnet test --filter "FullyQualifiedName~Exercise1"               just this exercise
///   dotnet test --filter "FullyQualifiedName~Add_TwoSmallNumbers"     one test by name
///
/// Done looks like all 13 tests in this class passing and none skipped.
///
/// THE FULL BRIEF, with the guide's own test plan table, is in
/// tasks/01_testing_existing_code.md.
/// </summary>
[TestFixture]
public class Exercise1_CalculatorTests
{
    private Calculator _calculator;

    // [SetUp] runs before EVERY test, exactly like JUnit's @BeforeEach. NUnit reuses one
    // instance of this class for the whole fixture, so building a fresh calculator here is
    // what stops one test affecting another.
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. This is row 1 of the test plan in the exercise guide.
    // Copy this shape for the rest. Notice the three labelled steps, and the name:
    // Method_Scenario_ExpectedResult.
    // ---------------------------------------------------------------------------------
    [Test]
    public void Add_TwoSmallNumbers_ReturnsTheirSum()
    {
        // Arrange: set up the inputs and the answer we expect.
        double num1 = 10;
        double num2 = 30;
        double expected = 40;

        // Act: call the one method under test.
        double actual = _calculator.Add(num1, num2);

        // Assert: check what came back.
        Assert.That(actual, Is.EqualTo(expected));
        // In JUnit this assertion is assertEquals(expected, actual), with the expected
        // value FIRST. NUnit's constraint form puts the actual value first. Getting the
        // order backwards still compiles and still passes, but names the wrong side when
        // it fails.
    }

    // ---------------------------------------------------------------------------------
    // Add
    // ---------------------------------------------------------------------------------

    [Test]
    public void Add_TwoNegativeNumbers_ReturnsNegativeSum()
    {
        double num1 = -10;
        double num2 = -30;
        double expected = -40;

        double actual = _calculator.Add(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Add_MaxValueToMaxValue_ReturnsPositiveInfinity()
    {
        double num1 = double.MaxValue;
        double num2 = double.MaxValue;
        double expected = double.PositiveInfinity;

        double actual = _calculator.Add(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Add_TwoVerySmallNumbers_ReturnsSumOfSmallestValues()
    {
        double num1 = double.Epsilon;
        double num2 = double.Epsilon;
        double expected = 2 * double.Epsilon;

        double actual = _calculator.Add(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // ---------------------------------------------------------------------------------
    // Subtract
    // ---------------------------------------------------------------------------------

    [Test]
    public void Subtract_LargerFromSmaller_ReturnsNegativeResult()
    {
        double num1 = 10;
        double num2 = 30;
        double expected = -20;

        double actual = _calculator.Subtract(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Subtract_NumberFromItself_ReturnsZero()
    {
        double num1 = 42;
        double num2 = 42;
        double expected = 0;

        double actual = _calculator.Subtract(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Subtract_MinValueMinusMaxValue_ReturnsNegativeInfinity()
    {
        double num1 = double.MinValue;
        double num2 = double.MaxValue;
        double expected = double.NegativeInfinity;

        double actual = _calculator.Subtract(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // ---------------------------------------------------------------------------------
    // Multiply
    // ---------------------------------------------------------------------------------

    [Test]
    public void Multiply_TwoNormalNumbers_ReturnsProduct()
    {
        double num1 = 6;
        double num2 = 7;
        double expected = 42;

        double actual = _calculator.Multiply(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Multiply_AnyNumberByZero_ReturnsZero()
    {
        double num1 = 42;
        double num2 = 0;
        double expected = 0;

        double actual = _calculator.Multiply(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Multiply_MaxValueByTwo_ReturnsPositiveInfinity()
    {
        double num1 = double.MaxValue;
        double num2 = 2;
        double expected = double.PositiveInfinity;

        double actual = _calculator.Multiply(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // ---------------------------------------------------------------------------------
    // Divide
    // ---------------------------------------------------------------------------------

    [Test]
    public void Divide_TwoNormalNumbers_ReturnsQuotient()
    {
        double num1 = 10;
        double num2 = 4;
        double expected = 2.5;

        double actual = _calculator.Divide(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Divide_ByZero_ThrowsArgumentException()
    {
        double num1 = 10;
        double num2 = 0;
        string expectedMessage = "Division by zero: divisor must not be 0";

        ArgumentException exception = Assert.Throws<ArgumentException>(() => _calculator.Divide(num1, num2));

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void Divide_ZeroByNonZero_ReturnsZero()
    {
        double num1 = 0;
        double num2 = 10;
        double expected = 0;

        double actual = _calculator.Divide(num1, num2);

        Assert.That(actual, Is.EqualTo(expected));
    }
}
