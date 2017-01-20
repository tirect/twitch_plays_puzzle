using System.Collections.Generic;
using UnityEngine;

public class place_field : MonoBehaviour
{

    [SerializeField] private Texture bounds;
    [SerializeField] private List<Texture> scarlet;

	// Use this for initialization
    private void Start() {
        var scarlet_index = 50;
        for (var i = 0; i < 14; i++)
        {
            for (var j = 0; j < 18; j++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(8.5f - j, 0, 6f - i);
                var renderer_component = cube.GetComponent<Renderer>();
                if (j >= 4 && j < 14 && i > 5 && i <= 11)
                {
                    Debug.Log(scarlet_index);
                    renderer_component.material.mainTexture = scarlet[scarlet_index++];
                } else
                {
                    renderer_component.material.mainTexture = bounds;
                }
            }
            if (i > 5 && i <= 11)
            {
                scarlet_index -= 20;
            }
        }
    }

    // Update is called once per frame
    private void Update () {
		
	}
}
