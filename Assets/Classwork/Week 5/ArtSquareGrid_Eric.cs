using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script randomly selects items from an array
 * and scatters them randomly
 * It parents the items to the root object
 * so that they will be destroyed when 'rebuild' is true
 */

namespace Art
{
    public class ArtSquareGrid_Eric : ArtMakerTemplate
    {
        public GameObject[] objects;
        public int amount = 10;

        public override void MakeArt()
        {
            for (int i = 0; i < amount; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    GameObject thing = Instantiate(objects[0]);
                    thing.transform.position = new Vector3(i, j, 0);
                    float scale = Random.value;
                    thing.transform.localScale = new Vector3(scale, scale, scale);
                    thing.transform.parent = root.transform;
                }
            }
        }
    }
}