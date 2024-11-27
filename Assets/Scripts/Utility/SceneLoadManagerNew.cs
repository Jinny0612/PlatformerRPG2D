using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManagerNew : SingletonMonoBehvior<SceneLoadManagerNew>
{

    [Header("基础参数")]

    /// <summary>
    /// 渐入渐出时间
    /// </summary>
    public float fadeDuration;
    /// <summary>
    /// player  在inspector中绑定，因为刚进入游戏这个物体不可用无法获取，只能这样绑定
    /// </summary>
    [SerializeField] private PlayerController player;
    /// <summary>
    /// 跟随player的相机
    /// </summary>
    [SerializeField] private CinemachineVirtualCamera followPlayerCamera;

    /// <summary>
    /// 刚进入时的位置坐标
    /// </summary>
    public Vector3 firstEnterGamePlayerPosition;
    /// <summary>
    /// 当前已加载的场景
    /// </summary>
    [SerializeField] private GameSceneSO currentLoadScene;

    

    [Header("广播")] 
    /// <summary>
    /// 场景加载淡入淡出广播事件
    /// </summary>
    public FadeEventSO fadeEvent;
    


    [Header("场景")]
    /// <summary>
    /// 初始场景
    /// </summary>
    public GameSceneSO firstLoadScene;
    /// <summary>
    /// 主菜单场景
    /// </summary>
    public GameSceneSO menuScene;

    #region 私有变量
    /// <summary>
    /// 将要加载的场景
    /// </summary>
    [SerializeField]private GameSceneSO sceneToGo;
    private Vector3 positionToGo;
    private bool fadeScene;
    /// <summary>
    /// 是否正在加载
    /// </summary>
    private bool isLoading;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }


    private void Start()
    {
       
        // 加载主菜单场景
        EventHandler.CallSceneLoadEvent(menuScene, firstEnterGamePlayerPosition, true);

        // 加载首个游戏场景
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

    #region 事件绑定方法

    /// <summary>
    /// 加载场景事件
    /// </summary>
    /// <param name="sceneToLoad">要加载的场景</param>
    /// <param name="positionToLoad">player在新场景的初始位置</param>
    /// <param name="fadeScene">是否淡入淡出</param>
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
            //暂时关闭player
            SetPlayerActiveState(false);
            //Debug.Log("start close player.  player active = " + player.isActiveAndEnabled);

            LoadNewScene();
            
        }
        


    }

    /// <summary>
    /// 场景加载完成后执行的事件
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
     
        // 此时场景已经加载完成，因此当前场景为之前设置好的将要加载的场景
        currentLoadScene = sceneToGo;
        
        if (fadeScene)
        {
            
            //变透明
            fadeEvent.FadeOut(fadeDuration);
        }
        //场景彻底完成切换，不再处于加载中
        isLoading = false;
        
        if (currentLoadScene.sceneType == SceneType.Menu)
        {
            // 当前是主菜单场景，禁止UI按键
            UIManager.Instance.SetInputControlUIState(false);
        }

        if (currentLoadScene.sceneType == SceneType.Location)
        {
            // UI按键可用
            UIManager.Instance.SetInputControlUIState(true);
           
            // 将player放到预定的位置
            player.transform.position = positionToGo;
            SetPlayerActiveState(true);

            GameObject enemyCanvas = GameObject.Find("EnemyHeaderCanvas");
            if(enemyCanvas != null)
            {
                enemyCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
            }

            // 更新character基本信息 血量等。。。
            EventHandler.CallResetCharacterBasicInfoWhenLoadSceneEvent();

           
            //广播场景加载完成事件
            //afterSceneLoadedEvent.CallEvent();
            EventHandler.CallAfterSceneLoadEvent();

            // 如果player未激活则激活
            /*if (!player.gameObject.activeInHierarchy)
            {
                player.gameObject.SetActive(true);
                followPlayerCamera.gameObject.SetActive(true);
            }*/
        }

    }


    #endregion

    #region 协程方法

    /// <summary>
    /// 卸载前一个场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnloadPreviousScene()
    {
       
        if (fadeScene)
        {
            //场景变黑
            fadeEvent.FadeIn(fadeDuration);

        }
        //等待渐入渐出结束后再 继续执行
        yield return new WaitForSeconds(fadeDuration);

        // 广播场景卸载事件，UI不显示
        //sceneUnloadedEvent.CallLoadRequestEvent(sceneToGo, positionToGo, true);
        //EventHandler.CallSceneUnloadEvent(sceneToGo,positionToGo, true);

        EventHandler.CallSetPlayerStatusBarEvent(sceneToGo);

        //卸载当前场景
        yield return currentLoadScene.sceneReference.UnLoadScene();

        //暂时关闭player，避免player一直掉落的问题
        SetPlayerActiveState(false);

        //加载新场景
        LoadNewScene();

    }


    #endregion

    /// <summary>
    /// 设置player的active状态
    /// </summary>
    /// <param name="active"></param>
    private void SetPlayerActiveState(bool active)
    {
        player.gameObject.SetActive(active);
        followPlayerCamera.gameObject.SetActive(active);
        
    }

    /// <summary>
    /// 加载新场景
    /// </summary>
    private void LoadNewScene()
    {
        
        var loadingOption = sceneToGo.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        // 等待上面场景加载完成后触发事件
        loadingOption.Completed += OnLoadCompleted;
    }


    /// <summary>
    /// 进行新游戏加载初始场景
    /// </summary>
    private void NewGame()
    {
        sceneToGo = firstLoadScene;
        // 广播场景加载请求
        //loadSceneEvent.CallLoadRequestEvent(sceneToGo, firstEnterGamePlayerPosition, true);
        
        EventHandler.CallSceneLoadEvent(sceneToGo, firstEnterGamePlayerPosition, true);
    }

}
