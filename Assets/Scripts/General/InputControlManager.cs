
/// <summary>
/// ������ƹ���
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
    /// ������Ϸ����
    /// </summary>
    public void EnableGameplayControl()
    {
        _inputControl.GamePlay.Enable();
    }

    /// <summary>
    /// ������Ϸ����
    /// </summary>
    public void DisableGameplayControl()
    {
        _inputControl.GamePlay.Disable();
    }

    /// <summary>
    /// ����UI����
    /// </summary>
    public void EnableUIControl()
    {
        _inputControl.UI.Enable();
    }

    /// <summary>
    /// ����UI����
    /// </summary>
    public void DisableUIControl()
    {
        _inputControl.UI.Disable();
    }


}
