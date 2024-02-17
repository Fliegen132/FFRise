using System.Collections.Generic;
using System.Linq;
namespace InventorySystem
{
    public class Inventory : IService
    {
        private List<Item> _weaponItems = new List<Item>(20);

        private ViewInventory _viewInventory;
        public Inventory(ViewInventory viewInventory)
        {
            _viewInventory = viewInventory;
        }

        public void AddItem(Item item)
        {
            if(_weaponItems.Count >= 20)
                return;
            _weaponItems.Add(item);
            _viewInventory.AddItemInventoryView(item);
        }

        public void RemoveItem(Item item)
        {
            _weaponItems.Remove(item);
        }

        public Item GetItemByName(string itemName)
        {
            Item item = _weaponItems.Find(x => x.NameEng == itemName);
            return item;
        }

     

    }
}