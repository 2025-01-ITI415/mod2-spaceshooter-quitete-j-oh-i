using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class ProjectileHero : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;

    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField]                                                         // a
    private eWeaponType _type;

    //Addition 1
    // Homing parameters
    public float homingStrength = 0.5f; // Controls how fast the missile tracks the target
    private Transform target;        // The target the missile is homing towards

    // This public property masks the private field _type
    public eWeaponType type
    {                                              // c
        get { return (_type); }
        set { SetType(value); }
    }


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();                                     // d
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }

        //Addition 2
        // Homing behavior if this is a missile
        if (target != null && _type == eWeaponType.missile)
        {
            HomeInOnTarget();
        }
    }

    //Addition 3
    // This function will find the nearest enemy target for homing
    private void FindTarget()
    {
        // Look for enemies (assuming they are tagged as "Enemy")
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        // Assign the closest enemy to the target variable
        target = closestEnemy;
    }

    // This function handles the homing behavior
    private void HomeInOnTarget()
    {
        if (target != null)
        {
            // Calculate direction to the target
            Vector3 directionToTarget = target.position - transform.position;

            // Smoothly rotate the missile towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, homingStrength * Time.deltaTime);

            // Update missile velocity towards the target
            rigid.velocity = transform.forward * rigid.velocity.magnitude;
        }
    }
    /// <summary>
    /// Sets the _type private field and colors this projectile to match the 
    ///   WeaponDefinition.
    /// </summary>
    /// <param name="eType">The eWeaponType to use.</param>
    public void SetType(eWeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(_type);
        rend.material.color = def.projectileColor;
    
            // If it's a missile, find the target
    if (_type == eWeaponType.missile)
     {
        Debug.Log("Missile fired, finding target...");  // Debugging line
        FindTarget();  // Locate the nearest enemy
     }
    }


    /// <summary>
    /// Allows Weapon to easily set the velocity of this ProjectileHero
    /// </summary>
    public Vector3 vel
    {
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }

}
