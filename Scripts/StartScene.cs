using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public float rotateSpeed;
    public GameManager gameManager;
    public GameObject startObject;
    public GameObject endObject;
    public GameObject restart;
    public GameObject playerWin;
    public GameObject enemyWin;
    public Camera camera;
    public GameObject startScene;
    public GameObject mainScene;
    public GameObject gameOverScene;

    void Update()
    {
        camera.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject == startObject)
                {
                    startScene.SetActive(false);
                    mainScene.SetActive(true);
                    playerWin.SetActive(false);
                    enemyWin.SetActive(false);
                }
                if (hitInfo.collider.gameObject == endObject)
                {
                    Application.Quit();
                }
                if (hitInfo.collider.gameObject == restart)
                {
                    
                    startScene.SetActive(true);
                    mainScene.SetActive(false);
                    gameOverScene.SetActive(false);
                }
            }
        }
    }
}
