using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class ItemList : MonoBehaviour, IService
    {
        public List<Item> _weapons = new List<Item>();
    }
}