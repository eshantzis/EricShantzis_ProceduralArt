using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Art
{
    public class EricsTreeGenerator_01 : ArtMakerTemplate
    {

        public GameObject trunkPrefab;
        public float trunkHeight = 10;
        public float trunkWidth = 1;

        public GameObject[] branchPrefab;
        public float branchWidth = 1;
        public int numberOfBranches = 1;
        public float branchScalerMin = 0;
        public float branchScalerMax = 100;

        public int numberOfRows = 1;
        public float initialRowHeight = 0;
        public float rowHeightMult = 1;
        public float rowScaler = 1;
        private float rowScalerPrivate = 1;


        bool initialized = false;

        public override void MakeArt()
        {

            GameObject tree = MakeTree();
            tree.transform.parent = root.transform;
            tree.name = "Tree_GRP";

            initialized = true;
        }


        GameObject MakeTrunk(){
            GameObject Root = new GameObject("Tree_GRP");
            Root.name = "Root";
            GameObject Trunk = Instantiate(trunkPrefab);
            Trunk.transform.parent = Root.transform;
            Trunk.transform.localPosition = new Vector3(0, trunkHeight, 0);
            Trunk.transform.localScale = new Vector3(trunkWidth, trunkHeight, trunkWidth);
            return Root;

        }

        GameObject MakeBranch(){
            GameObject branch = Instantiate(branchPrefab[0]);
            branch.name = "Branch";
            GameObject branchRoot = new GameObject("Branch Root");
            GameObject branchRotationRoot = new GameObject("Branch Rotation Root");
            float branchHorizontalDistance = trunkWidth * 0.5f;
            branch.transform.parent = branchRoot.transform;
            branchRoot.transform.parent = branchRotationRoot.transform;
            branch.transform.localPosition = new Vector3(branchWidth*0.5f,0,0);
            branchRoot.transform.localPosition = new Vector3(branchHorizontalDistance, 0, 0);
            branchRoot.transform.localScale = new Vector3(1, 1, 1);
            return branchRotationRoot;
        }

        GameObject MakeRow(){
            GameObject rowBranchRoot = new GameObject("Branch Row");

            for (int i = 0; i < numberOfBranches; i++)
            {
                GameObject branch = MakeBranch();
                float branchScaler = 0.01f*Random.Range(branchScalerMin, branchScalerMax);
                branch.transform.parent = rowBranchRoot.transform;
                branch.transform.localEulerAngles = new Vector3(0, (360/numberOfBranches)*i, 0);
                branch.transform.GetChild(0).localScale = new Vector3(branchScaler, branchScaler, 1);

            }
            return rowBranchRoot;
        }

        GameObject MakeTree()
        {
            GameObject treeTrunk = MakeTrunk();
            treeTrunk.transform.parent = root.transform;
            treeTrunk.name = "Tree Trunk";

            for (int i = 0; i < numberOfRows; i++)
            {
               
                GameObject row = MakeRow();
                row.transform.parent = treeTrunk.transform;
                row.transform.localPosition = new Vector3(0, initialRowHeight + rowHeightMult * i, 0);
                print(row.transform.childCount);


                //to scale the branches down overtime
                //kinda funky rn, work on it 
                for (int j = 0; j < row.transform.childCount; j++)
                {
                    row.transform.GetChild(j).GetChild(0).localScale = Vector3.one * rowScalerPrivate;
                    print(row.transform.GetChild(j).GetChild(0).name);
                }
                rowScalerPrivate *= rowScaler * .9f;
            }
            return treeTrunk;
        }
        //adjust rowscaler to not get smaller every time it is created

        //this is to select from a specific array of prefabs (aka select from only stick prefabs vs selecting from leafy prefabs)
        // use if statements to select certain branch prefabs so like
        // if row.transform. Y component? idk > 3 then ... (look at next line)
        //int index = Random.Range(0, branchPrefab.Length);
        //branchPrefab[index];

        //using the arttube script ot create geometry  for a trunk
        //the art tube creates a tube along an array of vector3s
        //make the tube, get the array of vector 3s, then spawn rows along the vector 3
        // he also asked about rotating branches a little to make sure they don't point straight at the camera


}
}