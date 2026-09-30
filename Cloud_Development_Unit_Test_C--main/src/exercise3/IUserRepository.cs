namespace Exercises.Exercise3;

/// <summary>
/// The storage boundary the controller talks to.
/// </summary>
/// <remarks>
/// NAMING NOTE: the Java original calls this interface UserRepository. The .NET convention
/// is to prefix interface names with I, so it becomes IUserRepository. That leaves the plain
/// name UserRepository free for a real implementation, which is exactly what the stretch task
/// (ConcreteUserRepository) is about.
///
/// This is the type you MOCK in exercise 3: the controller only ever sees the interface, so a
/// test can hand it a fake and control what storage appears to do.
/// </remarks>
public interface IUserRepository
{
    /// <summary>True if a user with that username is already stored.</summary>
    bool Exists(string trimmedUsername);

    /// <summary>Stores the user and returns the stored instance.</summary>
    User Register(User user);

    /// <summary>Returns the matching stored user.</summary>
    User Login(User user);
}
