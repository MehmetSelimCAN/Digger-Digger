using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour {

    private LayerMask blockLayer;
    private RaycastHit2D checkBlock;
    private Vector2 direction;
    private float timeBetweenDiggings;

    private Animator animator;
    private string currentAnimationState;

    private const string PLAYER_IDLE = "Idle";
    private const string PLAYER_DIG_W = "DigW";
    private const string PLAYER_DIG_S = "DigS";
    private const string PLAYER_DIG_HORIZONTAL = "DigHorizontal";

    private void Awake() {
        blockLayer = LayerMask.GetMask("Block");
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!GameManager.fightMode) {
            timeBetweenDiggings -= Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && timeBetweenDiggings < 0f) {
                direction = new Vector3(-1f, 0f);
                Dig(direction);
            }

            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && timeBetweenDiggings < 0f) {
                direction = new Vector3(1f, 0f);
                Dig(direction);
            }

            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && timeBetweenDiggings < 0f) {
                direction = new Vector3(0f, -1f);
                Dig(direction);
            }

            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && timeBetweenDiggings < 0f) {
                direction = new Vector3(0f, 1f);
                Dig(direction);
            }
        }
    }

    private void Dig(Vector2 direction) {
        //digging animation
        checkBlock = Physics2D.Raycast(transform.position, direction, 0.6f, blockLayer);

        if (checkBlock.collider != null) {
            checkBlock.collider.GetComponent<Block>().TakeDamage(PlayerPrefs.GetInt("DiggingMultiplier") * 10f);
            timeBetweenDiggings = 0.1f;
            StopAllCoroutines();
            StartCoroutine(WaitForAnimation(direction));
        }
    }

    private IEnumerator WaitForAnimation(Vector2 direction) {
        if (direction == Vector2.up) {
            ChangeAnimationState(PLAYER_DIG_W);
        }
        else if (direction == Vector2.down) {
            ChangeAnimationState(PLAYER_DIG_S);
        }
        else {
            ChangeAnimationState(PLAYER_DIG_HORIZONTAL);
        }
        yield return new WaitForSeconds(1/3f);
        ChangeAnimationState(PLAYER_IDLE);
    }


    private void ChangeAnimationState(string newState) {
        if (currentAnimationState == newState)
            return;

        animator.Play(newState);
        currentAnimationState = newState;
    }
}
