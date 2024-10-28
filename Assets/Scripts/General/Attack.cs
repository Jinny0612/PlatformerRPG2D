using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击逻辑脚本
/// </summary>
public class Attack : MonoBehaviour
{
    [Header("基本属性")]
    public int damage;
    public float attackRange;
    public float attackRate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        //Debug.Log(transform.tag);
        
        collision.GetComponent<Character>()?.TakeDamage(this);
        
    }
}
