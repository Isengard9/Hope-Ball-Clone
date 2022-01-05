using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameStarted = false;//Controls for is game started
    public static bool isGameEnded = false;//Controls for is game ended

   
    private int maxHeight = 0;
    public int _maxHeight
    {
        get { return maxHeight; }
        set { maxHeight = value;
            PlayerPrefs.SetInt("MaxHeight",value);
            UIManager.instance.ChangeHighScore(value);
        }
    }//Player max height


    private int currentHeight = 0;
    public int _currentHeight
    {
        get { return currentHeight; }
        set { currentHeight = value; UIManager.instance.ChangeCurrentScore(value); }
    }//Player current height


    [SerializeField]
    GameObject HighScorePrefab;//High score line prefab
    private void Awake()
    {
        if (instance == null)
            instance = this;


    }

    private void Start()
    {
        InitializeSystem();
       
    }

    private void Update()
    {
        if(BallPhysics.instance !=null && _currentHeight < BallPhysics.instance.transform.position.y)//Control the player reach max height
        {
            _currentHeight = Mathf.FloorToInt(BallPhysics.instance.transform.position.y);
            if(_maxHeight < _currentHeight)
            {
                _maxHeight = _currentHeight;
            }
        }
    }

    private void InitializeSystem()
    {
        _maxHeight = PlayerPrefs.GetInt("MaxHeight", 0);//Getting max height from prefs if isnot initalized set that 0
        if (_maxHeight < 0)
            _maxHeight = 0;

        _currentHeight = 0;

        if (_maxHeight > 4)//if the max height is bigger than 4 meter show the max height line else close that
        {
            HighScorePrefab.transform.position = new Vector3(0, _maxHeight, 0);
            HighScorePrefab.GetComponentInChildren<UnityEngine.UI.Text>().text = _maxHeight + "m";
        }
        else { HighScorePrefab.SetActive(false); }

        StartCoroutine(ControlIsGameStarted());
    }


    /// <summary>
    /// On game started works
    /// </summary>
    public void OnGameStarted()
    {

    }

    /// <summary>
    /// On game ended works
    /// </summary>
    public void OnGameEnded()
    {
        UIManager.instance.OpenEndPanel();
    }

    /// <summary>
    /// Restarting game using scene build index
    /// </summary>
    public void RestartGame() { UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); }


    /// <summary>
    /// Controlling is the player touch with two fingers
    /// else the game not starting
    /// </summary>
    /// <returns></returns>
    IEnumerator ControlIsGameStarted()
    {
        bool isStarted = false;

        while (!isStarted)
        {
            yield return new WaitForEndOfFrame();
            if(Input.touchCount>1)
            {
                isStarted = true;
                BallPhysics.instance.StartGame();
            }
        }
    }
}
