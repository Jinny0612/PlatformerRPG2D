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

    #region inspector�а󶨵��¼�����
    public void SetOpen()
    {
        animator.SetTrigger(Settings.open);
    }

    #endregion
}
