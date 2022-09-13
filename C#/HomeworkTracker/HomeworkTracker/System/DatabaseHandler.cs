using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker;

internal static class DatabaseHandler
{
    private static readonly string _databasePath = "C:\\Users\\Brandon\\OneDrive\\Desktop\\Code\\C#\\HomeworkTracker\\HomeworkTracker\\Database\\database.ht";

    public static List<Course> Import()
    {
        return Parse(File.ReadAllLines(_databasePath));
    }

    public static void Export(List<Course> courses)
    {
        var courseFormat = new List<string>();

        for (var i = 0; i < courses.Count; i++)
        {
            courseFormat.Add($"[{courses[i].Name}]");
            foreach (var assignment in courses[i].Assignments)
            {
                courseFormat.Add($"{assignment.Name} ({assignment.DueDate},{assignment.IsCompleted})");
            }

            if (i < courses.Count - 1)
            {
                courseFormat.Add(string.Empty);
            }
        }

        File.WriteAllLines(_databasePath, courseFormat);
    }

    private static List<Course> Parse(string[] context)
    {
        List<Course> courses = new();

        string courseName = string.Empty;
        List<Assignment> assignments = new();

        foreach (var data in context)
        {
            if (string.IsNullOrEmpty(data))
            {
                courses.Add(new(courseName, assignments));

                courseName = string.Empty;
                assignments = new();
            }
            else if (data.StartsWith('['))
            {
                courseName = data.Trim('[', ']');
            }
            else
            {
                assignments.Add(ToAssignment(data));
            }
        }

        courses.Add(new(courseName, assignments));
        return courses;
    }

    private static Assignment ToAssignment(string data)
    {
        string name = data[..(data.IndexOf('(') - 1)];

        var parsedData = data.Substring(data.IndexOf('(')).Trim('(',')').Split(',');
        var dueDate = DateTime.Parse(parsedData[0]);
        var isCompleted = bool.Parse(parsedData[1].Trim());
        return new(name, dueDate, isCompleted);
    }
}
