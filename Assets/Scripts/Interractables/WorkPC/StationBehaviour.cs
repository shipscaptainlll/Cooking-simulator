using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBehaviour : MonoBehaviour
{
    [SerializeField] private StationContacter _stationContacter;


    // Start is called before the first frame update
    void Start()
    {
        _stationContacter.OnStartedUsing += OpenStation;
        _stationContacter.OnFinishedUsing += CloseStation;
    }

    private void OpenStation()
    {
        
    }

    private void CloseStation()
    {
        
    }


}
