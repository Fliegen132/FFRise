using BattleUnits;
using PlayerBattleInput;
using UnityEngine;
using UnityEngine.Events;

public class InputAroundUnit : MonoBehaviour
{
    private ViewAroundUnit _viewAround;
    [SerializeField] private UnityEvent _aroundEvent;
    [field: SerializeField] private IUnit _unit;
    
    private void Start()
    {
        _viewAround = ServiceLocator.Current.Get<ViewAroundUnit>();
       
    }
    private void Update()
    {
        OnMouseUnit();
    }

    private void OnMouseUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out _unit))
            {
                _viewAround.SetInfo(_unit.GetStats());
                _aroundEvent?.Invoke();
            }
            else
            {
                _viewAround.DisableViewWindow();
            }
        }
    }
}
