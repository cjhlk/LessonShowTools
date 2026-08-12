using System;
using LessonShowTools.Shared.IPC.Abstractions.Services;

namespace LessonShowTools.Services;

public class FooService : IFooService
{
    public void DoSomething()
    {
        Console.WriteLine("Foobar");
    }
}