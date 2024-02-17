using InventorySystem;
using UnityEngine;

public static class MainCharacter 
{
    //stats
    public static int ST { get; set; } = 10; //сила 
    public static int DX { get; set; } = 10; //ловкость отвечает дальность хода
    public static int IQ { get; set; } = 10; //iq отвечает за воприятие и тд(не для боёвки)
    public static int HT { get; set; } = 10; // Здоровье отражает энергию и жизнестойкость. В него входят выносливость, сопротивляемость организма
    //per stats
    public static int HP { get; set; } //хп
    public static int MOVE { get; set; } //дальность хода 
    public static int WILL { get; set; } //воля
    public static int PER { get; set; } //восприятие 
    public static int FP { get; set; }// усталость
    
    //установка оружия
    private static Weapon _weapon;

    public static void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        Debug.Log($"Оружие: {weapon.name} установлено");
    }

    public static Weapon GetWeapon() => _weapon;
    //всё ради визуала 
    public static void SetPerStats()
    {
        HP = HT;
        MOVE += DX * 2;     // <- Move задается от двух значений немного переделать так как у нас distance от юнита к клетке
        MOVE += HT / 2;     
        WILL = IQ;
        PER = IQ;
        FP = HT; 
    }

    public static string GetStats() => $"{ST} {DX} {IQ} {HT}";

    
}
