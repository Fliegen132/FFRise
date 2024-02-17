using System;
using System.Collections;
using System.Collections.Generic;
using BattleUnits;
using PlayerBattleInput;
using UnityEngine;
//ввести лучников
public class BotsInput : MonoBehaviour
{ 
   //for moveble cells 
   private List<Transform> cells;
   //for current enemy
   [SerializeField] private Unit _unit;
   //for animation
   private bool _move;
   //for list radiantUnits
   private BattleManager _battleManager;
   //for distance target;
   private float _distanceTarget;
   [SerializeField] private Transform _target;
   private Transform _cell;
   
   [SerializeField] private Transform currentCell;

   private bool _attack;
   private void Start()
   {
      cells = ServiceLocator.Current.Get<Cells>()._cells;
      _battleManager = ServiceLocator.Current.Get<BattleManager>();
   }
   public void StartMotion()
   {
      if(_battleManager.GetRadiantUnits().Count <= 0)
         return;
      _attack = false;
      GetNearTargetPosition();
      GetCell();
   }
   private void Update()
   {
      if(currentCell != null && _unit.transform.position != currentCell.position && !_attack)
          Move();
   }
   
   private void Move()
   {
      _unit.Direct(currentCell, _battleManager.DarkCountPlus);
   }

   private void GetCell()
   {
      if (_attack)
         return;
      
      float minimalDistance = Single.MaxValue;
      currentCell = null;

      foreach (var cell in cells)
      {
         float distance = Vector3.Distance(_unit.transform.position, cell.position);

         bool isCellOccupied = false;

         foreach (var radiantUnit in _battleManager.GetRadiantUnits())
         {
            if (radiantUnit.GetTransform().position == cell.position)
            {
               isCellOccupied = true;
               break;
            }
         }
         if (!isCellOccupied)
         {
            foreach (var darkUnit in _battleManager.GetDarkUnits())
            {
               if (darkUnit.GetTransform().position == cell.position)
               {
                  isCellOccupied = true;
                  break;
               }
            }
         }
         // Проверяем, свободна ли клетка и дистанцию
         if (distance < _unit.Range && _unit.transform.position != cell.position && !isCellOccupied)
         {
            float distance2 = Vector3.Distance(_target.position, cell.position);
            if (distance2 < minimalDistance)
            {
               minimalDistance = distance2;
               currentCell = cell;
            }
         }
      }
   }
   private void GetNearTargetPosition()
   {
      float minimalDistance = Single.MaxValue;
     
      foreach (var radiantUnit in _battleManager.GetRadiantUnits())
      {
         float _distanceTarget = Vector3.Distance(_unit.transform.position, radiantUnit.GetTransform().position);
         {
            if (_distanceTarget < minimalDistance)
            {
               minimalDistance = _distanceTarget;
               _target = radiantUnit.GetTransform();
               if (_distanceTarget < 10 && !_attack)
               {
                  _attack = true;
                  StartCoroutine(Attack());
               }
            }
         }
      }
   }

   private IEnumerator Attack()
   {
      _unit.Attack(_target.GetComponent<Unit>());
      yield return new WaitForSeconds(1f);
      _battleManager.DarkCountPlus();
   }

   public void SetUnit(Unit unit)
   {
      _unit = unit;
   }
}
