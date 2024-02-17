using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChoosedTile : MonoBehaviour, IService
{
    [SerializeField] private Tile[] tiles;
    
    [SerializeField] private Tile returnTile;
    [SerializeField] private string returnName;

    private Dictionary<string, int> _prices = new Dictionary<string, int>();

    public (Tile, string) ChooseTile(string _name)
    {
        _prices.Clear();
        switch (_name)
        {
            case "Forest":
                returnTile = tiles.FirstOrDefault(x => x.name == "Lamb1"); //постройка лесопилки
                returnName = "Построить лесопилку";
                break;
            case "Lamb1":
                returnName = "Улучшить лесопилку";
                returnTile = tiles.FirstOrDefault(x => x.name == "Lamb2");
                break;
            case "Stone":
                returnTile = tiles.FirstOrDefault(x => x.name == "Mine1");
                returnName = "Построить шахту";
                break;
            case "Mine1":
                returnTile = tiles.FirstOrDefault(x => x.name == "Mine2");
                returnName = "Улучшить шахту";
                break;
            case "Field":
                returnTile = tiles.FirstOrDefault(x => x.name == "Farm1");
                returnName = "Построить ферму";
                break;
            case "Farm1":
                returnTile = tiles.FirstOrDefault(x => x.name == "Farm2");
                returnName = "Улучшить ферму";
                break;
            default:
                returnTile = null;
                break;
            
        }

        return (returnTile, returnName);
    }

    public Dictionary<string, int> GetPrices() => _prices;

}
