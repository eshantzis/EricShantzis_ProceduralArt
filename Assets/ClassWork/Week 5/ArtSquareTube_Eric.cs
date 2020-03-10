using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Art
{
    public class ArtSquareTube_Eric : ArtMakerTemplate
    {
        public int detail = 100;
        public float mult = 1;

		public override void MakeArt()
		{
            GameObject g = new GameObject();
            g.transform.SetParent(root.transform);
            TubeRenderer tube = g.AddComponent<TubeRenderer>();

            Vector3[] vecs = new Vector3[detail];
            float[] radius = new float[detail];
            float r = Random.value*1000;

            for (int i = 0; i < detail; i++)
            {
                float j = i*mult+r;
                vecs[i] = new Vector3(
                    Mathf.PerlinNoise(j*4.3321f,j)-.5f,
                    Mathf.PerlinNoise(j,j*3.342f)-.5f,
                    Mathf.PerlinNoise(j * 4.3321f, j*.345f) - .5f)*2;
                radius[i] = (Mathf.Cos((i/detail*Mathf.PI*2)-1)*-.015f);

            }

            tube.SetPoints(vecs, radius, Color.white);
            tube.material = new Material(Shader.Find("Standard"));


        }
	}

}