using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMonster : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new DarkMonsterPatrolState();
        chaseState = new DarkMonsterChaseState();
    }
}
