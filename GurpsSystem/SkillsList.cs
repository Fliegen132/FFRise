using System.Collections.Generic;
using System.Linq;

namespace GurpsSystem
{
    public static class SkillsList
    {
        private static List<Skill> buySkills = new List<Skill>();
        public static Skill BuySkill(Skill skill)
        {
            buySkills.Add(skill);
            return skill;
        }
        
        public static string UpLvl(Skill skill)
        {
            string a = skill.UpSkill();
            return a;
        }

        public static Skill FindSkillByName(string _name)
        {
            return buySkills.SingleOrDefault(x => x.GetName() == _name) ?? null;
        }

        public static bool CheckBuyed(string _name)
        {
            return buySkills.Any(skill => skill.GetName() == _name);
        }
        
    }
}