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
        public float branchHorizontalDistance = 1;
        public float branchHeightMult = 1;
        public float branchWidth = 1;
        public float branchScaler = 1;
        public float branchScalerMin = 0;
        public float branchScalerMax = 100;
        public int numberOfBranches = 1;

        bool initialized = false;

        public override void MakeArt()
        {

            GameObject treeTrunk = MakeTrunk();
            treeTrunk.transform.parent = root.transform;
            treeTrunk.name = "Tree Trunk";

            //GameObject treeBranch = MakeBranch();
            //treeBranch.transform.parent = treeTrunk.transform;
            //treeBranch.name = "Tree Branch";

            GameObject treeRow = MakeRow();
            treeRow.transform.parent = treeTrunk.transform;
            treeRow.name = "Branch Row";

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

            for (int j = 0; j < numberOfBranches; j++){
                GameObject rowBranch = MakeBranch();
                float branchScaler = Random.Range(branchScalerMin, branchScalerMax);
                rowBranch.transform.parent = rowBranchRoot.transform;
                rowBranch.transform.localEulerAngles = new Vector3(0, 360 / (j+1), 0);
                transform.GetChild(1).localScale = new Vector3((1 + (branchScaler * 0.01f)), 1, 1);
            }
            return rowBranchRoot;
        }
}
}