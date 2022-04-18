using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lighting : MonoBehaviour
{
    private static Light2D torchLight;
    private static float torchTimer;

    private void Awake() {
        torchLight = transform.GetComponentInChildren<Light2D>();
        torchTimer = PlayerPrefs.GetInt("MaximumTorchTime");
    }

    private void Update() {
        if (!GameManager.fightMode) {
            torchTimer -= Time.deltaTime;

            if (torchLight.pointLightOuterRadius > 0.15f) {
                torchLight.pointLightInnerRadius -= (Time.deltaTime * 5f) / PlayerPrefs.GetInt("MaximumTorchTime");
                torchLight.pointLightOuterRadius -= (Time.deltaTime * 5f) / PlayerPrefs.GetInt("MaximumTorchTime");
                GameManager.RefreshTorchUI(torchTimer);
            }
            else if (!GameManager.gameOver){
                GameManager.Die();
            }
        }
    }

    public static void TorchBuff() {
        if(torchTimer + PlayerPrefs.GetInt("ExtraTorchTime") < PlayerPrefs.GetInt("MaximumTorchTime")) {
            torchTimer += PlayerPrefs.GetInt("ExtraTorchTime");
            torchLight.pointLightInnerRadius = (5.15f) * torchTimer / PlayerPrefs.GetInt("MaximumTorchTime");
            torchLight.pointLightOuterRadius = (5.15f) * torchTimer / PlayerPrefs.GetInt("MaximumTorchTime");
        }
        else {
            torchTimer = PlayerPrefs.GetInt("MaximumTorchTime");
            torchLight.pointLightInnerRadius = 5.15f;
            torchLight.pointLightOuterRadius = 5.15f;
        }
    }

}
