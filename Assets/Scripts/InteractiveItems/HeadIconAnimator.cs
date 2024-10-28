using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可与Player互动的物体头顶的互动标识
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
