namespace ConsoleExamples.Examples
{
    using System;
    using Encog.Examples;

    public interface IExampleInfo
    {
        String Command { get; set; }
        String Title { get; set; }
        String Description { get; set; }
        Type ExampleType { get; set; }
        int CompareTo(ExampleInfo other);
        IExample CreateInstance();
    }
}