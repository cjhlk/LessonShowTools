// See https://aka.ms/new-console-template for more information

using XmlDocMarkdown.Core;

namespace LessonShowTools.DocsGenerator;

static class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine("LessonShowTools Document Generator");
        return XmlDocMarkdownApp.Run(args);
    }
}