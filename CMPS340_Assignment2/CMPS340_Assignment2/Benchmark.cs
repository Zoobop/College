using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Benchmark : IDisposable
{
    private Stopwatch _stopwatch;
    private string _handle;

    public Benchmark(string timerHandle)
    {
        _handle = timerHandle;

        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    ~Benchmark()
    {
        Dispose();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _stopwatch.Stop();

        Console.WriteLine($"{_handle}: {_stopwatch.ElapsedMilliseconds} ms");
    }
}