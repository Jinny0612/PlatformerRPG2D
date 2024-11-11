using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(EnemyCodeDescriptionAttribute))]
public class EnemyCodeDescriptionDrawer : PropertyDrawer
{
    /// <summary>
    /// 设置属性高度
    /// 因为是根据物品代码显示物品描述，所以需要两倍的高度
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 2;
    }

    /// <summary>
    /// 绘制属性
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // todo:这里发现如果是在list下面显示的，那么前面的itemcode和itemDescription位置不对，需要往后面调整一下
        //使用beginproperty / endproperty 确保绘制逻辑适用于整个属性
        EditorGUI.BeginProperty(position, label, property);//开始绘制属性
        //绘制物品代码
        var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
        //绘制物品描述
        EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description",
            GetItemDescription(property.intValue));

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = newValue;
        }

        EditorGUI.EndProperty();//结束绘制
    }

    /// <summary>
    /// 获取物品描述
    /// </summary>
    /// <returns></returns>
    private string GetItemDescription(int enemyCode)
    {
        // 根据路径加载asset资源
        EnemyInfoSO enemyInfoSO = AssetDatabase.LoadAssetAtPath("Assets/DataSO/ObjectsInfoSO/EnemyInfoSO.asset", typeof(EnemyInfoSO)) as EnemyInfoSO;
        List<EnemyInfo> enemyList = enemyInfoSO.Enemies;
        EnemyInfo enemyInfo = enemyList.Find(i => enemyCode == i.enemyCode);
        if (enemyInfo != null)
        {
            return enemyInfo.enemyName;
        }
        return null;
    }
}
