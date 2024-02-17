using System.Collections.Generic;

namespace GurpsSystem
{
    public static class AllPerks
    {
        public static Dictionary<string, PerkType> dxPerks = new Dictionary<string, PerkType>
        {
            //weapons
            ["Sword"] = PerkType.mediumType ,
            ["Battle Staff"] = PerkType.mediumType ,
            ["Axe"] = PerkType.mediumType,
            ["Shield"] = PerkType.easyType,
            ["Two-Handed Sword"] = PerkType.hardType,
            ["Two-Handed Axe"] = PerkType.hardType,
            ["Bow"] = PerkType.hardType,
            ["Crossbow"] = PerkType.hardType,
            ["Brawling"] = PerkType.easyType,
            ["Wrestling"] = PerkType.mediumType,
            //---------------------------------
            ["Stealth"] = PerkType.superHardType,
            ["Erotic Art"] = PerkType.superHardType,
            ["Escape"] = PerkType.hardType,
            ["Acrobatics"] = PerkType.hardType,
            ["Climbing"] = PerkType.mediumType,
            ["Sleight of Hand"] = PerkType.superHardType,
            ["Dancing"] = PerkType.mediumType,
            ["Filch"] = PerkType.mediumType

        };

        public static Dictionary<string, PerkType> iqPerks = new Dictionary<string, PerkType>
        {
            ["Traps"] = PerkType.hardType,
            ["Administration"] = PerkType.hardType,
            ["Engineer"] = PerkType.hardType,
            ["Animal Handling"] = PerkType.hardType,
            ["Armoury"] = PerkType.hardType,
            ["Tactics"] = PerkType.superHardType,
            ["Alchemy"] = PerkType.superHardType,
            ["Artist"] = PerkType.hardType,
            ["Criminology"] = PerkType.mediumType,
            ["Psychology"] = PerkType.hardType,
            ["Merchant"] = PerkType.mediumType,
            ["Public Speaking"] = PerkType.mediumType,
            ["Gesture"] = PerkType.mediumType,
            ["First Aid"] = PerkType.hardType,
            ["Poisons"] = PerkType.hardType,
            ["Fast-Talk"] = PerkType.mediumType,
            ["Acting"] = PerkType.mediumType,
            ["Seamanship"] = PerkType.superHardType,
            ["Diplomacy"] = PerkType.superHardType,
            ["Cooking"] = PerkType.mediumType,
            ["Blind Fighting"] = PerkType.superHardType,
            ["Search"] = PerkType.mediumType,
            ["Survival"] = PerkType.hardType,
            ["Intimidation"] = PerkType.mediumType,
            ["Tracking"] = PerkType.mediumType,
            ["Body Language"] = PerkType.hardType,
            ["Detect Lies"] = PerkType.hardType,
        };
    }
}