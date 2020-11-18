using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StartMenu : MonoBehaviour
{
    [SerializeField]
    Canvas startMenuCanvas;

    public static event System.Action OnStartButtonPressed;

    public void StartGame()
    {
        OnStartButtonPressed?.Invoke();
        startMenuCanvas.enabled = false;
    }
}
