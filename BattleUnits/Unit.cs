using System;
using System.Collections;
using System.Collections.Generic;
using GurpsSystem;
using InventorySystem;
using PlayerBattleInput;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleUnits
{
    public class Unit : MonoBehaviour, IUnit
    {
        [field: SerializeField] public int ST { get; set; } //сила 
        [field: SerializeField] public int DX { get; set; } //ловкость отвечает дальность хода
        [field: SerializeField] public int IQ { get; set; } //iq отвечает за воприятие и тд(не для боёвки)
        [field: SerializeField] public int HT { get; set; }
        //unit stats
        [field: SerializeField] private int MaxHP { get; set; }
        [field: SerializeField] private int CurrentHP { get; set; }
        [field: SerializeField] public string Name { get; set; }
        public Sprite Icon { get; set; }

        //for range move
        [field: SerializeField] public float Range { get; set; } 

        //-----------
        //cells for move
        private List<Transform> cells { get; set; }
        //-----------
        //for animation
        [field: SerializeField] private bool Move { get; set; }

        [field: SerializeField] private Animator _animator;
        //---------

        public bool _isDead = false;
        public event Action<IUnit> OnDeath;

        [SerializeField] private Weapon _weapon;
        private void Start()
        {
            _animator = transform.GetChild(0).GetComponent<Animator>();
            transform.GetChild(0).gameObject.AddComponent<ChildUnitEvent>();
            InitCells();
            AnimationEventCreator();
        }
        public void Init(int st, int dx, int iq, int ht, string _name, Weapon weapon, Sprite _icon)
        {
            ST = st;
            DX = dx;
            IQ = iq;
            HT = ht;
            Name = _name;
            Icon = _icon;
            Transform[] allChildren = transform.GetComponentsInChildren<Transform>(); // для загрузки оружия
            foreach (var children in allChildren)
            {
                if (children.name == "Weapons")
                {
                    for (int i = 0; i < children.childCount; i++)
                    {
                        children.GetChild(i).gameObject.SetActive(false);
                    }
                    if (weapon != null)
                    {
                        _weapon = weapon;
                        Debug.Log(_weapon.name);
                        
                        children.Find(weapon.NameEng).gameObject.SetActive(true);
                        break;
                    }
                }
                else
                {
                    _weapon = Resources.Load<Weapon>("SOWeapons/Fist");
                }
            } // -------------------------------------
            SetPerStats();
        }
        private void SetPerStats()
        {
            MaxHP = HT;
            CurrentHP = MaxHP;
            Range = DX;
        }

        private void InitCells()
        {
            cells = ServiceLocator.Current.Get<Cells>()._cells;
        }
        
        //нужно для вызыва метода на анимации смерти
        private void AnimationEventCreator()
        {
            ChildUnitEvent childUnitEvent = transform.GetChild(0).GetComponent<ChildUnitEvent>();
            childUnitEvent._action += DeadEventAnimation;
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

            foreach (var clip in clips)
            {
                if (clip.name == "Dead")
                {
                    AnimationEvent @event = new AnimationEvent
                    {
                        functionName = nameof(childUnitEvent.DoAction), time = clip.length
                    };

                    clip.AddEvent(@event);
                    return;
                }
            }
        }
        
        public virtual void Attack(Unit unit)
        {
            if(_isDead)
                return;
            
            IDice dice = new Dice();
            int damage = dice.RollDice(_weapon.Keys[ST - 10], _weapon.Values[ST - 10]);
            Skill skill = SkillsList.FindSkillByName(_weapon.NameEng);
            int perk;
            if (skill != null)
            {
                perk = skill.GetPerk();
                Debug.Log($"{skill.GetPerk()} ЭТО DX от нашего {_weapon.NameRu}");
            }
            else
            {
                int a = Random.Range(-3, 2);
                perk = DX + a;
                Debug.Log($"Оружие не найдено так что DX{a}");
            }

            if(damage > perk) //проверяем смогли ли мы ударить
                return;

            if (damage <= 0)
                damage = 1;
            
            Debug.Log($"Урон = {damage}");

            Vector3 direction = unit.transform.position - transform.position;
            direction.y = 0; // Убедимся, что поворот происходит только по горизонтали

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, float.MaxValue * Time.deltaTime);

            // После того, как персонаж повернулся к цели, запускаем анимацию атаки и наносим урон
            if (Quaternion.Angle(transform.rotation, targetRotation) < 5f) // Убедимся, что персонаж повернулся достаточно близко к цели
            {
                _animator.Play("Attack");
                bool dodge = unit.DodgeAttack(); //если смогли то проверяем сможет ли цель увернуться
                if(dodge)
                    return;
                
                unit.TakeDamage(damage);
            }
        }
        private bool DodgeAttack()
        {
            IDice dice = new Dice();
            int result = dice.RollDice(3,0);
            if(result > Range / 2 + 3)
                return false;
            
            Debug.Log("уверунлся");
            return true;
        }

        public void TakeDamage(int damage)
        {
            if(_isDead)
                return;
            _animator.Play("TakeDamage");
            CurrentHP -= damage;
            if (CurrentHP <= 0)
                StartCoroutine(Dead());
        }

        private IEnumerator Dead()
        {
            yield return new WaitForSeconds(0.5f);
            _isDead = true;
            _animator.applyRootMotion = true;
            _animator.Play("Dead");
            OnDeath?.Invoke(this);
        }

        public void DeadEventAnimation()
        {
            gameObject.SetActive(false);
        }

        private bool _actionCall;
        public void Direct(Transform target, Action action)
        {
            if(_isDead)
                return;
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, 30 * Time.deltaTime);
            Vector3 direction = (target.position - transform.position).normalized;
            if(transform.position != target.position)
                transform.rotation = Quaternion.LookRotation(direction);
            _animator.SetBool("Move", true);
            if (transform.position == target.position && _actionCall == false)
            {
                _animator.SetBool("Move", false);
                BattleManager battleManager = ServiceLocator.Current.Get<BattleManager>();
                
                action?.Invoke();
                
                _actionCall = true;
            }
            else
            {
                _actionCall = false;
            }
        }

        public bool SetMove(bool _move)
        {
            Move = _move;
            return _move;
        }
        public string GetStats()
        {
            if(_weapon.Values[ST - 10] < 0) // если у нас меньше 0 значение то минус всё равно допишется
                return $"{Name} {CurrentHP} {MaxHP} {_weapon.Keys[ST - 10]}D{_weapon.Values[ST - 10]}";
            else if(_weapon.Values[ST - 10] == 0)
            {
                return $"{Name} {CurrentHP} {MaxHP} {_weapon.Keys[ST - 10]}D";
            }
            else
                return $"{Name} {CurrentHP} {MaxHP} {_weapon.Keys[ST - 10]}D+{_weapon.Values[ST - 10]}";
        }

        public Transform GetTransform() => transform;
    }
}
