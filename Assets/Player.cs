using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private MatchMover _matchMover;

    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.DragAndDrop.started += ctx => _matchMover.TryFreeCell();
        _playerInput.Player.DragAndDrop.performed += ctx => _matchMover.Drag();
        _playerInput.Player.DragAndDrop.canceled += ctx => _matchMover.Drop();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
