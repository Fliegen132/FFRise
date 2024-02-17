using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;

public class ViewGurps : MonoBehaviour
{
    private Dictionary<string, int> _stats;

    [SerializeField] private TextMeshProUGUI[] _textStats;
    [SerializeField] private TextMeshProUGUI[] _textSkills;
    private ViewInventory _viewInventory;
    private void Awake()
    {
        _stats = new Dictionary<string, int>
        {
            ["ST"] = MainCharacter.ST,
            ["DX"] = MainCharacter.DX,
            ["IQ"] = MainCharacter.IQ,
            ["HT"] = MainCharacter.HT
        };
        RedrawStats();

        _viewInventory = GetComponent<ViewInventory>();
        MainCharacter.SetPerStats();
    }

    public void IncrementValue(string paramName)
    {
        if(_stats[paramName] >= 20)
            return;
        UpdateMainCharacterValue(paramName, 1);
        _stats[paramName]++;
        RedrawStats();
        _viewInventory.RedrawDamageView();
    }
    public void DecrementValue(string paramName)
    {
        if(_stats[paramName] <= 10)
            return;
        UpdateMainCharacterValue(paramName, -1);
        _stats[paramName]--;
        RedrawStats();
        _viewInventory.RedrawDamageView();
    }
    
    private static void UpdateMainCharacterValue(string paramName, int change)
    {
        var prop = typeof(MainCharacter).GetProperty(paramName);
        if (prop != null && prop.PropertyType == typeof(int) && change != 0)
        {
            int currentValue = (int)prop.GetValue(typeof(MainCharacter));
            int newValue = currentValue + change;
            prop.SetValue(typeof(MainCharacter), newValue);
        }
    }
    private void RedrawStats()
    {
        int i = 0;
        foreach (var stat in _stats)
        {
            _textStats[i].text = $"{stat.Key} {stat.Value}"; 
            
            i++;
        }
        Debug.Log(MainCharacter.GetStats());
    }
}
