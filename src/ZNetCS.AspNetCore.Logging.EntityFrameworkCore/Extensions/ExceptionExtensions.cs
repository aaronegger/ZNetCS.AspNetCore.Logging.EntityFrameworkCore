namespace ZNetCS.AspNetCore.Logging.EntityFrameworkCore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Stores extensions for exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Retrieve the stacktrace as a minfied string.
        /// E.g. <c>Examplefile.cs:12 at Otherfile.cs:252 at ... </c>.
        /// </summary>
        /// <param name="exception">The exception which will be used to retrieve the stack.</param>
        /// <returns>The minified stacktrace.</returns>
        public static string? MinifyStacktrace(this Exception exception)
        {
            IEnumerable<StackFrame> stackFrames = new StackTrace(exception, true)
                .GetFrames()
                .OfType<StackFrame>()
                .Where(frame => frame.GetFileLineNumber() > 0);

            if (stackFrames.Any())
            {
                return string.Join(" at ", stackFrames.Select(frame => $"{Path.GetFileName(frame.GetFileName())}:{frame.GetFileLineNumber()}"));
            }

            return null;
        }
    }
}
