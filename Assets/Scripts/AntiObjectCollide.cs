using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiObjectCollide : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider blockCollide;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, blockCollide, true);
    }

}
