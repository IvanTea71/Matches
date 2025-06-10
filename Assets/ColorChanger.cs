using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _endColor;

    private Renderer _colorRenderer;
    private Color _startColor;

    private void Start()
    {
        _colorRenderer = GetComponent<Renderer>();
        _startColor = _colorRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Colorer>(out Colorer colorer))
        {
            _colorRenderer.material.color = _endColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Colorer>(out Colorer colorer))
        {
            _colorRenderer.material.color = _startColor;
        }
    }
}
