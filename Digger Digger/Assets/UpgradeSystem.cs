using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    private PlayerUpgrades playerUpgrades;
    [SerializeField] private UI_UpgradeTree uiUpgradeTree;
    public static List<RequirementOres> requirementOresList;

    private static Transform buyMenu;
    private static Sprite[] buyMenuRequirements;

    public enum RequirementOreType {
        Gold,
        Emerald,
        Ruby,
        Diamond
    }

    private void Awake() {
        buyMenu = GameObject.Find("BuyMenu").transform;
        buyMenu.gameObject.SetActive(false);
        buyMenuRequirements = Resources.LoadAll<Sprite>("Sprites/Shop/Requirements");

        playerUpgrades = new PlayerUpgrades();
        playerUpgrades.OnUpgradeUnlocked += PlayerUpgrades_OnUpgradeUnlocked;
        requirementOresList = new List<RequirementOres>();

        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Health1, new RequirementOreType[] { RequirementOreType.Gold }, new int[] { 50 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Health2, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Ruby }, new int[] { 150, 4 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Health3, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Ruby }, new int[] { 200, 12 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Bomb1, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Ruby }, new int[] { 75, 1 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Bomb2, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Ruby, RequirementOreType.Diamond }, new int[] { 200, 5, 1 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Pickaxe1, new RequirementOreType[] { RequirementOreType.Gold }, new int[] { 50 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Pickaxe2, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Diamond }, new int[] { 150, 1 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Pickaxe3, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Diamond }, new int[] { 250, 2 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Attack1, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Emerald }, new int[] { 50, 2 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.Attack2, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Emerald, RequirementOreType.Diamond }, new int[] { 150, 8, 1 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.MaximumTorchTime1, new RequirementOreType[] { RequirementOreType.Gold }, new int[] { 75 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.MaximumTorchTime2, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Emerald }, new int[] { 175, 8 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.MaximumTorchTime3, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Emerald, RequirementOreType.Diamond }, new int[] { 250, 20, 1 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.ExtraTorchTime1, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Emerald }, new int[] { 100, 5 }));
        requirementOresList.Add(new RequirementOres(PlayerUpgrades.UpgradeType.ExtraTorchTime2, new RequirementOreType[] { RequirementOreType.Gold, RequirementOreType.Emerald }, new int[] { 225, 12 }));
    }

    private void Start() {
        uiUpgradeTree.SetPlayerUpgrades(playerUpgrades);
    }

    private void PlayerUpgrades_OnUpgradeUnlocked(object sender, PlayerUpgrades.OnUpgradeUnlockedEventArgs e) {
        foreach (var requirementOres in requirementOresList) {
            if (requirementOres.upgradeType == e.upgradeType) {
                for (int i = 0; i < requirementOres.requirementOreTypesCount.Length; i++) {
                    PlayerPrefs.SetInt("TotalNumberOf" + requirementOres.requirementOreTypes[i], PlayerPrefs.GetInt("TotalNumberOf" + requirementOres.requirementOreTypes[i]) - requirementOres.requirementOreTypesCount[i]);
                    MenuManager.RefreshStorageUI();
                }
            }
        }

        switch (e.upgradeType) {
            case PlayerUpgrades.UpgradeType.Health1:
                SetHealthAmountMax(10);
                break;
            case PlayerUpgrades.UpgradeType.Health2:
                SetHealthAmountMax(13);
                break;
            case PlayerUpgrades.UpgradeType.Health3:
                SetHealthAmountMax(18);
                break;
            case PlayerUpgrades.UpgradeType.Bomb1:
                SetBombDamage(4);
                break;
            case PlayerUpgrades.UpgradeType.Bomb2:
                SetBombDamage(2);
                break;
            case PlayerUpgrades.UpgradeType.Pickaxe1:
                SetMineSpeed(2);
                break;
            case PlayerUpgrades.UpgradeType.Pickaxe2:
                SetMineSpeed(3);
                break;
            case PlayerUpgrades.UpgradeType.Pickaxe3:
                SetMineSpeed(6);
                break;
            case PlayerUpgrades.UpgradeType.Attack1:
                SetAttackDamage(20);
                break;
            case PlayerUpgrades.UpgradeType.Attack2:
                SetAttackDamage(40);
                break;
            case PlayerUpgrades.UpgradeType.MaximumTorchTime1:
                SetTorchTimeMax(25);
                break;
            case PlayerUpgrades.UpgradeType.MaximumTorchTime2:
                SetTorchTimeMax(35);
                break;
            case PlayerUpgrades.UpgradeType.MaximumTorchTime3:
                SetTorchTimeMax(45);
                break;
            case PlayerUpgrades.UpgradeType.ExtraTorchTime1:
                SetExtraTorchTime(11);
                break;
            case PlayerUpgrades.UpgradeType.ExtraTorchTime2:
                SetExtraTorchTime(14);
                break;
        }
    }

    public PlayerUpgrades GetPlayerUpgrades() {
        return playerUpgrades;
    }

    public void SetTorchTimeMax(int maximumTorchTime) {
        PlayerPrefs.SetInt("MaximumTorchTime", maximumTorchTime);
    }

    public void SetExtraTorchTime(int extraTorchTime) {
        PlayerPrefs.SetInt("ExtraTorchTime", extraTorchTime);
    }

    public void SetMineSpeed(int diggingMultiplier) {
        PlayerPrefs.SetInt("DiggingMultiplier", diggingMultiplier);
    }

    private void SetAttackDamage(int attackDamage) {
        PlayerPrefs.SetInt("AttackDamage", attackDamage);
    }

    private void SetHealthAmountMax(int healthAmountMax) {
        PlayerPrefs.SetInt("HealthMax", healthAmountMax);
    }

    private void SetBombDamage(int bombDamage) {
        PlayerPrefs.SetInt("BombDamage", bombDamage);
    }


    public class RequirementOres {

        public PlayerUpgrades.UpgradeType upgradeType;
        public RequirementOreType[] requirementOreTypes = new RequirementOreType[4];
        public int[] requirementOreTypesCount = new int[4];

        public RequirementOres(PlayerUpgrades.UpgradeType upgradeType, RequirementOreType[] requirementOreTypes, int[] requirementOreTypesCount) {
            this.upgradeType = upgradeType;
            this.requirementOreTypes = requirementOreTypes;
            this.requirementOreTypesCount = requirementOreTypesCount;

        }
    }

    public static void UpdateBuyMenuUI(PlayerUpgrades.UpgradeType upgradeType) {
        buyMenu.gameObject.SetActive(true);
        foreach (var requirementOres in requirementOresList) {
            if (requirementOres.upgradeType == upgradeType) {
                buyMenu.Find("Requirements").Find("background").GetComponent<Image>().sprite = buyMenuRequirements[requirementOres.requirementOreTypes.Length - 1];
                for (int i = 0; i < requirementOres.requirementOreTypes.Length; i++) {
                    buyMenu.Find("Requirements").Find("requirementImages").GetChild(i).GetComponent<Image>().gameObject.SetActive(true);
                    buyMenu.Find("Requirements").Find("requirementTexts").GetChild(i).GetComponent<Text>().gameObject.SetActive(true);
                    buyMenu.Find("Requirements").Find("requirementImages").GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Ores/sp" + requirementOres.requirementOreTypes[i].ToString());
                    buyMenu.Find("Requirements").Find("requirementTexts").GetChild(i).GetComponent<Text>().text = requirementOres.requirementOreTypesCount[i].ToString();
                }
                for (int i = requirementOres.requirementOreTypes.Length; i < 3; i++) {
                    buyMenu.Find("Requirements").Find("requirementImages").GetChild(i).GetComponent<Image>().gameObject.SetActive(false);
                    buyMenu.Find("Requirements").Find("requirementTexts").GetChild(i).GetComponent<Text>().gameObject.SetActive(false);
                }
            }
        }
    }

    public static bool CheckRequirementOres(PlayerUpgrades.UpgradeType upgradeType) {
        foreach (var requirementOres in requirementOresList) {
            if (requirementOres.upgradeType == upgradeType) {
                for (int i = 0; i < requirementOres.requirementOreTypesCount.Length; i++) {
                    if (!(PlayerPrefs.GetInt("TotalNumberOf" + requirementOres.requirementOreTypes[i].ToString()) >= requirementOres.requirementOreTypesCount[i])) {
                        return false;
                    }
                }
                break;
            }
        }
        return true;
    }

}
