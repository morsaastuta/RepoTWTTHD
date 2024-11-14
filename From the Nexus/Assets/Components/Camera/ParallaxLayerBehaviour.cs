using UnityEngine;

public class ParallaxLayerBehaviour : MonoBehaviour
{
    public float parallaxFactorX;
    public float parallaxFactorY;
    public Transform followedCamera;

    Vector3 lastCamPos = new();
    Vector3 distance = new();

    public void SetConfig(Sprite spriteToRender, float pfX, float pfY, Transform camToFollow, int sort)
    {
        GetComponent<SpriteRenderer>().sprite = spriteToRender;
        parallaxFactorX = pfX;
        parallaxFactorY = pfY;
        followedCamera = camToFollow;
        GetComponent<SpriteRenderer>().sortingOrder = sort;
    }

    public void CopyConfig(ParallaxLayerBehaviour original)
    {
        parallaxFactorX = original.parallaxFactorX;
        parallaxFactorY = original.parallaxFactorY;
        followedCamera = original.followedCamera;
    }

    void Start()
    {
        lastCamPos = followedCamera.position;
    }

    void Update()
    {
        distance = followedCamera.position - lastCamPos;
        lastCamPos = followedCamera.position;

        transform.position += new Vector3(distance.x * parallaxFactorX, distance.y * parallaxFactorY, 0f);
    }
}
