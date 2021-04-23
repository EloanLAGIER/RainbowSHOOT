using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public int score;
    public Text go;
    public Text scoreT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()
    {
        go.text = "GAME OVER \nScore : " + scoreT.text + "\n (r) for restart";
        GameObject.FindObjectOfType<WaveManager>().game=false;
        foreach (TirAlien g in GameObject.FindObjectsOfType<TirAlien>())
        {
            Destroy(g.gameObject);
        }
        foreach (Alien g in GameObject.FindObjectsOfType<Alien>())
        {
            Destroy(g.gameObject);
        }
    }
}
