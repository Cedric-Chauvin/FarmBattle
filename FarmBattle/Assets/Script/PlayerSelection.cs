using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public enum STATUS
    {
        UNSELECTED,
        SELECTED,
        VALIDATED
    }

    [HideInInspector]
    public STATUS status;
    [HideInInspector]
    public int playerId;

    private void Awake()
    {
        status = STATUS.UNSELECTED;
        playerId = -1;
    }
}
