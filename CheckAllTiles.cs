using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckAllTiles : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private Color fadeColor;
    private StudyOrUpgrade _studyOrUpgrade;
    private void Awake()
    {
        TilesException.SetExceptions();

        FindAllTiles();
        FindBuyTiles();
        
    }

    private void Start()
    {
        Tiles.SetTilemap(tilemap);
        Tiles.UpdateTiles();       
    }

    private void FindAllTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetColor(tilePosition, fadeColor);
            }
        }
    }
    //для статра
    private void FindBuyTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                TileBase tile = allTiles[(x - bounds.xMin) + (y - bounds.yMin) * bounds.size.x];
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (tile != null && tile.name == "Gabriel Alisima")
                {
                    Tiles.SetStudiedTiles(cellPosition, tile.name);
                    
                    tilemap.SetColor(cellPosition, Color.white);
                    Vector3Int[] neighbors =
                    {
                        new Vector3Int(x - 1, y, 0), // Left
                        new Vector3Int(x - 1, y + 1, 0), //left up
                        new Vector3Int(x + 1, y, 0), // Right
                        new Vector3Int(x + 1, y - 1, 0), // right down
                        new Vector3Int(x, y - 1, 0), // Down
                        new Vector3Int(x - 1, y - 1, 0),// left down
                        new Vector3Int(x, y + 1, 0), // Up
                        new Vector3Int(x + 1, y + 1, 0) // right up
                    };

                    foreach (Vector3Int neighbor in neighbors)
                    {
                        TileBase neighborTile = tilemap.GetTile(neighbor);

                        if (neighborTile != null)
                        {
                            if (TilesException.Exceptions.Contains(neighborTile.name))
                                continue;
                            
                            Debug.Log($"{neighborTile.name} x:{neighbor.x} y:{neighbor.y}");
                            tilemap.SetColor(neighbor, Color.green);
                        }
                    }
                }
            }
        }

    }
}
