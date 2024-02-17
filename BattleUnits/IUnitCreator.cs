
using InventorySystem;

namespace BattleUnits
{
    public interface IUnitCreator
    {
        public int ST { get; set; } //сила 
        public int DX { get; set; } //ловкость отвечает дальность хода
        public int IQ { get; set; } //iq отвечает за воприятие и тд(не для боёвки)
        public int HT { get; set; } // Здоровье отражает энергию и жизнестойкость. В него входят выносливость, сопротивляемость организма
        public string Name { get; set; }
        public Weapon _weapon { get; set; }
        public void SetStats(int st, int dx, int iq, int ht, string name, Weapon weapon);

    }
}