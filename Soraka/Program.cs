using EloBuddy.SDK.Events;

namespace Soraka
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Brain.Loading_OnLoadingComplete;
        }
   }
}
