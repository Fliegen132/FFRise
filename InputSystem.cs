using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputSystem : MonoBehaviour, IService
{
    [SerializeField]private Tilemap _tilemap;
    [SerializeField] private TileBase clickedTile;
    [SerializeField] private Tile _tile;
    private Dictionary<string, int> _priceTiles;
    public bool choosed = false;
    private ChoosedTile _choosedTile;

    [SerializeField] private ViewException _viewException;
    private ViewBuyUpgrade _viewBuyUpgrade;
    private StudyOrUpgrade _studyOrUpgrade;


    private void Start()
    {
        _studyOrUpgrade = ServiceLocator.Current.Get<StudyOrUpgrade>();
        _choosedTile = ServiceLocator.Current.Get<ChoosedTile>();
        _viewBuyUpgrade = ServiceLocator.Current.Get<ViewBuyUpgrade>();
        _studyOrUpgrade.SetTilemap(_tilemap);
    }

   public void ClickInput()
   { 
       if (choosed) 
           return;

        Vector3 camWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cellPosition = _tilemap.WorldToCell(camWorldPosition);
        clickedTile = _tilemap.GetTile(cellPosition);
        if (clickedTile != null)
        {
            if (TilesException.Exceptions.Contains(clickedTile.name))
            {
                Debug.Log("Туда опасно идти, всё может обвалиться");
                _viewException.ViewMessage();
                return;
            }

            Debug.Log($"Clicked on tile: {clickedTile.name}: x:{cellPosition.x} y:{cellPosition.y}");
            _studyOrUpgrade.Init(cellPosition, clickedTile.name,  _viewBuyUpgrade);
            var studiedTiles = Tiles.GetAllStudiedVector3Ints().Item1;

            foreach (var studiedTile in studiedTiles)
            {
                int x = studiedTile.x;
                int y = studiedTile.y;
                Vector3Int[] neighbors = {
                    new Vector3Int(x - 1, y, 0),      // Left
                    new Vector3Int(x - 1, y + 1, 0),  // Left up
                    new Vector3Int(x + 1, y, 0),      // Right
                    new Vector3Int(x + 1, y - 1, 0),  // Right down
                    new Vector3Int(x, y - 1, 0),      // Down
                    new Vector3Int(x - 1, y - 1, 0),  // Left down
                    new Vector3Int(x, y + 1, 0),      // Up
                    new Vector3Int(x + 1, y + 1, 0)   // Right up
                };

                foreach (var neighbor in neighbors)
                {
                    if (cellPosition == neighbor)
                    {
                        if (studiedTiles.Contains(cellPosition))
                        {
                            _tile = _choosedTile.ChooseTile(clickedTile.name).Item1;
                            _priceTiles = _choosedTile.GetPrices();
                            if (_tile != null)
                            {
                                UpdatebleTile(_choosedTile.ChooseTile(clickedTile.name).Item2);
                                _tile = null;
                            }
                            return; 
                        }
                        _tile = _choosedTile.ChooseTile(clickedTile.name).Item1;
                        if (_tile != null)
                        {
                            StudiedTile("Изучить!");
                            _tile = null;
                        }
                    }
                }
            }
        }
}

   private void UpdatebleTile(string text)
   {
       choosed = true;
       _viewBuyUpgrade.SetUpdateBtn(_tile, text, _priceTiles);
       _viewBuyUpgrade.SetAnimation("Open");
   }

   private void StudiedTile(string text)
   {
       choosed = true;

       _viewBuyUpgrade.SetStudiedBtn(text);
       _viewBuyUpgrade.SetAnimation("Open");
   }

}
