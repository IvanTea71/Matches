using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    private void Start()
    {
        IsOccupied = false;
    }

    public void Occupy(bool status)
    {
        if (status == true)
        {
            Debug.Log("занял");
        }
        else
        {
            Debug.Log("освободил");
        }
        IsOccupied = status;
    }
}
