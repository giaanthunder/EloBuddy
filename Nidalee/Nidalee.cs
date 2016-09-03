using EloBuddy.SDK.Events;

namespace Nidalee
{
    class Nidalee
    {
        static void Main(string[] args)
        {
            {
                Loading.OnLoadingComplete += More.Loading_OnLoadingComplete;
            }
        }
    }
}
