using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class WalkerAgent : Agent
{
    HumanController controller;
    public Transform target;
    Rigidbody rBody;
    public Transform[] targetPoints;
    List<Rigidbody> legsParts = new List<Rigidbody>();
    float forceMultiplier = 12f;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        controller = GetComponent<HumanController>();
        legsParts.Add(controller.rightUpperLeg);
        legsParts.Add(controller.leftUpperLeg);
        legsParts.Add(controller.rightLowerLeg);
        legsParts.Add(controller.leftLowerLeg);
        legsParts.Add(controller.rightFoot);
        legsParts.Add(controller.leftFoot);
    }

    public override void OnEpisodeBegin()
    {
        // spawna na posição
        transform.localPosition = new Vector3(0, 0.4f, 0);

        // zera os movimentos
        rBody.angularVelocity = Vector3.zero;
        rBody.velocity = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);


        foreach (Rigidbody part in legsParts)
        {
            part.angularVelocity = Vector3.zero;
            part.velocity = Vector3.zero;
            part.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }


        // escolhe um spawn para target
        int randomIndex = Random.Range(0, targetPoints.Length);
        target.transform.position = targetPoints[randomIndex].position;

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localRotation);

        foreach (Rigidbody part in legsParts)
        {
            sensor.AddObservation(part.velocity.x);
            sensor.AddObservation(part.velocity.z);
            sensor.AddObservation(part.position);
            sensor.AddObservation(part.rotation);
        }

    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // mover as partes
        controller.MoveLeftUpper(actionBuffers.ContinuousActions[0] * forceMultiplier);
        controller.MoveRightUpper(actionBuffers.ContinuousActions[1] * forceMultiplier);

        controller.MoveLeftLower(actionBuffers.ContinuousActions[2] * forceMultiplier);
        controller.MoveRightLower(actionBuffers.ContinuousActions[3] * forceMultiplier);

        controller.MoveLeftFoot(actionBuffers.ContinuousActions[4] * forceMultiplier);
        controller.MoveRightFoot(actionBuffers.ContinuousActions[5] * forceMultiplier);
        controller.MoveHips(actionBuffers.ContinuousActions[6] * forceMultiplier);

        float sqrDistance = Vector3.SqrMagnitude(target.localPosition - transform.localPosition);

        // dar recompensa se chegou no alvo
        if (sqrDistance < 1.5f)
        {
            AddReward(1.0f);
            EndEpisode();
        }

        // punir se está muito próximo ao chão (corpo e partes)
        if (transform.localPosition.y < -1.8f)
        {
            AddReward(-0.6f);
        }

        if (controller.leftUpperLeg.transform.localPosition.y < -0.15f || controller.rightUpperLeg.transform.localPosition.y < -0.15f)
        {
            AddReward(-0.2f);
        }

        if (controller.leftLowerLeg.transform.localPosition.y < -0.3f || controller.rightLowerLeg.transform.localPosition.y < -0.3f)
        {
            AddReward(-0.1f);
        }


        // punir e terminar episódio se caiu ou voou
        if (transform.localPosition.y < -5f || transform.localPosition.y > 8.0f)
        {
            AddReward(-1f);
            EndEpisode();
        }

    }

}
