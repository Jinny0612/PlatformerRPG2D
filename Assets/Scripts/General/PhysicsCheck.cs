using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物理碰撞检测
/// </summary>
public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("检测参数")]
    /// <summary>
    /// 是否自动获取偏移量
    /// </summary>
    public bool autoOffset;
    /// <summary>
    /// 地面检测点偏移量
    /// </summary>
    public Vector2 bottomOffset;
    /// <summary>
    /// 左侧检测点偏移量
    /// </summary>
    //public Vector2 leftOffset;
    /// <summary>
    /// 右侧检测点偏移量
    /// </summary>
    //public Vector2 rightOffset;
    /// <summary>
    /// 敌人前方检测点偏移量
    /// </summary>
    public Vector2 forwardOffset;
    /// <summary>
    /// 检测范围
    /// </summary>
    public float checkRaduis;
    /// <summary>
    /// 碰撞层  ground
    /// </summary>
    public LayerMask groundLayer;
    /// <summary>
    /// 碰撞体的绝对偏移量，会随着scale变化
    /// </summary>
    private Vector2 collPos;
    private Vector2 firstBottomOffset;
    //private Vector2 firstLeftOffset;
    //private Vector2 firstRightOffset;
    private Vector2 firstForwardOffset;
    

    [Header("状态")]
    /// <summary>
    /// 是否在地面上
    /// </summary>
    public bool isGround;
    /// <summary>
    /// 是否触碰左侧墙壁
    /// </summary>
    public bool touchLeftWall;
    /// <summary>
    /// 是否触碰右侧墙壁
    /// </summary>
    public bool touchRightWall;
    public bool touchWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        firstBottomOffset = bottomOffset;
        //firstLeftOffset = leftOffset;
        //firstRightOffset = rightOffset;
        firstForwardOffset = forwardOffset;
        if(autoOffset)
        {
            AutoCaculateOffset(transform.localScale);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void FixedUpdate()
    {
        UpdateOffset(transform.localScale);
    }

    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis,groundLayer);

        //检测墙壁
        //touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis,groundLayer);
        //touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
        touchWall = Physics2D.OverlapCircle((Vector2)transform.position + forwardOffset, checkRaduis, groundLayer);
    }

    /// <summary>
    /// 更新检测点偏移量
    /// </summary>
    /// <param name="faceDir"></param>
    public void UpdateOffset(Vector3 faceDir)
    {
        if (autoOffset)
        {
            AutoCaculateOffset(faceDir);
        }
        else
        {
            //rightOffset = firstRightOffset * faceDir.x;
            //leftOffset = firstLeftOffset * faceDir.x;
            forwardOffset = firstForwardOffset * faceDir.x;
        }
        bottomOffset = firstBottomOffset * faceDir.x;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        //Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        //Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + forwardOffset, checkRaduis);
    }

    /// <summary>
    /// 自动计算偏移量
    /// </summary>
    /// <param name="scale"></param>
    private void AutoCaculateOffset(Vector3 scale)
    {
        // coll.offset是相对偏移量，如果scale变化这个值不会变，但是碰撞体实际的大小是会一起变化的
        collPos = coll.offset * scale;
        // 自动获取偏移量，根据coll的size和实际的offset来计算
        //leftOffset = new Vector2(collPos.x - coll.size.x / 2f - checkRaduis, coll.size.y / 2f);
        //rightOffset = new Vector2(collPos.x + coll.size.x / 2f + checkRaduis, coll.size.y / 2f );
        forwardOffset = new Vector2(collPos.x + scale.x * coll.size.x / 2f + scale.x * checkRaduis, coll.size.y / 2f);
    }


}
