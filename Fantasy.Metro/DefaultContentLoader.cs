using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Fantasy.Metro.Properties;

namespace Fantasy.Metro
{
    public class DefaultContentLoader : IContentLoader
    {
        public Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                throw new InvalidOperationException("Operation requires the UI thread");
            }

            // scheduler ensures LoadContent is executed on the current UI thread
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(uri), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        public virtual Object LoadContent(Uri uri)
        {
            try
            {
                return Application.LoadComponent(uri);
            }
            catch 
            {
                return null;
            }
        }
    }
}
