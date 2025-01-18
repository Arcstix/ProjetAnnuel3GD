using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tool_LD.Script
{
    [CreateAssetMenu(fileName = "New Palette", menuName = "LD/New Palette", order = 0)]
    public class Palette : ScriptableObject
    {
        // List of prefab path
        public List<string> listOfPrefabPath = new List<string>();
    }
}
