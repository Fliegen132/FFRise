using BattleUnits;
using HexesEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class BattleEvent : IEvent, IService
{
    //сделать разделение врагов на разных регионах
    private ViewEventNotify _eventNotify;
    private CameraController _cameraController;

    public BattleEvent()
    {
        _cameraController = ServiceLocator.Current.Get<CameraController>();
    }

    public async void StartEvent()
    {
        _cameraController.EnableGameAwait(); //выключаем управление 
        ListBattleUnits.ClearListDark(); //удаляем старых врагов
        ListBattleUnits.ClearListRadiant(); //удаляем старых героев
        
        RegisterPlayerUnits(); // регистируем новых для игрока
        TryRegisterLustyMobs(); //регистрируем новых для врагов
        
        await Task.Delay(2000);
        LoadBattleScene();
    }
    private void RegisterPlayerUnits()
    {
        //получение статы главого героя 
        CreateUnit createMainHero = new CreateUnit();
        createMainHero.SetStats(MainCharacter.ST,MainCharacter.DX, MainCharacter.IQ,MainCharacter.HT, "MainHero", MainCharacter.GetWeapon()); //<= нужен всегда
        createMainHero.ApplyRadiant(1);
        //------------------- ВСЕГДА
        CreateUnit createMainHero1 = new CreateUnit();
        createMainHero1.SetStats(MainCharacter.ST,MainCharacter.DX, MainCharacter.IQ,MainCharacter.HT, "Idalin", MainCharacter.GetWeapon()); //<= нужен всегда
        createMainHero1.ApplyRadiant(1);
        //создавать из списка отряда 
    }
    private void TryRegisterLustyMobs()
    {
        WeaponsList.InitWeapons();

        CreateUnit createGoblin = new CreateUnit();
        createGoblin.SetStats(10,10, 5,15, "Goblin", null); // <---- заполнение в списки radiantUnits и darknessUnits идет внутри Init()
        createGoblin.ApplyDarkness(Random.Range(2,4));
    }
    private void LoadBattleScene()
    {
        ServiceLocator.Current.UnregisterAll();
        Debug.Log("load battleScene ");
        SceneManager.LoadSceneAsync("BattleScene");
    }
}
