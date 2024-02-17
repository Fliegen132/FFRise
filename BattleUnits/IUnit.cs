using System;
using InventorySystem;
using UnityEngine;

public interface IUnit
{
    public int ST { get; set; } //сила 
    public int DX { get; set; } //ловкость отвечает дальность хода
    public int IQ { get; set; } //iq отвечает за воприятие и тд(не для боёвки)
    public int HT { get; set; } // Здоровье отражает энергию и жизнестойкость. В него входят выносливость, сопротивляемость организма
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public void Init(int st, int dx, int iq, int ht, string _name, Weapon weapon, Sprite _icon);
    public event Action<IUnit> OnDeath;
    public void TakeDamage(int damage);
    public string GetStats();
    public Transform GetTransform();
}
