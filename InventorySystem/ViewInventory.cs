using System.Collections.Generic;
using System.Linq;
using GurpsSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ViewInventory : MonoBehaviour, IService, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject[] _itemCells;
        [SerializeField] private TextMeshProUGUI _descriptionTextItem;
        private Inventory _inventory;
        //for open
        public static bool _open;
        //windows
        [SerializeField] private GameObject _inventoryWindow;
        [SerializeField] private GameObject _gurpsWindow;
        
        //for stats
        
        //for drag item
        [SerializeField] private Transform _startPosition;
        [SerializeField]private bool isDrag;
        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private Transform _itemTransform;
        [SerializeField] private Transform _choosedCell;
        private Item _item;

        private void Start()
        {
            _inventory = ServiceLocator.Current.Get<Inventory>();
            
        }
        public void AddItemInventoryView(Item item)
        {
            foreach (var _itemCell in _itemCells)
            {
                if (_itemCell.transform.GetChild(0).name == "ItemSprite" || _itemCell.transform.GetChild(0).name == "WeaponSprite")
                {
                    _itemCell.transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;
                    _itemCell.transform.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,1);
                    _itemCell.transform.GetChild(0).name = item.NameEng;
                    RedrawDamageView();
                    
                    
                    break;

                }
               
            }
        }

        public void RedrawDamageView()
        {
            for (int i = 0; i < _itemCells.Length; i++)
            {
                if (_itemCells[i].transform.GetChild(0).name != "ItemSprite" && _itemCells[i].transform.GetChild(0).name != "WeaponSprite")
                {
                    string keyValue = "";
                    _itemCells[i].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                    keyValue = _itemCells[i].transform.GetChild(0).name;
                    Weapon weapon = (Weapon)_inventory.GetItemByName(keyValue);
                    if (weapon.Values[MainCharacter.ST - 10] > 0)
                        keyValue = $"+{weapon.Values[MainCharacter.ST - 10]}";
                    else if (weapon.Values[MainCharacter.ST - 10] < 0)
                        keyValue = $"{weapon.Values[MainCharacter.ST - 10]}";
                    else
                        keyValue = "";

                    _itemCells[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0)
                        .GetComponent<TextMeshProUGUI>()
                        .text = $"{weapon.Keys[MainCharacter.ST - 10]}D{keyValue}";
                }
            }
        }
        
        public void SetWeapon(Weapon weapon)
        {
            MainCharacter.SetWeapon(weapon);
        }
        
        public void OpenInventory()
        {
            if (!_open)
            {
               
                _open = true;
            }
            else
            {
                _open = false;
            }
            _gurpsWindow.SetActive(false);
            _inventoryWindow.SetActive(_open);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerEnter != null && _itemCells.Contains(eventData.pointerEnter.gameObject))
            {
                string itemName = "";
                Debug.Log(eventData.pointerEnter.gameObject.name);
                if(eventData.pointerEnter.transform.childCount > 0 )
                    itemName = eventData.pointerEnter.transform.GetChild(0).name;
                
                _item = _inventory.GetItemByName(itemName);

                _choosedCell = eventData.pointerEnter.transform;
                
                if (_item != null && !isDrag)
                {
                    _itemTransform = eventData.pointerEnter.transform.GetChild(0).transform;
                    _descriptionTextItem.text = _item.Description;
                }
                else
                {
                    _descriptionTextItem.text = "";
                }
            }
            else
            {
                _descriptionTextItem.text = "";
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if(isDrag)
                return;
            _itemTransform = null;
            _choosedCell = null;
        }

        private bool _taked;
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
            if (_itemTransform == null)
                return;
            if (!_taked)
            {
                _startPosition = _itemTransform.parent.transform;
                _itemTransform.SetParent(_itemTransform.parent.parent);
                _taked = true;
            }
        }


        public void OnDrag(PointerEventData eventData)
        {
            if(_itemTransform == null && isDrag)
                return;
            _itemTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _taked = false;
            isDrag = false;
            if (_itemTransform == null )
                return;

            if (_choosedCell != null && SkillsList.CheckBuyed(_itemTransform.name))
            {
                _itemTransform.position = _choosedCell.position;
                _itemTransform.SetParent(_choosedCell);
                
                _choosedCell.GetChild(0).transform.position = _startPosition.position;
                _choosedCell.GetChild(0).transform.SetParent(_startPosition);
                if (_choosedCell.name == "WeaponCell")
                {
                    Weapon weapon = (Weapon) _inventory.GetItemByName(_itemTransform.name); // установка оружия переделать чтобы если убрать оружие то его не было
                    SetWeapon(weapon);
                }

                _choosedCell = null;
                _itemTransform = null;
                return;
                
            }
            _itemTransform.SetParent(_startPosition);
            _itemTransform.position = _startPosition.position;

            
            _itemTransform = null;
        }
    }
}