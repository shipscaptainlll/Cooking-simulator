using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour, IContactable
{
    [SerializeField] private Animator _animator;

    private bool IsOpened;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetContacted()
    {
        if (IsOpened)
        {
            CloseDoor();
        } else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        _animator.CrossFade("Open", 0.2f, 0);
        IsOpened = true;
    }

    private void CloseDoor()
    {
        _animator.CrossFade("Close", 0.2f, 0);
        IsOpened = false;
    }


}
