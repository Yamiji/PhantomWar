using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int _hpMax = 100;
    public int hpMax { get => _hpMax; set => _hpMax = value; }
    private int _hpCurrent;
    public int hpCurrent { get => _hpCurrent; set => _hpCurrent = value; }
    [SerializeField]
    private int _damageBase = 10;
    public int damageBase { get => _damageBase; set => _damageBase = value; }


    private bool _hasAttacked = false;
    public bool hasAttacked { get => _hasAttacked; set => _hasAttacked = value; }

    
    public EnemyController instance;

    // Start is called before the first frame update
    void Start()
    {
        hpCurrent = hpMax;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.turnOrder == GameController.TurnOrder.Enemy)
        {
            if (!hasAttacked)
            {
                hasAttacked = true;
                GameController.instance.EnemyAttack(this);
            }

        }
    }

    private void OnMouseDown()
    {
        if(GameController.instance.turnOrder == GameController.TurnOrder.Player)
        {
            if (GameController.instance.playerAction == GameController.PlayerAction.Attack)
            {
                ButtonController.instance.UnselectAll();
                GameController.instance.playerAction = GameController.PlayerAction.Empty;
                GameController.instance.PlayerAttack(this);
            }
            if (GameController.instance.playerAction == GameController.PlayerAction.Magic)
            {
                ButtonController.instance.UnselectAll();
                GameController.instance.playerAction = GameController.PlayerAction.Empty;
                GameController.instance.PlayerCastSpell(this);
            }
        }
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    
}
