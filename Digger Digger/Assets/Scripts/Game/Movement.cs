using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public static Movement Instance;

    private LayerMask blockLayer;
    private LayerMask wallLayer;
    private LayerMask elevatorLayer;
    private RaycastHit2D checkBlock;
    private RaycastHit2D checkWall;
    private RaycastHit2D checkElevator;
    private Vector3 direction;
    private Vector3 nextPosition;
    private bool moving;

    public static int health;
    public static int maximumHealth;

    private Rigidbody2D rb;
    private Transform cameraTransform;
    private Vector3 temp;

    private void Awake() {
        Instance = this;
        blockLayer = LayerMask.GetMask("Block");
        wallLayer = LayerMask.GetMask("Wall");
        elevatorLayer = LayerMask.GetMask("Elevator");
        health = PlayerPrefs.GetInt("HealthMax");
        rb = GetComponent<Rigidbody2D>();
        cameraTransform = Camera.main.transform;
    }

    private void Update() {
        if (!GameManager.fightMode) {
            #region Movement
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !moving) {
                GetComponent<SpriteRenderer>().flipX = false;
                direction = new Vector3(-1f, 0f);
                Move(direction);
            }

            else if ((Input.GetKeyDown(KeyCode.D) ||Input.GetKeyDown(KeyCode.RightArrow)) && !moving) {
                GetComponent<SpriteRenderer>().flipX = true;
                direction = new Vector3(1f, 0f);
                Move(direction);
            }

            if (moving && Vector2.Distance(transform.position, nextPosition) > 0.01f) {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, 10f * Time.deltaTime);
            }

            temp = new Vector3(0f, transform.position.y, -10f);
            cameraTransform.position = temp;
            #endregion
        }
        if (rb.velocity.y < 0) {
            GameManager.RefreshDepthUI();
        }
    }

    public void Move(Vector3 direction) {
        checkBlock = Physics2D.Raycast(transform.position, direction, 0.6f, blockLayer);
        checkWall = Physics2D.Raycast(transform.position, direction, 0.6f, wallLayer);
        checkElevator = Physics2D.Raycast(transform.position, direction, 0.6f, elevatorLayer);

        if (checkElevator.collider != null) {
            GameManager.Elevator();
        }

        if (checkBlock.collider == null && checkWall.collider == null) {
            nextPosition = transform.position + direction;
            StartCoroutine(SmoothMove());
        }
    }

    private IEnumerator SmoothMove() {
        moving = true;
        yield return new WaitForSeconds(0.1f);
        moving = false;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        GameManager.RefreshHealthUI();
        if (health < 1) {
            GameManager.Die();
        }
    }
}
