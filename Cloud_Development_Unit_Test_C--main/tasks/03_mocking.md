# Exercise 3: Mocking in a unit test

This exercise relies on the **`User`** and **`UserController`** classes and the
**`IUserRepository`** interface, all in `src/exercise3/`, namespace
`Exercises.Exercise3`.

| The guide says | Here |
| --- | --- |
| Repository: the `exercise3` package | `src/exercise3/` |
| `UserRepository` interface | `IUserRepository.cs`. The .NET convention prefixes interface names with `I`, which also leaves the plain name free for the stretch task's implementation |

---

## Part 1: Update the test plan from exercise 2

The test plan from exercise 2 can be reused for this example. Modify the test plan to
accommodate the changes to the `Login` and `Register` methods present in the
`UserController` class.

- As we are now dealing with multiple classes, it is also recommended to add a **Class**
  column to the test table.
- There is a **new exception** that could be thrown in the `Register` method: the username
  already exists, which the controller now learns by asking the repository.
- Some exceptions have been **removed** from the `Login` method, as it is expected that the
  repository implementation would handle those cases in this example, i.e., invalid
  usernames or passwords.

The exercise 3 table in [`TEST_PLAN_TEMPLATE.md`](TEST_PLAN_TEMPLATE.md) already has the
`Class` column, and one more besides: a **Mock set-up** column. What you tell the mock to do
is now part of the inputs, so it belongs in the plan.

---

## Part 2: Implement the tests

Implement your unit test plan, as done with the previous examples.

**The file you edit:** `tests/exercise3/Exercise3_UserControllerTests.cs`

Be careful when writing your tests for the `Login` and `Register` methods. It is expected
that you mock interactions with the repository.

### Mockito and Moq

The guide is written for Java and tells you to use **Mockito**, mocking the repository and
injecting it into the controller with the **`@Mock`** and **`@InjectMocks`** annotations.

This repository is C#, and uses **Moq**, the most widely used mocking library in .NET.
There are no `@Mock` and `@InjectMocks` annotations in .NET, and you do not need them: you
create the mock yourself and pass it to the constructor. That **is** the injection, done by
hand, and it is clearer about what dependency injection actually is. If you are reading the
original worksheet alongside this one, that is the only difference that matters.

| Mockito, as the guide describes | Moq, as you will write it |
| --- | --- |
| `@Mock UserRepository repository;` | `var repository = new Mock<IUserRepository>();` |
| `@InjectMocks UserController controller;` | `var controller = new UserController(repository.Object);` |
| `when(repository.exists("bobby")).thenReturn(false);` | `repository.Setup(r => r.Exists("bobby")).Returns(false);` |
| `verify(repository).register(user);` | `repository.Verify(r => r.Register(user), Times.Once);` |
| `verify(repository, never()).register(any());` | `repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never);` |

Put together:

```csharp
var repository = new Mock<IUserRepository>();                      // create the mock
repository.Setup(r => r.Exists("bobby")).Returns(false);           // tell it how to behave
var controller = new UserController(repository.Object);            // inject it

controller.Register(new User(0, "bobby", "Codes123"));

repository.Verify(r => r.Register(It.IsAny<User>()), Times.Once);  // check it was used
```

The test class already creates a fresh mock and controller for every test, in its `[SetUp]`
method, so you can use the `_repository` and `_controller` fields directly. `[SetUp]` is
NUnit's equivalent of JUnit's `@BeforeEach`: it runs before each test, and it is what stops
one test's mock set-up leaking into the next.

The **repository** methods being mocked are: `IUserRepository.Exists()`,
`IUserRepository.Register()` and `IUserRepository.Login()`.

**To run them:**

```bash
dotnet test --filter "FullyQualifiedName~Exercise3_UserControllerTests"
```

**Done looks like:** all 16 tests in `Exercise3_UserControllerTests` passing, none skipped.

---

## Advice

- A mock is not just a stand-in. Half its value is `Verify`: asserting the repository was
  called, with what, and how many times. For every test where validation fails, also assert
  the repository was **never** touched. That is what proves the controller rejected the
  input rather than leaning on storage to do it.
- `Register` trims the username before asking `Exists`. Set the mock up on the trimmed form
  and verify the untrimmed form never reaches it.
- `UserController.Login` does **not** trim, unlike `Register`. A username of `"   "` passes
  straight through to the repository. That is faithful to the Java original, and it is the
  boundary the guide is describing when it says the repository handles invalid usernames.
  Write the test that pins it down.
- `User` has value equality, so `Assert.That(actualUser, Is.EqualTo(expectedUser))` compares
  field by field. You do not need to compare properties one at a time.
