namespace Exercises.Tests;

/// <summary>
/// EXERCISE 3, PART 3 (stretch): test-driven development.
///
/// Exercise 3 mocked IUserRepository. Here you build the real thing, test first. There is
/// no ConcreteUserRepository class yet, and that is the point: the test is written before
/// the code it tests, so writing the test is how you decide what the class should do.
///
/// WHAT YOU DO HERE. Implement IUserRepository as a class called ConcreteUserRepository in
/// src/exercise3/ConcreteUserRepository.cs, storing its users in a private
/// List&lt;User&gt; field, and drive it out of the five stubs below. This is the one place in
/// these exercises where you add a file to src/. The interface is three methods:
///
///   bool Exists(string trimmedUsername)   true if that username is already stored
///   User Register(User user)              stores the user and returns the stored instance
///   User Login(User user)                 returns the matching stored user
///
/// TWO PARTS, IN THIS ORDER.
///   1. Write the test plan FIRST, before the class exists. The stretch table in
///      tasks/TEST_PLAN_TEMPLATE.md has a worked row to start you off; copy the template
///      to TEST_PLAN.md at the root of this repository. The interface does not answer
///      these, so your plan has to: who assigns the Id, the caller or the repository? What
///      does Login do on a wrong password, throw or return null? What does Register do if
///      the username is already stored? Does Exists trim, given the parameter is called
///      trimmedUsername?
///   2. Then implement it, in this loop:
///        a. Create ConcreteUserRepository with empty method bodies that
///           throw new NotImplementedException(). The project must COMPILE before you can
///           run a failing test, and a stub that throws fails loudly rather than quietly
///           returning a wrong answer.
///        b. Write ONE test below. Delete its [Ignore] line. Run it. Watch it fail. That
///           red is not a setback, it is the evidence that the test can detect the thing
///           it claims to test.
///        c. Write just enough of the implementation to turn it green, and no more.
///        d. Tidy up while both are fresh, then go back to (b) for the next test.
///
/// Add "using Exercises.Exercise3;" at the top of this file once your class exists.
///
/// THE FIXTURE. This class has no [SetUp] method yet, because there is nothing to build
/// until your class exists. Add one, the way the other three exercise classes do: a
/// private ConcreteUserRepository field, and a [SetUp] method that assigns a brand new one.
/// [SetUp] is NUnit's equivalent of JUnit's @BeforeEach: it runs before EVERY test. NUnit
/// reuses a single instance of this class for the whole fixture, so without it the users
/// registered by one test would still be in the list when the next test ran, and your
/// tests would start passing or failing depending on the order they happened to run in.
///
/// HOW THE TODOs WORK. Every unwritten test carries an [Ignore("TODO ...")] attribute, so
/// NUnit reports it as SKIPPED rather than failed and a fresh clone is green, even though
/// the class these tests need does not exist. To activate one, delete its [Ignore] line,
/// leaving [Test] alone. Then replace the comment and the Assert.Fail with arrange, act
/// and assert.
///
/// TO RUN, from the csharp folder at the root of this repository:
///   dotnet build                                                     compile
///   dotnet test                                                      the whole suite
///   dotnet test --filter "FullyQualifiedName~Stretch"                just this exercise
///   dotnet test --filter "FullyQualifiedName~Exists_UsernameNotStored"  one test by name
///
/// Done looks like all 5 stubs implemented and passing, and a ConcreteUserRepository in
/// src/ that nothing but your own tests has ever needed to run. The five are a floor, not
/// a ceiling: extra tests of your own are better still.
///
/// THE FULL BRIEF, with the guide's own step list, is in
/// tasks/04_stretch_tdd_repository.md.
/// </summary>
[TestFixture]
public class Exercise3_Stretch_ConcreteUserRepositoryTests
{
    [Test]
    [Ignore("TODO - stretch task. Create ConcreteUserRepository first, then write this.")]
    public void Exists_UsernameNotStored_ReturnsFalse()
    {
        // Should assert that Exists("bobby") returns FALSE on a brand new, empty
        // repository: nothing has been registered, so it knows no username at all.
        // Start here. It is the smallest test, and the smallest implementation that
        // passes it is a List<User> and a search that finds nothing.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - stretch task.")]
    public void Exists_UsernameAlreadyRegistered_ReturnsTrue()
    {
        // Should Register(new User(0, "bobby", "Codes123")) first, then assert
        // Exists("bobby") returns TRUE. The other side of the same boundary, and the pair
        // is what proves Exists actually looks at what is stored.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - stretch task.")]
    public void Register_NewUser_StoresUserAndReturnsIt()
    {
        // Should assert that Register(new User(0, "bobby", "Codes123")) returns a user
        // whose Username and Password match what went in, and that Exists("bobby") now
        // returns true, so it really was stored rather than just handed back.
        // Then pin the Id down: your plan decided whether the caller or the repository
        // assigns it. If the repository does, assert the returned Id is the one it gives
        // (1 for the first user, say). Asserting it is what makes that a decision rather
        // than an accident.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - stretch task.")]
    public void Login_MatchingUsernameAndPassword_ReturnsStoredUser()
    {
        // Should Register(new User(0, "bobby", "Codes123")), then assert that
        // Login(new User(0, "bobby", "Codes123")) returns the STORED user, including
        // whatever Id the repository gave it, not the object you passed in. User has value
        // equality, so Assert.That(actual, Is.EqualTo(expected)) compares field by field
        // and the Id difference will show up.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - stretch task.")]
    public void Login_WrongPassword_DoesNotReturnAUser()
    {
        // Should Register(new User(0, "bobby", "Codes123")), then assert what your design
        // does when Login is given "bobby" with the wrong password: either it throws, in
        // which case assert the type and message you chose with Assert.Throws, or it
        // returns null, in which case assert Is.Null. Either is defensible. Whichever you
        // choose, the test is what makes it a decision rather than an accident.
        // An unknown username is the same question again, and worth its own row.
        Assert.Fail("Not implemented yet");
    }
}
