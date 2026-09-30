# Exercise 3, Part 3: Test-driven development (stretch task)

If you complete exercise 3, create a test plan for the methods of the `IUserRepository`
interface.

Once a suitable plan is created, create your tests, and implement the interface as a class.
Call it **`ConcreteUserRepository`**. The **Concrete** in the name indicates that it is a
class and not an interface or abstract class.

Store the instances of `User` in a `List<User>` instance variable on the concrete repository
class.

| The guide says | Here |
| --- | --- |
| `UserRepository` interface | `src/exercise3/IUserRepository.cs` |
| Create `ConcreteUserRepository` | a new file, `src/exercise3/ConcreteUserRepository.cs` |
| Create the test class `UserRepositoryTest` | it already exists as a skeleton: `tests/exercise3/Exercise3_Stretch_ConcreteUserRepositoryTests.cs` |

This is the one place in these exercises where you **do** add to `src/`. Everywhere else,
the code under test is given and you only write tests.

The interface you are implementing:

```csharp
public interface IUserRepository
{
    bool Exists(string trimmedUsername);   // true if that username is already stored
    User Register(User user);              // stores the user and returns the stored instance
    User Login(User user);                 // returns the matching stored user
}
```

---

## Part 1: The test plan

Plan these **before** the class exists. That is the whole point: you are deciding what the
class should do by writing down what you will be able to observe.

The stretch table in [`TEST_PLAN_TEMPLATE.md`](TEST_PLAN_TEMPLATE.md) has one worked row to
start you off. Questions your plan has to answer, because the interface does not:

- Who assigns the `Id`? The caller, or the repository? Write the row that pins your answer
  down.
- What does `Login` do when the password does not match: throw, or return `null`? Either is
  defensible. Whichever you choose, the test is what makes it a **decision** rather than an
  accident.
- What does `Register` do if the username is already stored?
- Does `Exists` trim? The parameter is called `trimmedUsername`, which is a hint about whose
  job that is.

---

## Part 2: Advice, the order to work in

This is the guide's own step list, translated to this repository:

1. After creating the plan, create the concrete repository class and implement the
   `IUserRepository` interface: a new `ConcreteUserRepository.cs` in
   `src/exercise3/`.
2. Add the **empty method stubs**. In C#, `throw new NotImplementedException();` in each
   body. The project must compile before you can run a failing test, and a stub that throws
   fails loudly rather than quietly returning a wrong answer.
3. Create the test class. It already exists here:
   `tests/exercise3/Exercise3_Stretch_ConcreteUserRepositoryTests.cs`. Add
   `using Exercises.Exercise3;` at the top once your class exists.
4. Start creating the `Register` tests. Write **one** test. Delete its `[Ignore(...)]` line.
   Run it. Watch it fail. That red is not a setback, it is the evidence that the test can detect the thing
   it claims to test.
5. Create the implementation of `ConcreteUserRepository.Register()` as you write the test:
   just enough code to turn it green, and no more.
6. Repeat steps 4 and 5 for the `Login` and `Exists` methods.

Tidy up between each round, while both the test and the implementation are fresh and green.

**To run them:**

```bash
dotnet test --filter "FullyQualifiedName~Stretch"
```

**Done looks like:** all 5 stubs in `Exercise3_Stretch_ConcreteUserRepositoryTests`
implemented and passing, and a `ConcreteUserRepository` in `src/` that nothing but your own
tests has ever needed to run. If you added tests of your own beyond the five stubs, better
still: the five are a floor, not a ceiling.

Once the whole repository is finished, `dotnet test` should report `Skipped: 0` and
everything passing.
