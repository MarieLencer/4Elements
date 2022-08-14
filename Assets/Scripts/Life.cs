using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Life : MonoBehaviour {
    protected int lifePoints { get; set; }
    protected int maxLifePoints { get; set; }
    protected Animator animator { get; set; }

    public abstract void Dying();
    public abstract void Spawn();
    public abstract void CheckLife();

}