using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public interface IContentLoader
    {
        // Asynchronously loads content from specified uri.
        Task<Object> LoadContentAsync(Uri uri, CancellationToken cancellationToken);
    }
}
