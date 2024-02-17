using BattleUnits;
using PlayerBattleInput;
using UnityEngine;

public class BATTLEServiceLocator : MonoBehaviour
{
    [SerializeField] private Cells _cells;
    [SerializeField] private CharacterInput _characterInput;
    [SerializeField] private BotsInput _botsInput;
    [SerializeField] private ViewAroundUnit _viewAroundUnit;
    [SerializeField] private ViewFinishBattle _viewFinishBattle;
    private void Awake()
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        ServiceLocator.Current.Register<Cells>(_cells);
        ServiceLocator.Current.Register<ViewAroundUnit>(_viewAroundUnit);
        ServiceLocator.Current.Register<ViewFinishBattle>(_viewFinishBattle);
        BattleManager battleManager = new BattleManager(_characterInput, _botsInput);
        ServiceLocator.Current.Register<BattleManager>(battleManager);
    }
}
