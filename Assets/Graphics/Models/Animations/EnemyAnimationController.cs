using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {
    Animator animator;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
     //   AnimationController ac = GetComponent<AnimationController>()
     //       ac.
	}

    void applyDamage(float dmg)
    {
    }
}
