using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Art
{
    public class ArtSquareEric : ArtMakerTemplate
    {
        public GameObject[] objects;
        public int amount = 10;
        private float hueValue = 1;

        public override void MakeArt()
        {
            hueValue = Random.Range(0, .9f);
            int amt = Random.Range(30, amount);
            for (int i = 0; i < amt; i++)
            {
                GameObject g;
                if (i == 1)
                {
                    g = Instantiate(objects[1]);
                    amount = 1;
                    g.transform.parent = root.transform;
                    g.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV(hueValue, hueValue + .1f, 1, 1, 1, 1));
                    float t = Random.value * .4f;
                    g.transform.localScale = new Vector3(t, t, t);
                    g.transform.position = Random.insideUnitSphere * .5f;
                }
                g = Instantiate(objects[0]);
                g.transform.parent = root.transform;
                g.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV(hueValue, hueValue + .1f, .25f, .25f, 1, 1));
                float s = Random.value * .1f;
                g.transform.localScale = new Vector3(s, s, s);
                g.transform.position = Random.insideUnitSphere*.5f;
            }
        }
    }
}