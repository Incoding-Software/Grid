using System;

namespace GridUI
{
    public static class CurrentSettings
    {
        public static string CurrentVersion
        {
            get
            {
#if DEBUG
                return Guid.NewGuid().ToString();

#else
                return Assembly.Load("GridUI")
                        .GetName()
                        .Version.ToString();
#endif
            }
        } 
    }
}