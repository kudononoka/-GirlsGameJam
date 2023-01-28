using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerTestSctipt : MonoBehaviour
{
    [SerializeField] PlayerCursor _pc = default;
    Text _pow = default;

    // Start is called before the first frame update
    void Start()
    {
        _pow = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _pow.text = _pc.Power.ToString();
    }
}
