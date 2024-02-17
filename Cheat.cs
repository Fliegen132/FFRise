using System;
using System.Linq;
using InventorySystem;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cheat : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private string text;
    
    private ResourcesCounter _resourcesCounter;
    private ViewResources _viewResources;

    private Inventory _inventory;


    /// <summary>
    /// weapons
    /// </summary>

    [SerializeField] private Item[] items;
    
    private void Start()
    {
        _inventory = ServiceLocator.Current.Get<Inventory>();
        _resourcesCounter = ServiceLocator.Current.Get<ResourcesCounter>();
        _viewResources = ServiceLocator.Current.Get<ViewResources>();
        _inputField.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inputField.gameObject.SetActive(true);
            _inputField.Select();
            EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
            _inputField.ActivateInputField();
        }
    }

    public void UseCheat()
    {
        _inputField.gameObject.SetActive(false);

        text = _inputField.text;

        var a = _inputField.text.Split(' ').ToArray();
        
        if (text.Contains("/getres"))
        {
            if (a.Length <= 2)
                return;
            
            if (string.IsNullOrEmpty(a[2]))
                a[2] = "0";
            _resourcesCounter.AddResource(a[1], int.Parse(a[2]));
            _resourcesCounter.Apply();
            _viewResources.UpdateText();
        }

        if (text.Contains("/getweapon"))
        {
            for (int i = 0; i < items.Length; i++)
            {
                if(a.Length != 2)
                    return;
                if (items[i].name == a[1])
                {
                    if (text.Length < 1)
                        return;
                    _inventory.AddItem(items[i]);
                    break; 
                }
            }
        }

        text = "";
        _inputField.text = "";
    }

}
