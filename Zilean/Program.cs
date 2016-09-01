using EloBuddy.SDK.Events;

namespace Zilean
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Brain.Init;
        }
    }
}