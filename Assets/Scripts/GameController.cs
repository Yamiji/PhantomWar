using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public enum TurnOrder { Player, Enemy, Animation};
    public TurnOrder turnOrder;

    public enum PlayerAction { Empty, Attack, Magic, Defend, Escape}
    public PlayerAction playerAction;

    [SerializeField]
    GameObject icicle;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        turnOrder = TurnOrder.Player;
        playerAction = PlayerAction.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyAttack(EnemyController source)
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        Vector3 startPos = source.instance.transform.position;
        Vector3 endPos = playerController.transform.position + new Vector3(1, 0, 0);

        GameController.instance.turnOrder = GameController.TurnOrder.Animation;
        var seq = LeanTween.sequence();
        seq.append(LeanTween.move(source.instance.gameObject, endPos, 1f).setEaseInCubic());
        seq.append(() => DealDamage(source, playerController));
        seq.append(LeanTween.move(source.instance.gameObject, startPos, 1f).setEaseOutCubic());
        seq.append(() => CheckEnemyTurn());
    }

    public void PlayerAttack(EnemyController target)
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        Vector3 startPos = playerController.transform.position;
        Vector3 endPos = target.instance.transform.position + new Vector3(-1, 0, 0);
        var seq = LeanTween.sequence();
        seq.append(LeanTween.move(playerController.gameObject, endPos, 1f).setEaseInCubic());
        seq.append(() => GameController.instance.DealDamage(target));

        seq.append(LeanTween.move(playerController.gameObject, startPos, 1f).setEaseOutCubic());
        seq.append(() => GameController.instance.turnOrder = GameController.TurnOrder.Enemy);
    }

    public void PlayerCastSpell(EnemyController target)
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        Vector3 rot = target.instance.transform.position - playerController.instance.transform.position;
        //rot = rot.normalized;
        GameObject spell = Instantiate(icicle, playerController.transform.position, Quaternion.identity);
        spell.GetComponent<Rigidbody2D>().AddForce(rot);
    }

    public void DealDamage(EnemyController target)
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        target.hpCurrent -= playerController.instance.damageBase;
        Debug.Log("Enemy HP = " + target.hpCurrent);
        if (target.hpCurrent <= 0)
        {
            target.Die();
        }
    }

    public void DealDamage(EnemyController source, PlayerController target)
    {
        target.hpCurrent -= source.damageBase;
        Debug.Log("Player HP = " + target.hpCurrent);
        if (target.hpCurrent <= 0)
        {
            target.Die();
        }
    }

    private void CheckEnemyTurn()
    {
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

        foreach(EnemyController e in enemies)
        {
            if (!e.instance.hasAttacked)
            {
                GameController.instance.turnOrder = GameController.TurnOrder.Enemy;
                return;
            }

        }

        foreach (EnemyController e in enemies)
            e.instance.hasAttacked = false;

        GameController.instance.turnOrder = GameController.TurnOrder.Player;
    }
}
