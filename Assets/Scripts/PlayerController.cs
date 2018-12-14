using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int _hpMax = 100;
    public int hpMax { get => _hpMax; set => _hpMax = value; }
    private int _hpCurrent;
    public int hpCurrent { get => _hpCurrent; set => _hpCurrent = value; }
    [SerializeField]
    private int _damageBase = 10;
    public int damageBase { get => _damageBase; set => _damageBase = value; }

    public PlayerController instance;
    // Start is called before the first frame update
    void Start()
    {
        hpCurrent = hpMax;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    
}
