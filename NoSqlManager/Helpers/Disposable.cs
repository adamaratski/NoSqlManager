using System;

namespace NoSqlManager.Helpers
{
    public abstract class Disposable : IDisposable
    {
        #region IDisposable

        bool _disposed;

        ~Disposable()
        {
            if (!this._disposed)
            {
                this._disposed = true;
                Dispose(false);
            }
        }

        public void Dispose()
        {
            if (!this._disposed)
            {
                this._disposed = true;
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // managed objects
            }
            // unmanaged objects and resources
        }

        #endregion
    }
}
