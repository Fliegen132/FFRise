using System.Collections.Generic;
using HexesEvents;
using UnityEngine;
using UnityEngine.Tilemaps;
public class StudyOrUpgrade : IService, IAction
{
    private Vector3Int vector3Int;
    private string Name;
    public Tilemap _tilemap;

    private ResourcesCounter _resourcesCounter;
    
    private ViewBuyUpgrade _viewBuyUpgrade;

    private int buyPrice = 3;

    private Tile _tile;

    private ViewEventNotify _viewEventNotify;

    private IEvent _event;
    public void SetTilemap(Tilemap tilemap)
    {
       _tilemap = tilemap;
    }
    public void Init(Vector3Int vec, string _name, ViewBuyUpgrade viewBuyUpgrade)
    {
        _viewBuyUpgrade = viewBuyUpgrade;
        _resourcesCounter = ServiceLocator.Current.Get<ResourcesCounter>();
        _viewEventNotify = ServiceLocator.Current.Get<ViewEventNotify>();
        Name = _name;
        vector3Int = vec;
        
    }
    public void SetTile(Tile tile)
    {
        _tile = tile;
    }
    //*Создать систему покупки за разные ресурсы*

    public void Upgrade(Dictionary<string, int> resources)
    {
       bool currentCoin = _resourcesCounter.CheckResource("gold",buyPrice);
       bool currentEnergy = UseEnergy();
        if(!currentCoin || !currentEnergy)
            return;
        _resourcesCounter.BuyForResource("gold",buyPrice);
        _resourcesCounter.BuyForResource("energy",10);
        _resourcesCounter.BuyForResource("wood",2);
        if (vector3Int != null)
        {
            _tilemap.SetTile(vector3Int,_tile);
        }
        SetDefault(); //закрытие окна
        Tiles.UpdateTiles();
    }

    public void Study()
    {
        Tiles.SetStudiedTiles(vector3Int, Name);
        string vec = $"{vector3Int.x} {vector3Int.y}";
        if (SetHexesEvents.battleHexes.Contains(vec)) // *сделать метод для проверки всех эвентов в будущем*
        {
            _event = new BattleEvent();
            _viewEventNotify.ViewMessage("На вас напали!");
            _tilemap.SetColor(vector3Int, Color.white);
            _event?.StartEvent();
        }
        else
        {
            _viewEventNotify.ViewMessage("Местность пуста");
            _tilemap.SetColor(vector3Int, Color.white);
        }
        SetDefault(); //закрытие окна
        Tiles.UpdateTiles();
    }

    private void SetDefault()
    {
        _viewBuyUpgrade.SetAnimation("Close");
    }

    
    public bool UseEnergy()
    {
        bool current = _resourcesCounter.CheckResource("energy",10);
        if (current)
            return true;
        
        return false;
    }
}
