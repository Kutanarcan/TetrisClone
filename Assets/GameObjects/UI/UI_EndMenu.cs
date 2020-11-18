using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndMenu : MonoBehaviour
{
    [SerializeField]
    Canvas endMenuCanvas;

    private void Awake()
    {
        BlockSpawner.OnGameFinished += OnGameFinished;
    }

    private void OnGameFinished()
    {
        endMenuCanvas.enabled = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        BlockSpawner.OnGameFinished -= OnGameFinished;
    }
}
