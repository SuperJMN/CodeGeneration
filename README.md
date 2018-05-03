# CodeGeneration

Code Generation sample (courtesy of Javier JBM)

This is a simple code generation example using C#. It has been created originally by **Javier (JBM)** in Java and ported to C# by me (@SuperJMN). So, all credits go to him :)

The application translates high level expressions to **Intermediate Code**.

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

- **Tests**. this projects has tests, so run them and see what's under the hood :)
- **Program.cs**. In addition, you will find code in the Main method that generates code from an AST. If you debug it from Visual Studio it will close almost immediately, so it's better to run it "without debugging", that keeps the console window until you press a key :)

**PLEASE NOTICE** that the code is generated from an **AST**, not from the actual source code (a string). The generation of an AST from source code belongs to another (previous) stage of the compiling process.

Feel free to copy or investigate / ask!
