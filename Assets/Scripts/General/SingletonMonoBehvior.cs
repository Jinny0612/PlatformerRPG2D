
using UnityEngine;

/// <summary>
/// ���п�����ӵ���Ϸ�����ϵ���Ϸ�ű��ĵ���������
/// </summary>
public abstract class SingletonMonoBehvior<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// ʵ��
    /// </summary>
    private static T instance;

    /// <summary>
    /// ��ȡʵ��
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }

    /// <summary>
    /// �����ĳ�ʼ��
    /// �ɱ��̳��า��
    /// Awake��������Ϸ�����ʵ��������ʱ���ã���ʹ�ű���������ã�Ҳ��ִ��Awake����
    /// </summary>
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
