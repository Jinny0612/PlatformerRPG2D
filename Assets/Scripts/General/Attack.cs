using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����߼��ű�
/// </summary>
public class Attack : MonoBehaviour
{
    [Header("��������")]
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
