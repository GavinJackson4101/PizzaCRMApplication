namespace TestBlazorAppGJackson.Services
{
    using System;

    namespace TestBlazorAppGJackson.Services
    {
        public class NavMenuVisibilityService
        {
            public bool IsNavVisible { get; private set; } = true;  // Default to visible

            // Method to change the visibility
            public void SetNavMenuVisibility(bool isVisible)
            {
                IsNavVisible = isVisible;
            }
        }
    }

}
