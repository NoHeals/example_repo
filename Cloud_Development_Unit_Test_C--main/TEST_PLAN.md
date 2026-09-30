# Test plan template

Copy this file, rename it `TEST_PLAN.md`, and fill it in **before** you write any test code.
Writing the plan first is the exercise: the tests are just the plan turned into C#.

Fill the "Actual output" column in after you have run the test. Where it differs from
"Expected output", you have either found a bug in your test or a bug in the code. Both are
useful. Say which you think it is.

---

## Exercise 1: Calculator

Aim for at least **three cases per method**, and make sure they include:

- borderline values (what is the largest pair you can add? the smallest?)
- at least one ordinary, everyday combination

The first row is the worked example supplied in the exercise guide.

| ID | Method | Description | Inputs | Expected output | Actual output |
| --- | --- | --- | --- | --- | --- |
| 1 | `Add(double num1, double num2)` | Adding two small numbers | num1=10, num2=30 | 40 | |
| 2 | `Add(double num1, double num2)` | Adding two negative numbers | num1=-10, num2=-30 | -40 | |
| 3 | `Add(double num1, double num2)` | Adding the largest positive values overflows | num1=double.MaxValue, num2=double.MaxValue | `double.PositiveInfinity` | |
| 4 | `Add(double num1, double num2)` | Adding the smallest positive values | num1=double.Epsilon, num2=double.Epsilon | `2 * double.Epsilon` | |
| 5 | `Subtract(double num1, double num2)` | Subtracting a larger number from a smaller one | num1=10, num2=30 | -20 | |
| 6 | `Subtract(double num1, double num2)` | Subtracting a number from itself | num1=42, num2=42 | 0 | |
| 7 | `Subtract(double num1, double num2)` | Subtracting the largest positive value from the most negative value underflows | num1=double.MinValue, num2=double.MaxValue | `double.NegativeInfinity` | |
| 8 | `Multiply(double num1, double num2)` | Multiplying two ordinary numbers | num1=6, num2=7 | 42 | |
| 9 | `Multiply(double num1, double num2)` | Multiplying a number by zero | num1=42, num2=0 | 0 | |
| 10 | `Multiply(double num1, double num2)` | Multiplying the largest value by two overflows | num1=double.MaxValue, num2=2 | `double.PositiveInfinity` | |
| 11 | `Divide(double num1, double num2)` | Dividing two ordinary numbers | num1=10, num2=4 | 2.5 | |
| 12 | `Divide(double num1, double num2)` | Dividing by zero throws with the documented message | num1=10, num2=0 | `ArgumentException("Division by zero: divisor must not be 0")` | |
| 13 | `Divide(double num1, double num2)` | Dividing zero by a nonzero number | num1=0, num2=10 | 0 | |

---

## Exercise 2: UserService

Write a case for **every exception that could be thrown**, by either method.

Two rows are supplied as worked examples in the exercise guide. Note that .NET's
`ArgumentException` is the equivalent of Java's `IllegalArgumentException`, so that is the
type you will assert on.

| ID | Method | Description | Inputs | Expected output | Actual output |
| --- | --- | --- | --- | --- | --- |
| 1 | `Login(string username, string password)` | Register a valid user, then log in successfully with that user | Register: username="bobby", password="Codes123". Login: username="bobby", password="Codes123" | `"bobby"` | |
| 2 | `Register(string username, string password)` | Register a user with an invalid password, missing a number | username="bobby", password="Codesss" | `ArgumentException("Password must contain at least 1 number character")` | |
| 3 | `Register(string username, string password)` | Should assert that Register("  bobby  ", "Codes123") returns "bobby". Register | `ArgumentException("Not implemented yet")` | | 
| 4 | `Register(string username, string password)` | Register with a whitespace-only username | username="   ", password="Codes123" | `ArgumentException("Username must be whitespace only")` | |
| 5 | `Register(string username, string password)` | Register with a null password | username="bobby", password=null | `ArgumentException("Password must not be null")` | |
| 6 | `Register(string username, string password)` | Register with a whitespace-only password | username="bobby", password="      " | `ArgumentException("Password must be whitespace only")` | |
| 7 | `Register(string username, string password)` | Register with a username shorter than four characters | username="bob", password="Codes123" | `ArgumentException("Username must contain at least 4 characters")` | |
| 8 | `Register(string username, string password)` | Register a username that is already registered | First: username="bobby", password="Codes123". Second: username="bobby", password="Codes456" | `ArgumentException("Username already exists")` | |
| 9 | `Register(string username, string password)` | Register with a password shorter than six characters | username="bobby", password="Cod1" | `ArgumentException("Password must contain at least 6 characters")` | |
| 10 | `Register(string username, string password)` | Register with a password that has no uppercase letter | username="bobby", password="codes123" | `ArgumentException("Password must contain at least 1 uppercase character")` | |
| 11 | `Register(string username, string password)` | Register with a password that has no lowercase letter | username="bobby", password="CODES123" | `ArgumentException("Password must contain at least 1 lowercase character")` | |
| 12 | `Register(string username, string password)` | Register with zero as the only digit in the password | username="bobby", password="Codes0" | `"bobby"` | |

A hint on row 2: the guide itself writes this password as `"Codes"`. Try both and look
carefully at the message you actually get. The **order** in which the code checks its rules
is part of its behaviour.

---

## Exercise 3: UserController, with the repository mocked

Reuse the exercise 2 plan and adapt it. As the guide says:

- add a **Class** column, because more than one class is now involved
- `Register` gains a new exception, raised when the repository reports the username exists
- `Login` loses most of its exceptions: checking whether the user is real is now the
  repository's job, not the controller's

Add a column for what the **mock** is set up to do, because that is now part of the inputs.

| ID | Class | Method | Description | Inputs | Mock set-up | Expected output | Actual output |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | `UserController` | `Register(User user)` | Register a valid user | User(0, "bobby", "Codes123") | `Exists("bobby")` returns false; `Register(user)` returns User(1, "bobby", "Codes123") | User(1, "bobby", "Codes123") | |
| 2 | `UserController` | `Register(User user)` | Username already taken | User(0, "bobby", "Codes123") | `Exists("bobby")` returns **true** | `ArgumentException("Username already exists")` | |
| 3 | | | | | | | |
| 4 | | | | | | | |
| 5 | | | | | | | |
| 6 | | | | | | | |
| 7 | | | | | | | |
| 8 | | | | | | | |

---

## Exercise 3, part 3: ConcreteUserRepository (stretch, test driven)

Plan these **before** the class exists. Then write one test, watch it fail, write just
enough code to pass it, and repeat.

| ID | Method | Description | Inputs | Expected output | Actual output |
| --- | --- | --- | --- | --- | --- |
| 1 | `Exists(string trimmedUsername)` | Empty repository knows nobody | "bobby" | false | |
| 2 | | | | | |
| 3 | | | | | |
| 4 | | | | | |
| 5 | | | | | |
| 6 | | | | | |
