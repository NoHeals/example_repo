using Exercises.Exercise3;
using Moq;
using NUnit.Framework;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 3: mocking in a unit test.
///
/// UserController, in src/exercise3/UserController.cs, depends on
/// IUserRepository. We do not want a real database in a unit test, so we hand the
/// controller a MOCK repository: a stand-in we fully control, and can interrogate
/// afterwards about how it was used.
///
/// WHAT YOU DO HERE. Fill in the fifteen stubs below: the same validation rules as
/// exercise 2, plus one new exception the controller learns by ASKING the repository
/// ("Username already exists"), and a much shorter Login, because deciding whether a user
/// is real is now the repository's job. For every test where validation fails, also verify
/// the repository was never touched. That is what proves the controller rejected the input
/// rather than leaning on storage to do it.
///
/// TWO PARTS, IN THIS ORDER.
///   1. Update the test plan FIRST. Reuse your exercise 2 plan: the exercise 3 table in
///      tasks/TEST_PLAN_TEMPLATE.md adds a Class column and a Mock set-up column, because
///      what you tell the mock to do is now part of the inputs. Copy the template to
///      TEST_PLAN.md at the root of this repository if you have not already.
///   2. Then implement it. Each row becomes one test down here.
///
/// The Java version of this exercise uses Mockito with @Mock and @InjectMocks.
/// In .NET there are no such annotations: you create the mock yourself and pass it to the
/// constructor. That is the "injection" part, done by hand, and it is clearer for it.
///
/// Moq cheat sheet:
///   new Mock&lt;IUserRepository&gt;()                               create the mock
///   mock.Object                                                the IUserRepository to pass in
///   mock.Setup(r =&gt; r.Exists("bobby")).Returns(true)           make a call return something
///   mock.Verify(r =&gt; r.Register(user), Times.Once)             assert a call happened
///   mock.Verify(r =&gt; r.Register(It.IsAny&lt;User&gt;()), Times.Never) assert it did not
///
/// HOW THE TODOs WORK. Every unwritten test carries an [Ignore("TODO ...")] attribute, so
/// NUnit reports it as SKIPPED rather than failed and a fresh clone is green. To activate
/// one, delete its [Ignore] line, leaving [Test] alone. It will now fail, which is correct.
/// Then replace the comment and the Assert.Fail with arrange, act and assert.
///
/// TO RUN, from the csharp folder at the root of this repository:
///   dotnet build                                                          compile
///   dotnet test                                                           the whole suite
///   dotnet test --filter "FullyQualifiedName~Exercise3_UserControllerTests"  this exercise
///   dotnet test --filter "FullyQualifiedName~Register_NullUser"           one test by name
///
/// Done looks like all 16 tests in this class passing and none skipped.
///
/// THE FULL BRIEF, with the Mockito to Moq table and the advice on trimming, is in
/// tasks/03_mocking.md.
/// </summary>
[TestFixture]
public class Exercise3_UserControllerTests
{
    private Mock<IUserRepository> _repository;
    private UserController _controller;

    // A fresh mock and controller for each test. [SetUp] is NUnit's @BeforeEach: it runs
    // before every test, and it is what stops one test's mock set-up, or the record of
    // which calls it received, leaking into the next. NUnit reuses one instance of this
    // class for the whole fixture, so rebuilding both fields here is not optional.
    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IUserRepository>();

