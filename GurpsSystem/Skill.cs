namespace GurpsSystem
{
    public class Skill
    {
        private string NAME;
        public int LVL = 0;
        private int[] CURRENTPRICE = { 1, 2, 2, 4, 4, 4 };
        private int PERK;
        private string TEXTPERK;
        public Skill(string _name)
        {
            NAME = _name;
        }

        public string UpSkill()
        {
            LVL++;
            return LVL.ToString();
        }

        public string GetName() => NAME;

        public void SetPerk(int _percent)
        {
            PERK = _percent;
        }
        public void SetPerk(string _percentText)
        {
            TEXTPERK = _percentText;
        }

        public int GetPerk() => PERK;
        public string GetPerkText() => TEXTPERK;
    }
}