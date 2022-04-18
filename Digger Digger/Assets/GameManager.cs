using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private static Transform storageUI;
    private static Transform healthUI;
    private static Transform depthUI;
    private static Transform torchUI;
    private static Transform gameOverUI;
    private static Button backToMenuButton;
    private static Transform player;

    public static bool fightMode;
    private static Transform fightAreaPrefab;
    private static Transform fightArea;
    private static Transform fightUI;

    public static bool gameOver;

    private void Awake() {
        Instance = this;
        gameOver = false;
        depthUI = GameObject.Find("Canvas").transform.Find("Depth").transform;
        torchUI = GameObject.Find("Canvas").transform.Find("Torch").transform;
        healthUI = GameObject.Find("Canvas").transform.Find("Health").transform;
        storageUI = GameObject.Find("Canvas").transform.Find("Storage").transform;
        fightUI = GameObject.Find("Canvas").transform.Find("Fight").transform;
        gameOverUI = GameObject.Find("Canvas").transform.Find("GameOver").transform;
        player = GameObject.Find("Player").transform;

        fightUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

        fightAreaPrefab = Resources.Load<Transform>("Prefabs/pfFightArea");

        backToMenuButton = gameOverUI.Find("BackToMenuButton").GetComponent<Button>();
        backToMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Menu");
        });
        backToMenuButton.gameObject.SetActive(false);

    }

    public static void RefreshTorchUI(float time) {
        torchUI.Find("Timer").Find("time").GetComponent<Image>().fillAmount = time / PlayerPrefs.GetInt("MaximumTorchTime");
    }

    public static void RefreshHealthUI() {
        healthUI.Find("text").GetComponent<Text>().text = Movement.health.ToString();
        healthUI.Find("fill").GetComponent<Image>().fillAmount = (float)Movement.health / PlayerPrefs.GetInt("HealthMax");
    }

    public static void RefreshItemUI() {
        storageUI.Find("texts").Find("goldText").GetComponent<Text>().text = Storage.currentNumberOfGold.ToString();
        storageUI.Find("texts").Find("emeraldText").GetComponent<Text>().text = Storage.currentNumberOfEmerald.ToString();
        storageUI.Find("texts").Find("rubyText").GetComponent<Text>().text = Storage.currentNumberOfRuby.ToString();
        storageUI.Find("texts").Find("diamondText").GetComponent<Text>().text = Storage.currentNumberOfDiamond.ToString();
    }

    public static void RefreshDepthUI() {
        depthUI.GetComponentInChildren<Text>().text = -Mathf.Round(player.transform.position.y) + "M";
    }

    public static void Elevator() {
        PlayerPrefs.SetInt("TotalNumberOfGold", PlayerPrefs.GetInt("TotalNumberOfGold") + Storage.currentNumberOfGold);
        PlayerPrefs.SetInt("TotalNumberOfEmerald", PlayerPrefs.GetInt("TotalNumberOfEmerald") + Storage.currentNumberOfEmerald);
        PlayerPrefs.SetInt("TotalNumberOfRuby", PlayerPrefs.GetInt("TotalNumberOfRuby") + Storage.currentNumberOfRuby);
        PlayerPrefs.SetInt("TotalNumberOfDiamond", PlayerPrefs.GetInt("TotalNumberOfDiamond") + Storage.currentNumberOfDiamond);

        Storage.currentNumberOfGold = 0;
        Storage.currentNumberOfEmerald = 0;
        Storage.currentNumberOfRuby = 0;
        Storage.currentNumberOfDiamond = 0;

        GameObject.Find("Canvas").SetActive(false);
        Vector3 temp = new Vector3(0f, Camera.main.transform.position.y, -5f);
        GameObject.Find("ElevatorFade").GetComponent<Transform>().transform.position = temp;
        GameObject.Find("ElevatorFade").GetComponent<Animator>().Play("Elevator");
        Instance.StartCoroutine(Instance.WaitForElevatorFade());
    }

    private IEnumerator WaitForElevatorFade() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Menu");
    }

    public static void Die() {
        gameOver = true;
        depthUI.gameObject.SetActive(false);
        storageUI.gameObject.SetActive(false);
        torchUI.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        fightUI.gameObject.SetActive(false);

        Vector3 temp = new Vector3(0f, Camera.main.transform.position.y, -5f);
        GameObject.Find("GameOverFade").GetComponent<Transform>().transform.position = temp;
        GameObject.Find("GameOverFade").GetComponent<Animator>().Play("GameOver");
        Instance.StartCoroutine(Instance.WaitForGameOverFade());

    }

    private IEnumerator WaitForGameOverFade() {

        yield return new WaitForSeconds(0.8f);
        gameOverUI.gameObject.SetActive(true);
        gameOverUI.Find("gameOverStorage").Find("texts").Find("goldText").GetComponent<Text>().text = Storage.currentNumberOfGold.ToString();
        gameOverUI.Find("gameOverStorage").Find("texts").Find("emeraldText").GetComponent<Text>().text = Storage.currentNumberOfEmerald.ToString();
        gameOverUI.Find("gameOverStorage").Find("texts").Find("rubyText").GetComponent<Text>().text = Storage.currentNumberOfRuby.ToString();
        gameOverUI.Find("gameOverStorage").Find("texts").Find("diamondText").GetComponent<Text>().text = Storage.currentNumberOfDiamond.ToString();

        Storage.currentNumberOfGold /= 2;
        Storage.currentNumberOfEmerald /= 2;
        Storage.currentNumberOfRuby /= 2;
        Storage.currentNumberOfDiamond /= 2;

        PlayerPrefs.SetInt("TotalNumberOfGold", PlayerPrefs.GetInt("TotalNumberOfGold") + Storage.currentNumberOfGold);
        PlayerPrefs.SetInt("TotalNumberOfEmerald", PlayerPrefs.GetInt("TotalNumberOfEmerald") + Storage.currentNumberOfEmerald);
        PlayerPrefs.SetInt("TotalNumberOfRuby", PlayerPrefs.GetInt("TotalNumberOfRuby") + Storage.currentNumberOfRuby);
        PlayerPrefs.SetInt("TotalNumberOfDiamond", PlayerPrefs.GetInt("TotalNumberOfDiamond") + Storage.currentNumberOfDiamond);

        gameOverUI.Find("text").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        gameOverUI.Find("gameOverStorage").Find("texts").Find("goldText").GetComponent<Text>().text = Storage.currentNumberOfGold.ToString();
        yield return new WaitForSeconds(0.2f);
        gameOverUI.Find("gameOverStorage").Find("texts").Find("emeraldText").GetComponent<Text>().text = Storage.currentNumberOfEmerald.ToString();
        yield return new WaitForSeconds(0.2f);
        gameOverUI.Find("gameOverStorage").Find("texts").Find("rubyText").GetComponent<Text>().text = Storage.currentNumberOfRuby.ToString();
        yield return new WaitForSeconds(0.2f);
        gameOverUI.Find("gameOverStorage").Find("texts").Find("diamondText").GetComponent<Text>().text = Storage.currentNumberOfDiamond.ToString();
        yield return new WaitForSeconds(0.2f);

        backToMenuButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
    }

    public static void Fight() {
        Vector3 temp = new Vector3(0f, Camera.main.transform.position.y, -5f);
        GameObject.Find("FightFade").GetComponent<Transform>().transform.position = temp;
        GameObject.Find("FightFade").GetComponent<Animator>().Play("Fight");
        depthUI.gameObject.SetActive(false);
        storageUI.gameObject.SetActive(false);
        torchUI.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        fightMode = true;
        Instance.StartCoroutine(Instance.WaitForFadeIn());
    }

    private IEnumerator WaitForFadeIn() {
        yield return new WaitForSeconds(0.5f);
        healthUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fightUI.gameObject.SetActive(true);
        fightArea = Instantiate(fightAreaPrefab, new Vector3(0f, player.transform.position.y - 2f, 0f), Quaternion.identity);
    }

    public static void FightFinish() {
        Vector3 temp = new Vector3(0f, Camera.main.transform.position.y, -5f);
        GameObject.Find("FightFade").GetComponent<Transform>().transform.position = temp;
        GameObject.Find("FightFade").GetComponent<Animator>().Play("FightWin");
        fightUI.gameObject.SetActive(false);
        Storage.currentNumberOfGold +=  10;
        Destroy(fightArea.gameObject);
        
        healthUI.gameObject.SetActive(false);
        RefreshItemUI();
        fightMode = false;
        Instance.StartCoroutine(Instance.WaitForFadeOut());

        GameObject[] enemyAttacks = GameObject.FindGameObjectsWithTag("EnemyAttack");

        foreach (var enemyAttack in enemyAttacks) {
            Destroy(enemyAttack.gameObject);
        }
    }

    private IEnumerator WaitForFadeOut() {
        yield return new WaitForSeconds(0.75f);
        depthUI.gameObject.SetActive(true);
        storageUI.gameObject.SetActive(true);
        torchUI.gameObject.SetActive(true);
        healthUI.gameObject.SetActive(true);
    }
}
