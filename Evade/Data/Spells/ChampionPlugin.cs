namespace Evade.Data.Spells
{
    public interface IChampionPlugin
    {
        string GetChampionName();
        void LoadSpecialSpell(SpellData spellData);
    }
}