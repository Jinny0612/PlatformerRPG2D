
/// <summary>
/// 输入控制管理
/// </summary>
public class InputControlManager : SingletonMonoBehvior<InputControlManager>
{
    private PlayerInputControls _inputControl;

    public PlayerInputControls InputControl { get { return _inputControl; } }

    protected override void Awake()
    {
        base.Awake();

        _inputControl = new PlayerInputControls();
    }

    private void OnEnable()
    {
        _inputControl.Enable();
    }

    private void OnDisable()
    {
        _inputControl.Disable();
    }

    /// <summary>
    /// 启用游戏控制
    /// </summary>
    public void EnableGameplayControl()
    {
        _inputControl.GamePlay.Enable();
    }

    /// <summary>
    /// 禁用游戏控制
    /// </summary>
    public void DisableGameplayControl()
    {
        _inputControl.GamePlay.Disable();
    }

    /// <summary>
    /// 启用UI控制
    /// </summary>
    public void EnableUIControl()
    {
        _inputControl.UI.Enable();
    }

    /// <summary>
    /// 禁用UI控制
    /// </summary>
    public void DisableUIControl()
    {
        _inputControl.UI.Disable();
    }


}
