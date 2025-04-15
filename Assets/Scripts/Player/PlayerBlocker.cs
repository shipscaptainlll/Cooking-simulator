using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlocker : MonoBehaviour
{
    [Header("Optional dependencies")]
    [SerializeField] private StationContacter _stationContacter;
    private bool _stationUsed;
    public bool MovementBlocked { get; private set; }
    public bool RotationBlocked { get; private set; }
    public bool RaycastingBlocked { get; private set; }
    public bool ContactingBlocked { get; private set; }
    public bool ZoomingBlocked { get; private set; }
    public bool UIBlocked { get; private set; }
    public bool UIBlockedEscaping { get; private set; }



    void Awake()
    {
        if (_stationContacter != null)
        {
            _stationContacter.OnStartedUsing += () => { _stationUsed = true; UpdateBlockages(); };
            _stationContacter.OnFinishedUsing += () => { _stationUsed = false; UpdateBlockages(); };
        }
    }


    private void UpdateBlockages()
    {
        UpdateMovementBlockage();
        UpdateRotationBlockage();
        UpdateRaycastingBlockage();
        UpdateContactingBlockage();
        UpdateUIBlockage();
        UpdateUIBlockageEscaping();
    }

    private void UpdateMovementBlockage()
    {
        MovementBlocked = (
            _stationUsed);
    }

    private void UpdateRotationBlockage()
    {
        RotationBlocked = (
            _stationUsed);
    }

    private void UpdateRaycastingBlockage()
    {
        RaycastingBlocked = (
            _stationUsed);
    }

    private void UpdateContactingBlockage()
    {
        ContactingBlocked = (
            _stationUsed);
    }

    private void UpdateUIBlockage()
    {
        UIBlocked = (
            _stationUsed);
    }

    private void UpdateUIBlockageEscaping()
    {
        UIBlockedEscaping = (
            _stationUsed);
    }

}
