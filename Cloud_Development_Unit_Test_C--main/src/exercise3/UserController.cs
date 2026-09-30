using System.Text.RegularExpressions;

namespace Exercises.Exercise3;

/// <summary>
/// Validates users and then delegates storage to an <see cref="IUserRepository"/>.
/// This is the class under test in exercise 3. The repository is a collaborator, so in a
/// unit test it gets replaced with a mock.
/// </summary>
/// <remarks>
/// MAPPING NOTE: as in <c>UserService</c>, Java's IllegalArgumentException becomes
/// ArgumentException, the standard .NET "bad argument value" exception.
///
/// The password rules here are the same three rules used in exercise 2, corrected in the
/// same way.
/// </remarks>
public class UserController
{
    private readonly IUserRepository _repository;

    // One character class per rule. Each is used as a "contains" test, so it needs no
    // anchors and no surrounding .* padding.
    private const string HasUppercase = "[A-Z]";
    private const string HasLowercase = "[a-z]";
    private const string HasNumber = "[0-9]";

    /// <summary>
    /// The repository is injected through the constructor. That is what makes this class
    /// testable: a test can pass in a mock instead of a real database.
    /// </summary>
    public UserController(IUserRepository userRepository)
    {
        _repository = userRepository;
    }

    /// <summary>Validates the user, then asks the repository to store it.</summary>
    public User Register(User user)
    {
        // user must not be null
        if (user == null) throw new ArgumentException("User must not be null");

        string username = user.Username;
        string password = user.Password;

        // username must not be null or empty
        if (username == null) throw new ArgumentException("Username must not be null");
        string trimmedUsername = username.Trim();
        if (trimmedUsername.Length == 0) throw new ArgumentException("Username must not be whitespace only");

        // password must not be null or empty
        if (password == null) throw new ArgumentException("Password must not be null");
        string trimmedPassword = password.Trim();
        if (trimmedPassword.Length == 0) throw new ArgumentException("Password must not be whitespace only");

        // username must be at least 4 characters
        if (trimmedUsername.Length < 4) throw new ArgumentException("Username must contain at least 4 characters");

        // username must be unique. This is the call the test mocks.
        if (_repository.Exists(trimmedUsername)) throw new ArgumentException("Username already exists");

        // password must be at least 6 characters
        if (trimmedPassword.Length < 6) throw new ArgumentException("Password must contain at least 6 characters");

        // password must contain at least 1 uppercase character. A "contains" check, not a
        // whole-string match: a whole-string match fails on any character the class does not
        // list, so a single unexpected symbol would report the wrong rule.
        if (!Regex.IsMatch(trimmedPassword, HasUppercase)) throw new ArgumentException("Password must contain at least 1 uppercase character");

        // password must contain at least 1 lowercase character
        if (!Regex.IsMatch(trimmedPassword, HasLowercase)) throw new ArgumentException("Password must contain at least 1 lowercase character");

        // password must contain at least 1 number. The class is [0-9], not [1-9]: zero is
        // a number too.
        if (!Regex.IsMatch(trimmedPassword, HasNumber)) throw new ArgumentException("Password must contain at least 1 number character");

        // add user to the database
        return _repository.Register(user);
    }

    /// <summary>
    /// Checks the details are present, then asks the repository to log the user in.
    /// Unlike exercise 2, "is this user real" is the repository's job here.
    /// </summary>
    public User Login(User user)
    {
        // user must not be null
        if (user == null) throw new ArgumentException("User must not be null");

        string username = user.Username;
        string password = user.Password;

        // username and password must not be null
        if (username == null || password == null) throw new ArgumentException("Username and password must not be null");

        // username and password must not be empty.
        // NOTE: the Java original does NOT trim here, so a username of " " gets through.
        // Ported as-is so the behaviour matches.
        if (username.Length == 0 || password.Length == 0) throw new ArgumentException("Username and password must not be empty");

        return _repository.Login(user);
    }
}
