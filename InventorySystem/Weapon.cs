using System;
using UnityEngine;

namespace InventorySystem
{
   [CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon")]
   [Serializable]
   public class Weapon : Item
   {
      public int Damage { get; set; }
      
      public string GetDescription() => Description;
      
      //for damage

      public int[] Keys;
      public int[] Values;
   }
}
