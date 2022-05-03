using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private static Text goldText;
    private static Text emeraldText;
    private static Text rubyText;
    private static Text diamondText;

    private Button enterButton;

    private void Awake() {
        goldText = GameObject.Find("goldText").GetComponent<Text>();
        emeraldText = GameObject.Find("emeraldText").GetComponent<Text>();
        rubyText = GameObject.Find("rubyText").GetComponent<Text>();
        diamondText = GameObject.Find("diamondText").GetComponent<Text>();

        goldText.text = PlayerPrefs.GetInt("TotalNumberOfGold").ToString();
        emeraldText.text = PlayerPrefs.GetInt("TotalNumberOfEmerald").ToString();
        rubyText.text = PlayerPrefs.GetInt("TotalNumberOfRuby").ToString();
        diamondText.text = PlayerPrefs.GetInt("TotalNumberOfDiamond").ToString();

        enterButton = GameObject.Find("EnterButton").GetComponent<Button>();
        enterButton.onClick.AddListener(() => {
            Play();
        });
    }

    public static void RefreshStorageUI() {
        goldText.text = PlayerPrefs.GetInt("TotalNumberOfGold").ToString();
        emeraldText.text = PlayerPrefs.GetInt("TotalNumberOfEmerald").ToString();
        rubyText.text = PlayerPrefs.GetInt("TotalNumberOfRuby").ToString();
        diamondText.text = PlayerPrefs.GetInt("TotalNumberOfDiamond").ToString();
    }

    private void Play() {
        //animation
        SceneManager.LoadScene("Mine");
    }

}
