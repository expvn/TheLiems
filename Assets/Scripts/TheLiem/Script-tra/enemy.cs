using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Hit();
            Loadlever();
            
        }
    }
    private void Loadlever()
    {
        string currenSceneGame= SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currenSceneGame);
    }
    void Update()
    {
        
    }
}
