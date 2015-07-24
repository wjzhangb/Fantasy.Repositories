using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Fantasy.Metro
{
    public interface IContent
    {
        // Called when navigation to a content fragment begins.
        void OnFragmentNavigation(FragmentNavigationEventArgs e);

        // Called when this instance is no longer the active content in a frame.
        void OnNavigatedFrom(NavigationEventArgs e);

        // Called when a this instance becomes the active content in a frame.
        void OnNavigatedTo(NavigationEventArgs e);

        // Called just before this instance is no longer the active content in a frame.
        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }
}
