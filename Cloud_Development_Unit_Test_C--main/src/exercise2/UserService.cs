using System.Text.RegularExpressions;

namespace Exercises.Exercise2;

/// <summary>
/// An in-memory user service. This is the "existing code" for exercise 2: your job is
/// to write tests that pin down every exception it can throw.
/// </summary>
/// <remarks>
/// MAPPING NOTE: everywhere the Java original throws IllegalArgumentException this class
/// throws ArgumentException. Both are the language's standard "you gave me a bad argument"
/// signal, so the behaviour a test observes is the same shape in both languages.
/// Where the Java throws a bare RuntimeException (an unchecked "something went wrong that
/// is not about one argument") we throw InvalidOperationException, .NET's closest equivalent.
///
/// This is a translation of the Java original. The original carried two genuine bugs; both
/// have been corrected here and the reasoning is commented alongside each one.
/// </remarks>
public class UserService
{
    // Key = username, value = password. Registration writes username -> password.
    private readonly Dictionary<string, string> _users = new Dictionary<string, string>();

    // One character class per rule. Each is used as a "contains" test, so it needs no
    // anchors and no surrounding .* padding.
    private const string HasUppercase = "[A-Z]";
    private const string HasLowercase = "[a-z]";
    private const string HasNumber = "[0-9]";

    /// <summary>
    /// Validates the details and stores the user. Returns the trimmed username.
    /// </summary>
    public string Register(string username, string password)
    {
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

        // username must be unique
        if (_users.ContainsKey(trimmedUsername)) throw new ArgumentException("Username already exists");

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

        // add user to the map
        _users[trimmedUsername] = trimmedPassword;

        return trimmedUsername;
    }

    /// <summary>
    /// Looks the user up and returns the username on success.
    /// </summary>
    public string Login(string username, string password)
    {
        // username and password must not be null
        if (username == null || password == null) throw new ArgumentException("Username and password must not be null");
        string trimmedUsername = username.Trim();
        string trimmedPassword = password.Trim();

        // username and password must not be empty
        if (trimmedUsername.Length == 0 || trimmedPassword.Length == 0) throw new ArgumentException("Username and password must not be empty");

        // look the user up by their username, then check the password they supplied
        // against the one we stored. Keying the map by password would only ever match
        // a user whose name happened to equal their own password.
        _users.TryGetValue(trimmedUsername, out string savedPassword);
        if (savedPassword == null) throw new InvalidOperationException("Invalid username supplied");

        if (!trimmedPassword.Equals(savedPassword)) throw new ArgumentException("Invalid password supplied");

        // return the trimmed name, the same form Register returns and stores
        return trimmedUsername;
    }
}
