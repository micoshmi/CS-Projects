using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Colliderble
{
    // Damage structrure
    public int[] damagePoint = { 1,2,3,4,5,6};
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f }; // pushing the enemy when weapon collides

    // Weapon Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Weapon Swing
    private Animator anim;
    private float cooldown = 0.5f; //attack animation cooldown(how often can you swing)
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
        if(Input.GetKeyDown(KeyCode.Space)) // Hitting left mouse button to swing our sword
        {
            //Swing timer
            if (Time.time - lastSwing > cooldown) 
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter") 
        {
            if (coll.name == "Player")
            {
                return;
            }
            // Create a new damage object, then we'll send it to the fighter(monster) we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }

    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

}
