using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����Ѫ������
/// </summary>
public class EnemyHealthBarManager : SingletonMonoBehvior<EnemyHealthBarManager>
{
    /// <summary>
    /// ����Ѫ�����ڵ�canvas
    /// </summary>
    public Canvas parentCanvas;

    /// <summary>
    /// enemy��Ѫ������Ķ�Ӧ��ϵ
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
    /// ����Ѫ���ڻ����ϵ�λ��
    /// </summary>
    /// <param name="enemy">enemyʵ��</param>
    /// <param name="healthBarInstance">Ѫ��ʵ��</param>
    private void SetHealthBarRectTransfromPositionOnParentCanvas(Enemy enemy, GameObject healthBarInstance)
    {
        RectTransform rectTransform;
        if (enemy.curHealthBar == null)
        {
            // ��δ����Ѫ��ʱ��ȡ����ʵ��
            rectTransform = healthBarInstance.GetComponent<RectTransform>();

        }
        else
        {
            // �Ѿ�����Ѫ��ʱ��ȡenemy�󶨵�ʵ��
            rectTransform = enemy.curHealthBar.GetComponent<RectTransform>();
        }
        Vector2 screenPoint = Camera.main.WorldToScreenPoint((Vector2)enemy.transform.position + enemy.healthBarOffset);
        rectTransform.position = screenPoint;

        

    }

    /// <summary>
    /// ����Ѫ���ٷֱ�
    /// </summary>
    private void SetHealthBarFillAmount(Character character,Enemy enemy,GameObject healthBarInstance)
    {
        // ��ȡѪ��
        Image healthImage = enemyHealthBarDictionary.ContainsKey(enemy)? 
            enemyHealthBarDictionary[enemy] : 
            healthBarInstance.GetComponentsInChildren<Image>().ToList().Find(i=>i.CompareTag(Tags.ENEMYHEALTHBAR));
        
        // ����ٷֱ�
        float persentage = character.currentHealth / character.maxHealth;

        //���Ͱٷֱ�
        healthImage.fillAmount = persentage;

        // �����ֵ���Ϣ
        if(enemyHealthBarDictionary.ContainsKey(enemy) )
        {
            enemyHealthBarDictionary[enemy] = healthImage;
        }
    }


    /// <summary>
    /// Ѫ���仯
    /// </summary>
    /// <param name="character"></param>
    /// <param name="enemy"></param>
    private void OnHealthBarChange(Character character,Enemy enemy, GameObject healthBar)
    {
        
        if (!enemyHealthBarDictionary.ContainsKey(enemy))
        {
            // δ����Ѫ������Ҫ����Ѫ��
            GameObject healthBarInstance = Instantiate(enemy.healthPrefab, parentCanvas.transform);
            
            if (healthBarInstance != null)
            {
                // ������canvas�ϵ�λ��
                SetHealthBarRectTransfromPositionOnParentCanvas(enemy, healthBarInstance);
                // ����Ѫ��
                SetHealthBarFillAmount(character,enemy,healthBarInstance);
                
                // ����enemy
                enemy.curHealthBar = healthBarInstance;

                Image image = healthBarInstance.GetComponentsInChildren<Image>().ToList().Find(i => i.CompareTag(Tags.ENEMYHEALTHBAR));

                enemyHealthBarDictionary.Add(enemy,image);
            }
        }
        else
        {
            // ����λ��
            SetHealthBarRectTransfromPositionOnParentCanvas(enemy, healthBar);
            if (character != null)
            {
                // ����Ѫ��
                SetHealthBarFillAmount(character, enemy, healthBar);
            }
        }
        
    }

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    /// <param name="enemy"></param>
    private void DestroyHealthBar(Enemy enemy)
    {
        //ɾ���ֵ��е���Ϣ
        enemyHealthBarDictionary.Remove(enemy);
        //���ٶ�Ӧ��Ѫ������
        Destroy(enemy.curHealthBar);
    }
}
