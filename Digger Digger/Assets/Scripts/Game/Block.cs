using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

    private float health;
    private float maxHealth = 50f;
    private Transform canvas;

    private LayerMask blockLayer;
    private BlockType blockType;
    private Sprite dirtSprite;
    private Sprite cobblestoneSprite;
    private Sprite obsidianSprite;
    private Sprite monsterSprite;

    private OreType oreType;
    private SpriteRenderer oreSpriteRenderer;
    private Sprite twoGoldSprite;
    private Sprite threeGoldSprite;
    private Sprite fourGoldSprite;
    private Sprite oneEmeraldSprite;
    private Sprite twoEmeraldSprite;
    private Sprite threeEmeraldSprite;
    private Sprite oneRubySprite;
    private Sprite twoRubySprite;
    private Sprite threeRubySprite;
    private Sprite oneDiamondSprite;
    private Sprite torchSprite;
    private Sprite bombSprite;

    private Transform dirtParticle;
    private Transform stoneParticle;
    private Transform deepslateParticle;
    private Transform particleEffect;

    public enum BlockType {
        Dirt,
        Stone,
        Deepslate,
        Monster
    }

    public enum OreType {                               
        Zero,                                           
        TwoGold,                                        
        ThreeGold,
        FourGold,
        OneEmerald,
        TwoEmerald,
        ThreeEmerald,
        OneRuby,
        TwoRuby,
        ThreeRuby,
        OneDiamond,

        Torch,
        Bomb
    }

    private void Awake() {
        dirtParticle = Resources.Load<Transform>("Prefabs/dirtParticle");
        stoneParticle = Resources.Load<Transform>("Prefabs/stoneParticle");
        deepslateParticle = Resources.Load<Transform>("Prefabs/deepslateParticle");

        blockLayer = LayerMask.GetMask("Block");

        dirtSprite = Resources.Load<Sprite>("Sprites/BlockTypes/spDirt");
        cobblestoneSprite = Resources.Load<Sprite>("Sprites/BlockTypes/spStone");
        obsidianSprite = Resources.Load<Sprite>("Sprites/BlockTypes/spDeepslate");
        monsterSprite = Resources.Load<Sprite>("Sprites/BlockTypes/spMonster");

        oreSpriteRenderer = transform.Find("oreSprite").GetComponent<SpriteRenderer>();
        twoGoldSprite = Resources.Load<Sprite>("Sprites/OreTypes/spTwoGold");
        threeGoldSprite = Resources.Load<Sprite>("Sprites/OreTypes/spThreeGold");
        fourGoldSprite = Resources.Load<Sprite>("Sprites/OreTypes/spFourGold");
        oneEmeraldSprite = Resources.Load<Sprite>("Sprites/OreTypes/spOneEmerald");
        twoEmeraldSprite = Resources.Load<Sprite>("Sprites/OreTypes/spTwoEmerald");
        threeEmeraldSprite = Resources.Load<Sprite>("Sprites/OreTypes/spThreeEmerald");
        oneRubySprite = Resources.Load<Sprite>("Sprites/OreTypes/spOneRuby");
        twoRubySprite = Resources.Load<Sprite>("Sprites/OreTypes/spTwoRuby");
        threeRubySprite = Resources.Load<Sprite>("Sprites/OreTypes/spThreeRuby");
        oneDiamondSprite = Resources.Load<Sprite>("Sprites/OreTypes/spDiamond");
        torchSprite = Resources.Load<Sprite>("Sprites/OreTypes/spTorch");
        bombSprite = Resources.Load<Sprite>("Sprites/OreTypes/spBomb");


        int blockTypeRandomNumber = Random.Range(0,100);

        if (blockTypeRandomNumber < 10) {
            blockType = BlockType.Deepslate;
            GetComponent<SpriteRenderer>().sprite = obsidianSprite;
            maxHealth = 200;

            //Obsidian -> zero %30, oneruby %30, tworuby %20, onediamond %5, torch%15
            int oreTypeRandomNumber = Random.Range(0,100);

            if (oreTypeRandomNumber < 5) {
                if (transform.position.y < -50) {
                    oreType = OreType.OneDiamond;
                }
                else {
                    oreType = OreType.Zero;
                }
            }
            else if(oreTypeRandomNumber < 10) {
                oreType = OreType.Torch;
            }
            else if(oreTypeRandomNumber < 20) {
                oreType = OreType.ThreeRuby;
            }
            else if(oreTypeRandomNumber < 35) {
                oreType = OreType.TwoRuby;
            }
            else if (oreTypeRandomNumber < 100) {
                oreType = OreType.Zero;
            }
        }

        else if (blockTypeRandomNumber < 20) {
            blockType = BlockType.Monster;
            GetComponent<SpriteRenderer>().sprite = monsterSprite;
            maxHealth = 10;
        }

        else if (blockTypeRandomNumber < 55) {
            blockType = BlockType.Stone;
            GetComponent<SpriteRenderer>().sprite = cobblestoneSprite;
            maxHealth = 60;

            //Cobblestone -> zero %35, twogold %15, threegold %10, fourgold %5, oneemerald %10, twoemerald %10, threeemerald %5, torch%10
            int oreTypeRandomNumber = Random.Range(0, 100);
            if (oreTypeRandomNumber < 5) {
                oreType = OreType.ThreeEmerald;
            }
            else if (oreTypeRandomNumber < 10) {
                oreType = OreType.Torch;
            }
            else if (oreTypeRandomNumber < 16) {
                oreType = OreType.TwoEmerald;
            }
            else if (oreTypeRandomNumber < 23) {
                oreType = OreType.OneEmerald;
            }
            else if (oreTypeRandomNumber < 30) {
                oreType = OreType.FourGold;
            }
            else if (oreTypeRandomNumber < 40) {
                oreType = OreType.ThreeGold;
            }
            else if (oreTypeRandomNumber < 50) {
                oreType = OreType.TwoGold;
            }
            else if (oreTypeRandomNumber < 100) {
                oreType = OreType.Zero;
            }
        }

        else if (blockTypeRandomNumber < 100) {
            blockType = BlockType.Dirt;
            GetComponent<SpriteRenderer>().sprite = dirtSprite;
            maxHealth = 30;

            //Dirt -> zero %30, twogold % 25, threegold %15, fourgold %10, torch %10, bomb %10
            int oreTypeRandomNumber = Random.Range(0, 100);
            if (oreTypeRandomNumber < 10) {
                oreType = OreType.Bomb;
                maxHealth = 10;
            }
            else if (oreTypeRandomNumber < 15) {
                oreType = OreType.Torch;
            }
            else if (oreTypeRandomNumber < 25) {
                oreType = OreType.FourGold;
            }
            else if (oreTypeRandomNumber < 35) {
                oreType = OreType.ThreeGold;
            }
            else if (oreTypeRandomNumber < 50) {
                oreType = OreType.TwoGold;
            }
            else if (oreTypeRandomNumber < 100) {
                oreType = OreType.Zero;
            }
        }

        ChangeOreSprites();
        health = maxHealth;
        canvas = transform.Find("BlockCanvas").transform;
        canvas.gameObject.SetActive(false);

        if (blockType == BlockType.Monster || oreType == OreType.Bomb) {
            Destroy(canvas.gameObject);
        }
    }

    private void Start() {
        if (blockType == BlockType.Dirt) {
            particleEffect = dirtParticle;
        }
        if (blockType == BlockType.Stone) {
            particleEffect = stoneParticle;
        }
        if (blockType == BlockType.Deepslate) {
            particleEffect = deepslateParticle;
        }
    }

    private void ChangeOreSprites() {
        if (oreType == OreType.TwoGold) {
            oreSpriteRenderer.sprite = twoGoldSprite;
        }
        if (oreType == OreType.ThreeGold) {
            oreSpriteRenderer.sprite = threeGoldSprite;
        }
        if (oreType == OreType.FourGold) {
            oreSpriteRenderer.sprite = fourGoldSprite;
        }
        if (oreType == OreType.OneEmerald) {
            oreSpriteRenderer.sprite = oneEmeraldSprite;
        }
        if (oreType == OreType.TwoEmerald) {
            oreSpriteRenderer.sprite = twoEmeraldSprite;
        }
        if (oreType == OreType.ThreeEmerald) {
            oreSpriteRenderer.sprite = threeEmeraldSprite;
        }
        if (oreType == OreType.OneRuby) {
            oreSpriteRenderer.sprite = oneRubySprite;
        }
        if (oreType == OreType.TwoRuby) {
            oreSpriteRenderer.sprite = twoRubySprite;
        }
        if (oreType == OreType.ThreeRuby) {
            oreSpriteRenderer.sprite = threeRubySprite;
        }
        if (oreType == OreType.OneDiamond) {
            oreSpriteRenderer.sprite = oneDiamondSprite;
        }
        if (oreType == OreType.Torch) {
            oreSpriteRenderer.sprite = torchSprite;
        }
        if (oreType == OreType.Bomb) {
            oreSpriteRenderer.sprite = bombSprite;

        }
    }

    public void TakeDamage(float damage) {
        if (particleEffect != null) {
            Transform particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(particle.gameObject, 1f);
        }
        health -= damage;

        if (health <= 0f) {
            Break();
        }

        if (canvas != null) {
            canvas.gameObject.SetActive(true);
            canvas.Find("health").GetComponent<Image>().fillAmount = health / maxHealth;
        }
    }

    private void Break() {
        DropItem();
        GameManager.RefreshItemUI();
        Destroy(gameObject, 0.1f);
    }

    private void DropItem() {
        if (oreType == OreType.TwoGold) {
            Storage.currentNumberOfGold += 2;
        }
        if (oreType == OreType.ThreeGold) {
            Storage.currentNumberOfGold += 3;
        }
        if (oreType == OreType.FourGold) {
            Storage.currentNumberOfGold += 4;
        }
        if (oreType == OreType.OneEmerald) {
            Storage.currentNumberOfEmerald += 1;
        }
        if (oreType == OreType.TwoEmerald) {
            Storage.currentNumberOfEmerald += 2;
        }
        if (oreType == OreType.ThreeEmerald) {
            Storage.currentNumberOfEmerald += 3;
        }
        if (oreType == OreType.OneRuby) {
            Storage.currentNumberOfRuby += 1;
        }
        if (oreType == OreType.TwoRuby) {
            Storage.currentNumberOfRuby += 2;
        }
        if (oreType == OreType.ThreeRuby) {
            Storage.currentNumberOfRuby += 3;
        }
        if (oreType == OreType.OneDiamond) {
            Storage.currentNumberOfDiamond += 1;
        }
        if (oreType == OreType.Torch) {
            Lighting.TorchBuff();
        }
        if (oreType == OreType.Bomb) {
            Explode();
        }
        if (blockType == BlockType.Monster) {
            Fight();
        }

        GameManager.RefreshItemUI();
    }

    private void Explode() {
        //Explode animation
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f, blockLayer);
        Movement.Instance.TakeDamage(PlayerPrefs.GetInt("BombDamage"));
        foreach (var collider in colliders) {
            Destroy(collider.gameObject);
        }
    }

    private void Fight() {
        GameManager.Fight();
    }
}
