using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    Vector3 _origPos;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 10);
        _origPos = transform.position;
    }

    void Update()
    {
        Vector3 moveDirection = gameObject.transform.position - _origPos;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.instance.turnOrder = GameController.TurnOrder.Enemy;
        Destroy(gameObject);
    }
}
