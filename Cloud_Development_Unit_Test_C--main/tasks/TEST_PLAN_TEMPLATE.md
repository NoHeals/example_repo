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
| 2 | | | | | |
| 3 | | | | | |
| 4 | | | | | |
| 5 | | | | | |
| 6 | | | | | |
| 7 | | | | | |
| 8 | | | | | |
| 9 | | | | | |
| 10 | | | | | |
| 11 | | | | | |
| 12 | | | | | |

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
| 3 | | | | | |
| 4 | | | | | |
| 5 | | | | | |
| 6 | | | | | |
| 7 | | | | | |
| 8 | | | | | |
| 9 | | | | | |
| 10 | | | | | |
| 11 | | | | | |
| 12 | | | | | |

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
