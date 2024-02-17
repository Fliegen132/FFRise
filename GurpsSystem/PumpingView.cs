using System;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GurpsSystem
{
   
    public class PumpingView : MonoBehaviour
    {
        [SerializeField] private Transform _windowForDXSkills;
        [SerializeField] private Transform _windowForIQSkills;
        [SerializeField] private Transform _windowForOtherSkills;
        [SerializeField] private List<string> _skillsName = new List<string>();

        [SerializeField] private GameObject _boxPrefab;
        private void Awake()
        {
            InitDXSkills();
            InitIQSkills();
        }
        //инициализации Кнопок для покупки
        private void InitDXSkills()
        {
            int i = 0;
            foreach (var perk in AllPerks.dxPerks.Keys)
            {
                GameObject box = Instantiate(_boxPrefab, _windowForDXSkills.transform);
                string _name = box.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    perk.ToString();
                box.transform.name = perk.ToString();
                for (int j = 0; j < AllPerks.dxPerks.Count; j++)
                {
                    if (AllPerks.dxPerks.ContainsKey(_name))
                    {
                        TextMeshProUGUI _textView = box.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                        int perkValue = Convert.ToInt32(AllPerks.dxPerks[_name]);
                        _textView.text = $"DX{perkValue} = {MainCharacter.DX + perkValue} | - |";
                        break;
                    }
                }
                _skillsName.Add(_name);
                //инициализация buySkill
                Button btn = box.transform.Find("Btns/LockBtn").GetComponent<Button>();
                btn.onClick.AddListener(() => BuySkill(btn.gameObject.transform));
                //-----------------------
                //инициализация UpSkill
                Button btn1 = box.transform.Find("Btns/UpBtn").GetComponent<Button>();
                btn1.onClick.AddListener(() => UpSkill(btn1.gameObject.transform));
                //-----------------------
                i++;
            }
        }

        private void InitIQSkills()
        {
            int i = 0;
            foreach (var perk in AllPerks.iqPerks.Keys)
            {
                GameObject box = Instantiate(_boxPrefab, _windowForIQSkills.transform);
                string _name = box.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    perk.ToString();
                box.transform.name = perk.ToString();
                for (int j = 0; j < AllPerks.iqPerks.Count; j++)
                {
                    if (AllPerks.iqPerks.ContainsKey(_name))
                    {
                        TextMeshProUGUI _textView = box.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                        int perkValue = Convert.ToInt32(AllPerks.iqPerks[_name]);
                        _textView.text = $"IQ{perkValue} = {MainCharacter.IQ + perkValue} | - |";
                        break;
                    }
                }
                _skillsName.Add(_name);
                //инициализация buySkill
                Button btn = box.transform.Find("Btns/LockBtn").GetComponent<Button>();
                btn.onClick.AddListener(() => BuySkill(btn.gameObject.transform));
                //-----------------------
                //инициализация UpSkill
                Button btn1 = box.transform.Find("Btns/UpBtn").GetComponent<Button>();
                btn1.onClick.AddListener(() => UpSkill(btn1.gameObject.transform));
                //-----------------------
                i++;
            }
        }
        public void BuySkill(Transform button)
        {
            var parent = button.parent;
            Skill skill = SkillsList.BuySkill(new Skill(parent.parent.name));
            //если хватило очков для открытия сделать это условие 
            button.gameObject.SetActive(false);
            parent.GetChild(0).gameObject.SetActive(true); // <= pumping btns plus
            parent.GetChild(1).gameObject.SetActive(true); // <= pumping btns minus
            
            //изменение текста pumping
            TextMeshProUGUI _textView = parent.parent.GetChild(1).GetComponent<TextMeshProUGUI>(); 
            string _name = parent.parent.name; // <= для получения box например Sword
            int perk = 0;
            if (AllPerks.dxPerks.ContainsKey(_name))
            {
                perk = Convert.ToInt32(AllPerks.dxPerks[_name]);
                skill.SetPerk(perk + MainCharacter.DX);
                skill.SetPerk($"DX{perk}");
                _textView.text = $"DX{perk} = {MainCharacter.DX + perk} | {skill.LVL + 1} |";

            }
            else if (AllPerks.iqPerks.ContainsKey(_name))
            {
                perk = Convert.ToInt32(AllPerks.iqPerks[_name]);
                skill.SetPerk(perk + MainCharacter.IQ);
                skill.SetPerk($"IQ{perk}");
                _textView.text = $"IQ{perk} = {MainCharacter.IQ + perk} | {skill.LVL + 1} |";
            }
        }

        public void UpSkill(Transform button)
        {
            var parent = button.parent;
            Skill skill = SkillsList.FindSkillByName(parent.parent.name);
            //в skill есть цена, выводить в кнопку поднятия лвла
            if (skill.LVL > 5)
                return;
            SkillsList.UpLvl(skill);
            
            TextMeshProUGUI _textView = parent.parent.GetChild(1).GetComponent<TextMeshProUGUI>(); 
            string _name = parent.parent.name; // <= для получения box например Sword
            if (AllPerks.dxPerks.ContainsKey(_name))
            {
                int perk = Convert.ToInt32(AllPerks.dxPerks[_name]) + skill.LVL;
                skill.SetPerk(perk + MainCharacter.DX);
                skill.SetPerk($"DX{perk}");
                _textView.text = $"DX{perk} = {MainCharacter.DX + perk} | {skill.LVL + 1} |";
            }
            else if (AllPerks.iqPerks.ContainsKey(_name))
            {
                int perk = Convert.ToInt32(AllPerks.iqPerks[_name]) + skill.LVL;
                skill.SetPerk(perk + MainCharacter.IQ);
                skill.SetPerk($"IQ{perk}");
                _textView.text = $"IQ{perk} = {MainCharacter.IQ + perk} | {skill.LVL + 1} |";
            }
        }
    }
}
