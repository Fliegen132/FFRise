using UnityEngine;
using System;
public class ChildUnitEvent : MonoBehaviour
{
    public Action _action;

    public void DoAction()
    {
        _action.Invoke();
    }
}
