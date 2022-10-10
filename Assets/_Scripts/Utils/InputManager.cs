using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public Vector2 Player1MoveValue => _input.Player1.Movement.ReadValue<Vector2>();
    public Vector2 Player2MoveValue => _input.Player2.Movement.ReadValue<Vector2>();
}
