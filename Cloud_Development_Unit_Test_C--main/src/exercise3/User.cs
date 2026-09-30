namespace Exercises.Exercise3;

/// <summary>
/// A plain user record. Idiomatic C#: auto-properties instead of Java getters and setters.
/// </summary>
public class User
{
    /// <summary>Parameterless constructor, matching the Java no-arg constructor.</summary>
    public User()
    {
    }

    public User(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }

    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    // Value equality, matching the Java equals()/hashCode() pair.
    // Tests rely on this: Assert.That(actual, Is.EqualTo(expected)) compares field by field.
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not User other) return false;
        return Id == other.Id
            && Username == other.Username
            && Password == other.Password;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Username, Password);
    }

    public override string ToString()
    {
        return $"User [id={Id}, username={Username}, password={Password}]";
    }
}
