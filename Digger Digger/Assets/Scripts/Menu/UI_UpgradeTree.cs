using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeTree : MonoBehaviour
{

    [SerializeField] private UpgradeUnlockPath[] upgradeUnlockPathArray;

    private PlayerUpgrades playerUpgrades;
    private List<UpgradeButton> upgradeButtonList;
    private static Transform buyMenu;
    private static Sprite canBuySprite;
    private static Sprite cantBuySprite;

    private void Awake() {
        buyMenu = GameObject.Find("BuyMenu").transform;
        canBuySprite = Resources.Load<Sprite>("Sprites/Shop/spCanBuy");
        cantBuySprite = Resources.Load<Sprite>("Sprites/Shop/spCantBuy");
    }

    public void SetPlayerUpgrades(PlayerUpgrades playerUpgrades) {
        this.playerUpgrades = playerUpgrades;

        upgradeButtonList = new List<UpgradeButton>();
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Health1Button"),            playerUpgrades, PlayerUpgrades.UpgradeType.Health1,             "Healthy Digger I",       "Healthy! +2 max HP"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Health2Button"),            playerUpgrades, PlayerUpgrades.UpgradeType.Health2,             "Healthy Digger II",      "Healthy and strong! +3 max HP"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Health3Button"),            playerUpgrades, PlayerUpgrades.UpgradeType.Health3,             "Healthy Digger III",     "Rock-hard body. +5 max HP"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Bomb1Button"),              playerUpgrades, PlayerUpgrades.UpgradeType.Bomb1,               "Bomb Shield I",        "Bomb deals -1 dmg to you"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Bomb2Button"),              playerUpgrades, PlayerUpgrades.UpgradeType.Bomb2,               "Bomb Shield II",       "Bomb deals -2 dmg to you"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Pickaxe1Button"),           playerUpgrades, PlayerUpgrades.UpgradeType.Pickaxe1,            "Pickaxe I",            "A better pickaxe!"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Pickaxe2Button"),           playerUpgrades, PlayerUpgrades.UpgradeType.Pickaxe2,            "Pickaxe II",           "An outstanding pickaxe!"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Pickaxe3Button"),           playerUpgrades, PlayerUpgrades.UpgradeType.Pickaxe3,            "Pickaxe III",          "The best pickaxe a human ever had."));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Attack1Button"),            playerUpgrades, PlayerUpgrades.UpgradeType.Attack1,             "Sharpen I",            "+1 dmg in combat"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("Attack2Button"),            playerUpgrades, PlayerUpgrades.UpgradeType.Attack2,             "Sharpen II",           "+2 dmg in combat"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("MaximumTorchTime1Button"),  playerUpgrades, PlayerUpgrades.UpgradeType.MaximumTorchTime1,   "Efficient Torch I",    "Your torch lasts +10s"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("MaximumTorchTime2Button"),  playerUpgrades, PlayerUpgrades.UpgradeType.MaximumTorchTime2,   "Efficient Torch II" ,  "Your torch lasts +10s"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("MaximumTorchTime3Button"),  playerUpgrades, PlayerUpgrades.UpgradeType.MaximumTorchTime3,   "Efficient Torch III" , "Your torch lasts +10s"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("ExtraTorchTime1Button"),    playerUpgrades, PlayerUpgrades.UpgradeType.ExtraTorchTime1,     "Lucky Torch I",        "+3s when picking up Torches"));
        upgradeButtonList.Add(new UpgradeButton(transform.Find("ExtraTorchTime2Button"),    playerUpgrades, PlayerUpgrades.UpgradeType.ExtraTorchTime2,     "Lucky Torch II",       "+3s when picking up Torches"));

        playerUpgrades.OnUpgradeUnlocked += PlayerUpgrades_OnUpgradeUnlocked;

        UpdateVisuals();
    }

    private void PlayerUpgrades_OnUpgradeUnlocked(object sender, PlayerUpgrades.OnUpgradeUnlockedEventArgs e) {
        UpdateVisuals();
    }

    private void UpdateVisuals() {
        foreach (UpgradeButton upgradeButton in upgradeButtonList) {
            upgradeButton.UpdateVisual();
        }

        // Darken all links
        /*foreach (UpgradeUnlockPath upgradeUnlockPath in upgradeUnlockPathArray) {
            foreach (Image linkImage in upgradeUnlockPath.linkImageArray) {
                linkImage.color = new Color(.4f, .4f, .4f);
            }
        }*/

        foreach (UpgradeUnlockPath upgradeUnlockPath in upgradeUnlockPathArray) {
            if (playerUpgrades.IsUpgradeUnlocked(upgradeUnlockPath.upgradeType) || PlayerPrefs.GetInt("" + (PlayerUpgrades.GetUpgradeRequirement(upgradeUnlockPath.upgradeType))) == 1) {
                // Skill unlocked or can be unlocked
                foreach (Image linkImage in upgradeUnlockPath.linkImageArray) {
                    linkImage.color = Color.white;
                }
            }
        }
    }

    /*
     * Represents a single Skill Button
     * */
    private class UpgradeButton {
        private Image backgroundImage;
        private PlayerUpgrades playerUpgrades;
        private PlayerUpgrades.UpgradeType upgradeType;

        public UpgradeButton(Transform transform, PlayerUpgrades playerUpgrades, PlayerUpgrades.UpgradeType upgradeType, string upgradeName, string instruction) {
            this.playerUpgrades = playerUpgrades;
            this.upgradeType = upgradeType;

            backgroundImage = transform.Find("background").GetComponent<Image>();
            

            transform.GetComponent<Button>().onClick.AddListener(() => {
                buyMenu.Find("upgradeName").GetComponent<Text>().text = upgradeName;
                buyMenu.Find("upgradeInstruction").GetComponent<Text>().text = instruction;
                UpgradeSystem.UpdateBuyMenuUI(upgradeType);
                UpdateBuyButtonOnClick(upgradeType);

                if (playerUpgrades.IsUpgradeUnlocked(upgradeType) || PlayerPrefs.GetInt("" + upgradeType) == 1) {
                    buyMenu.Find("BuyButton").gameObject.SetActive(false);
                }
                else {
                    buyMenu.Find("BuyButton").gameObject.SetActive(true);

                    if (playerUpgrades.CanUnlock(upgradeType)) {
                        buyMenu.Find("BuyButton").GetComponent<Image>().sprite = canBuySprite;
                    }
                    else {
                        buyMenu.Find("BuyButton").GetComponent<Image>().sprite = cantBuySprite;
                    }
                }
            });
        }

        public void UpdateBuyButtonOnClick(PlayerUpgrades.UpgradeType upgradeType) {
            buyMenu.Find("BuyButton").GetComponent<Button>().onClick.RemoveAllListeners();
            buyMenu.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => {
                if (!playerUpgrades.IsUpgradeUnlocked(upgradeType)) {
                    if (playerUpgrades.TryUnlockUpgrade(upgradeType)) {
                        buyMenu.Find("BuyButton").gameObject.SetActive(false);
                    }
                }
            });
        }

        public void UpdateVisual() {
            if (playerUpgrades.IsUpgradeUnlocked(upgradeType) || PlayerPrefs.GetInt("" + upgradeType) == 1) {
                backgroundImage.color = new Color(0, 0, 0, 0);
            }
            else {
                if (PlayerUpgrades.GetUpgradeRequirement(upgradeType) == PlayerUpgrades.UpgradeType.None || PlayerPrefs.GetInt("" + PlayerUpgrades.GetUpgradeRequirement(upgradeType)) == 1) {
                    backgroundImage.color = new Color(0, 0, 0, 0.35f);
                }
                else {
                    backgroundImage.color = new Color(0, 0, 0, 0.75f);
                }
            }
        }
    }

    [System.Serializable]
    public class UpgradeUnlockPath {
        public PlayerUpgrades.UpgradeType upgradeType;
        public Image[] linkImageArray;
    }
}
