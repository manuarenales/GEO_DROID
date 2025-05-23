using Microsoft.AspNetCore.Components;
 

namespace GEO_DROID.Services
{
    class CustomNavigationManager
    {
        private static NavigationManager _navigationManager;
        public CustomNavigationManager(NavigationManager MyNavigationManager)
        {
            _navigationManager = MyNavigationManager;
        }

        public static void Navigation(string url)
        {
            if (_navigationManager != null)
            {
                _navigationManager.NavigateTo(url);
            }
        }
    }
}
