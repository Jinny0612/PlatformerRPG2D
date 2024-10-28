using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���˵�����ƶ�����
/// </summary>
public class MenuCameraMoveControl : MonoBehaviour
{
    public float moveSpeed;

    //public float waitTime;
    //private float waitTimeCounter;
    /// <summary>
    /// ����߽�
    /// </summary>
    private PolygonCollider2D confinerCollider;

    float leftBound;
    float rightBound;


    private bool moveRight = true;

    private void Awake()
    {
    }

    private void Start()
    {
        #region ����������ұ߽�x����
        confinerCollider = GameObject.FindGameObjectWithTag(Tags.CONFINERBOUNDS)?.GetComponent<PolygonCollider2D>();
        Vector2[] poinst = confinerCollider.points;
        //ת��Ϊ��������
        Vector3[] worldPoints = poinst.Select(point => confinerCollider.transform.TransformPoint(point)).ToArray();
        leftBound = worldPoints.Min(p => p.x);
        rightBound = worldPoints.Max(p => p.x);
        #endregion

    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            //����
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            
        }
        else
        {
            //����
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
        }

        if(transform.position.x <= leftBound)
        {
            // ����ƶ����ӽ���߽�ĵط�����Ҫ��ͷ����
            moveRight = true;
        }
        if (transform.position.x >= rightBound)
        {
            // ����ƶ����ӽ��ұ߽�ĵط�����Ҫ��ͷ����
            moveRight = false;
        }

    }

}
