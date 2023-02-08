using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;
    string _sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange(string sceneName)
    {
        if (sceneName == "Title" || sceneName == "Normal" || sceneName == "Hard")
        {
            _sceneName = sceneName;
            SpawnManager._count = 0;
            StartCoroutine(Audio());
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator Audio()
    {
        _audioSource.PlayOneShot(_audioClip);
        yield return new WaitForSeconds(0.35f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
    }
}
