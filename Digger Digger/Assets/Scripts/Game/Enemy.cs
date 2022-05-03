using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float health;
    private float healthMax = 200f;
    private Transform healthBar;

    private float moveTime;
    private float moveTimeMax = 4f;

    private float attackTime;
    private float attackTimeMax = 4f;
    private Transform enemyAttackPrefab;

    private Vector3 nextPosition;
    private bool moving;


    private List<Vector3> positions = new List<Vector3>();

    private void Awake() {
        health = healthMax;
        healthBar = transform.Find("healthCanvas").transform;
        enemyAttackPrefab = Resources.Load<Transform>("Prefabs/pfEnemyAttack");
        attackTime = 2f;
        positions.Add(new Vector3(2, 1.75f));
        positions.Add(new Vector3(1, 1.75f));
        positions.Add(new Vector3(0, 1.75f));
        positions.Add(new Vector3(-1, 1.75f));
        positions.Add(new Vector3(-2, 1.75f));
    }

    private void Update() {
        moveTime -= Time.deltaTime;
        attackTime -= Time.deltaTime;

        if (moveTime < 0f) {
            Move();
        }

        if (attackTime < 0f) {
            Attack();
        }

        if (moving && Vector2.Distance(transform.position, nextPosition) > 0.01f) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPosition, 10f * Time.deltaTime);
        }
    }

    public void Move() {
        moveTime = moveTimeMax;
        nextPosition = positions[Random.Range(0,5)];
        StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove() {
        moving = true;
        yield return new WaitForSeconds(0.2f);
        moving = false;
    }

    public void Attack() {
        attackTime = attackTimeMax;
        Instantiate(enemyAttackPrefab, transform.position, Quaternion.identity);
    }

    public void TakeDamage(float damage) {
        health -= damage;

        healthBar.Find("healthBar").GetComponent<Image>().fillAmount = health / healthMax;
        if (health <= 0) {
            //Win combat
            GameManager.fightMode = false;
            GameManager.FightFinish();
            Destroy(gameObject);
        }
    }
}
