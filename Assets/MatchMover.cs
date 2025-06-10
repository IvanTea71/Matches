using UnityEngine;
using UnityEngine.InputSystem;

public class MatchMover : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Match _currentMatch;
    
    public void TryFreeCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity,_layerMask))
        {
            if (hitInfo.transform.TryGetComponent<Cell>(out Cell cell))
            {
                if (cell.IsOccupied == true)
                {
                    cell.Occupy(false);
                }
            }
        }
    }

    public void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (hitInfo.transform.TryGetComponent<Match>(out Match match))
            {
                _currentMatch = match;
                _currentMatch.Drag();
            }
        }
    }

    public void Drop()
    {
        if (_currentMatch == null)
        {
            return;
        }            

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            if (hit.transform.TryGetComponent<Cell>(out Cell cell) && !cell.IsOccupied)
            {
                _currentMatch.Drop(cell.transform.position);
                cell.Occupy(true);
            }
            else
            {
                _currentMatch.Drop(_currentMatch.StartPosition);
            }
        }
        else
        {
            _currentMatch.Drop(_currentMatch.StartPosition);
        }

        _currentMatch = null;
    }
}
