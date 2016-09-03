using EloBuddy.SDK.Menu.Values;

namespace Evade.Config.Controls
{
    public interface IDynamicControl<T>
    {
        ConfigValue GetConfigValue();
        object GetValue();
        ValueBase<T> GetControl();
    }
}