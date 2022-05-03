using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer : MonoBehaviour {

    private LayerMask blockLayer;
    private RaycastHit2D checkBlock;
    private Vector3 direction;
    private Vector3 nextPosition;
    private Rigidbody2D rb;


    private float attackTimer;
    private float attackTimerMax = 0.4f;
    private bool moving;

    private Sprite fightPlayerIdle;
    private Sprite fightPlayerAttack;

    private void Awake() {
        blockLayer = LayerMask.GetMask("FightAreaBlock");
        fightPlayerIdle = Resources.Load<Sprite>("Sprites/spFightPlayerIdle");
        fightPlayerAttack = Resources.Load<Sprite>("Sprites/spFightPlayerAttack");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        attackTimer -= Time.deltaTime;

        #region Movement
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !moving) {
            direction = new Vector3(-1f, 0f);
            Move(direction);
        }

        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !moving) {
            direction = new Vector3(1f, 0f);
            Move(direction);
        }

        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !moving && attackTimer < 0f) {
            attackTimer = attackTimerMax;
            Attack();
        }

        if (moving && Vector2.Distance(transform.position, nextPosition) > 0.01f && transform.localPosition.y <= 0.001f) {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, 10f * Time.deltaTime);
        }
        #endregion
    }

    public void Move(Vector3 direction) {
        checkBlock = Physics2D.Raycast(transform.position, direction, 0.6f, blockLayer);

        if (checkBlock.collider == null) {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);
            nextPosition = transform.position + direction;
            StartCoroutine(SmoothMove());
        }
    }

    public void Attack() {
        StartCoroutine(SmoothAttack());
        nextPosition = new Vector3(transform.position.x, transform.position.y + 1f);
    }

    private IEnumerator SmoothAttack() {
        rb.AddForce(new Vector2(0f, 11f), ForceMode2D.Impulse);
        GetComponent<SpriteRenderer>().sprite = fightPlayerAttack;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().sprite = fightPlayerIdle;
    }

    private IEnumerator SmoothMove() {
        moving = true;
        yield return new WaitForSeconds(0.1f);
        moving = false;
    }

    public void TakeDamage(int damage) {
        Movement.health -= damage;
        GameManager.RefreshHealthUI();
        if (Movement.health < 1) {
            GameManager.Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Enemy") {
            collision.collider.GetComponent<Enemy>().TakeDamage(PlayerPrefs.GetInt("AttackDamage"));
        }
    }
}
