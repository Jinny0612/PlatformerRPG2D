using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Player����������ͷ���Ļ�����ʶ
/// </summary>
public class HeadIconAnimator : MonoBehaviour
{
    public GameObject iconSprite;
    private Animator anim;

    protected HeadIconController headIcon;
    

    private void Awake()
    {
        headIcon = GetComponent<HeadIconController>();
        anim = iconSprite.GetComponent<Animator>();
        
    }

    private void Update()
    {
        if(iconSprite.activeInHierarchy)
            SetAnimationParameters();

    }

    public void SetAnimationParameters()
    {
        
        anim.SetBool(Settings.canOpen, headIcon.canOpen);
    }


}
