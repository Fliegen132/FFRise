using System.Collections.Generic;
using UnityEngine;

namespace BattleUnits
{
    public class Cells : MonoBehaviour, IService
    {
        public List<Transform> _cells = new List<Transform>();
    }
}
