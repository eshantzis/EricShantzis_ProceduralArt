using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Art
{
    public class ArtSquareTube_Eric : ArtMakerTemplate
    {
        public int detail = 20;
        public float mult = 50;
        public float startRadius = 1;
        private float startRadiusPrivate = 1;
        public float heightIncrement = 0.5f;

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
                if (i < 0)  {
                    vecs[i] = Vector3.zero;
                    radius[i] = startRadius;
                }
                else
                {
                    float j = i * (mult * 0.001f) + r;
                    vecs[i] = new Vector3(Mathf.PerlinNoise(0, j), heightIncrement*i, Mathf.PerlinNoise(0,j));
                    //radius[i] = startRadius-(startRadius*(i/detail));
                    startRadiusPrivate += -(startRadius / detail);
                    radius[i] = startRadiusPrivate;

                }

            }

            startRadiusPrivate = startRadius;
            tube.SetPoints(vecs, radius, Color.white);
            tube.material = new Material(Shader.Find("Standard"));


        }
	}

}