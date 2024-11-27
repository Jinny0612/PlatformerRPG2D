using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestAnimation : MonoBehaviour
{
    public Animator animator;
    public Chest chest;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        chest = GetComponent<Chest>();
    }

    #region inspector中绑定的事件方法
    public void SetOpen()
    {
        animator.SetTrigger(Settings.open);
    }

    #endregion
}
