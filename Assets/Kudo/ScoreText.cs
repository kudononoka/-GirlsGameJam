using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = SpawnManager._count.ToString();
    }
}
