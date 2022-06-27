using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] private string _toScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IPlayer>() != null)
        {
            SceneManager.LoadScene(_toScene);
        }
    }
}
