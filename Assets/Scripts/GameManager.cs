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


    public bool Paused { get; private set; }

    public GameObject player;

    public GameObject pauseCanvas;
    public GameObject eventSys;
    public bool dev;

    int currentLevel = 0;

    public L15Manager l15Manager;

    public LevelScene StartLevel;

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
        DontDestroyOnLoad(pauseCanvas);
        DontDestroyOnLoad(eventSys);
        DontDestroyOnLoad(player);
    }

    private void Start()
    {
        pauseCanvas.SetActive(false);
        //devConsole.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene((int)StartLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "Title")
            {
                if (Time.timeScale == 0)
                {
                    pauseCanvas.SetActive(false);
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        if (dev)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentLevel++;
                LoadLevel("Level " + currentLevel);
            }
        }
    }

    void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        
        if (s.name != "Title" && s.name != "Blank Screen")
        {
            StopAllCoroutines();
            player.SetActive(true);
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            // Display on screen what level they are on with fading text
            levelText.gameObject.SetActive(true);


            if (reloadBoss)
            {
                StartCoroutine(WaitForLevelLoad());
                reloadBoss = false;
            }

            if (s.name == "Tutorial")
            {
                StartCoroutine(TutorialFadeText());
            } else
            {
                currentLevel = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);
                levelText.text = s.name;
                StartCoroutine(FadeText());
            }
        }
    }

    // coroutine to fade text out
    private IEnumerator FadeText()
    {
        levelText.CrossFadeAlpha(1, 1, false);

        yield return new WaitForSeconds(1);
        if (currentLevel == 15)
        {
            levelText.CrossFadeColor(Color.red, 1, false, true);
            yield return new WaitForSeconds(1);
        }
        levelText.CrossFadeAlpha(0, 2, false);

        

        yield return new WaitForSeconds(3);
        levelText.CrossFadeColor(Color.white, 0, false, false);
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

        levelText.text = "Left Click to throw a sonar, hold to throw further";
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

    [Space(20)]
    [SerializeField] GameObject bossDamagePrefab;

    private bool reloadBoss = false;

    public void DamageBoss()
    {
        // Spawn Cube and drop on boss
        Instantiate(bossDamagePrefab, new Vector3(0, 20, 0), Quaternion.identity);
    }

    public void ReloadScene(bool atBoss = false)
    {
        reloadBoss = atBoss;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator WaitForLevelLoad()
    {
        yield return null;
        l15Manager.RestartBoss();
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

    }

    public void LoadLevel(string name)
    {
        if (l15Manager)
        {
            l15Manager.NextLevel();
        } else
        {
            SceneManager.LoadScene(name);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        Paused = true;
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.SetActive(true);
    }

    [Space(20)]
    [Header("Game Win Stuff")]
    public GameObject gameWinScreen;
    public GameObject pausePanel;

    private IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);

        var async = SceneManager.LoadSceneAsync("Blank Screen");

        yield return new WaitForSeconds(0.5f); 
        while (!async.isDone)
        {
            yield return null;
        }

        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.SetActive(true);
        pausePanel.SetActive(false);
        gameWinScreen.SetActive(true);
    }
    public void GameWin()
    {
        StartCoroutine(WaitForSeconds(10));
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ToTitleScreen()
    {

        SceneManager.LoadScene("Title");
    }

}

public enum LevelScene
{
    None,
    Title,
    Tutorial,
    Level_1,
    Level_2,
    Level_3,
    Level_4,
    Level_5,
    Level_6,
    Level_7,
    Level_8,
    Level_9,
    Level_10,
    Level_11,
    Level_12,
    Level_13,
    Level_14,
    Level_15,

}