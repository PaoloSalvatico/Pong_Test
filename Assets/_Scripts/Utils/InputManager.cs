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
        _input.Player1.Movement.started += PerformMovement;
        _input.Player1.Movement.canceled += PerformStopMovement;
        _input.Player2.Movement.started += PerformMovement;
        _input.Player2.Movement.canceled += PerformStopMovement;
        _input.UI.Pause.performed += PerformPause;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player1.Movement.started -= PerformMovement;
        _input.Player1.Movement.canceled -= PerformStopMovement;
        _input.Player2.Movement.started -= PerformMovement;
        _input.Player2.Movement.canceled -= PerformStopMovement;
        _input.UI.Pause.performed -= PerformPause;
    }

    /// <summary>
    /// Getter vector 2 input
    /// </summary>
    public Vector2 Player1MoveValue => _input.Player1.Movement.ReadValue<Vector2>();
    public Vector2 Player2MoveValue => _input.Player2.Movement.ReadValue<Vector2>();

    #region Delegate Events

    #region Player Movement
    public delegate void MovePerformed();
    public MovePerformed OnMovePerformed;

    private void PerformMovement(InputAction.CallbackContext context)
    {
        if (OnMovePerformed == null) return;
        OnMovePerformed();
    }

    public delegate void StopMovePerformed();
    public StopMovePerformed OnStopMovePerformed;

    public void  PerformStopMovement(InputAction.CallbackContext context)
    {
        if (OnStopMovePerformed == null) return;
        OnStopMovePerformed();
    }
    #endregion

    #region UI
    public delegate void PausePerformed();
    public PausePerformed OnPausePerformed;

    private void PerformPause(InputAction.CallbackContext context)
    {
        if (OnPausePerformed == null) return;
        OnPausePerformed();
    }
    #endregion

    #endregion
}
