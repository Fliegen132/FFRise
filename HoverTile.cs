using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverTile : MonoBehaviour
{
    private TileBase hoverTile;

    [SerializeField] private Grid grid;
    [SerializeField] private GameObject borderGameObject;
    private InputSystem _inputSystem;

    private void Start()
    {
        _inputSystem = ServiceLocator.Current.Get<InputSystem>();
    }

    private void Update()
    {
        if(_inputSystem.choosed || ViewInventory._open)
            return;
        
        Hover();
    }
    private void Hover()
    {
        Vector3 camWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = grid.WorldToCell(camWorldPosition);
        
        hoverTile = grid.GetComponent<Tilemap>().GetTile(cellPosition);
        if (hoverTile != null)
        {
            borderGameObject.SetActive(true);
            borderGameObject.transform.position = grid.GetComponent<Tilemap>().GetCellCenterWorld(cellPosition);
        }

        if (hoverTile == null)
        {
            borderGameObject.SetActive(false);
        }
    }
}
