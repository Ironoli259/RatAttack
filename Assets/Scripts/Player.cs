using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] BaseWeapon[] weapons;
    [SerializeField] GameObject levelUpMenu;
    [SerializeField] GameObject magnetEffect;

    AudioSource source;
    SpriteRenderer spriteRenderer;
    Animator animator;

    Material material;

    [SerializeField] internal int playerMaxHP;
    internal float playerHP;
    public float playerPower;
    bool isInvincible;

    internal int goldCoins;

    internal int currentExp;
    internal int expToLevel = 5;
    internal int currentLevel;

    internal Action<int, int> OnExpGained;

    protected void Start()
    {
        this.weapons[0].LevelUp();

        this.playerMaxHP += TitleManager.saveData.permHealthBoost;
        this.playerPower += TitleManager.saveData.permPowerBoost;

        this.goldCoins = 0;
        this.playerPower = 1;
        this.playerHP = this.playerMaxHP;
        this.isInvincible = false;

        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.source = GetComponent<AudioSource>();

        this.material = spriteRenderer.material;
    }

    // Update is called once per frame
    protected void Update()
    {
        //Move
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        transform.position += new Vector3(inputX, inputY) * speed * Time.deltaTime;

        //Flip Sprite
        if (inputX != 0)
        {
            transform.localScale = new Vector3(inputX < 0 ? -1f : 1, 1, 1);
        }

        //Running animation
        animator.SetBool("IsRunning", inputX != 0 || inputY != 0);
    }

    internal void AddExp()
    {
        this.currentExp++;

        if (this.currentExp >= this.expToLevel)
        {
            source.Play();
            this.currentExp -= this.expToLevel;
            this.currentLevel++;
            this.expToLevel *= 2;

            Time.timeScale = 0;
            Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(0.1f);
            levelUpMenu.SetActive(true);
        }

        OnExpGained(currentExp, expToLevel);
    }

    #region LevelUp Methods
    public void LevelUpAxe()
    {
        levelUpMenu.SetActive(false);
        weapons[0].LevelUp();
        weapons[0].transform.localScale = new Vector3(1, 1, 1) * (float)(1 + (0.2 * weapons[0].level));
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpLightning()
    {
        levelUpMenu.SetActive(false);
        weapons[1].LevelUp();
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpFireball()
    {
        levelUpMenu.SetActive(false);
        weapons[2].LevelUp();
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpShield()
    {
        levelUpMenu.SetActive(false);
        weapons[3].LevelUp();
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpArmor()
    {
        levelUpMenu.SetActive(false);
        weapons[4].LevelUp();
        Armor armor = weapons[4].GetComponent<Armor>();
        this.playerMaxHP += armor.armorPoints * weapons[4].level;
        this.playerHP = this.playerMaxHP;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    public void LevelUpPower()
    {
        levelUpMenu.SetActive(false);
        weapons[5].LevelUp();
        PowerOrb powerOrb = weapons[5].GetComponent<PowerOrb>();
        this.playerPower += powerOrb.powerPoints * weapons[5].level;
        Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(10f);
        Time.timeScale = 1;
    }
    #endregion

    internal void AddHealth(float percentage)
    {
        this.playerHP += this.playerMaxHP * (percentage / 100);
        if (this.playerHP > this.playerMaxHP)
            this.playerHP = this.playerMaxHP;
    }

    public float GetHPRatio()
    {
        return (float)playerHP / playerMaxHP;
    }

    internal void OnDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());
            //StartCoroutine(CameraShakeCoroutine());
            StartCoroutine(DamageCoroutine());
            this.playerHP -= damage;
            if (this.playerHP <= 0)
            {
                TitleManager.saveData.goldCoins += this.goldCoins;
                SceneManager.LoadScene("Title");
            }
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        this.isInvincible = true;
        //this.spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        //this.spriteRenderer.color = Color.white;
        this.isInvincible = false;
    }
    
    IEnumerator CameraShakeCoroutine()
    {
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = this.transform.position;
    }

    IEnumerator DamageCoroutine()
    {
        material.SetFloat("_Flash", 0.5f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Flash", 0);
    }

    public void GenerateShield()
    {
        StartCoroutine(ShieldCoroutine());
    }
    IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(3);
        weapons[3].Activate();
    }

    public void StartMagnet()
    {
        StartCoroutine(MagnetCoroutine());
    }
    IEnumerator MagnetCoroutine()
    {
        Time.timeScale = 0.1f;
        Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.33f);
        Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.66f);
        Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(1f);
        magnetEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        magnetEffect.SetActive(false);
        Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.66f);
        Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.33f);
        Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0f);
        Time.timeScale = 1;
    }
}
