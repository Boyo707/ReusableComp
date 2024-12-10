using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _gameOver;

    private KeyboardInput _kb;

    private bool _paused;

    private Scene _currentScene;

    // Start is called before the first frame update
    void Start()
    {
        _kb = _player.GetComponent<KeyboardInput>();
        _currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (_kb.PauseInput)
        {
            _pause.SetActive(true);
        }


        /*if (_player.GetComponent<Health>().isDead)
        {
            //_paused = false;
            _pause.SetActive(false);
            _gameOver.SetActive(true);
        }*/

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Continue()
    {
        Debug.Log("Continue");
        _pause.SetActive(false);
    }

    public void Retry()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene(_currentScene.name);
    }
}
