using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;

    [SerializeField] private TMP_Text levelText;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);

        Debug.Log(Instance.name);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Tutorial");
    }

    void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        StopAllCoroutines();
        if (s.name != "Title")
        {
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            // Display on screen what level they are on with fading text
            levelText.gameObject.SetActive(true);
            levelText.CrossFadeAlpha(1, 0, false);

            if (s.name == "Tutorial")
            {
                StartCoroutine(TutorialFadeText());
            } else
            {

                levelText.text = s.name;
                StartCoroutine(FadeText());
            }
        }
    }

    // coroutine to fade text out
    private IEnumerator FadeText()
    {
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 0, false);
        levelText.gameObject.SetActive(false);
    }

    private IEnumerator TutorialFadeText()
    {
        levelText.text = "Tutorial";
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "WASD to move";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Mouse to look around";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Left Click to sense, hold to throw further";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Find your way to the exit";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Each Exit is pulsing with Blue";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);

        levelText.text = "Good Luck!";
        yield return new WaitForSeconds(1);
        levelText.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
        levelText.CrossFadeAlpha(1, 1, false);


        levelText.gameObject.SetActive(false);
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
