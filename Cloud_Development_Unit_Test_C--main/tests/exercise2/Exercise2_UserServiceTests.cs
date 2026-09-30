using Exercises.Exercise2;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 2: testing exceptions.
///
/// UserService, in src/exercise2/UserService.cs, validates a registration and a
/// login and rejects bad input by throwing. This exercise is about proving each rejection
/// happens for the RIGHT reason: most of these throws are the same ArgumentException type,
/// and only the message tells them apart.
///
/// WHAT YOU DO HERE. Read UserService.cs from the top of each method downwards, write a
/// case for every throw you find, and fill in the nineteen stubs below. Assert the type AND
/// the message. Two of the throws are InvalidOperationException, not ArgumentException.
///
/// TWO PARTS, IN THIS ORDER.
///   1. Write the test plan FIRST. Copy tasks/TEST_PLAN_TEMPLATE.md to TEST_PLAN.md at the
///      root of this repository and fill in the exercise 2 table, one row per exception.
///      Writing the plan is the exercise.
///   2. Then implement it. Each row becomes one test down here.
///
/// The ORDER the rules are checked in is part of the behaviour. An input that breaks two
/// rules only ever reports the first, so choose inputs that break exactly the one rule you
/// are testing. Register checks, in order: username null, username whitespace only,
/// password null, password whitespace only, username shorter than 4, username already
/// registered, password shorter than 6, no uppercase, no lowercase, no digit.
///
/// HOW THE TODOs WORK. Every unwritten test carries an [Ignore("TODO ...")] attribute, so
/// NUnit reports it as SKIPPED rather than failed and a fresh clone is green. To activate
/// one, delete its [Ignore] line, leaving [Test] alone. It will now fail, which is correct.
/// Then replace the comment and the Assert.Fail with arrange, act and assert.
///
/// TO RUN, from the csharp folder at the root of this repository:
///   dotnet build                                                        compile
///   dotnet test                                                         the whole suite
///   dotnet test --filter "FullyQualifiedName~Exercise2"                 just this exercise
///   dotnet test --filter "FullyQualifiedName~Register_NullUsername"     one test by name
///
/// Done looks like all 20 tests in this class passing and none skipped.
///
/// THE FULL BRIEF, with the guide's own test plan table and the footnote on row 2, is in
/// tasks/02_testing_exceptions.md. The two bugs that were corrected in this class are
/// commented in the source alongside each correction, and both are worth reading before
/// you start.
/// </summary>
[TestFixture]
public class Exercise2_UserServiceTests
{
    private UserService _service;

    // [SetUp] runs before every test, like JUnit's @BeforeEach. A fresh service each time,
    // so one test's registered users cannot leak into another. NUnit reuses a single
    // instance of this class for the whole fixture, which is why the field has to be
    // rebuilt here rather than initialised once where it is declared.
    [SetUp]
    public void SetUp()
    {
        _service = new UserService();
    }

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. This is row 2 of the test plan in the exercise guide: registering
    // with a password that has no number in it.
    // ---------------------------------------------------------------------------------
    [Test]
    public void Register_PasswordWithNoNumber_ThrowsArgumentException()
    {
        // Arrange
        string username = "bobby";
        // The guide's own example uses "Codes", but that is only 5 characters, so the
        // "at least 6 characters" rule fires first and you never reach the number rule.
        // That is an error in the worksheet, not in the code: see tasks/02, the footnote
        // under the test plan table. We use a 7 character password with no digit, which
        // is what the guide meant to test.
        string password = "Codesss";

        // Act: wrap the call in a lambda so the assertion can catch what it throws.
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));
        // In JUnit this is
        // assertThrows(IllegalArgumentException.class, () -> service.register(...)).
        // Both forms return the caught exception so you can assert on its message; C#
        // passes the type as a generic parameter and writes the lambda with => not ->.

        // Assert: check we got the RIGHT exception, not just any exception.
        Assert.That(error.Message, Is.EqualTo("Password must contain at least 1 number character"));
    }

    // ---------------------------------------------------------------------------------
    // Register: the happy path and the validation rules, in the order the code checks them.
    // ---------------------------------------------------------------------------------

    [Test]
    public void Register_ValidDetails_ReturnsTrimmedUsername()
    {
        string username = "  bobby  ";
        string password = "Codes123";
        string expected = "bobby";

        string actual = _service.Register(username, password);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        string username = null;
        string password = "Codes123";
        string expected = "Username must not be null";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        string username = "   ";
        string password = "Codes123";
        string expected = "Username must not be whitespace only";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = null;
        string expected = "Password must not be null";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "      ";
        string expected = "Password must be whitespace only";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        string username = "bob";
        string password = "Codes123";
        string expected = "Username must contain at least 4 characters";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_UsernameAlreadyRegistered_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "Codes123";
        string secondPassword = "Codes456";
        string expected = "Username already exists";
        _service.Register(username, password);

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, secondPassword));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "Cod1";
        string expected = "Password must contain at least 6 characters";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "codes123";
        string expected = "Password must contain at least 1 uppercase character";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "CODES123";
        string expected = "Password must contain at least 1 lowercase character";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Register_PasswordWhoseOnlyDigitIsZero_IsAccepted()
    {
        string username = "bobby";
        string password = "Codes0";
        string expected = "bobby";

        string actual = _service.Register(username, password);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Register_PasswordContainingASymbol_IsAccepted()
    {
        string username = "bobby";
        string password = "Cod|es1";
        string expected = "bobby";

        string actual = _service.Register(username, password);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // ---------------------------------------------------------------------------------
    // Login
    // ---------------------------------------------------------------------------------

    [Test]
    public void Login_RegisteredUserWithCorrectPassword_ReturnsUsername()
    {
        string username = "bobby";
        string password = "Codes123";
        string expected = "bobby";
        _service.Register(username, password);

        string actual = _service.Login(username, password);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Login_RegisteredUserWithWrongPassword_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "Codes123";
        string wrongPassword = "Wrong123";
        string expected = "Invalid password supplied";
        _service.Register(username, password);

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login(username, wrongPassword));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Login_NullUsername_ThrowsArgumentException()
    {
        string username = null;
        string password = "Codes123";
        string expected = "Username and password must not be null";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Login_NullPassword_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = null;
        string expected = "Username and password must not be null";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Login_EmptyUsername_ThrowsArgumentException()
    {
        string username = "";
        string password = "Codes123";
        string expected = "Username and password must not be empty";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Login_EmptyPassword_ThrowsArgumentException()
    {
        string username = "bobby";
        string password = "";
        string expected = "Username and password must not be empty";

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }

    [Test]
    public void Login_UnknownUsername_ThrowsInvalidOperationException()
    {
        string username = "nobody";
        string password = "Codes123";
        string expected = "Invalid username supplied";

        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => _service.Login(username, password));

        Assert.That(error.Message, Is.EqualTo(expected));
    }
}
