using DefaultNamespace;
using HexesEvents;
using InventorySystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GAMEPLAYSCENE_ServiceLocator : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private ViewBuyUpgrade _viewBuyUpgrade;
    [SerializeField] private ViewResources _viewResources;
    [SerializeField] private ChoosedTile _choosedTile;
    [SerializeField] private ViewEventNotify _viewEventNotify;
    [SerializeField] private CameraController _cameraController;

    private StudyOrUpgrade _studyOrUpgrade;
    private ResourcesCounter _resources;
    private BattleEvent _battleEvent;

    [SerializeField] private ViewInventory _viewInventory;
    private Inventory _inventory;
    [SerializeField] private ItemList _itemList;
    private void Awake()
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        ServiceLocator.Current.Register<ViewResources>(_viewResources);
        ServiceLocator.Current.Register<InputSystem>(_inputSystem);
        ServiceLocator.Current.Register<ViewBuyUpgrade>(_viewBuyUpgrade);
        ServiceLocator.Current.Register<ChoosedTile>(_choosedTile);
        ServiceLocator.Current.Register<ViewEventNotify>(_viewEventNotify);
        ServiceLocator.Current.Register<CameraController>(_cameraController);
        
        _studyOrUpgrade = new StudyOrUpgrade();
        _battleEvent = new BattleEvent();
        _resources = new ResourcesCounter();
        ServiceLocator.Current.Register<StudyOrUpgrade>(_studyOrUpgrade);
        ServiceLocator.Current.Register<ResourcesCounter>(_resources);
        ServiceLocator.Current.Register<BattleEvent>(_battleEvent);
        ServiceLocator.Current.Register<ViewInventory>(_viewInventory);
        _inventory = new Inventory(_viewInventory);
        ServiceLocator.Current.Register<Inventory>(_inventory);
        
        ServiceLocator.Current.Register<ItemList>(_itemList);

    }
    
}
