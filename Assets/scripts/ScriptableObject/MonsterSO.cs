using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Monster", menuName = "Monster/New Monster")]
public class MonsterSO : ScriptableObject
{
	public string monsterName;
	public int monsterHP;
	public int monsterReward;
	public int monsterLevel;
	public int monsterDefence;
	public int monsterAttack;
	public float monsterAtkPerSec;
	public Sprite monsterImage;
}
