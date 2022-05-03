using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades {

    public event EventHandler<OnUpgradeUnlockedEventArgs> OnUpgradeUnlocked;
    public class OnUpgradeUnlockedEventArgs : EventArgs {
        public UpgradeType upgradeType;
    }

    public enum UpgradeType {
        None,
        Health1,
        Health2,
        Health3,
        Bomb1,
        Bomb2,
        Pickaxe1,
        Pickaxe2,
        Pickaxe3,
        Attack1,
        Attack2,
        MaximumTorchTime1,
        MaximumTorchTime2,
        MaximumTorchTime3,
        ExtraTorchTime1,
        ExtraTorchTime2
    }

    private List<UpgradeType> unlockedUpgradeTypeList;

    public PlayerUpgrades() {
        unlockedUpgradeTypeList = new List<UpgradeType>();
    }

    private void UnlockUpgrade(UpgradeType upgradeType) {
        PlayerPrefs.SetInt("" + upgradeType, 1);
        unlockedUpgradeTypeList.Add(upgradeType);
        OnUpgradeUnlocked?.Invoke(this, new OnUpgradeUnlockedEventArgs { upgradeType = upgradeType });
    }

    public bool IsUpgradeUnlocked(UpgradeType upgradeType) {
        return unlockedUpgradeTypeList.Contains(upgradeType);
    }

    public bool CanUnlock(UpgradeType upgradeType) {
        UpgradeType upgradeRequirement = GetUpgradeRequirement(upgradeType);

        if (upgradeRequirement != UpgradeType.None) {
            if (UpgradeSystem.CheckRequirementOres(upgradeType)) {
                if (IsUpgradeUnlocked(upgradeRequirement) || PlayerPrefs.GetInt("" + upgradeRequirement) == 1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        else {
            if (UpgradeSystem.CheckRequirementOres(upgradeType)) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    public static UpgradeType GetUpgradeRequirement(UpgradeType upgradeType) {
        switch (upgradeType) {
            case UpgradeType.Health2: return UpgradeType.Health1;
            case UpgradeType.Health3: return UpgradeType.Health2;

            case UpgradeType.Bomb1: return UpgradeType.Health1;
            case UpgradeType.Bomb2: return UpgradeType.Bomb1;

            case UpgradeType.Pickaxe2: return UpgradeType.Pickaxe1;
            case UpgradeType.Pickaxe3: return UpgradeType.Pickaxe2;

            case UpgradeType.Attack2: return UpgradeType.Attack1;
            case UpgradeType.Attack1: return UpgradeType.Pickaxe1;

            case UpgradeType.MaximumTorchTime2: return UpgradeType.MaximumTorchTime1;
            case UpgradeType.MaximumTorchTime3: return UpgradeType.MaximumTorchTime2;

            case UpgradeType.ExtraTorchTime1: return UpgradeType.MaximumTorchTime1;
            case UpgradeType.ExtraTorchTime2: return UpgradeType.ExtraTorchTime1;
        }
        return UpgradeType.None;
    }

    public bool TryUnlockUpgrade(UpgradeType upgradeType) {
        if (CanUnlock(upgradeType)) {
            UnlockUpgrade(upgradeType);
            return true;
        }
        else {
            return false;
        }
    }
}
