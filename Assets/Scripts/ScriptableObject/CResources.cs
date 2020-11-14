using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CResources", menuName = "ScriptableObjects/CResources", order = 0)]
public class CResources : ScriptableObject {
    public CWord[] words = new CWord[] { };
}
