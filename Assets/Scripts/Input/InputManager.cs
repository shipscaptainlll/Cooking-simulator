using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputController InputController;


    void Awake()
    {
        InputController = new InputController();
        InputController.Enable();
    }

    void OnDestroy()
    {
        InputController.Disable();
    }


}
