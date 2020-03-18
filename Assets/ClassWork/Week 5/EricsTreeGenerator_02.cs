using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Art
{
    public class EricsTreeGenerator_02 : ArtMakerTemplate
    {

        [Header("Trunk")]

        public Material trunkMaterial;
        public float mult = 50;
        public float startRadius = 1;
        private float startRadiusPrivate = 1;

        public float heightIncrement = 1;
        private float heightIncrementPrivate = 1;

        private float heightIncRandomDivPrivate = 0;
        public float heightIncRanDivMin = 0;
        public float heightIncRanDivMax = 1;
        private float currentTotalHeight = 0;

        public int bareTrunk = 0;

        private float xCoord = 0;
        private float yCoord = 0;
        private float zCoord = 0;

        public float xScale = 1;
        public float xOffset = 0;

        public float zScale = 1;
        public float zOffset = 0;

        [Header("Branches")]

        public GameObject[] branchPrefab;
        public float branchWidth = 1;

        private int numberOfBranches = 1;
        public int branchNumberMin = 0;
        public int branchNumberMax = 24;

        public float branchScalerMin = 0;
        public float branchScalerMax = 100;

        private float branchAngle = 0;
        private float branchAnglePrivate = 0;



        [Header("Rows")]
        public int rowNumberMin = 0;
        public int rowNumberMax = 12;
        private int numberOfRows = 1;
        private int numberOfRowsPrivate = 1;

        [Header("Row Scaler")]
        public float rowScaler = 0;
        private float rowScaleMult = 0;
        public float rowScaleMultMin = 0;
        public float rowScaleMultMax = 1;
        private float rowScaleMultPrivate = 1;

        private float rowScaleMultCap = 1;
        public float rowScaleMultCapMin = 0;
        public float rowScaleMultCapmax = 1;

        [Header("Row Rotation")]
        public float initialRowRotationMin = 0;
        public float initialRowRotationMax = 360;
        private float initialRowRotation = 0;

        private float rowRotationMult = 0;
        public float rowRotationMultMin = 0;
        public float rowRotationMultMax = 360;

        private float rowRotationMultPrivate = 0;

        private float rowRotationMultCap = 1;
        public float rowRotationMultCapMin = 0;
        public float rowRotationMultCapMax = 1;


        bool initialized = false;

        public override void MakeArt()
        {

            GameObject tree = MakeTree();
            tree.transform.parent = root.transform;
            tree.name = "Tree_GRP";

            initialized = true;
        }

        GameObject MakeBranch(){
            int branchIndex = Random.Range(0, branchPrefab.Length);
            GameObject branch = Instantiate(branchPrefab[branchIndex]);
            branch.name = "Branch";
            GameObject branchRoot = new GameObject("Branch Root");
            GameObject branchRotationRoot = new GameObject("Branch Rotation Root");
            branch.transform.parent = branchRoot.transform;
            branchRoot.transform.parent = branchRotationRoot.transform;
            branch.transform.localPosition = new Vector3(branchWidth*0.5f,0,0);
            branchRoot.transform.localPosition = Vector3.zero;
            branchRoot.transform.localScale = new Vector3(1, 1, 1);
            return branchRotationRoot;
        }

        GameObject MakeRow()
        {
            GameObject rowBranchRoot = new GameObject("Branch Row");
            numberOfBranches = Random.Range(branchNumberMin, branchNumberMax);

            for (int i = 0; i < numberOfBranches; i++)
            {
                GameObject branch = MakeBranch();
                float branchScaler = 0.01f*Random.Range(branchScalerMin, branchScalerMax);
                branchAngle = (360 / numberOfBranches) * i;
                branch.transform.parent = rowBranchRoot.transform;
                branch.transform.localEulerAngles = new Vector3(0, branchAngle, 0);
                branch.transform.GetChild(0).localScale = new Vector3(branchScaler, branchScaler, 1);

            }

            branchAnglePrivate = 0;
            return rowBranchRoot;
        }

        GameObject MakeTree()
        {
            GameObject Root = new GameObject("Tree_GRP");
            Root.name = "Root";
            GameObject trunk = new GameObject();
            trunk.transform.parent = Root.transform;
            trunk.name = "Tree Trunk";
            TubeRenderer tube = trunk.AddComponent<TubeRenderer>();

            numberOfRows = Random.Range(rowNumberMin, rowNumberMax);
            numberOfRowsPrivate = numberOfRows + bareTrunk + 1;
            startRadiusPrivate = startRadius;

            Vector3[] vecs = new Vector3[numberOfRowsPrivate];
            float[] radius = new float[numberOfRowsPrivate];

            for (int i = 0; i < numberOfRowsPrivate; i++)
            {
                startRadiusPrivate = startRadius * (1 - (i / numberOfRowsPrivate));
                radius[i] = startRadiusPrivate;

                if (i < 1)
                {
                    vecs[i] = Vector3.zero;
                    radius[i] = startRadiusPrivate;

                }
                else 
                {
                    float xNoise = i * (mult * 0.001f) + Random.value * 1000;
                    float zNoise = i * (mult * 0.001f) + Random.value * 1000;

                    int posNegx = (int)(Mathf.Pow(-1, Random.Range(1, 2)));
                    int posNegZ = (int)(Mathf.Pow(-1, Random.Range(1, 2)));

                    heightIncRandomDivPrivate = Random.Range(heightIncRanDivMin, heightIncRanDivMax);
                    heightIncrementPrivate = heightIncrement*(1+heightIncRandomDivPrivate);
                    currentTotalHeight += heightIncrementPrivate;

                    xCoord = ((Mathf.PerlinNoise(0, xNoise)) * xScale * posNegx)+xOffset;
                    yCoord = heightIncrementPrivate + (currentTotalHeight - heightIncrementPrivate);
                    zCoord = ((Mathf.PerlinNoise(0, zNoise)) * zScale * posNegZ)+zOffset;


                    vecs[i] = new Vector3(xCoord, yCoord, zCoord);
                    radius[i] = startRadiusPrivate;

                    if (i > (bareTrunk + 1))
                    {
                        GameObject row = MakeRow();
                        row.transform.parent = trunk.transform;

                        rowScaleMult = Random.Range(rowScaleMultMin, rowScaleMultMax);
                        rowScaleMultPrivate *= 1 - (i * rowScaleMult);


                        initialRowRotation = Random.Range(initialRowRotationMin, initialRowRotationMax);
                        rowRotationMult = Random.Range(rowRotationMultMin, rowRotationMultMax);
                        rowRotationMultCap = Random.Range(rowRotationMultCapMin, rowRotationMultCapMax);

                        rowRotationMultPrivate = initialRowRotation;
                        rowRotationMultPrivate += (i * rowRotationMult);

                        rowScaleMultCap = Random.Range(rowScaleMultCapMin, rowScaleMultCapmax);


                        for (int j = 0; j < row.transform.childCount; j++)
                        {
                            if (rowScaleMultPrivate < rowScaleMultCap)
                            {
                                rowScaleMultPrivate = rowScaleMultCap;
                                row.transform.GetChild(j).GetChild(0).localScale *= (1 + rowScaler) * rowScaleMultPrivate;
                                print(row.transform.GetChild(j).GetChild(0).name);
                            }
                            else
                            {
                                row.transform.GetChild(j).GetChild(0).localScale *= (1 + rowScaler) * rowScaleMultPrivate;
                                print(row.transform.GetChild(j).GetChild(0).name);
                            }

                            if (rowRotationMultPrivate < rowRotationMultCap)
                            {
                                Vector3 rowRotation = row.transform.GetChild(j).localEulerAngles;
                                rowRotation.z = rowRotationMultCap;
                                row.transform.GetChild(j).localEulerAngles = rowRotation;
                                print(row.transform.GetChild(j).name);
                            }
                            else
                            {
                                Vector3 rowRotation = row.transform.GetChild(j).localEulerAngles;
                                rowRotation.z = rowRotationMultPrivate;
                                row.transform.GetChild(j).localEulerAngles = rowRotation;
                                print(row.transform.GetChild(j).name);
                            }
                        }

                        row.transform.localPosition = vecs[i];

                    }
                }
            }

            startRadiusPrivate = 1;
            tube.SetPoints(vecs, radius, Color.white);
            tube.material = trunkMaterial;
            xCoord = 0;
            yCoord = 0;
            zCoord = 0;

            rowScaleMultPrivate = 1;
            rowRotationMultPrivate = 0;
            currentTotalHeight = 0;
            return Root;
        }

        //this is to select from a specific array of prefabs (aka select from only stick prefabs vs selecting from leafy prefabs)
        // use if statements to select certain branch prefabs so like
        // if row.transform. Y component? idk > 3 then ... (look at next line)
        //int index = Random.Range(0, branchPrefab.Length);
        //branchPrefab[index];
        

}
}