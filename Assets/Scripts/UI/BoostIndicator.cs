using UnityEngine;

public class BoostIndicator : MonoBehaviour
{
    bool isActive = false;


    void FixedUpdate()
    {
        if (isActive)
        {
            //Rotate and grow
            //transform.Rotate(0, 0, 5);
            transform.localScale *= 1.02f;
        }
    }

    public void Activate()
    {
        isActive = true;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;

        //Reset rotation and size
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        this.gameObject.SetActive(false);
    }
}
