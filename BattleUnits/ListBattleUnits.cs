using System.Collections.Generic;

namespace BattleUnits
{
    public static class ListBattleUnits
    {
        private static readonly List<IUnitCreator> _radiantUnits = new List<IUnitCreator>();
        private static readonly List<IUnitCreator> _darknessUnits = new List<IUnitCreator>();

        public static void SetRadiantUnits(IUnitCreator _unit)
        {
            _radiantUnits.Add(_unit);
        }
        public static void SetDarknessUnits(IUnitCreator _unit)
        {
            _darknessUnits.Add(_unit);
        }
        public static List<IUnitCreator> GetRadiantUnits() => _radiantUnits;
        public static List<IUnitCreator> GetDarknessUnits() => _darknessUnits;

        public static void ClearListDark()
        {
            _darknessUnits.Clear();
        }
        public static void ClearListRadiant()
        {
            _radiantUnits.Clear();
        }
    }
}
