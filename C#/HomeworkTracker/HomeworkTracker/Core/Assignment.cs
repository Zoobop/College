namespace HomeworkTracker;

internal class Assignment
{
    public Course Course { get; private set; }
    public string Name { get; init; }
    public DateTime DueDate { get; init; }
    public bool IsCompleted { get; private set; }
    public bool IsExpired => DateTime.Now >= DueDate;

    public Assignment(string name, DateTime dueDate, bool isCompleted)
    {
        Course = default;
        Name = name;
        DueDate = dueDate;
        IsCompleted = isCompleted;
    }

    public void SetCourse(Course course) => Course = course;

    public void ToggleStatus() => IsCompleted = !IsCompleted;

    public override string ToString()
    {
        string text = $"{Name}  --  Due: {DueDate}";

        if (IsExpired && !IsCompleted)
        {
            text += " [Expired]";
        }
        else if (IsCompleted)
        {
            text += " [Completed]";
        }
        else
        {
            var time = DueDate - DateTime.Now;
            var days = (time.Days > 0) ? $"{time.Days}D " : "";
            var hours = (time.Hours > 0) ? $"{time.Hours}HR " : "";
            var minutes = (time.Minutes > 0) ? $"{time.Minutes}MIN " : "";
            var seconds = (time.Seconds > 0) ? $"{time.Seconds}SEC" : "";
            text += $" (Time Remaining: {days}{hours}{minutes}{seconds}) [In Progress]";
        }
        return text;
    }
}
