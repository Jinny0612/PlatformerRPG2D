using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 主菜单相机移动控制
/// </summary>
public class MenuCameraMoveControl : MonoBehaviour
{
    public float moveSpeed;

    //public float waitTime;
    //private float waitTimeCounter;
    /// <summary>
    /// 相机边界
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
        #region 计算相机左右边界x坐标
        confinerCollider = GameObject.FindGameObjectWithTag(Tags.CONFINERBOUNDS)?.GetComponent<PolygonCollider2D>();
        Vector2[] poinst = confinerCollider.points;
        //转换为世界坐标
        Vector3[] worldPoints = poinst.Select(point => confinerCollider.transform.TransformPoint(point)).ToArray();
        leftBound = worldPoints.Min(p => p.x);
        rightBound = worldPoints.Max(p => p.x);
        #endregion

    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            //右移
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            
        }
        else
        {
            //左移
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
        }

        if(transform.position.x <= leftBound)
        {
            // 相机移动到接近左边界的地方，需要掉头右移
            moveRight = true;
        }
        if (transform.position.x >= rightBound)
        {
            // 相机移动到接近右边界的地方，需要掉头左移
            moveRight = false;
        }

    }

}
