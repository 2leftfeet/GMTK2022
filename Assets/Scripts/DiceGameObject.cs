using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Dice face indices are as follows:
//0 - forward
//1 - down
//2 - right
//3 - back
//4 - left
//5 - up


public static class DiceRotations
{
    public static Quaternion[] rotations = {
        Quaternion.Euler(-90f,0f,-90f),
        Quaternion.Euler(0f,0f,180f),
        Quaternion.Euler(0f, 180f, 90f),
        Quaternion.Euler(90f, 0f, 90f),
        Quaternion.Euler(0f, 90f, -90f),
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
    Vector3 targetOverviewPosition;
    Quaternion targetOverviewRotation;

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

        dotProducts[0] = Vector3.Dot(Vector3.up, transform.forward);
        dotProducts[1] = Vector3.Dot(Vector3.up, -transform.up);
        dotProducts[2] = Vector3.Dot(Vector3.up, transform.right);
        dotProducts[3] = Vector3.Dot(Vector3.up, -transform.forward);
        dotProducts[4] = Vector3.Dot(Vector3.up, -transform.right);
        dotProducts[5] = Vector3.Dot(Vector3.up, transform.up);

        float maxDot = dotProducts.Max();
        int maxIndex = System.Array.IndexOf(dotProducts, maxDot);

        return maxIndex;
    }

    public void MoveToOverview(Vector3 position)
    {
        body.isKinematic = true;
        isMovingToOverview = true;

        targetOverviewPosition = position;
        targetOverviewRotation = DiceRotations.rotations[GetSideUp()];
    }

    void Update()
    {
        if(isMovingToOverview)
        {
            transform.position = Vector3.Lerp(transform.position, targetOverviewPosition, 3f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetOverviewRotation, 3f * Time.deltaTime);
        }
    }

}
