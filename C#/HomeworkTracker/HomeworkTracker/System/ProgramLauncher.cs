using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker;

internal class ProgramLauncher
{
    private List<Course> _courses;
    private int CourseCount => _courses.Count();

    public ProgramLauncher()
    {
        Console.SetWindowSize(100, 40);

        _courses = DatabaseHandler.Import();
    }

    public void Run()
    {
        var inputField = new InputField<int?>
        {
            Prompt = "Selection: ",
            Requirement = (int? choice) =>
            {
                return (choice > 0 && choice < CourseCount + 1) || (choice >= 'a' && choice <= 'c');
            },
            Description = "Select a course."
        };

        while (true)
        {
            Display.DisplayTitle();
            Display.CourseSelection(_courses);

            var result = InputHandler.GetInput(ref inputField);
            if (result)
            {
                var choice = inputField.Result!.Value;
                if (choice < CourseCount + 1)
                {
                    UpdateCourse(_courses[choice - 1]);
                }
                else
                {
                    switch (choice)
                    {
                        case 'a' or 'A':
                            AddCourse();
                            break;
                        case 'b' or 'B':
                            RemoveCourse();
                            break;
                        case 'c' or 'C':
                            SearchQuery();
                            break;
                    }
                }
                DatabaseHandler.Export(_courses);
            }
        }
    }

    private void UpdateCourse(in Course course)
    {
        int assignmentCount = course.Assignments.Count;
        string info = "Assignment Options:\n";

        for (var i = 0; i < course.Assignments.Count; i++)
        {
            info += $"{i + 1}) {course.Assignments[i].Name}\n";
        }

        info += "A) Add Assignment\nQ) Return\n";

        var inputField = new InputField<int?>
        {
            Prompt = "Selection: ",
            Requirement = (int? choice) =>
            {
                return (choice > 0 && choice < assignmentCount + 1) || choice == 'q' || choice == 'Q' || choice == 'a' || choice == 'A';
            },
            Description = "Select an assignment to toggle completion status, if not expired.",
            Info = info
        };

        while (true)
        {
            Display.ViewCourse(course);

            var result = InputHandler.GetInput(ref inputField);
            if (result)
            {
                var choice = inputField.Result!.Value;
                if (choice < assignmentCount + 1)
                {
                    if (!course.Assignments[choice - 1].IsExpired)
                        course.Assignments[choice - 1].ToggleStatus();
                }
                else if (choice == 'a' || choice == 'A')
                {
                    AddAssignment(course);
                }
                else
                {
                    break;
                }
                DatabaseHandler.Export(_courses);
            }
        }
    }

    private void AddAssignment(in Course course)
    {
        var inputFields = new InputField<string>[2]
        {
            new InputField<string>
            {
                Prompt = "Assignment Name: ",
                Description = "Name must be at least 3 characters long.",
                Requirement = (string text) => text.Length >= 3 || (text.Length == 1 && text.ToUpper()[0] == 'Q'),
                Info = "Q) Return\n"
            },
            new InputField<string>
            {
                Prompt = "Due Date: ",
                Description = "Date must follow the form: \'MM/DD/YYYY HH:MM:SS TT\' i.e. \'1/1/2022 11:59:00 PM\'.",
                Requirement = (string text) => DateTime.TryParse(text, out var _) || (text.Length == 1 && text.ToUpper()[0] == 'Q'),
                Info = "Q) Return\n"
            }
        };

        var index = 0;
        var titleParts = new string[2];
        while (index < inputFields.Length)
        {
            Display.DisplayTitle();

            var result = InputHandler.GetInput(ref inputFields[index]);
            if (result)
            {
                if (char.TryParse(inputFields[index].Result, out var c) && (c == 'q' || c == 'Q'))
                {
                    if (index - 1 >= 0)
                    {
                        --index;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                titleParts[index] = inputFields[index].Result!;
                index++;
            }
        }

        if (index > 0)
            course.AddAssignments(new Assignment(titleParts[0], DateTime.Parse(titleParts[1]), false));
    }

    private void AddCourse()
    {
        var inputFields = new InputField<string>[2]
        {
            new InputField<string>
            {
                Prompt = "Course Abbreviation: ",
                Description = "Abbreviation must be 4 characters long.",
                Requirement = (string text) => text.Length == 4 || (text.Length == 1 && text.ToUpper()[0] == 'Q'),
                Info = "Q) Return\n"
            },
            new InputField<string>
            {
                Prompt = "Course Number: ",
                Description = "Number must be 3 digits long.",
                Requirement = (string text) => text.Length == 3 || (text.Length == 1 && text.ToUpper()[0] == 'Q'),
                Info = "Q) Return\n"
            }
        };

        var index = 0;
        var titleParts = new string[2];
        while (index < inputFields.Length)
        {
            Display.DisplayTitle();

            var result = InputHandler.GetInput(ref inputFields[index]);
            if (result)
            {
                if (char.TryParse(inputFields[index].Result, out var c) && (c == 'q' || c == 'Q'))
                {
                    if (index - 1 >= 0)
                    {
                        --index;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                titleParts[index] = inputFields[index].Result!;
                index++;
            }
        }

        if (index > 0)
            _courses.Add(new($"{titleParts[0].ToUpper()} {titleParts[1]}", new()));
    }

    private void RemoveCourse()
    {
        var inputField = new InputField<string>
        {
            Prompt = "Course Title: ",
            Description = "Input must match a valid course title.",
            Requirement = (string text) =>
            {
                if (text.Length == 1 && text.ToUpper()[0] == 'Q')
                    return true;

                var course = _courses.Find(x => x.Name == text);
                if (course != null)
                {
                    _courses.Remove(course);
                    return true;
                }
                return false;
            },
            Info = "Q) Return\n"
        };

        while (true)
        {
            Display.DisplayTitle();

            var result = InputHandler.GetInput(ref inputField);
            if (result)
                break;
        }
    }

    private void SearchQuery()
    {
        var assignments = GetAllAssignments();
        var queryResults = assignments;

        var inputField = new InputField<int?>
        {
            Prompt = "Selection: ",
            Description = $"Input must be within the specified range.",
            Requirement = (int? number) => (number > 0 && number <= 3) || number == 'q' || number == 'Q',
            Info = "Query Methods:\n1) Sort By Course\n2) Sort By Due Date (Ascending)\n3) Sort By Due Date (Descending)\nQ) Return\n"
        };

        var functions = new Func<List<Assignment>>[]
        {
            () => GetAllAssignments(),
            () => assignments.OrderBy(x => x.DueDate).ToList(),
            () => assignments.OrderBy(x => x.DueDate).Reverse().ToList()
        };

        while (true)
        {
            Display.DisplayTitle();
            Display.ViewQueryResults(queryResults);

            var result = InputHandler.GetInput(ref inputField);
            if (result)
            {
                var number = inputField.Result!.Value;
                if (number == 'q' || number == 'Q')
                {
                    break;
                }

                queryResults = functions[number - 1]();
            }
        }
    }

    private List<Assignment> GetAllAssignments()
    {
        var assignments = new List<Assignment>();
        foreach (var course in _courses)
        {
            foreach (var assignment in course.Assignments)
            {
                assignments.Add(assignment);
            }
        }
        return assignments;
    }
}
