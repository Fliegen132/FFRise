using System;
using System.Collections.Generic;
using System.Linq;
using BattleUnits;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SequenceMovesView
{
    private List<Unit> allUnits = new List<Unit>();
    private List<GameObject> icons = new List<GameObject>();
    public SequenceMovesView(List<Unit> units)
    {
        allUnits = units;

        int x = -304;
        int y = -94;
        for (int i = 0; i < units.Count - 1; i++)
        {
            var Icon = Resources.Load<GameObject>("CharacterImage");
            GameObject go = Object.Instantiate(Icon);
            icons.Add(go);
            go.gameObject.transform.SetParent(GameObject.Find("Canvas/Background/OtherCharacters").transform);

            if (i > 0)
            {
                go.transform.localPosition = new Vector3(x + 200, y, 0);
                x += 200;
                continue;
            }
            go.transform.localPosition = new Vector3(x, y, 0);
        }
    }


    public void SetUnitsUI(Unit currentUnit)
    {
        if (allUnits.Contains(currentUnit)) // для установки текущего юнита вначале
        {
            var currentIcon = GameObject.Find("Canvas/Background/CurrentCharacter/CharacterImage");
            currentIcon.GetComponent<Image>().sprite = currentUnit.Icon;
            var HP = GameObject.Find("Canvas/Background/CurrentCharacter/CharacterImage/Icon/HP");
            var stats = currentUnit.GetStats().Split(' ');
            HP.GetComponent<Image>().fillAmount = float.Parse(stats[1]) / float.Parse(stats[2]);
        }
        
        int i = 0;
        foreach (var unit in allUnits)
        {
            if(allUnits[0] == unit)
                continue;
            icons[i].GetComponent<Image>().sprite = unit.Icon;
            var HP = icons[i].gameObject.transform.Find("Icon/HP");
            var stats = unit.GetStats().Split(' ');
            HP.GetComponent<Image>().fillAmount = float.Parse(stats[1]) / float.Parse(stats[2]);
            i++;
        }
    }
    
    public void UpdateUnitsSequence()
    {
        //добавление в конец списка
        Unit a = allUnits[0];
        allUnits.RemoveAt(0);
        allUnits.Add(a);
        //--------------------------
        var currentIcon = GameObject.Find("Canvas/Background/CurrentCharacter/CharacterImage");
        currentIcon.GetComponent<Image>().sprite = allUnits[0].Icon;
        var HP1 = GameObject.Find("Canvas/Background/CurrentCharacter/CharacterImage/Icon/HP");
        var stats1 = allUnits[0].GetStats().Split(' ');
        Debug.Log(float.Parse(stats1[1]));
        HP1.GetComponent<Image>().fillAmount = float.Parse(stats1[1]) / float.Parse(stats1[2]);
        int i = 0;
        foreach (var unit in allUnits)
        {
            if(allUnits[0] == unit)
                continue;
            icons[i].GetComponent<Image>().sprite = unit.Icon;
            var HP = icons[i].gameObject.transform.Find("Icon/HP");
            var stats = unit.GetStats().Split(' ');
            HP.GetComponent<Image>().fillAmount = float.Parse(stats[1]) / float.Parse(stats[2]);
            i++;
        }
    }

    public void DeleteIcon()
    {
        //icons.RemoveAt(icons.Count);
    }
}
