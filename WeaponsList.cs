using System;
using InventorySystem;
using UnityEngine;

public static class WeaponsList
{
    private static GameObject[] Weapons;

    public static void InitWeapons()
    {
        Weapons = Resources.LoadAll<GameObject>("SOWeapons");
    }

    public static GameObject GetWeaponByName(string WeaponName)
    {
        return Array.Find(Weapons, x => x.GetComponent<Weapon>().NameEng == WeaponName);
    }
}
