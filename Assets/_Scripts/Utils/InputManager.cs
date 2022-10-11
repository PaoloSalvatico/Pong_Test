using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    private GameInputs _input;

    protected override void Awake()
    {
        base.Awake();
        _input = new GameInputs();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.UI.Pause.performed += PerformPause;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.UI.Pause.performed -= PerformPause;
    }

    public Vector2 Player1MoveValue => _input.Player1.Movement.ReadValue<Vector2>();
    public Vector2 Player2MoveValue => _input.Player2.Movement.ReadValue<Vector2>();
    public Vector2 PalyerMoveMouse => _input.UI.OpenUI.ReadValue<Vector2>();

    public delegate void PausePerformed();
    public PausePerformed OnPausePerformed;

    private void PerformPause(InputAction.CallbackContext context)
    {
        if (OnPausePerformed == null) return;
        OnPausePerformed();
    }

}
