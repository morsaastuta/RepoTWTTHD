using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform camToFollow;
    [SerializeField] GameObject layerPrefab;

    [Header("Layers")]
    [SerializeField] List<Sprite> layers = new();

    [Header("Horizontal parallax")]
    [SerializeField] bool activeX;
    [SerializeField] bool repeatedX;
    [SerializeField] List<float> parallaxFactorX = new();

    [Header("Vertical parallax")]
    [SerializeField] bool activeY;
    [SerializeField] bool repeatedY;
    [SerializeField] List<float> parallaxFactorY = new();

    List<GameObject> instantiatedLayers = new();
    List<GameObject> horizontalLesserCopies = new();
    List<GameObject> horizontalGreaterCopies = new();
    List<GameObject> verticalLesserCopies = new();
    List<GameObject> verticalGreaterCopies = new();

    void Awake()
    {
        transform.position = new Vector3(camToFollow.position.x, camToFollow.position.y, 0);

        int index = 0;

        foreach (Sprite s in layers)
        {
            if (s != null)
            {
                ParallaxLayerBehaviour layer = Instantiate(layerPrefab, transform).GetComponent<ParallaxLayerBehaviour>();

                if (activeX && activeY) layer.SetConfig(s, parallaxFactorX[index], parallaxFactorY[index], camToFollow, -index);
                else if (activeX) layer.SetConfig(s, parallaxFactorX[index], 1, camToFollow, -index);
                else if (activeY) layer.SetConfig(s, 1, parallaxFactorY[index], camToFollow, -index);

                instantiatedLayers.Add(layer.gameObject);
            }

            index++;
        }
        CreateCopies();
    }

    void CreateCopies()
    {
        foreach (GameObject layer in instantiatedLayers)
        {
            if (activeX && repeatedX)
            {
                GameObject lesserX = Instantiate(layer, transform);
                lesserX.GetComponent<ParallaxLayerBehaviour>().CopyConfig(layer.GetComponent<ParallaxLayerBehaviour>());
                lesserX.transform.position = new Vector3(layer.transform.position.x - layer.GetComponent<SpriteRenderer>().size.x, layer.transform.position.y, 0);
                horizontalLesserCopies.Add(lesserX);

                GameObject greaterX = Instantiate(layer, transform);
                greaterX.GetComponent<ParallaxLayerBehaviour>().CopyConfig(layer.GetComponent<ParallaxLayerBehaviour>());
                greaterX.transform.position = new Vector3(layer.transform.position.x + layer.GetComponent<SpriteRenderer>().size.x, layer.transform.position.y, 0);
                horizontalGreaterCopies.Add(greaterX);
            }

            if (activeY && repeatedY)
            {
                GameObject lesserY = Instantiate(layer, transform);
                lesserY.GetComponent<ParallaxLayerBehaviour>().CopyConfig(layer.GetComponent<ParallaxLayerBehaviour>());
                lesserY.transform.position = new Vector3(layer.transform.position.x, layer.transform.position.y - layer.GetComponent<SpriteRenderer>().size.y, 0);
                verticalLesserCopies.Add(lesserY);

                GameObject greaterY = Instantiate(layer, transform);
                greaterY.GetComponent<ParallaxLayerBehaviour>().CopyConfig(layer.GetComponent<ParallaxLayerBehaviour>());
                greaterY.transform.position = new Vector3(layer.transform.position.x, layer.transform.position.y + layer.GetComponent<SpriteRenderer>().size.y, 0);
                verticalGreaterCopies.Add(greaterY);
            }

        }
    }

    void Update()
    {
        for (int i = 0; i < instantiatedLayers.Count; i++)
        {
            GameObject layer = instantiatedLayers[i];

            if (activeX && repeatedX)
            {
                if (Vector3.Distance(layer.transform.position, camToFollow.position) >
                    Vector3.Distance(horizontalGreaterCopies[i].transform.position, camToFollow.position))
                {
                    horizontalLesserCopies[i].transform.position += new Vector3(layer.GetComponent<SpriteRenderer>().size.x, 0, 0);
                    layer.transform.position += new Vector3(layer.GetComponent<SpriteRenderer>().size.x, 0, 0);
                    horizontalGreaterCopies[i].transform.position += new Vector3(layer.GetComponent<SpriteRenderer>().size.x, 0, 0);
                }
                else if (Vector3.Distance(layer.transform.position, camToFollow.position) >
                    Vector3.Distance(horizontalLesserCopies[i].transform.position, camToFollow.position))
                {
                    horizontalLesserCopies[i].transform.position -= new Vector3(layer.GetComponent<SpriteRenderer>().size.x, 0, 0);
                    layer.transform.position -= new Vector3(layer.GetComponent<SpriteRenderer>().size.x, 0, 0);
                    horizontalGreaterCopies[i].transform.position -= new Vector3(layer.GetComponent<SpriteRenderer>().size.x, 0, 0);
                }
            }

            if (activeY && repeatedY)
            {
                if (Vector3.Distance(layer.transform.position, camToFollow.position) >
                    Vector3.Distance(verticalGreaterCopies[i].transform.position, camToFollow.position))
                {
                    verticalLesserCopies[i].transform.position += new Vector3(0, layer.GetComponent<SpriteRenderer>().size.y, 0);
                    layer.transform.position += new Vector3(0, layer.GetComponent<SpriteRenderer>().size.y, 0);
                    verticalGreaterCopies[i].transform.position += new Vector3(0, layer.GetComponent<SpriteRenderer>().size.y, 0);
                }
                else if (Vector3.Distance(layer.transform.position, camToFollow.position) >
                    Vector3.Distance(verticalLesserCopies[i].transform.position, camToFollow.position))
                {
                    verticalLesserCopies[i].transform.position -= new Vector3(0, layer.GetComponent<SpriteRenderer>().size.y, 0);
                    layer.transform.position -= new Vector3(0, layer.GetComponent<SpriteRenderer>().size.y, 0);
                    verticalGreaterCopies[i].transform.position -= new Vector3(0, layer.GetComponent<SpriteRenderer>().size.y, 0);
                }
            }
        }
    }
}
