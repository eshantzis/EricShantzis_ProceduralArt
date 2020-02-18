using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Art
{
    public class EricsTreeGenerator_01 : ArtMakerTemplate
    {
        public GameObject[] branchPrefab;
        public int branchRows = 10;
        public float branchDistance = 0;
        public float branchHeightMult = 1;
        public float maxScale = 1;

        public override void MakeArt()
        {
            for (int j = 0; j < branchRows; j++)
            {
                GameObject thing = Instantiate(branchPrefab[0]);
                thing.transform.position = new Vector3(branchDistance, j* branchHeightMult, 0);
                float scale = Random.value;
                thing.transform.localScale = new Vector3(scale, scale, scale);
                thing.transform.parent = root.transform;
            }
        }
    }
}