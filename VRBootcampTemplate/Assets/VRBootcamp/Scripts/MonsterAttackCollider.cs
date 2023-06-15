using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCollider : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            gameManager.PlayerDie();
        }
    }
}
