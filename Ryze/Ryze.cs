using EloBuddy.SDK.Events;

namespace Ryze
{
    class Ryze
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
        }
    }
}
