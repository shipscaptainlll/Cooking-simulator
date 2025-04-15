using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [Header("Setups")]
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private LayerMask _raycastableLayer;
    [SerializeField] private PlayerBlocker _playerBlocker;

    private RaycastHit _lastRaycast;
    public RaycastHit LastRaycastHit => _lastRaycast;


    [Header("Settings")]
    [SerializeField] private float _raycastDistance;

    public event Action<RaycastHit> OnFoundRaycastable = delegate { };
    public event Action OnLostRaycastable = delegate { };

    private void Start()
    {
        _inputManager.InputController.Player.LMB.started += (callback) => { RaycastContactable(); };
    }

    private void FixedUpdate()
    {
        Raycast();
        Debug.DrawRay(_raycastPoint.position, transform.forward * _raycastDistance, Color.green);

    }

    private void Raycast()
    {
        if (_playerBlocker.RaycastingBlocked)
        {
            return;
        }

        if (Physics.Raycast(_raycastPoint.position, transform.forward, out RaycastHit hit, _raycastDistance))
        {
            if (((1 << hit.transform.gameObject.layer) & _raycastableLayer) != 0)
            {
                if (_lastRaycast.transform == null && hit.transform != null)
                {
                    _lastRaycast = hit;
                    OnFoundRaycastable?.Invoke(hit);
                }
                else if (_lastRaycast.transform != null && hit.transform == null)
                {
                    _lastRaycast = hit;
                    OnLostRaycastable?.Invoke();
                }
                else if (_lastRaycast.transform != null && hit.transform != null
                    && _lastRaycast.transform != hit.transform)
                {
                    _lastRaycast = hit;
                    OnLostRaycastable?.Invoke();
                    OnFoundRaycastable?.Invoke(hit);
                }
            }
            else
            {
                if (_lastRaycast.transform != null)
                {
                    _lastRaycast = new RaycastHit();
                    OnLostRaycastable?.Invoke();
                }
                _lastRaycast = new RaycastHit();
                return;
            }
        }
        else
        {
            if (_lastRaycast.transform != null)
            {
                _lastRaycast = new RaycastHit();
                OnLostRaycastable?.Invoke();
            }
            _lastRaycast = new RaycastHit();
        }
    }


    private void RaycastContactable()
    {
        if (_playerBlocker.ContactingBlocked)
        {
            return;
        }

        if (Physics.Raycast(_raycastPoint.position, transform.forward, out RaycastHit hit, _raycastDistance))
        {
            if (((1 << hit.transform.gameObject.layer) & _raycastableLayer) != 0)
            {
                if (hit.transform != null && hit.transform.GetComponent<IContactable>() != null)
                {
                    hit.transform.GetComponent<IContactable>().GetContacted();
                }
            }
            else
            {
                return;
            }
        }
    }

}
