using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameTurn = false;
    public GameObject[] players;
    public GameObject[] enemies;
    public int enemyCount;
    public int playerCount;
    public int spawnableEnemyCount;
    public int spawnablePlayerCount;
    public int turnCount;
    public bool turnCountStop;
    public bool playerSpawned;
    public bool enemySpawned;
    public int livePlayers;
    public int liveEnemies;
    public bool playerWin;
    public GameObject playerWinButton;
    public GameObject enemyWinButton;
    public GameObject startScene;
    public GameObject mainScene;
    public GameObject gameOverScene;

    private void Start()
    {
        turnCount = 0;
        enemyCount = enemies.Length;
        playerCount = players.Length;
        spawnablePlayerCount = playerCount;
        spawnableEnemyCount = enemyCount;

    }

    public void Update()
    {
        spawnManager();
        
    }

    private void FixedUpdate()
    {
        AvaiblePlayers();
        AvaibleEnemies();

        if (livePlayers == 0 && turnCount != 0)
        {
            playerWin = false;
            GameOver();
        }
        if (liveEnemies == 0 && turnCount != 0)
        {
            playerWin = true;
            GameOver();
        }
        
        if (!gameTurn && !turnCountStop)
        {
            turnCount = turnCount + 1;
            turnCountStop = true;
        }
        else if (gameTurn && turnCountStop)
        {
            turnCount = turnCount + 1;
            turnCountStop = false;
        }
    }

    public void spawnManager()
    {
        for (int i = 5; i < turnCount+1; i += 4)
        {
            if (turnCount == i && !playerSpawned)
            {
                CanSpawnPlayer();
            }
        }
        for (int i = 6; i < turnCount + 1; i += 4)
        {
            if (turnCount == i && !enemySpawned)
            {
                CanSpawnEnemy();
            }
        }
        for (int i = 7; i < turnCount + 1; i += 4)
        {
            if (turnCount == i)
            {
                enemySpawned = false;
                playerSpawned = false;
            }
        }
    }

    public void CanSpawnEnemy()
    {
        if (spawnableEnemyCount != 1)
        {
            enemies[spawnableEnemyCount - 2].SetActive(true);
            spawnableEnemyCount--;
            enemySpawned = true;
            return;
        }
        
    }

    public void CanSpawnPlayer()
    {
        if (spawnablePlayerCount != 1)
        {
            players[spawnablePlayerCount - 2].SetActive(true);
            spawnablePlayerCount--;
            playerSpawned = true;
            return;
        }
        
    }
    
    public void GameOver()
    {
        if (playerWin)
        {
            playerWinButton.SetActive(true);
        }
        else
        {
            enemyWinButton.SetActive(true);
        }
        gameOverScene.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {

            players[i].transform.position = new Vector3(37, -1.5f, 0);
            players[players.Length - 1].SetActive(true);
            players[i].GetComponent<PlayerController>().isDead = false;
            players[i].GetComponent<PlayerController>().healtBar = players[i].GetComponent<PlayerController>().saveHealth;
            players[i].GetComponent<PlayerController>().isOnGround = false;
            players[i].GetComponent<PlayerController>().CloseAttackButtons();
            players[i].GetComponent<PlayerController>().DeActiveMovementButton();
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = new Vector3(-37, -1.5f, 0);
            enemies[enemies.Length - 1].SetActive(true);
            enemies[i].GetComponent<PlayerController>().isDead = false;
            enemies[i].GetComponent<PlayerController>().healtBar = players[i].GetComponent<PlayerController>().saveHealth;
            enemies[i].GetComponent<PlayerController>().isOnGround = false;
            enemies[i].GetComponent<PlayerController>().CloseAttackButtons();
            enemies[i].GetComponent<PlayerController>().DeActiveMovementButton();
        }
        turnCount = 0;
        gameTurn = false;
        enemyCount = enemies.Length;
        playerCount = players.Length;
        spawnablePlayerCount = playerCount;
        spawnableEnemyCount = enemyCount;
    }

    public void AvaiblePlayers()
    {
        livePlayers = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].active)
            {
                livePlayers++;
            }
        }
    }

    public void AvaibleEnemies()
    {
        liveEnemies = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].active)
            {
                liveEnemies++;
            }
        }
    }
}
