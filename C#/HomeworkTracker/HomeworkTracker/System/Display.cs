using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker;

internal class Display
{
    public static void DisplayTitle()
    {
        Console.Clear();
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("|         Homework Tracker        |");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine();
    }

    public static void CourseSelection(List<Course> courses)
    {
        for (var i = 0; i < courses.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {courses[i]}");
        }
        Console.WriteLine();
        Console.WriteLine("a) Add Course");
        Console.WriteLine("b) Remove Course");
        Console.WriteLine("c) Assignment Search Query");
        Console.WriteLine();
    }

    public static void ViewCourse(in Course course)
    {
        DisplayTitle();
        Console.WriteLine(course.Name + ":");
        Console.WriteLine(new string('-', course.Name.Length + 1));

        if (course.Assignments.Count == 0)
        {
            Console.WriteLine("No assignments available at this time.");
        }

        foreach (var assignment in course.Assignments)
        {
            Console.WriteLine(assignment);
        }
        Console.WriteLine();
    }

    public static void ViewQueryResults(List<Assignment> assignments)
    {
        if (assignments.Count == 0)
        {
            Console.WriteLine("There are currently no assignments.");
            return;
        }

        Console.WriteLine("============================================================================");
        foreach (var assignment in assignments)
        {
            string progress = string.Empty;
            if (assignment.IsCompleted)
            {
                progress = "[Completed]";
            }
            else if (assignment.IsExpired)
            {
                progress = "[Expired]";
            }
            else
            {
                progress = "[In Progress]";
            }

            Console.WriteLine($"| {assignment.Course!.Name, -8} | {assignment.Name,-20} | {assignment.DueDate, -22} | {progress, -13} |");
        }
        Console.WriteLine("============================================================================");
        Console.WriteLine();
    }
}
