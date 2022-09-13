using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker;

internal class Course
{
    public string Name { get; init; }
    public List<Assignment> Assignments { get; private set; }

    public Course(string name, List<Assignment> assignments)
    {
        Name = name;
        Assignments = assignments;

        ApplyAssignments();
    }

    private void ApplyAssignments()
    {
        var assignments = Assignments.ToArray();
        for (var i = 0; i < Assignments.Count; i++)
        {
            assignments[i].SetCourse(this);
        }
        Assignments = assignments.ToList();
    }

    public void AddAssignments(params Assignment[] assignments)
    {
        Assignments.AddRange(assignments);
    }

    public override string ToString()
    {
        return Name;
    }
}
