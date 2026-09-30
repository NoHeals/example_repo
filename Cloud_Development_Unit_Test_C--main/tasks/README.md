# SDL3 Module 5: Testing, Exercise Guide

This folder is the exercise guide for the module, written out in full so that this
repository is all you need. You do not need the PDF and you do not need any other
repository: the exercise code and the worksheets are all here.

This is the C# edition. The exercises are the same three exercises the guide describes,
translated from the Java original.

## Before you start

You need the **.NET SDK 9.0** or newer. Check with:

```bash
dotnet --list-sdks
```

Then, from the `csharp` folder at the root of this repository:

```bash
dotnet build      # compiles the exercise code and your tests
dotnet test       # runs your tests
```

On a fresh clone `dotnet test` **passes**, because every test you have not written yet
carries an `[Ignore]` attribute, so NUnit reports it as skipped rather than failed:

```
Passed!  - Failed:     0, Passed:     3, Skipped:    51, Total:    54
```

Those 3 passing tests are the worked examples, one per exercise. The 51 skipped ones are
your job. As you implement each test you delete its `[Ignore(...)]` line, and the skipped
count goes down by one.

The guide says to "import the project into Eclipse". The .NET equivalent is to open
`UnitTestExercises.sln` in Visual Studio, Visual Studio Code with the C# Dev Kit extension,
or JetBrains Rider. You do not have to: `dotnet build` and `dotnet test` in a terminal do
the same job, and a plain text editor is enough.

## The order of work

| | Task | You edit |
| --- | --- | --- |
| 1 | [Testing existing code](01_testing_existing_code.md) | `tests/exercise1/Exercise1_CalculatorTests.cs` |
| 2 | [Testing exceptions](02_testing_exceptions.md) | `tests/exercise2/Exercise2_UserServiceTests.cs` |
| 3 | [Mocking in a unit test](03_mocking.md) | `tests/exercise3/Exercise3_UserControllerTests.cs` |
| 4 | [Test-driven development](04_stretch_tdd_repository.md) (stretch) | `tests/exercise3/Exercise3_Stretch_ConcreteUserRepositoryTests.cs` and a new class in `src/` |

Do them in order. Exercise 3 reuses the test plan you wrote for exercise 2, and the stretch
task builds the repository that exercise 3 mocked.

## The test plan template

Every exercise starts with **Part 1: create a test plan**, before you write any test code.

The tables are in [`TEST_PLAN_TEMPLATE.md`](TEST_PLAN_TEMPLATE.md), in this folder. Copy it
to `TEST_PLAN.md` at the root of the repository and fill it in as you go. It already holds
the worked example rows from the guide.

Writing the plan first is the exercise. The tests are just the plan turned into C#.

## Where the code lives

The guide links to a GitHub repository for the exercise code. This repository replaces it.

| The guide says | Here it is |
| --- | --- |
| Exercise repository, `exercise1` package | `src/exercise1/` |
| Exercise repository, `exercise2` package | `src/exercise2/` |
| Exercise repository, `exercise3` package | `src/exercise3/` |

The solutions are not in this repository. Your trainer has them. The whole point of the
exercise is the plan you write and the tests you write from it, and there is nothing here
you cannot work out by reading `src/` and running `dotnet test`.
