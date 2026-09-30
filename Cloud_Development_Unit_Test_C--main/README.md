# Unit testing exercises, C# edition

This repository is the **C# edition of the SDL3 Module 5: Testing unit testing exercises**.
It is a translation of the Java original, and the three exercises (plus one stretch task)
are the same ones described in the module's exercise guide.

**The exercise guide is in this repository. Start at
[`tasks/README.md`](tasks/README.md).** You do not need the PDF, and you do not need any
other repository. Everything you need is here.

You do not need to know Java to do these. If you have just seen the Java version, the
[Java to C# differences](#java-to-c-differences) table near the bottom is the shortest route
across.

**The Java original these classes were translated from carried two real bugs, and both have
been corrected here.** The exercise code behaves correctly, so every test you write should
assert correct behaviour, and each correction carries a short comment in the source
explaining why the code is written the way it is. One error is still live in the worksheet
itself: the guide's own row 2 for exercise 2 uses a password that is too short for the rule
it is meant to illustrate, and
[`tasks/02_testing_exceptions.md`](tasks/02_testing_exceptions.md) reproduces that row with a
footnote explaining what actually happens.

---

## Prerequisites and setup

**The .NET SDK, version 9.0.** Check what you have with:

```bash
dotnet --list-sdks
```

`global.json` in this folder pins the build to the **9.0 SDK band**, with
`rollForward: latestFeature`. That is there so that everybody in the room gets identical
behaviour: a machine that also has .NET 10 installed will still build these exercises with
the 9.0 SDK, and a machine with only a later 9.0.x patch will roll forward to it rather than
refusing to build. If `dotnet --list-sdks` shows no 9.0 SDK at all, install one from
<https://dotnet.microsoft.com/download>.

**Opening the project.** Open `UnitTestExercises.sln` in any of:

- Visual Studio
- JetBrains Rider
- Visual Studio Code with the **C# Dev Kit** extension

**Or do not open it at all.** The command line on its own is enough for every exercise in
here. `dotnet build` and `dotnet test` in a terminal, plus any text editor, will get you all
the way through.

**The first build needs the internet**, because NuGet has to download the test packages
(NUnit, the NUnit test adapter, the test SDK and Moq) from nuget.org. After that first
restore they are cached on your machine and everything works offline.

---

## How to run the tests

From this `csharp` folder:

```bash
dotnet build      # compiles the exercise code and your tests
dotnet test       # runs your tests
```

### What a green run looks like on a fresh clone

`dotnet test` **passes** the moment you clone. This is real output from an untouched clone:

```
Passed!  - Failed:     0, Passed:     3, Skipped:    51, Total:    54, Duration: 51 ms - Exercises.Tests.dll (net9.0)
```

Nothing is broken. Every test you have not written yet carries an `[Ignore]` attribute, so
NUnit reports it as **skipped** rather than run. The 3 that pass are the worked examples,
one per exercise, already written for you to copy.

### The skip count is your progress bar

**Skipped: 51** is your to-do list. Every time you implement a test and delete its
`[Ignore]` line, that number drops by one and the passed count goes up by one. When you
have finished the whole repository you are looking for:

```
Passed!  - Failed:     0, Passed:    54, Skipped:     0, Total:    54
```

To run one exercise at a time:

```bash
dotnet test --filter "FullyQualifiedName~Exercise1"
```

---

## How to do one TODO

Every unwritten test looks like this:

```csharp
[Test]
[Ignore("TODO - implement me, then delete this [Ignore] line")]
public void Divide_ByZero_ThrowsArgumentException()
{
    // Should assert that Divide(x, 0) throws ArgumentException with the
    // message "Division by zero: divisor must not be 0".
    Assert.Fail("Not implemented yet");
}
```

Two steps:

1. **Delete the `[Ignore(...)]` line.** Leave the `[Test]` attribute alone. That is what
   turns the test from skipped into run, and it will now fail, which is correct.
2. **Replace the comment and the `Assert.Fail` with arrange, act and assert**, in the body
   of the method:

```csharp
[Test]
public void Divide_ByZero_ThrowsArgumentException()
{
    // Arrange: the inputs, and what you expect.
    double num1 = 10;
    double num2 = 0;

    // Act: call the one method under test.
    ArgumentException error = Assert.Throws<ArgumentException>(
        () => _calculator.Divide(num1, num2));

    // Assert: check what came back.
    Assert.That(error.Message, Is.EqualTo("Division by zero: divisor must not be 0"));
}
```

Run `dotnet test` again. Skipped goes down by one, passed goes up by one.

---

## What code you are actually working with

Everything under `src/` is the **code under test**. Read it, do not change it. The
one exception is the stretch task, where you add a new class of your own to
`src/exercise3/`.

### `src/exercise1/Calculator.cs`

`public class Calculator`, namespace `Exercises.Exercise1`.

| Method | What it does |
| --- | --- |
| `double Add(double num1, double num2)` | returns `num1 + num2` |
| `double Subtract(double num1, double num2)` | returns `num1 - num2` |
| `double Multiply(double num1, double num2)` | returns `num1 * num2` |
| `double Divide(double num1, double num2)` | returns `num1 / num2`, but **throws `ArgumentException("Division by zero: divisor must not be 0")`** when `num2` is 0 |

Everything is `double`, so the borderline values worth planning rows for are
`double.MaxValue`, `double.MinValue`, `double.Epsilon`, `double.PositiveInfinity` and
`double.NegativeInfinity`.

### `src/exercise2/UserService.cs`

`public class UserService`, namespace `Exercises.Exercise2`. It stores users in a private
`Dictionary<string, string>` keyed by username.

| Method | What it does |
| --- | --- |
| `string Register(string username, string password)` | validates, stores the user, returns the **trimmed** username |
| `string Login(string username, string password)` | looks the user up and returns the trimmed username on success |

Both throw. `Register` checks its rules **in this order**, and stops at the first one that
fails, so an input that breaks two rules only ever reports the first:

| Order | Rule broken | Exception and message |
| --- | --- | --- |
| 1 | username is null | `ArgumentException("Username must not be null")` |
| 2 | username trims to empty | `ArgumentException("Username must not be whitespace only")` |
| 3 | password is null | `ArgumentException("Password must not be null")` |
| 4 | password trims to empty | `ArgumentException("Password must not be whitespace only")` |
| 5 | username shorter than 4 | `ArgumentException("Username must contain at least 4 characters")` |
| 6 | username already registered | `ArgumentException("Username already exists")` |
| 7 | password shorter than 6 | `ArgumentException("Password must contain at least 6 characters")` |
| 8 | no uppercase letter | `ArgumentException("Password must contain at least 1 uppercase character")` |
| 9 | no lowercase letter | `ArgumentException("Password must contain at least 1 lowercase character")` |
| 10 | no digit | `ArgumentException("Password must contain at least 1 number character")` |

`Login` throws `ArgumentException("Username and password must not be null")`,
`ArgumentException("Username and password must not be empty")`,
`InvalidOperationException("Invalid username supplied")` when nobody is registered under
that name, and `ArgumentException("Invalid password supplied")` when the password does not
match. Note that one of those is **not** an `ArgumentException`.

### `src/exercise3/User.cs`

`public class User`, namespace `Exercises.Exercise3`. A plain record of a user.

- `User()` and `User(int id, string username, string password)`
- properties `int Id`, `string Username`, `string Password`
- it overrides `Equals` and `GetHashCode`, so `Assert.That(actual, Is.EqualTo(expected))`
  compares two users **field by field**. You never have to compare properties one at a time.

### `src/exercise3/IUserRepository.cs`

`public interface IUserRepository`. **This is the type you mock in exercise 3**, and the type
you implement for the stretch task.

| Method | What it means |
| --- | --- |
| `bool Exists(string trimmedUsername)` | true if that username is already stored |
| `User Register(User user)` | stores the user, returns the stored instance |
| `User Login(User user)` | returns the matching stored user |

### `src/exercise3/UserController.cs`

`public class UserController`. Same validation as `UserService`, but storage is delegated to
a repository handed in through the constructor.

| Member | What it does |
| --- | --- |
| `UserController(IUserRepository userRepository)` | the repository is **injected here**. This is what lets a test pass in a mock |
| `User Register(User user)` | validates, calls `Exists`, then `Register` on the repository |
| `User Login(User user)` | checks the details are present, then calls `Login` on the repository |

Two things to notice. `Register` throws
`ArgumentException("User must not be null")` before anything else, and it learns whether the
username is taken by **asking the repository**. And `Login` does **not** trim, unlike
`Register`, so a username of `"   "` passes straight through to the repository. That is
faithful to the Java original and it is worth a test of its own.

---

## The framework: NUnit

This repository uses **NUnit** (with the `NUnit3TestAdapter` so that `dotnet test` finds and
runs it), and **Moq** for mocking in exercise 3. NUnit was chosen because it is the closest
.NET analogue of the JUnit used in the Java edition of these exercises: the attributes line
up almost one for one with the JUnit annotations.

These are the exact things you will be typing:

| What you type | What it is for |
| --- | --- |
| `[TestFixture]` | marks a class as holding tests. NUnit will also find a public test class without it, but it is there on every class here for clarity |
| `[Test]` | marks one method as a test. This is the one you will type most |
| `[SetUp]` | marks a method that NUnit runs **before every test** in the fixture, to build fresh objects |
| `[TearDown]` | marks a method that NUnit runs **after every test**, to clean up. Nothing here needs one, but it is the partner of `[SetUp]` and you will meet it |
| `[Ignore("reason")]` | tells NUnit to skip this test and report it as skipped rather than failed. This is what makes a fresh clone green |
| `Assert.That(actual, Is.EqualTo(expected))` | the standard assertion: the thing you got, then what it should equal |
| `Assert.That(value, Is.True)` and `Is.False` | assert a boolean |
| `Assert.Throws<T>(() => code)` | asserts that running `code` throws a `T`, and **returns that exception** so you can then assert on its `Message` |
| `Assert.Fail("message")` | fails on the spot. It is what every unwritten stub does |
| `new Mock<IUserRepository>()` | creates a mock of an interface (Moq) |
| `mock.Object` | the `IUserRepository` itself, which is what you pass to the constructor |
| `mock.Setup(r => r.Exists("bobby")).Returns(false)` | tells the mock how to behave when that call is made |
| `mock.Verify(r => r.Register(user), Times.Once)` | asserts the call really happened, once. `Times.Never` asserts it did not |
| `It.IsAny<User>()` | "any argument of this type", for use inside `Setup` and `Verify` |

One NUnit habit worth knowing early: NUnit creates **one instance of a test class** and runs
every test in it on that same instance. That is why each test class here builds its objects
in a `[SetUp]` method rather than in a field initialiser. `[SetUp]` runs again before each
test, so no test can be affected by the one before it.

---

## Java to C# differences

You do not need any of this to finish the exercises. It is here because seeing the same
exercise in two languages is the fastest way to learn what is a real difference and what is
just spelling, and that understanding is most of what you take away from doing the C# and
Java editions side by side. Read down the table and notice how little is actually different:
the concepts are identical and mostly the names have changed.

| Java and JUnit / Mockito | C# and NUnit / Moq | Worth knowing |
| --- | --- | --- |
| `@Test` | `[Test]` | Same idea, different bracket. C# calls these attributes, Java calls them annotations |
| `@BeforeEach` | `[SetUp]` | Runs before every test. In C# you need it more than in Java: JUnit builds a new test-class instance per test, NUnit reuses one, so shared fields must be rebuilt in `[SetUp]` |
| `@AfterEach` | `[TearDown]` | Runs after every test |
| `@BeforeAll` / `@AfterAll` | `[OneTimeSetUp]` / `[OneTimeTearDown]` | Once for the whole class |
| `@Disabled("reason")` | `[Ignore("reason")]` | Skips the test and reports it skipped. This is what makes a fresh clone of this repository green |
| `assertEquals(expected, actual)` | `Assert.That(actual, Is.EqualTo(expected))` | **The order flips.** JUnit puts the expected value first, NUnit's constraint form puts the actual value first. Get this backwards and the test still compiles and still passes, but the failure message names the wrong side |
| `assertThrows(IllegalArgumentException.class, () -> code)` | `Assert.Throws<ArgumentException>(() => code)` | Both return the caught exception, so you can go on to assert on its message. C# passes the type as a generic parameter rather than a class literal, and writes lambdas with `=>` rather than `->` |
| `assertTrue(x)` / `assertFalse(x)` | `Assert.That(x, Is.True)` / `Is.False` | Same thing |
| `fail("message")` | `Assert.Fail("message")` | Same thing |
| `@Mock UserRepository repo;` | `var repo = new Mock<IUserRepository>();` | **.NET has no `@Mock`.** You construct the mock yourself |
| `@InjectMocks UserController controller;` | `var controller = new UserController(repo.Object);` | **.NET has no `@InjectMocks` either**, and does not need it. You pass the mock to the constructor yourself. That *is* the dependency injection, done by hand, and it is more honest about what the annotation was doing for you. Note `repo.Object`: in Moq the mock and the object it pretends to be are two different things |
| `when(repo.exists("bobby")).thenReturn(false);` | `repo.Setup(r => r.Exists("bobby")).Returns(false);` | Mockito's `when`/`thenReturn` is Moq's `Setup`/`Returns` |
| `verify(repo).register(user);` | `repo.Verify(r => r.Register(user), Times.Once);` | Mockito's `verify` is Moq's `Verify`. Moq wants the expected number of calls spelled out |
| `verify(repo, never()).register(any());` | `repo.Verify(r => r.Register(It.IsAny<User>()), Times.Never);` | `never()` is `Times.Never`, `any()` is `It.IsAny<T>()` |
| `IllegalArgumentException` | `ArgumentException` | Both mean "a caller passed an argument value this method cannot accept", and both are unchecked, so a test observes the same behaviour |
| `RuntimeException` | `InvalidOperationException` | Where the Java throws a bare `RuntimeException`, the C# throws .NET's nearest equivalent: "the object is not in a state where this can work" |
| checked exceptions, `throws` on the signature | there are none | C# has no checked exceptions at all, so no method here declares what it throws. The only way to find out is to read the body, which is exactly what exercise 2 asks you to do |
| `UserRepository` (interface) | `IUserRepository` | .NET convention prefixes interface names with `I`. It also leaves the plain name `UserRepository` free for a real implementation, which is why the stretch task's class can keep the name `ConcreteUserRepository` without it feeling odd |
| `register()`, `login()`, `exists()` | `Register()`, `Login()`, `Exists()` | Java methods are camelCase, C# methods are PascalCase. Fields keep camelCase, and private fields here are written `_repository` with a leading underscore |
| `getUsername()` / `setUsername(...)` | the `Username` property | C# auto-properties replace Java getter and setter pairs. You read and write `user.Username` directly |
| `equals()` and `hashCode()` | `Equals()` and `GetHashCode()` | Same contract, different names. `User` overrides both, which is what makes `Is.EqualTo` compare two users field by field |
| `List<User> users = new ArrayList<>();` | `List<User> users = new List<User>();` | In C#, `List<T>` **is** the resizable array. There is no separate interface-and-implementation pair to choose between for the stretch task |
| `double` division by zero | `double` division by zero | **No difference.** `1.0 / 0.0` is `Infinity` in both, `0.0 / 0.0` is `NaN` in both, and neither throws. It is **integer** division by zero that throws, in both languages. That is exactly why `Calculator.Divide` guards the argument itself and throws `ArgumentException`: without that guard, dividing doubles by zero would quietly return `Infinity` rather than reporting the bad argument |
| `mvn test` | `dotnet test` | Same job: restore dependencies, compile, run the tests |

---

## What is in this repository

```
csharp/
  UnitTestExercises.sln          the solution: src and tests
  global.json                    pins the SDK to the 9.0 band
  README.md                      this file
  tasks/                         THE EXERCISE GUIDE. Start here.
    README.md                    contents page, order of work, how to run
    01_testing_existing_code.md  exercise 1, Calculator
    02_testing_exceptions.md     exercise 2, UserService
    03_mocking.md                exercise 3, UserController with the repository mocked
    04_stretch_tdd_repository.md the stretch task, TDD a ConcreteUserRepository
    TEST_PLAN_TEMPLATE.md        the test plan tables to fill in first
  src/                           the code under test. Do not change it.
    Exercises.csproj             the exercise project
    exercise1/Calculator.cs
    exercise2/UserService.cs
    exercise3/User.cs
    exercise3/IUserRepository.cs
    exercise3/UserController.cs
  tests/                         YOUR WORK GOES HERE
    Exercises.Tests.csproj       the test project
    exercise1/Exercise1_CalculatorTests.cs
    exercise2/Exercise2_UserServiceTests.cs
    exercise3/Exercise3_UserControllerTests.cs
    exercise3/Exercise3_Stretch_ConcreteUserRepositoryTests.cs
```

The exercise folders are lowercase `exercise1`, `exercise2` and `exercise3` on purpose: C#
would normally PascalCase them, but these exercises ship in Java, Python and C# editions and
the three repositories are laid out identically so the class can follow any of them. The
namespaces inside keep the usual C# casing, `Exercises.Exercise1` and so on.

The solutions are not in this repository. Your trainer has them.

---

## The exercises, in one paragraph each

Each one is written out in full in [`tasks/`](tasks/README.md), with the guide's own test
plan tables and worked examples. Every exercise starts with **writing a test plan**, before
any test code: copy `tasks/TEST_PLAN_TEMPLATE.md` to `TEST_PLAN.md` and fill it in. Writing
the plan first is the exercise. The tests are just the plan turned into C#.

**Exercise 1, testing existing code.** Plan and then write at least three tests per
`Calculator` method, covering the borderline values as well as ordinary ones. You edit
`tests/exercise1/Exercise1_CalculatorTests.cs`.

**Exercise 2, testing exceptions.** Plan and then write a test for every exception
`UserService.Register` and `UserService.Login` can throw. Assert the **message** as well as
the type: several different rules all throw `ArgumentException`, and only the message tells
them apart. Watch the order the rules are checked in. You edit
`tests/exercise2/Exercise2_UserServiceTests.cs`.

**Exercise 3, mocking.** `UserController` hands storage to an `IUserRepository`. In a unit
test you replace that repository with a **mock**: an object you create, tell how to behave,
and then interrogate afterwards about how it was used. You edit
`tests/exercise3/Exercise3_UserControllerTests.cs`.

**Stretch task, test driven.** Plan and then build a real `ConcreteUserRepository`
implementing `IUserRepository`, storing its users in a `List<User>`. Write the test first,
watch it fail, write the smallest implementation that makes it pass, then repeat. Skeletons
are in `tests/exercise3/Exercise3_Stretch_ConcreteUserRepositoryTests.cs`, and this is
the one place where you add a file to `src/`.

---

## Two notes on this translation

**Nullable reference types are switched off**, in both projects. Several exercises are
specifically about passing `null` in and checking the exception you get, and nullable
warnings would only get in the way.

**The target framework is `net9.0`**, and nothing in these exercises uses a language feature
newer than C# 10. If you can read C# at all, you can read all of this.
