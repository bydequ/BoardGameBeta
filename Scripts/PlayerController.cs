using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GroundController groundController;
    public EnemyDetect enemyDetect;
    public BaseController baseController;
    public GameManager gameManager;
    public GameObject player;
    public GameObject negativeX;
    public GameObject positiveX;
    public GameObject negativeZ;
    public GameObject positiveZ;
    public GameObject[] start;
    private Vector3 deathPos;
    public bool isDead;
    public float speed = 0.1f;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;

    public int healtBar = 90;
    public int saveHealth;
    public int attackDamage = 5;
    public bool enemyTurn = false;
    public bool isOnGround = false;
    private float moveDistance = 10.0f;
    public int count;

    void Start()
    {
        saveHealth = healtBar;
        deathPos = new Vector3(100, 0, 100);
    }

    void Update()
    {
        if (count == 0 && gameManager.gameTurn == false)
        {
            if (player.GetComponent<PlayerController>().healtBar <= 0)
            {
                player.SetActive(false);
                isDead = true;
                player.transform.position = deathPos;
            }
            else
            {
                Movement();
                enemyDetect.StartAttack();
            }
        }
        if (count == 1 && gameManager.gameTurn == true)
        {
            if (player.GetComponent<PlayerController>().healtBar <= 0)
            {
                player.SetActive(false);
                isDead=true;
                player.transform.position = deathPos;
            }
            else
            {
                Movement();
                enemyDetect.StartAttack();
            }
        }
        healthText.text = healtBar.ToString();
        attackText.text = attackDamage.ToString();
    }

    public void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == negativeX)
                {
                    player.transform.position += new Vector3(-moveDistance, 0, 0);
                    DeActiveMovementButton();
                    CloseAttackButtons();
                    NextTurn();
                }
                if (hitInfo.collider.gameObject == positiveX)
                {
                    player.transform.position += new Vector3(moveDistance, 0, 0);
                    DeActiveMovementButton();
                    CloseAttackButtons();
                    NextTurn();
                }
                if (hitInfo.collider.gameObject == negativeZ)
                {
                    player.transform.position += new Vector3(0, 0, -moveDistance);
                    DeActiveMovementButton();
                    CloseAttackButtons();
                    NextTurn();
                }
                if (hitInfo.collider.gameObject == positiveZ)
                {
                    player.transform.position += new Vector3(0, 0, moveDistance);
                    DeActiveMovementButton();
                    CloseAttackButtons();
                    NextTurn();
                }
                if (hitInfo.collider.gameObject == player)
                {
                    if (isOnGround == true)
                    {
                        ActiveMovementButton();
                    }
                    else
                    {
                        ActivateFirstMove();
                    }
                }
                for (int i = 0; i < baseController.baseGrounds.Length; i++)
                {
                    if (hitInfo.collider.gameObject == start[i])
                    {
                        player.transform.position = new Vector3(baseController.baseGrounds[i].transform.position.x, -0.2f, baseController.baseGrounds[i].transform.position.z);
                        isOnGround = true;
                        DeActivateFirstMove();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == player)
                {
                    DeActiveMovementButton();
                }
            }
        }
    }

    public void ActiveMovementButton()
    {
        for (int i = 0; i < groundController.ground.Length; i++)
        {
            if (player.transform.position.x - moveDistance == groundController.ground[i].transform.position.x)
            {
                negativeX.SetActive(true);
            }
            else if (player.transform.position.x + moveDistance == groundController.ground[i].transform.position.x)
            {
                positiveX.SetActive(true);
            }
            else if (player.transform.position.z + moveDistance == groundController.ground[i].transform.position.z)
            {
                positiveZ.SetActive(true);
            }
            else if (player.transform.position.z - moveDistance == groundController.ground[i].transform.position.z)
            {
                negativeZ.SetActive(true);
            }
        }
    }

    public void DeActiveMovementButton()
    {
        negativeX.SetActive(false);
        positiveX.SetActive(false);
        positiveZ.SetActive(false);
        negativeZ.SetActive(false);
    }

    public void ActivateFirstMove()
    {
        for (int i = 0; i < start.Length; i++)
        {
            start[i].SetActive(true);
        }
    }

    public void DeActivateFirstMove()
    {
        for (int i = 0; i < start.Length; i++)
        {
            start[i].SetActive(false);
        }
    }

    public void CloseAttackButtons()
    {
        for (int i = 0; i < enemyDetect.players.Length; i++)
        {
            enemyDetect.players[i].GetComponent<EnemyDetect>().DeAttack();
        }
    }

    public void NextTurn()
    {
        if (count == 0)
        {
            gameManager.gameTurn = true;
        }
        else
        {
            gameManager.gameTurn = false;
        }
    }
}
