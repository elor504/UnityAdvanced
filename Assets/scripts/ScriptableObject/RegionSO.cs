using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Region", menuName ="Region/New Region")]
public class RegionSO : ScriptableObject
{
    public GameRegions region;
    public MonsterSO[] monsters;
}
