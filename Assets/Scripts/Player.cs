using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] private int playerMaxHP;
    [SerializeField] public BaseWeapon[] weapons;
    [SerializeField] GameObject magnetEffect;    

    AudioSource source;
    SpriteRenderer spriteRenderer;
    Animator animator;

    Material material;

    private float playerHP;
    private float playerPower;
    bool isInvincible;

    internal int goldCoins;

    internal int currentExp;
    internal int expToLevel = 5;
    internal int currentLevel;

    internal Action<int, int> OnExpGained;
    int character;

    public int PlayerMaxHP { get => playerMaxHP; set => playerMaxHP = value; }
    internal float PlayerHP { get => playerHP; set => playerHP = value; }
    public float PlayerPower { get => playerPower; set => playerPower = value; }

    private void Start()
    {
        this.character = PlayerPrefs.GetInt("SelectedCharacter");

        this.PlayerMaxHP += TitleManager.saveData.permHealthBoost;
        this.PlayerPower += TitleManager.saveData.permPowerBoost;

        this.goldCoins = 0;
        this.PlayerPower = 1;
        this.PlayerHP = this.PlayerMaxHP;
        this.isInvincible = false;

        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.source = GetComponent<AudioSource>();

        this.material = spriteRenderer.material;   
        
        for(int i=0; i< weapons.Length; i++)
        {
            weapons[i].level = 0;
        }

        if(character == 0)
        {
            //levelUpMenu = GameObject.FindGameObjectWithTag("LvlAdv").GetComponent<GameObject>();
            this.weapons[0].LevelUp();
            //this.weapons[1].LevelUp();
            //this.weapons[2].LevelUp();
        }
        else if(character == 1)
        {
            //levelUpMenu = GameObject.FindGameObjectWithTag("LvlPro").GetComponent<GameObject>();
            this.weapons[0].level++;
            //this.weapons[1].level++;
            //this.weapons[2].level++;
            StartCoroutine(ProfessorCoroutine());
        }
        else if (character == 2)
        {
            //levelUpMenu = GameObject.FindGameObjectWithTag("LvlPal").GetComponent<GameObject>();
            this.weapons[0].level++;
            //this.weapons[1].level++;
            //this.weapons[2].level++;

            StartCoroutine(PaladinCoroutine());
        }

        Debug.Log(this.weapons[0].level);
        Debug.Log(this.weapons[1].level);
        Debug.Log(this.weapons[2].level);
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
        if (animator != null)
            animator.SetBool("IsRunning", inputX != 0 || inputY != 0);
        else
        {
            //this.animator = GetComponent<Animator>();
            Console.WriteLine("animator");
        }        
    }

    internal void AddExp()
    {
        this.currentExp++;

        if (this.currentExp >= this.expToLevel)
        {
            //source.Play();
            this.currentExp -= this.expToLevel;
            this.currentLevel++;
            this.expToLevel *= 2;

            Time.timeScale = 0;
            Camera.main.GetComponent<PlayerCamera>().depthOfField.focusDistance.Override(0.1f);
            PlayerManager.isLvlUpActive = true;
        }

        OnExpGained(currentExp, expToLevel);
    }


    internal void AddHealth(float percentage)
    {
        this.PlayerHP += this.PlayerMaxHP * (percentage / 100);
        if (this.PlayerHP > this.PlayerMaxHP)
            this.PlayerHP = this.PlayerMaxHP;
    }

    public float GetHPRatio()
    {
        return (float)PlayerHP / PlayerMaxHP;
    }

    internal void OnDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());            
            StartCoroutine(DamageCoroutine());
            this.PlayerHP -= damage;
            if (this.PlayerHP <= 0)
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
        yield return new WaitForSecondsRealtime(0.1f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.1f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.1f);
        Camera.main.GetComponent<PlayerCamera>().target.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSecondsRealtime(0.1f);
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
        if (TitleManager.IsPostProcessActive)
        {
            Time.timeScale = 0.1f;
            Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.33f);
            Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.66f);
            Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(1f);
        }        
        magnetEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        magnetEffect.SetActive(false);
        if(TitleManager.IsPostProcessActive)
        {
            Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.66f);
            Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0.33f);
            Camera.main.GetComponent<PlayerCamera>().chromaticAberration.intensity.Override(0f);
            Time.timeScale = 1;
        }        
    }

    //--------------- Paladin and Professor Attacks ---------------//
    public void ActivateWeapon(int index)
    {
        weapons[index].Activate();
    }
    public void DeactivateWeapon(int index)
    {
        weapons[index].Deactivate();
    }

    IEnumerator PaladinCoroutine()
    {
        while (true)
        {
            animator.SetTrigger("HAttack");
            yield return new WaitForSeconds(2f);

            Debug.Log(weapons[1].level);
            if (weapons[1].level > 0)
                animator.SetTrigger("GAttack");
            else
                animator.SetTrigger("HAttack");
            yield return new WaitForSeconds(2f);

            animator.SetTrigger("HAttack");
            yield return new WaitForSeconds(2f);

            Debug.Log(weapons[1].level);
            if (weapons[1].level > 0)
                animator.SetTrigger("GAttack");
            else
                animator.SetTrigger("HAttack");
            yield return new WaitForSeconds(2f);

            Debug.Log(weapons[2].level);
            if (weapons[2].level > 0)
            {
                animator.SetTrigger("Smite");
                if (TitleManager.IsPostProcessActive)
                {
                    yield return new WaitForSeconds(0.3f);
                    StartCoroutine(CameraShakeCoroutine());
                }                
            }
            else
                animator.SetTrigger("HAttack");
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ProfessorCoroutine()
    {
        while (true)
        {
            animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);

            Debug.Log("1:" + weapons[1].level);
            if (weapons[1].level > 0)
                animator.SetTrigger("ThrowGreen");
            else
                animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);

            animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);

            Debug.Log("2:" + weapons[2].level);
            if (weapons[2].level > 0)
                animator.SetTrigger("ThrowPurple");
            else
                animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);

            Debug.Log("1:" + weapons[1].level);
            if (weapons[1].level > 0)
                animator.SetTrigger("ThrowGreen");
            else
                animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);

            animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);

            Debug.Log("2:" + weapons[2].level);
            if (weapons[2].level > 0)
                animator.SetTrigger("ThrowPurple");
            else
                animator.SetTrigger("ThrowRed");
            yield return new WaitForSeconds(2f);
            DisplayLevels();
        }
    }

    public void DisplayLevels()
    {
        for(int i =0 ; i < 6 ; i++)
        {
            Debug.Log(weapons[i].level);
        }
    }
}
