using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemy;
    public GameObject[] players;
    public GameObject negativeXAttack;
    public GameObject positiveXAttack;
    public GameObject negativeZAttack;
    public GameObject positiveZAttack;
    public PlayerController playerController;
    public GameManager gameManager;
    private float detectDistance = 10.0f;
    private Vector3 playerPos;

    public void StartAttack()
    {
        if (playerController.count == 0 && gameManager.gameTurn == false)
        {
            EnemyDetectCode();
        }
        if (playerController.count == 1 && gameManager.gameTurn == true)
        {
            EnemyDetectCode();
        }
    }

    public void EnemyDetectCode()
    {
        playerPos = player.transform.position;

        for (int i = 0; i < enemy.Length; i++)
        {
            if (playerPos == enemy[i].transform.position + new Vector3(0, 0, -detectDistance))
            {
                negativeXAttack.SetActive(true);
                NegativeXAttack();
            }
            if (playerPos == enemy[i].transform.position + new Vector3(0, 0, detectDistance))
            {
                positiveXAttack.SetActive(true);
                PositiveXAttack();
            }
            if (playerPos == enemy[i].transform.position + new Vector3(-detectDistance, 0, 0))
            {
                positiveZAttack.SetActive(true);
                PositiveZAttack();
            }
            if (playerPos == enemy[i].transform.position + new Vector3(detectDistance, 0, 0))
            {
                negativeZAttack.SetActive(true);
                NegativeZAttack();
            }
        }
    }

    public void NegativeXAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == negativeXAttack)
                {
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        if (player.transform.position == enemy[i].transform.position + new Vector3(0, 0, -10))
                        {
                            enemy[i].GetComponent<PlayerController>().healtBar -= playerController.attackDamage;
                            DeAttack();
                            playerController.NextTurn();
                        }
                    }
                }
            }
        }
    }

    public void PositiveXAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == positiveXAttack)
                {
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        if (player.transform.position == enemy[i].transform.position + new Vector3(0, 0, 10))
                        {
                            enemy[i].GetComponent<PlayerController>().healtBar -= playerController.attackDamage;
                            DeAttack();
                            playerController.NextTurn();
                        }
                    }
                }
            }
        }
    }

    public void NegativeZAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == negativeZAttack)
                {
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        if (player.transform.position == enemy[i].transform.position + new Vector3(10, 0, 0))
                        {
                            enemy[i].GetComponent<PlayerController>().healtBar -= playerController.attackDamage;
                            DeAttack();
                            playerController.NextTurn();
                        }
                    }
                }
            }
        }
    }

    public void PositiveZAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == positiveZAttack)
                {
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        if (player.transform.position == enemy[i].transform.position + new Vector3(-10, 0, 0))
                        {
                            enemy[i].GetComponent<PlayerController>().healtBar -= playerController.attackDamage;
                            DeAttack();
                            playerController.NextTurn();
                        }
                    }
                }
            }
        }
    }

    public void DeAttack()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<EnemyDetect>().positiveXAttack.SetActive(false);
            players[i].GetComponent<EnemyDetect>().negativeXAttack.SetActive(false);
            players[i].GetComponent<EnemyDetect>().positiveZAttack.SetActive(false);
            players[i].GetComponent<EnemyDetect>().negativeZAttack.SetActive(false);
        }
    }

}
