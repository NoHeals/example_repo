# Exercise 2: Testing exceptions

This exercise uses the **`UserService`** class defined in
`src/exercise2/UserService.cs`, namespace `Exercises.Exercise2`.

The guide says to clone the exercise repository and import the project into Eclipse. In
this repository the code is already here. Open `UnitTestExercises.sln` in Visual Studio,
VS Code with the C# Dev Kit, or Rider, or work in a terminal.

| The guide says | Here |
| --- | --- |
| Repository: the `exercise2` package | `src/exercise2/UserService.cs` |

**One mapping to know.** Java's `IllegalArgumentException` becomes .NET's
`ArgumentException`, and Java's bare `RuntimeException` becomes `InvalidOperationException`.
Wherever the guide names a Java exception, assert on the C# one. The **messages** are
identical in both languages.

---

## Part 1: Create a test plan

You are required to create a test plan which consists of test cases for the `UserService`
class's two methods, `Register` and `Login`. Use the following template for creating your
test cases. The full table, with room to fill in, is in
[`TEST_PLAN_TEMPLATE.md`](TEST_PLAN_TEMPLATE.md).

| ID | Method | Desc. | Inputs | Exp. Output | Act. Output |
| --- | --- | --- | --- | --- | --- |
| 1 | `login(String username, String password)` | Register a valid user, login successfully with said valid user. | Login username="bobby" password="Codes123" Register username="bobby" | `"bobby"` | |
| 2 | `register(String username, String password)` | Register a user with an invalid password due to missing number. | Register username="bobby" password="Codes" | `IllegalArgumentException("Password must contain at least 1 number character")` | |

> **Footnote on row 2.** Reproduced above exactly as the guide writes it, and as written it
> does not hold. `"Codes"` is **five** characters. `Register` checks its rules in order, and
> the "at least 6 characters" rule is checked **before** the number rule, so the code never
> reaches the number check. What you actually get is:
>
> ```
> ArgumentException("Password must contain at least 6 characters")
> ```
>
> The guide meant a password long enough to reach the number rule but with no digit in it.
> Use `"Codesss"`, seven characters, and you will get the message the row expects. Try both
> and look carefully at what comes back.
>
> This is a real lesson, not a typo to skip past: **the order in which validation rules are
> checked is part of the behaviour**, and an input that breaks two rules only ever reports
> the first one. A test plan written without reading the code will get this wrong, which is
> exactly what happened to the guide.

In C#, row 1 is `Login(string username, string password)` and row 2 is
`Register(string username, string password)`, and the exception type to assert is
`ArgumentException`.

Two example test cases have been created for you. It is expected that you produce **a test
case for every possible exception that could be thrown**.

To find them all, read `UserService.cs` from the top of each method downwards and write a
row for every `throw`. There are more than you would guess, and two of them are
`InvalidOperationException`, not `ArgumentException`.

---

## Part 2: Implement your test plan

The `UserService` class has already been created. Use your test plan to guide the
development of tests for the methods of this class.

**The file you edit:** `tests/exercise2/Exercise2_UserServiceTests.cs`

The assertion you want is:

```csharp
ArgumentException error = Assert.Throws<ArgumentException>(
    () => _service.Register("bob", "Codes123"));

Assert.That(error.Message, Is.EqualTo("Username must contain at least 4 characters"));
```

Three things are going on there:

- The call is wrapped in a **lambda** so the assertion can run it and catch what it throws.
- `Assert.Throws<T>` checks the exception **type**, and fails if nothing is thrown at all.
- Checking the **message** as well is what stops a test passing for the wrong reason.
  Several different rules all throw `ArgumentException`, and only the message tells them
  apart. A test that checks the type alone would pass even if the wrong rule fired.

One worked example is already written and passing: it is row 2 of the table above, with the
password corrected to `"Codesss"`. The other nineteen tests are stubs, each marked `[Test]`
with an `[Ignore("TODO - ...")]` under it. Delete each `[Ignore(...)]` line as you implement
the test it belongs to.

**To run them:**

```bash
dotnet test --filter "FullyQualifiedName~Exercise2"
```

**Done looks like:** all 20 tests in `Exercise2_UserServiceTests` passing, none skipped.

---

## Advice

- Watch the **order** the rules are checked in. If an input breaks two rules, you only ever
  see the first. Choose inputs that break exactly the one rule you are testing.
- Test the boundary, not just the middle. A username of 3 characters is rejected and one of
  4 is accepted: both rows are worth having.
- `Register` **trims** before it validates and before it stores, and returns the trimmed
  name. `Login` trims too. That is worth a test of its own.
- A digit is any of 0 to 9. Zero is a digit. `"Codes0"` is a valid password.
- Nothing in the three character rules forbids symbols, so a password with a symbol in it
  must be accepted as long as it satisfies all three rules.
- A fresh `UserService` is created for each test, so one test's registered users cannot leak
  into another. That is deliberate: tests that share state fail in confusing orders.
