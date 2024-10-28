using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ײ���
/// </summary>
public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("������")]
    /// <summary>
    /// �Ƿ��Զ���ȡƫ����
    /// </summary>
    public bool autoOffset;
    /// <summary>
    /// �������ƫ����
    /// </summary>
    public Vector2 bottomOffset;
    /// <summary>
    /// ������ƫ����
    /// </summary>
    //public Vector2 leftOffset;
    /// <summary>
    /// �Ҳ����ƫ����
    /// </summary>
    //public Vector2 rightOffset;
    /// <summary>
    /// ����ǰ������ƫ����
    /// </summary>
    public Vector2 forwardOffset;
    /// <summary>
    /// ��ⷶΧ
    /// </summary>
    public float checkRaduis;
    /// <summary>
    /// ��ײ��  ground
    /// </summary>
    public LayerMask groundLayer;
    /// <summary>
    /// ��ײ��ľ���ƫ������������scale�仯
    /// </summary>
    private Vector2 collPos;
    private Vector2 firstBottomOffset;
    //private Vector2 firstLeftOffset;
    //private Vector2 firstRightOffset;
    private Vector2 firstForwardOffset;
    

    [Header("״̬")]
    /// <summary>
    /// �Ƿ��ڵ�����
    /// </summary>
    public bool isGround;
    /// <summary>
    /// �Ƿ������ǽ��
    /// </summary>
    public bool touchLeftWall;
    /// <summary>
    /// �Ƿ����Ҳ�ǽ��
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
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis,groundLayer);

        //���ǽ��
        //touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis,groundLayer);
        //touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
        touchWall = Physics2D.OverlapCircle((Vector2)transform.position + forwardOffset, checkRaduis, groundLayer);
    }

    /// <summary>
    /// ���¼���ƫ����
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
    /// �Զ�����ƫ����
    /// </summary>
    /// <param name="scale"></param>
    private void AutoCaculateOffset(Vector3 scale)
    {
        // coll.offset�����ƫ���������scale�仯���ֵ����䣬������ײ��ʵ�ʵĴ�С�ǻ�һ��仯��
        collPos = coll.offset * scale;
        // �Զ���ȡƫ����������coll��size��ʵ�ʵ�offset������
        //leftOffset = new Vector2(collPos.x - coll.size.x / 2f - checkRaduis, coll.size.y / 2f);
        //rightOffset = new Vector2(collPos.x + coll.size.x / 2f + checkRaduis, coll.size.y / 2f );
        forwardOffset = new Vector2(collPos.x + scale.x * coll.size.x / 2f + scale.x * checkRaduis, coll.size.y / 2f);
    }


}
