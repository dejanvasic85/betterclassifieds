using Microsoft.Practices.Unity;

namespace BetterClassified
{
    public static class Unity
    {
        private static IUnityContainer defaultContainer;

        public static IUnityContainer DefaultContainer
        {
            get { return defaultContainer ?? (defaultContainer = new UnityContainer()); }
        }
    }
}
