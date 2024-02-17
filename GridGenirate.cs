using UnityEngine;

public class GridGenirate : MonoBehaviour
{
    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private Transform parentGrid; 
    [SerializeField] private int xCount;
    [SerializeField] private int zCount;
    
    [ContextMenu("Genirate")]
    private void Generate()
    {
        var cellSize = hexPrefab.GetComponent<MeshRenderer>().bounds.size;
        for (int x = 0; x < xCount; x++)
        {
            for (int z = 0; z < zCount; z++)
            {
                float offset = z % 2 == 1 ? cellSize.x * .5f : -2; // вычисляем смещение для четных строк по z
                GameObject go = Instantiate(hexPrefab, new Vector3(
                        x * (cellSize.x * 1.5f) + offset,
                        1,
                        z * (cellSize.z * 0.42f)),
                    Quaternion.identity, parentGrid);
                go.name = $"x: {x} z: {z}";
            }
        }
    }



}
