using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Match : MonoBehaviour
{   
    public Vector3 StartPosition { get; private set; }

    [SerializeField] private float _dragHeigh;

    private Coroutine _dragControl;
    private Coroutine _dropControl;

    private void Awake()
    {
        StartPosition = transform.position;
    }

    public void Drag()
    {
        if (_dragControl != null || _dropControl != null) 
        {
            StopAllCoroutines();
        }

        _dragControl = StartCoroutine(DragControl());
    }

    private IEnumerator DragControl()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        while(true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); 

            if (groundPlane.Raycast(ray,out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                transform.position = new Vector3(worldPosition.x, _dragHeigh, worldPosition.z);
            }

            yield return null;
        }
    }

    public void Drop(Vector3 targetPosition)
    {
        if (_dragControl != null || _dropControl != null)
        {
            StopAllCoroutines();
        }

        _dropControl = StartCoroutine(DropControl(targetPosition));
    }

    private IEnumerator DropControl(Vector3 targetPosition)
    {
        float duration = 0.2f;
        float elapsed = 0f;
        Vector3 currentPosition = transform.position;
        targetPosition = new Vector3(targetPosition.x,_dragHeigh, targetPosition.z);

        while (elapsed < duration)
        {
            float t = Mathf.Pow(elapsed / duration, 2);
            transform.position = Vector3.Lerp(currentPosition, targetPosition, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
