using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    // membros
    public Rigidbody leftUpperLeg;
    public Rigidbody rightUpperLeg;
    public Rigidbody leftLowerLeg;
    public Rigidbody rightLowerLeg;
    public Rigidbody leftFoot;
    public Rigidbody rightFoot;
    List<Rigidbody> legsParts = new List<Rigidbody>();

    Rigidbody rBody;
     float force = 20f;

     Vector3 forceVector = Vector3.left * 10f;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();

        legsParts.Add(rightUpperLeg);
        legsParts.Add(leftUpperLeg);
        legsParts.Add(rightLowerLeg);
        legsParts.Add(leftLowerLeg);
        legsParts.Add(rightFoot);
        legsParts.Add(leftFoot);
    }


    public void MoveLeftUpper(float force)
    {
        leftUpperLeg.AddRelativeForce(forceVector * force);
    }

    public void MoveRightUpper(float force)
    {
        rightUpperLeg.AddRelativeForce(forceVector * force);
    }

    public void MoveLeftLower(float force)
    {
        leftLowerLeg.AddRelativeForce(forceVector * force);
    }

    public void MoveRightLower(float force)
    {
        rightLowerLeg.AddRelativeForce(forceVector * force);
    }

    public void MoveLeftFoot(float force)
    {
        leftFoot.AddRelativeForce(forceVector * force);
    }

    public void MoveRightFoot(float force)
    {
        rightFoot.AddRelativeForce(forceVector * force);
    }

    public void MoveHips(float force)
    {
        rBody.AddForce(Vector3.up * force);
    }

}
