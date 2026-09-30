# Exercise 1: Testing existing code

This exercise uses the **`Calculator`** class found in `src/exercise1/Calculator.cs`,
namespace `Exercises.Exercise1`.

The guide says to clone the exercise repository and import the project into Eclipse. In
this repository the code is already here. Open `UnitTestExercises.sln` in Visual Studio,
VS Code with the C# Dev Kit, or Rider, or just work in a terminal with `dotnet build` and
`dotnet test`.

| The guide says | Here |
| --- | --- |
| Exercise: the `exercise1` package | `src/exercise1/Calculator.cs` |

---

## Part 1: Create a test plan

You are required to create a test plan which consists of test cases for the `Calculator`
class's four methods: `Add`, `Subtract`, `Multiply` and `Divide`.

Use the following template for creating your test cases. The full table, with room to fill
in, is in [`TEST_PLAN_TEMPLATE.md`](TEST_PLAN_TEMPLATE.md): copy that file to `TEST_PLAN.md`
and work there.

| ID | Method | Description | Inputs | Expected output | Actual output |
| --- | --- | --- | --- | --- | --- |
| 1 | `Add(double num1, double num2)` | Adding two small numbers | num1=10, num2=30 | 40 | |
| 2 | | | | | |
| 3 | | | | | |

One test case has been created for you as an example. It is expected that you produce **at
least three test cases per method**:

- Test borderline input values, i.e., what are the highest values you can add? What about
  the smallest?
- Test at least one normal input combination.

Useful for the borderline rows in C#: `double.MaxValue`, `double.MinValue`,
`double.Epsilon`, `double.PositiveInfinity`, `double.NegativeInfinity`.

`Divide` is the one method here that can throw. It rejects a divisor of zero with
`ArgumentException("Division by zero: divisor must not be 0")`. Plan a row for it. Exercise
2 is all about that kind of case, so this is a gentle first sight of it.

Fill the "Actual output" column in after you have run the test. Where it differs from
"Expected output", you have found either a bug in your test or a bug in the code. Both are
useful. Say which you think it is.

---

## Part 2: Implement your test plan

The `Calculator` class has already been created. Use your test plan to guide the
development of tests for the methods of this class.

**The file you edit:** `tests/exercise1/Exercise1_CalculatorTests.cs`

One worked example is already written and passing. Copy its shape. The other twelve tests
are stubs, each carrying a `[Test]` attribute with an `[Ignore("TODO - ...")]` attribute
underneath it. For each one:

1. Delete the `[Ignore(...)]` line so the test actually runs.
2. Replace `Assert.Fail("Not implemented yet")` with your arrange, act and assert.

**To run them:**

```bash
dotnet test
```

or, to run just this exercise:

```bash
dotnet test --filter "FullyQualifiedName~Exercise1"
```

**Done looks like:** all 13 tests in `Exercise1_CalculatorTests` passing, none skipped.
Across the whole solution the skipped count drops from 51 to 39.

A note on comparing doubles: `Assert.That(actual, Is.EqualTo(expected))` on two doubles is
an exact comparison, and floating point arithmetic rarely lands exactly where you expect.
Add a tolerance when that bites:
`Assert.That(actual, Is.EqualTo(0.3).Within(0.0000000001))`. Deciding which of your rows
needs it is part of the exercise.
