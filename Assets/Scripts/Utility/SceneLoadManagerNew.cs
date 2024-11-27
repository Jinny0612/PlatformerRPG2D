using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManagerNew : SingletonMonoBehvior<SceneLoadManagerNew>
{

    [Header("��������")]

    /// <summary>
    /// ���뽥��ʱ��
    /// </summary>
    public float fadeDuration;
    /// <summary>
    /// player  ��inspector�а󶨣���Ϊ�ս�����Ϸ������岻�����޷���ȡ��ֻ��������
    /// </summary>
    [SerializeField] private PlayerController player;
    /// <summary>
    /// ����player�����
    /// </summary>
    [SerializeField] private CinemachineVirtualCamera followPlayerCamera;

    /// <summary>
    /// �ս���ʱ��λ������
    /// </summary>
    public Vector3 firstEnterGamePlayerPosition;
    /// <summary>
    /// ��ǰ�Ѽ��صĳ���
    /// </summary>
    [SerializeField] private GameSceneSO currentLoadScene;

    

    [Header("�㲥")] 
    /// <summary>
    /// �������ص��뵭���㲥�¼�
    /// </summary>
    public FadeEventSO fadeEvent;
    


    [Header("����")]
    /// <summary>
    /// ��ʼ����
    /// </summary>
    public GameSceneSO firstLoadScene;
    /// <summary>
    /// ���˵�����
    /// </summary>
    public GameSceneSO menuScene;

    #region ˽�б���
    /// <summary>
    /// ��Ҫ���صĳ���
    /// </summary>
    [SerializeField]private GameSceneSO sceneToGo;
    private Vector3 positionToGo;
    private bool fadeScene;
    /// <summary>
    /// �Ƿ����ڼ���
    /// </summary>
    private bool isLoading;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }


    private void Start()
    {
       
        // �������˵�����
        EventHandler.CallSceneLoadEvent(menuScene, firstEnterGamePlayerPosition, true);

        // �����׸���Ϸ����
        //EventHandler.CallSceneLoadEvent(firstLoadScene, firstEnterGamePlayerPosition, true);
        
    }

    private void OnEnable()
    {
        //loadSceneEvent.LoadRequestEvent += LoadScene;
        //newGameEvent.OnEventCalled += NewGame;


        EventHandler.OnNewGameEvent += NewGame;
        
        EventHandler.OnSceneLoadEvent += LoadScene;
    }


    private void OnDisable()
    {
        //loadSceneEvent.LoadRequestEvent -= LoadScene;
        //newGameEvent.OnEventCalled -= NewGame;


        EventHandler.OnNewGameEvent -= NewGame;
        EventHandler.OnSceneLoadEvent -= LoadScene;
    }

    #region �¼��󶨷���

    /// <summary>
    /// ���س����¼�
    /// </summary>
    /// <param name="sceneToLoad">Ҫ���صĳ���</param>
    /// <param name="positionToLoad">player���³����ĳ�ʼλ��</param>
    /// <param name="fadeScene">�Ƿ��뵭��</param>
    private void LoadScene(GameSceneSO sceneToLoad, Vector3 positionToLoad, bool fadeScene)
    {
        if (isLoading)
        {
            return;
        }

        

        isLoading = true;
        sceneToGo = sceneToLoad;
        positionToGo = positionToLoad;
        this.fadeScene = fadeScene;

        

        if (currentLoadScene != null)
        {
            
            StartCoroutine(UnloadPreviousScene());
        }
        else
        {
            //��ʱ�ر�player
            SetPlayerActiveState(false);
            //Debug.Log("start close player.  player active = " + player.isActiveAndEnabled);

            LoadNewScene();
            
        }
        


    }

    /// <summary>
    /// ����������ɺ�ִ�е��¼�
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
     
        // ��ʱ�����Ѿ�������ɣ���˵�ǰ����Ϊ֮ǰ���úõĽ�Ҫ���صĳ���
        currentLoadScene = sceneToGo;
        
        if (fadeScene)
        {
            
            //��͸��
            fadeEvent.FadeOut(fadeDuration);
        }
        //������������л������ٴ��ڼ�����
        isLoading = false;
        
        if (currentLoadScene.sceneType == SceneType.Menu)
        {
            // ��ǰ�����˵���������ֹUI����
            UIManager.Instance.SetInputControlUIState(false);
        }

        if (currentLoadScene.sceneType == SceneType.Location)
        {
            // UI��������
            UIManager.Instance.SetInputControlUIState(true);
           
            // ��player�ŵ�Ԥ����λ��
            player.transform.position = positionToGo;
            SetPlayerActiveState(true);

            GameObject enemyCanvas = GameObject.Find("EnemyHeaderCanvas");
            if(enemyCanvas != null)
            {
                enemyCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
            }

            // ����character������Ϣ Ѫ���ȡ�����
            EventHandler.CallResetCharacterBasicInfoWhenLoadSceneEvent();

           
            //�㲥������������¼�
            //afterSceneLoadedEvent.CallEvent();
            EventHandler.CallAfterSceneLoadEvent();

            // ���playerδ�����򼤻�
            /*if (!player.gameObject.activeInHierarchy)
            {
                player.gameObject.SetActive(true);
                followPlayerCamera.gameObject.SetActive(true);
            }*/
        }

    }


    #endregion

    #region Э�̷���

    /// <summary>
    /// ж��ǰһ������
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnloadPreviousScene()
    {
       
        if (fadeScene)
        {
            //�������
            fadeEvent.FadeIn(fadeDuration);

        }
        //�ȴ����뽥���������� ����ִ��
        yield return new WaitForSeconds(fadeDuration);

        // �㲥����ж���¼���UI����ʾ
        //sceneUnloadedEvent.CallLoadRequestEvent(sceneToGo, positionToGo, true);
        //EventHandler.CallSceneUnloadEvent(sceneToGo,positionToGo, true);

        EventHandler.CallSetPlayerStatusBarEvent(sceneToGo);

        //ж�ص�ǰ����
        yield return currentLoadScene.sceneReference.UnLoadScene();

        //��ʱ�ر�player������playerһֱ���������
        SetPlayerActiveState(false);

        //�����³���
        LoadNewScene();

    }


    #endregion

    /// <summary>
    /// ����player��active״̬
    /// </summary>
    /// <param name="active"></param>
    private void SetPlayerActiveState(bool active)
    {
        player.gameObject.SetActive(active);
        followPlayerCamera.gameObject.SetActive(active);
        
    }

    /// <summary>
    /// �����³���
    /// </summary>
    private void LoadNewScene()
    {
        
        var loadingOption = sceneToGo.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        // �ȴ����泡��������ɺ󴥷��¼�
        loadingOption.Completed += OnLoadCompleted;
    }


    /// <summary>
    /// ��������Ϸ���س�ʼ����
    /// </summary>
    private void NewGame()
    {
        sceneToGo = firstLoadScene;
        // �㲥������������
        //loadSceneEvent.CallLoadRequestEvent(sceneToGo, firstEnterGamePlayerPosition, true);
        
        EventHandler.CallSceneLoadEvent(sceneToGo, firstEnterGamePlayerPosition, true);
    }

}
