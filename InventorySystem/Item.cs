using UnityEngine;

namespace InventorySystem
{
   public class Item : ScriptableObject
   {
      public string NameEng;
      public string NameRu;
      public string PrefabName;
      public int Price;
      public Sprite Icon;
      [TextArea(3,5)]public string Description;

      
   }
}
