using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker;

internal struct InputField<T>
{
    public T? Result { get; private set; }
    public Func<T, bool> Requirement { get; init; }
    public string Prompt { get; init; }
    public string Description { get; init; }
    public string? Info { get; init; }

    public InputField(string prompt, string reqDescription, Func<T, bool> requirement, string? info = default)
    {
        Result = default;
        Requirement = requirement;
        Prompt = prompt;
        Description = reqDescription;
        Info = info;
    }

    public void SetResult(T result) => Result = result;
}

internal class InputHandler
{
    public static bool GetInput(ref InputField<string> inputField)
    {
        if (!string.IsNullOrEmpty(inputField.Info))
            Console.WriteLine(inputField.Info);

        Console.WriteLine($"Requirement(s):\n{inputField.Description}\n");
        Console.Write(inputField.Prompt);
        var input = Console.ReadLine();

        if (input != null)
        {
            if (inputField.Requirement(input))
            {
                inputField.SetResult(input);
                return true;
            }
        }
        return false;
    }

    public static bool GetInput(ref InputField<int?> inputField)
    {
        if (!string.IsNullOrEmpty(inputField.Info))
            Console.WriteLine(inputField.Info);

        Console.WriteLine($"Requirement(s):\n{inputField.Description}\n");
        Console.Write(inputField.Prompt);
        var input = Console.ReadLine();

        if (int.TryParse(input, out var choice))
        {
            if (inputField.Requirement(choice))
            {
                inputField.SetResult(choice);
                return true;
            }
        }
        else if (char.TryParse(input, out var letter))
        {
            if (inputField.Requirement(letter))
            {
                inputField.SetResult(letter);
                return true;
            }
        }
        return false;
    }

    public static char? GetChar(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (char.TryParse(input, out var c))
        {
            return c;
        }
        return null;
    }

    public static int? GetNumber(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (int.TryParse(input, out var n))
        {
            return n;
        }
        return null;
    }

    public static bool? GetBool(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (bool.TryParse(input, out var b))
        {
            return b;
        }
        return null;
    }
}
