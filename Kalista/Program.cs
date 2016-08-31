
using EloBuddy.SDK.Events;

namespace Kalista
{
    class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Kalista.OnLoadingComplete;
        }
    }
}
