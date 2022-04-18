using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour {

    private void Awake() {
        if (PlayerPrefs.GetInt("MaximumTorchTime") == 0) {
            PlayerPrefs.SetInt("MaximumTorchTime", 15);
        }
        if (PlayerPrefs.GetInt("ExtraTorchTime") == 0) {
            PlayerPrefs.SetInt("ExtraTorchTime", 8);
        }
        if (PlayerPrefs.GetInt("DiggingMultiplier") == 0) {
            PlayerPrefs.SetInt("DiggingMultiplier", 1);
        }
        if (PlayerPrefs.GetInt("AttackDamage") == 0) {
            PlayerPrefs.SetInt("AttackDamage", 10);
        }
        if (PlayerPrefs.GetInt("HealthMax") == 0) {
            PlayerPrefs.SetInt("HealthMax", 8);
        }
        if (PlayerPrefs.GetInt("BombDamage") == 0) {
            PlayerPrefs.SetInt("BombDamage", 5);
        }
    }

}
