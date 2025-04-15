using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideCursor();
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }


}
