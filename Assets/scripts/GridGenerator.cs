using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    const int COL = 13;
    const int ROW= 13;
    const int Off_ROW= 4;
    const int Off_COL= 4;

    [SerializeField] GameObject UnbreakableWallPref;
   
    [SerializeField] GameObject BreakableWallPref;
    void Start()
    {

        GenerateWalls();
        GenerateUnbreakbleWalls();
    }


    void Update()
    {
        
    }
    void GenerateWalls()
    {
        bool generateThisTile = false;

        generateThisTile = GenerateSpecialFirstandLastBreakables(generateThisTile, 0);
        generateThisTile = GenerateSpecialSecondFirstandLastBreakables(generateThisTile, 1);

        for (int i = 2; i < ROW - 2; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                if (generateThisTile)
                {

                    GameObject newUnBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newUnBreakable.transform.localPosition = new Vector3(j * Off_COL, 2, -i * Off_ROW);
                }
                generateThisTile = !generateThisTile;
            }
        }
        generateThisTile = GenerateSpecialSecondFirstandLastBreakables(generateThisTile, ROW - 2);
        generateThisTile = GenerateSpecialFirstandLastBreakables(generateThisTile, ROW - 1);
    }
  

    private bool GenerateSpecialSecondFirstandLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < COL; j++)
        {
            if (generateThisTile)
            {
                if (j != 0 && j != COL - 1)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * Off_COL, 2, -row * Off_ROW);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }
    private bool GenerateSpecialFirstandLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < COL; j++)
        {
            if (generateThisTile)
            {
                if (j != 1 && j != COL - 2)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * Off_COL, 2, -row * Off_ROW);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    void GenerateUnbreakbleWalls()
    {
        for (int i = 1; i < ROW; i += 2)
        {
            bool generateThisTile = false;
            for (int j = 0; j < COL; j++)
            {
                if(generateThisTile)
                {
                    GameObject newUnbreakable = Instantiate(UnbreakableWallPref, gameObject.transform);
                    newUnbreakable.transform.localPosition = new Vector3(j * Off_COL, 2, -i * Off_ROW);
                }
                generateThisTile = !generateThisTile;
            }
        }
    }
}
