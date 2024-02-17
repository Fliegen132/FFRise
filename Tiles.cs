using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Tiles
{
    private static readonly Dictionary<Vector3Int, string> _studietTiles = new Dictionary<Vector3Int, string>();

    private static Tilemap _tilemap;

    public static void SetTilemap(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }

    public static void SetStudiedTiles(Vector3Int cell, string name)
    {
        if (_studietTiles.ContainsKey(cell))
        {
            return;
        }
        _studietTiles.Add(cell, name);
    }
    public static (List<Vector3Int>, List<string>) GetAllStudiedVector3Ints()
    {
        List<Vector3Int> vector3Ints = new List<Vector3Int>();
        List<string> names = new List<string>();

        foreach (var keyValue in _studietTiles)
        {
            vector3Ints.Add(keyValue.Key);
            names.Add(keyValue.Value);
        }
        return (vector3Ints, names);
    }
    
    public static void UpdateTiles()
    {
        var studiedTiles = GetAllStudiedVector3Ints().Item1;
        foreach (var item in studiedTiles)
        {
            int x = item.x;
            int y = item.y;
            Vector3Int[] neighbors = {
                new Vector3Int(x - 1, y, 0), // Left
                new Vector3Int(x - 1, y + 1, 0), // Left up
                new Vector3Int(x + 1, y, 0), // Right
                new Vector3Int(x + 1, y - 1, 0), // Right down
                new Vector3Int(x, y - 1, 0), // Down
                new Vector3Int(x - 1, y - 1, 0), // Left down
                new Vector3Int(x, y + 1, 0), // Up
                new Vector3Int(x + 1, y + 1, 0) // Right up
            };

            foreach (Vector3Int neighbor in neighbors)
            {
                if (studiedTiles.Contains(neighbor))
                {
                    _tilemap.SetColor(neighbor, Color.white);
                    continue;
                }
                TileBase neighborTile = _tilemap.GetTile(neighbor);
                if (neighborTile != null)
                {
                    if (TilesException.Exceptions.Contains(neighborTile.name))
                        continue;
                    
                    _tilemap.SetColor(neighbor, Color.green);
                }
            }
        }
    }
}
