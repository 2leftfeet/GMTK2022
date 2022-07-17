using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Dice face indices are as follows:
//0 - forward -> left
//1 - down -> right
//2 - right -> down
//3 - back
//4 - left -> forward
//5 - up


public static class DiceRotations
{
    public static Quaternion[] rotations = {
        Quaternion.Euler(0f, 90f, -90f),
        Quaternion.Euler(0f, 180f, 90f), 
        Quaternion.Euler(0f,0f,180f), 
        Quaternion.Euler(90f, 0f, 90f), 
        Quaternion.Euler(-90f,0f,-90f), 
        Quaternion.Euler(0f, 90f, 0f) 
    };
}

public class DiceGameObject : MonoBehaviour
{
    [SerializeField] DiceSideDatabase diceSideData;
    
    public CardItem parentCard;

    MeshRenderer meshRenderer;
    Rigidbody body;

    bool isMovingToOverview = false;
    bool isMovingToResolution = false;
    Vector3 targetPosition;
    Quaternion targetRotation;
    Vector3 angularSpeed;
    Vector3 targetScale;

    void Start()
    {
        body = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();
        Material[] newMaterials = new Material[6];

        for(int i = 0; i < 6; i++)
        {
            newMaterials[i] = diceSideData.GetMaterial(parentCard.item.diceSides[i]);
        }

        meshRenderer.materials = newMaterials;
    }

    public int GetSideUp()
    {
        float[] dotProducts = new float[6];

        dotProducts[0] = Vector3.Dot(Vector3.up, -transform.right);
        dotProducts[1] = Vector3.Dot(Vector3.up, transform.right);
        dotProducts[2] = Vector3.Dot(Vector3.up, -transform.up);
        dotProducts[3] = Vector3.Dot(Vector3.up, -transform.forward);
        dotProducts[4] = Vector3.Dot(Vector3.up, transform.forward);
        dotProducts[5] = Vector3.Dot(Vector3.up, transform.up);

        float maxDot = dotProducts.Max();
        int maxIndex = System.Array.IndexOf(dotProducts, maxDot);

        return maxIndex;
    }

    public void MoveToOverview(Vector3 position)
    {
        body.isKinematic = true;
        isMovingToOverview = true;
        isMovingToResolution = false;

        targetPosition = position;
        targetRotation = DiceRotations.rotations[GetSideUp()];
    }

    public void MoveToResolution(Vector3 position)
    {
        body.isKinematic = true;
        isMovingToOverview = false;
        isMovingToResolution = true;

        targetPosition = position + Random.insideUnitSphere * 0.3f;
        angularSpeed = Random.onUnitSphere * 360f;
        //targetScale = Vector3.zero;

    }

    bool isScalingDown = false;
    public void ScaleDown()
    {
        isScalingDown = true;

        targetScale = Vector3.zero;
    }

    void Update()
    {
        if(isMovingToOverview)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 3f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
        }

        if(isMovingToResolution)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 3f * Time.deltaTime);
            transform.Rotate(angularSpeed * Time.deltaTime);
        }
        
        if(isScalingDown)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 3f * Time.deltaTime);
        }
    }

}
