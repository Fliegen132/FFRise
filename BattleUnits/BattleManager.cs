using System.Collections.Generic;
using System.Threading.Tasks;
using BattleUnits;
using UnityEngine;

namespace PlayerBattleInput
{
   public class BattleManager : IService
   {
      [SerializeField] private List<IUnit> radiantUnits = new List<IUnit>();
      [SerializeField] private List<IUnit> darknessUnits = new List<IUnit>();
      private List<Transform> cells;
      //for set units
      private CharacterInput _playerInput;
      private BotsInput _botsInput;
      //------------

      //for current unut
      private int _currentRadiantUnit;
      private int _currentDarknessUnit;
      //for current motion
      public bool playerMotion = true;
      //for finish window
      private ViewFinishBattle _finishBattle;


      private SequenceMovesView sequence;
      public BattleManager(CharacterInput characterInput, BotsInput botsInput)
      {
         _finishBattle = ServiceLocator.Current.Get<ViewFinishBattle>();
         _playerInput = characterInput;
         _botsInput = botsInput; 
         cells = ServiceLocator.Current.Get<Cells>()._cells;
         //Создание на поле врагов
         List<IUnitCreator> _darknessUnits = ListBattleUnits.GetDarknessUnits();
         List<IUnitCreator> _radiantUnits = ListBattleUnits.GetRadiantUnits();
         InstantiateUnit(_darknessUnits, darknessUnits, 84);
         InstantiateUnit(_radiantUnits, radiantUnits, 4);
         _playerInput.SetUnit((Unit)radiantUnits[0]);
         _botsInput.SetUnit((Unit)darknessUnits[0]);
         //для подписки на событие смерти         
         foreach (var unit in radiantUnits)
         {
            unit.OnDeath += RemoveUnitFromList; 
         }

         foreach (var unit in darknessUnits)
         {
            unit.OnDeath += RemoveUnitFromList; 
         }
         //viewModel??-----------------------
         List<IUnit> _iunits = new List<IUnit>();
         _iunits.AddRange(radiantUnits);
         _iunits.AddRange(darknessUnits);
         List<Unit> units = new List<Unit>();
         foreach (var _unit in _iunits)
         {
            units.Add((Unit) _unit);
         } 
         sequence = new SequenceMovesView(units);
         sequence.SetUnitsUI((Unit)radiantUnits[0]);
      }
      
      private void RemoveUnitFromList(IUnit unit)
      {
         if (radiantUnits.Contains(unit))
         {
            radiantUnits.Remove(unit);
         }

         if (darknessUnits.Contains(unit))
         {
            darknessUnits.Remove(unit);
         }
      }
      public void RadiantCountPlus()
      {
         if (GetDarkUnits().Count <= 0)
         {
            _finishBattle.SetWin();
            return;
         }
         if(playerMotion)
         {
            _currentRadiantUnit++;
            if (_currentRadiantUnit >= radiantUnits.Count)
            {
               _currentRadiantUnit = 0;
               playerMotion = !playerMotion;
               _playerInput.OffCells();
               sequence.UpdateUnitsSequence();
               Debug.Log("11111 radiantt");
               DarkCountPlus();
               return;
            }
            _playerInput.SetUnit((Unit)radiantUnits[_currentRadiantUnit]);
            sequence.UpdateUnitsSequence(); // всё норм
            Debug.Log("2222 radiantt");
            _playerInput.HighlightCells();
         }
      }
      public async void DarkCountPlus()
      {
         if (GetRadiantUnits().Count <= 0)
         {
            _finishBattle.SetLost();
            return;
         }
         if (!playerMotion)
         {
            _currentDarknessUnit++;
         }
         await Task.Delay(500);
         if (_currentDarknessUnit > darknessUnits.Count)
         {
            playerMotion = !playerMotion;
            _currentDarknessUnit = 0;
            sequence.UpdateUnitsSequence();
            _playerInput.SetUnit((Unit)radiantUnits[_currentRadiantUnit]);
            Debug.Log("1111 enemy");
            _playerInput.HighlightCells();
         }
         else
         {
            
            if(_currentDarknessUnit < darknessUnits.Count)
               sequence.UpdateUnitsSequence();
            _botsInput.SetUnit((Unit)darknessUnits[_currentDarknessUnit - 1]);
            Debug.Log("2222 enemy");
            _botsInput.StartMotion();
         }
      }
      private void InstantiateUnit(List<IUnitCreator> unitData, List<IUnit> unitList, int startIndex)
      {
         for (int i = 0; i < unitData.Count; i++)
         {
            var prefab = Resources.Load<GameObject>("HeroesPrefabs/" + unitData[i].Name);
            var go = Object.Instantiate(prefab);
            UnitMelee unit = go.AddComponent<UnitMelee>(); // <= заменить на мили или рейндж в зависимости от моба
            unit.Init(unitData[i].ST, unitData[i].DX, unitData[i].IQ, unitData[i].HT, unitData[i].Name, unitData[i]._weapon,
               Resources.Load<Sprite>("Icon/Characters/" + unitData[i].Name));
            unit.transform.position = cells[startIndex + i].transform.position;
            unitList.Add(unit);
            Debug.Log(unitList[i].GetStats());
         }
      }
      public List<IUnit> GetRadiantUnits() => radiantUnits;
      public List<IUnit> GetDarkUnits() => darknessUnits;
   }
}
