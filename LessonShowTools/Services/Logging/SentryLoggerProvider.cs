using Microsoft.Extensions.Logging;

namespace LessonShowTools.Services.Logging;

public class SentryLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new SentryEventLogger(categoryName);
    }
}