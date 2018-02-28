# CodeGeneration

Code Generation sample (courtesy of Javier JBM)

This is a simple code generation example using C#. It has been created originally by **Javier (JBM)** in Java and ported to C# by me (@SuperJMN). So, all credits go to him :)

The application translates high level expressions to **3 Address Code (3AC)**.

# What's code generation?

Code generation is the process of converting a language into another, usually simpler and closer to the machine that will execute it.

# Example

This high level expression:

`a = b + c * d`

is turned into:

```
T1 = c * d
T2 = b + T1
a = T2
```

# Running the application

**Program.cs** prints 2 examples:

```
private static void Main()
{
  PrintSample1();
  PrintSeparator();
  PrintSample2();
}
```

Each sample will print the generated code for the one expression. 

- Sample1 is the expression above. 
- Sample2 is a little more complex expression.

Feel free to copy or investigate!
