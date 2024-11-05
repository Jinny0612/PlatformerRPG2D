using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人血量控制
/// </summary>
public class EnemyHealthBarManager : SingletonMonoBehvior<EnemyHealthBarManager>
{
    /// <summary>
    /// 敌人血条所在的canvas
    /// </summary>
    public Canvas parentCanvas;

    /// <summary>
    /// enemy和血条物体的对应关系
    /// </summary>
    private Dictionary<Enemy, Image> enemyHealthBarDictionary = new Dictionary<Enemy, Image>();

    private void OnEnable()
    {
        EventHandler.OnEnemyHealthUIBarChangeEvent += OnHealthBarChange;
        EventHandler.OnDestroyHealthUIBarEvent += DestroyHealthBar;
    }

    private void OnDisable()
    {
        EventHandler.OnEnemyHealthUIBarChangeEvent -= OnHealthBarChange;
        EventHandler.OnDestroyHealthUIBarEvent -= DestroyHealthBar;
    }


    /// <summary>
    /// 设置血条在画布上的位置
    /// </summary>
    /// <param name="enemy">enemy实例</param>
    /// <param name="healthBarInstance">血条实例</param>
    private void SetHealthBarRectTransfromPositionOnParentCanvas(Enemy enemy, GameObject healthBarInstance)
    {
        RectTransform rectTransform;
        if (enemy.curHealthBar == null)
        {
            // 还未生成血条时，取生成实例
            rectTransform = healthBarInstance.GetComponent<RectTransform>();

        }
        else
        {
            // 已经生成血条时，取enemy绑定的实例
            rectTransform = enemy.curHealthBar.GetComponent<RectTransform>();
        }
        Vector2 screenPoint = Camera.main.WorldToScreenPoint((Vector2)enemy.transform.position + enemy.healthBarOffset);
        rectTransform.position = screenPoint;

        

    }

    /// <summary>
    /// 更新血条百分比
    /// </summary>
    private void SetHealthBarFillAmount(Character character,Enemy enemy,GameObject healthBarInstance)
    {
        // 获取血条
        Image healthImage = enemyHealthBarDictionary.ContainsKey(enemy)? 
            enemyHealthBarDictionary[enemy] : 
            healthBarInstance.GetComponentsInChildren<Image>().ToList().Find(i=>i.CompareTag(Tags.ENEMYHEALTHBAR));
        
        // 计算百分比
        float persentage = character.currentHealth / character.maxHealth;

        //发送百分比
        healthImage.fillAmount = persentage;

        // 更新字典信息
        if(enemyHealthBarDictionary.ContainsKey(enemy) )
        {
            enemyHealthBarDictionary[enemy] = healthImage;
        }
    }


    /// <summary>
    /// 血条变化
    /// </summary>
    /// <param name="character"></param>
    /// <param name="enemy"></param>
    private void OnHealthBarChange(Character character,Enemy enemy, GameObject healthBar)
    {
        
        if (!enemyHealthBarDictionary.ContainsKey(enemy))
        {
            // 未生成血条，需要生成血条
            GameObject healthBarInstance = Instantiate(enemy.healthPrefab, parentCanvas.transform);
            
            if (healthBarInstance != null)
            {
                // 设置在canvas上的位置
                SetHealthBarRectTransfromPositionOnParentCanvas(enemy, healthBarInstance);
                // 更新血量
                SetHealthBarFillAmount(character,enemy,healthBarInstance);
                
                // 关联enemy
                enemy.curHealthBar = healthBarInstance;

                Image image = healthBarInstance.GetComponentsInChildren<Image>().ToList().Find(i => i.CompareTag(Tags.ENEMYHEALTHBAR));

                enemyHealthBarDictionary.Add(enemy,image);
            }
        }
        else
        {
            // 更新位置
            SetHealthBarRectTransfromPositionOnParentCanvas(enemy, healthBar);
            if (character != null)
            {
                // 更新血量
                SetHealthBarFillAmount(character, enemy, healthBar);
            }
        }
        
    }

    /// <summary>
    /// 销毁血条
    /// </summary>
    /// <param name="enemy"></param>
    private void DestroyHealthBar(Enemy enemy)
    {
        //删除字典中的信息
        enemyHealthBarDictionary.Remove(enemy);
        //销毁对应的血条物体
        Destroy(enemy.curHealthBar);
    }
}
