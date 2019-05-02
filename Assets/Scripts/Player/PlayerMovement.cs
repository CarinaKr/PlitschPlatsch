using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed;

    private GameManager gameManager;
    private Vector3 moveDir;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        moveDir = new Vector3(0, 0, 0);
;    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPlaying)
            return;

        moveDir.y = (Input.acceleration.y - gameManager.calibratedTilt.y)*Time.deltaTime* startSpeed*gameManager.gameSpeed;
        moveDir.x = (Input.acceleration.x - gameManager.calibratedTilt.x) *Time.deltaTime * startSpeed * gameManager.gameSpeed;
        //moveDir.y = Mathf.Round(moveDir.y * 100f) / 100f;
        //Debug.Log( "acc.y: " + Input.acceleration.y +"; cal.y: " + gameManager.calibratedTilt.y + "; moveDir.y: " + moveDir.y);
        //transform.Translate(moveDir.normalized * Time.deltaTime * speed);
        rb.velocity = new Vector2(moveDir.x, moveDir.y);

    }
}