        // Constructor injection: this is where the mock replaces the real repository.
        // Java's @InjectMocks does this for you; here you do it yourself, and the line
        // above is the whole of the "dependency injection" the annotation was hiding.
        _controller = new UserController(_repository.Object);
    }

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. Registering a valid user: the controller should ask the repository
    // whether the username exists, and then store the user.
    // ---------------------------------------------------------------------------------
    [Test]
    public void Register_ValidUser_StoresUserViaRepository()
    {
        // Arrange
        User input = new User(0, "bobby", "Codes123");
        User saved = new User(1, "bobby", "Codes123");

        // Tell the mock how to behave: this username is not taken, and saving returns
        // the stored user complete with its new id.
        _repository.Setup(r => r.Exists("bobby")).Returns(false);
        _repository.Setup(r => r.Register(input)).Returns(saved);
        // In Mockito these two lines are
        // when(repository.exists("bobby")).thenReturn(false); and thenReturn(saved).

        // Act
        User actual = _controller.Register(input);

        // Assert: the right value came back ...
        Assert.That(actual, Is.EqualTo(saved));
        // ... and the controller really did talk to the repository, exactly once.
        _repository.Verify(r => r.Exists("bobby"), Times.Once);
        _repository.Verify(r => r.Register(input), Times.Once);
        // Mockito writes those as verify(repository).exists("bobby"). Moq wants the
        // expected number of calls spelled out, hence Times.Once.
    }

    // ---------------------------------------------------------------------------------
    // Register
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullUser_ThrowsArgumentException()
    {
        // Should assert that Register(null) throws ArgumentException with the message
        // "User must not be null", and that the repository was never touched:
        // _repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never).
        // This check comes before everything else in the method.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, null, "Codes123")) throws
        // ArgumentException with the message "Username must not be null", and verify the
        // repository was never asked to store anything.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "   ", "Codes123")) throws
        // ArgumentException with the message "Username must not be whitespace only", not
        // the "at least 4 characters" rule, which is checked later.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bobby", null)) throws ArgumentException
        // with the message "Password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bobby", "      ")) throws
        // ArgumentException with the message "Password must not be whitespace only".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bob", "Codes123")) throws
        // ArgumentException with the message "Username must contain at least 4
        // characters", and verify Exists was NEVER called: the length rule is checked
        // before the controller asks the repository anything.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_RepositorySaysUsernameExists_ThrowsArgumentException()
    {
        // The new exception the guide mentions. Should set the mock up so
        // Exists("bobby") returns TRUE, then assert Register(new User(0, "bobby",
        // "Codes123")) throws ArgumentException with the message "Username already
        // exists", and verify Register was never called on the repository. The controller
        // learns this ONLY by asking the mock, so the mock set-up is the input here.
        // Register trims before it asks, so a username of "  bobby  " must reach the mock
        // as "bobby": worth a second row that verifies Exists("  bobby  ") Times.Never.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bobby", "Cod1")) throws
        // ArgumentException with the message "Password must contain at least 6
        // characters". This rule is checked AFTER the Exists call, so set the mock up to
        // return false for Exists first, or the default false will do it for you.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bobby", "codes123")) throws
        // ArgumentException with the message "Password must contain at least 1 uppercase
        // character".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bobby", "CODES123")) throws
        // ArgumentException with the message "Password must contain at least 1 lowercase
        // character".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoNumber_ThrowsArgumentException()
    {
        // Should assert that Register(new User(0, "bobby", "Codesss")) throws
        // ArgumentException with the message "Password must contain at least 1 number
        // character", and that Register was never called on the repository. Seven
        // characters, so it clears the length rule and reaches this one.
        // Two boundary passwords are worth rows of their own here. "Codes0", whose only
        // digit is zero, must be ACCEPTED, because zero is a number. "Cod|es1", which
        // contains a symbol, must be ACCEPTED too, because none of the three character
        // rules forbids a symbol. You prove both by verifying the user reached the
        // repository.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Login. Note the guide's point: the controller no longer decides whether the user is
    // real, the repository does. So there is far less to check here.
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_ValidUser_ReturnsUserFromRepository()
    {
        // Should set the mock up so Login(input) returns a stored user, for example
        // new User(1, "bobby", "Codes123"), then assert the controller hands that exact
        // user straight back, and verify Login was called once. User has value equality,
        // so Is.EqualTo compares field by field.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_NullUser_ThrowsArgumentException()
    {
        // Should assert that Login(null) throws ArgumentException with the message
        // "User must not be null", and verify the repository was never asked to log
        // anyone in.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_NullUsernameOrPassword_ThrowsArgumentException()
    {
        // Should assert that Login(new User(0, null, "Codes123")) throws ArgumentException
        // with the message "Username and password must not be null". One check covers
        // both fields, so a null password is worth the matching row.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_EmptyUsernameOrPassword_ThrowsArgumentException()
    {
        // Should assert that Login(new User(0, "", "Codes123")) throws ArgumentException
        // with the message "Username and password must not be empty", and verify the
        // repository was never asked to log anyone in.
        // Then notice the boundary the guide points at: Login does NOT trim, unlike
        // Register, so a username of "   " is not empty and passes straight through to the
        // repository. Write that row too, and verify Login really was called with it.
        Assert.Fail("Not implemented yet");
    }
}
