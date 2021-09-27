using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NoSqlManager.Helpers
{
    /// <summary>
    /// A utility class to temporarily change the default cursor.
    /// </summary>
    /// <remarks>
    /// Example of usage:
    /// <code>
    /// using(new OverrideCursor(Cursors.AppStarting))
    /// {
    ///   ... operation here
    /// }
    /// </code>
    /// </remarks>
    public class OverrideCursor : Disposable
    {
        [ThreadStatic]
        private static readonly Stack<Cursor> Cursors = new Stack<Cursor>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideCursor"/> class.
        /// </summary>
        public OverrideCursor(Cursor cursor)
        {
            // Save current cursor
            Cursors.Push(Mouse.OverrideCursor);

            // Override with wait cursor
            Mouse.OverrideCursor = cursor;
        }

        private static void RestoreCursor()
        {
            // Restore previous cursor
            Mouse.OverrideCursor = Cursors.Pop();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            RestoreCursor();
        }
    }
}
