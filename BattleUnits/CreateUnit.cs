using InventorySystem;

namespace BattleUnits
{
    public class CreateUnit : IUnitCreator
    {
        public int ST { get; set; }
        public int DX { get; set; }
        public int IQ { get; set; }
        public int HT { get; set; }
        public string Name { get; set; }
        public Weapon _weapon { get; set; }

        public void SetStats(int st, int dx, int iq, int ht, string _name, Weapon weapon)
        {
            ST = st;
            DX = dx;
            IQ = iq;
            HT = ht;
            Name = _name;
            
            _weapon = weapon;
        }
       
   
        public void ApplyDarkness(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ListBattleUnits.SetDarknessUnits(this);
            }
        }
        
        public void ApplyRadiant(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ListBattleUnits.SetRadiantUnits(this);
            }
        }

        
    }
}
