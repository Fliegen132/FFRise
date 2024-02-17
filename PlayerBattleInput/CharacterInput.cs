using System;
using System.Collections;
using System.Collections.Generic;
using BattleUnits;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerBattleInput
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        //for cells color
        [SerializeField] private Color blackColor;
        [SerializeField] private Color greenColor;
        //for move
        [SerializeField] private bool _move;
        private Transform _direction;
        private List<Transform> cells;
        //for HighlightCells and battle manager
        private Action _action;
        private BattleManager _battleManager;

        //for enemy chose and attack
        [SerializeField] private bool _mouseEnterToEnemy;
        private Unit _enemyTarget;
        private bool _attack;
        
        private void Start()
        {
            cells = ServiceLocator.Current.Get<Cells>()._cells;
            _battleManager = ServiceLocator.Current.Get<BattleManager>();
            HighlightCells();
            _action += HighlightCells;
            _action += _battleManager.RadiantCountPlus;
        }
        private void Update()
        {
            if (_battleManager.playerMotion && _unit != null && _battleManager.GetRadiantUnits().Count > 0)
            {
                MoveInput();
                StartAttack();
            }
            OnMouseEnemyEnter();
           
        }

        private void MoveInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _move == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    _direction = GetTargetPosition(hit);
                    if (_direction != null)
                    {
                        _move = _unit.SetMove(true);
                    }
                }
            }
            if (_move && _direction != null)
            {
                _unit.Direct(_direction, _action);
            }
            
        }
        
        public void HighlightCells()
        {
            _move = false;
            foreach (var cell in cells)
            {
                bool isCellOccupied = IsCellOccupied(cell);
                float distance = Vector3.Distance(_unit.transform.position, cell.position);
                if (distance <= _unit.Range && _unit.transform.position != cell.transform.position && !isCellOccupied)
                {
                    cell.GetComponent<Renderer>().material.color = greenColor;
                }
                else
                {
                    cell.GetComponent<Renderer>().material.color = blackColor; 
                }
            }
        }
        private bool IsCellOccupied(Transform cell)
        {
            foreach (var unit in _battleManager.GetRadiantUnits())
            {
                if (unit.GetTransform().position == cell.position)
                {
                    return true;
                }
            }
            foreach (var unit in _battleManager.GetDarkUnits())
            {
                if (unit.GetTransform().position == cell.position)
                {
                    return true;
                }
            }

            return false;
        }
        private Transform GetTargetPosition(RaycastHit hit)
        {
            foreach (var cell in cells)
            {
                bool isCellOccupied = IsCellOccupied(cell);

                float distance = Vector3.Distance(_unit.transform.position, cell.position);
                if (distance < _unit.Range && _unit.transform.position != cell.transform.position && !isCellOccupied)
                {
                    if (hit.collider.name == cell.gameObject.name)
                    {
                        return cell;
                    }
                }
            }

            return null;
        }

        private void OnMouseEnemyEnter()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            _mouseEnterToEnemy = false; 

            foreach (var darkUnit in _battleManager.GetDarkUnits())
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.bounds.Contains(darkUnit.GetTransform().transform.position))
                    {
                        _enemyTarget = (Unit)darkUnit;
                        _mouseEnterToEnemy = true;

                    }
                    
                }
            }
        }

        private void StartAttack()
        {
            if(!_mouseEnterToEnemy)
                return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0) && _move == false && _attack == false)
            {
                if (Vector3.Distance(_unit.transform.position, _enemyTarget.transform.position) < 10)
                {
                    _attack = true;
                    StartCoroutine(Attack());
                }
            }
        }

        private IEnumerator Attack()
        {
            _unit.Attack(_enemyTarget);
            yield return new WaitForSeconds(2f);
            _battleManager.RadiantCountPlus();
            _attack = false;
        }

        public void OffCells()
        {
            foreach (var cell in cells)
            {
                cell.GetComponent<Renderer>().material.color = blackColor; 
            }
        }

        public void SetUnit(Unit unit)
        {
            _unit = unit;
        }
    }
}
