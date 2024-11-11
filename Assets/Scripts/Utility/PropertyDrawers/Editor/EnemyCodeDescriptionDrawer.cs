using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(EnemyCodeDescriptionAttribute))]
public class EnemyCodeDescriptionDrawer : PropertyDrawer
{
    /// <summary>
    /// �������Ը߶�
    /// ��Ϊ�Ǹ�����Ʒ������ʾ��Ʒ������������Ҫ�����ĸ߶�
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 2;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // todo:���﷢���������list������ʾ�ģ���ôǰ���itemcode��itemDescriptionλ�ò��ԣ���Ҫ���������һ��
        //ʹ��beginproperty / endproperty ȷ�������߼���������������
        EditorGUI.BeginProperty(position, label, property);//��ʼ��������
        //������Ʒ����
        var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
        //������Ʒ����
        EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description",
            GetItemDescription(property.intValue));

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = newValue;
        }

        EditorGUI.EndProperty();//��������
    }

    /// <summary>
    /// ��ȡ��Ʒ����
    /// </summary>
    /// <returns></returns>
    private string GetItemDescription(int enemyCode)
    {
        // ����·������asset��Դ
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
