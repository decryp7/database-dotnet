using System;
using System.Reactive.Disposables;

namespace SimpleDatabase
{
    public static class DisposableExtension
    {
        public static T DisposeWith<T>(this T disposable, CompositeDisposable compositeDisposable) where T : IDisposable
        {
            compositeDisposable.Add(disposable);
            return disposable;
        }
    }
}