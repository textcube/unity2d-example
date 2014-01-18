using UnityEngine;
using System.Collections;
public class Enemy2D : MonoBehaviour {
    public GameObject bloodPrefab;
    public GameObject deathPrefab;
    Animator animator;
    public int life = 3;
	void Start () {
        animator = GetComponent<Animator>();
	}
    void GotoState(string some)
    {
        //animator.SetTrigger(some);
        animator.Play(some);
    }
    void Damage()
    {
        if (life < 1) return;
        if (bloodPrefab) Instantiate(bloodPrefab, transform.position, Quaternion.identity);
        GotoState("Damage");
        life--;
        if (life < 1)
        {
            GotoState("Death");
        }
    }
    void OnDeath()
    {
        if (deathPrefab) Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }
    void Update()
    {
        if (animator == null) return;
    }

}
