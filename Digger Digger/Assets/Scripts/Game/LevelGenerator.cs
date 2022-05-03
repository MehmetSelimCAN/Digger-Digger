using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {


    //toprak, kaya, obsidian, canavar
    //altýn, emerald, ruby, diamond, bomba, meþale

    //altýn -> toprak, kaya
    //emerald -> kaya
    //ruby, diamond -> obsidian
    //bomba -> toprak
    //meþale -> toprak, kaya, obsidian

    //obsidian -> 50'den sonra

    //Pickaxe level 1
    //Dirt 3, cobblestone 6, obsidian 20

    private Transform blockPrefab;
    private Transform elevatorPrefab;

    private int xIndex;
    private int yIndex;

    private void Awake() {
        blockPrefab = Resources.Load<Transform>("Prefabs/pfBlock");
        elevatorPrefab = Resources.Load<Transform>("Prefabs/pfElevator");
    }

    private void Start() {
        xIndex = 0;
        yIndex = 0;

        var parent = new GameObject("Blocks");

        for (int row = 0; row < 200; row++) {
            xIndex = 0;
            for (int column = 0; column < 7; column++) {
                Transform block = Instantiate(blockPrefab, new Vector3(-3 + xIndex, -1 - yIndex), Quaternion.identity);
                block.SetParent(parent.transform);
                xIndex++;
            }
        yIndex++;
        }

        float randomNumberY = (int)Random.Range(10,30);
        int randomNumberX = Random.Range(0, 2);
        float x;
        if (randomNumberX == 0) {
            x = -3.8f;
        }
        else {
            x = 3.8f;
        }

        Instantiate(elevatorPrefab, new Vector3(x, -randomNumberY),Quaternion.identity);
        Instantiate(elevatorPrefab, new Vector3(-x, -randomNumberY),Quaternion.identity);

        randomNumberY = (int)Random.Range(45, 65);
        randomNumberX = Random.Range(0, 2);
        if (randomNumberX == 0) {
            x = -3.8f;
        }
        else {
            x = 3.8f;
        }

        Instantiate(elevatorPrefab, new Vector3(x, -randomNumberY),Quaternion.identity);

        randomNumberY = (int)Random.Range(80, 100);
        randomNumberX = Random.Range(0, 2);
        if (randomNumberX == 0) {
            x = -3.8f;
        }
        else {
            x = 3.8f;
        }

        Instantiate(elevatorPrefab, new Vector3(x, -randomNumberY), Quaternion.identity);

        randomNumberY = (int)Random.Range(115, 135);
        randomNumberX = Random.Range(0, 2);
        if (randomNumberX == 0) {
            x = -3.8f;
        }
        else {
            x = 3.8f;
        }

        Instantiate(elevatorPrefab, new Vector3(x, -randomNumberY), Quaternion.identity);

        randomNumberY = (int)Random.Range(150, 170);
        randomNumberX = Random.Range(0, 2);
        if (randomNumberX == 0) {
            x = -3.8f;
        }
        else {
            x = 3.8f;
        }

        Instantiate(elevatorPrefab, new Vector3(x, -randomNumberY), Quaternion.identity);


        Instantiate(elevatorPrefab, new Vector3(-3.8f, -200), Quaternion.identity);
        Instantiate(elevatorPrefab, new Vector3(3.8f, -200), Quaternion.identity);
    }
}
