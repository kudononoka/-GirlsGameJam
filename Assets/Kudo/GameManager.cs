using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>制限時間</summary>
    [SerializeField, Header("制限時間"), Tooltip("制限時間")] float _timeLimit;
    /// <summary>Game中のタイマー</summary>
    float _timer;
    /// <summary>ゲームの状態</summary>
    GameState _state = GameState.Game;

    [SerializeField, Header("クリア時のシーン名")] string _clearSceneName;
    [SerializeField, Header("ゲームオーバー時のシーン名")] string _unclearSceneName;

    public GameState State { get { return _state; } set { _state = value; }}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _state = GameState.Clear;
        }

        if (_state != GameState.Game)
        {
            switch (_state)
            {
                case GameState.Clear:
                    SceneChange(_clearSceneName);
                    break;
                case GameState.UnClear:
                    SceneChange(_unclearSceneName);
                    break;
            }
        }
    }
    /// <summary>ゲームの状態管理をするenum</summary>
    public enum GameState
    {
        Game,
        /// <summary>ゲームクリア</summary>
        Clear,
        /// <summary>ゲームオーバー</summary>
        UnClear,
    }

    void SceneChange(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
