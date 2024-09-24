using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L15Manager : MonoBehaviour
{

    [SerializeField] int currentLevel = 0;

    public List<GameObject> levelGameObjects = new List<GameObject>();

    void Start()
    {
        GameManager.Instance.l15Manager = this;
    }

    public void NextLevel()
    {
        levelGameObjects[currentLevel].SetActive(false);
        currentLevel++;
        levelGameObjects[currentLevel].SetActive(true);

        GameManager.Instance.player.transform.position = levelGameObjects[currentLevel].GetComponent<Level>().spawnpoint.position;

    }

    public void RestartBoss()
    {
        foreach (GameObject g in levelGameObjects)
        {
            g.SetActive(false);
            if (g.name == "L15")
            {
                g.SetActive(true);
            }
        }
    }
}
