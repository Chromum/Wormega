using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    public Vector2 worldSize = new Vector2(4, 4);
    [SerializeField] public Room[,] rooms;
    public List<Vector2> takenPositions = new List<Vector2>();
    public int gridSizeX, gridSizeY, numberOfRooms = 20;
    public List<GameObject> roomWhiteObj = new List<GameObject>();
    public List<GameObject> ShopOptions = new List<GameObject>();
    public GameObject BossRoom;
    public GameObject StartRoom;
    public List<GameObject> DebugBuildPrefab = new List<GameObject>();
    public List<GameObject> Offsets = new List<GameObject>();
    public Transform mapRoot;


    public delegate void LevelGenerated();
    public LevelGenerated LG;

    private Dictionary<Vector2, MapSpriteSelector> mapSprites = new Dictionary<Vector2, MapSpriteSelector>();
    public void Start()
    {
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        { // make sure we dont try to make more rooms than can fit in our grid
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms(); //lays out the actual mapWWWWWWWWWWW
        SetRoomDoors(); //assigns the doors where rooms would connect
        DrawMap(); //instantiates objects to make up a map
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        transform.localScale = new Vector3(5f, 5f, 5f);
        LG?.Invoke();
        gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        GameManager.instance.sceneLoaded = true;

    }
    public void CreateRooms()
    {
        //setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;
        //magic numbers
        float randomCompare = 0.1f, randomCompareStart = 0.1f, randomCompareEnd = 0.01f;
        //add rooms
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //grab new position
            checkPos = NewPosition();
            //test new position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }
            //finalize position
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);

        }



    }
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
            x = (int)takenPositions[index].x;//capture its x, y position
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
            bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
            if (UpDown)
            { //find the position bnased on the above bools
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
        return checkingPos; //makes the game work not sure how but its probably important
    }
    Vector2 SelectiveNewPosition()
    { // method differs from the above in the two commented ways
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                //instead of getting a room to find an adject empty space, we start with one that only 
                //as one neighbor. This will make it more likely that it returns a room that branches out
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        { // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
            print("Error: could not find position with only one neighbor");
        }
        return checkingPos;
    }
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0; // start at zero, add 1 for each side there is already a room
        if (usedPositions.Contains(checkingPos + Vector2.right))
        { //using Vector.[direction] as short hands, for simplicity
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    public GameObject ReturnObject(Vector2 checkingPos, List<Vector2> usedpositions, Vector2 Dir)
    {
        return mapSprites[checkingPos + Dir].gameObject;
    }
    public void DrawMap()
    {
        bool doneEnemy = false;
        bool doneShop = false;
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue; //skip where there is no room
            }
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 16;//aspect ratio of map sprite
            drawPos.y *= 16;
            if (NumberOfNeighbors(room.gridPos, takenPositions) == 1 && doneEnemy == false)
            {
                room.type = 3;
                doneEnemy = true;
            }

            if (NumberOfNeighbors(room.gridPos, takenPositions) == 1 && doneShop == false && room.type != 3)
            {
                room.type = 4;
                doneShop = true;
            }
            MapSpriteSelector mapper = null;

            switch (room.type)
            {
                case 0:
                    mapper = Object.Instantiate(roomWhiteObj[Random.Range(0,roomWhiteObj.Count)], drawPos, new Quaternion(180, 0f, 0f, 0f)).GetComponent<MapSpriteSelector>();
                    
                    break;
                case 1:
                    mapper = Object.Instantiate(StartRoom, drawPos, new Quaternion(180, 0f, 0f, 0f)).GetComponent<MapSpriteSelector>();
                    break;
                case 2:
                    mapper = Object.Instantiate(roomWhiteObj[Random.Range(0, roomWhiteObj.Count)], drawPos, new Quaternion(180, 0f, 0f, 0f)).GetComponent<MapSpriteSelector>();
                    break;
                case 3:
                    mapper = Object.Instantiate(BossRoom, drawPos, new Quaternion(180, 0f, 0f, 0f)).GetComponent<MapSpriteSelector>();
                    break;
                case 4:
                    mapper = Object.Instantiate(ShopOptions[Random.Range(0, ShopOptions.Count)], drawPos, new Quaternion(180, 0f, 0f, 0f)).GetComponent<MapSpriteSelector>();
                    break;

            }
            mapper.gameObject.transform.parent = mapRoot;
            mapper.type = room.type;
            GameObject g = Instantiate(Offsets[Random.RandomRange(0, Offsets.Count)], mapper.transform);
            g.transform.localScale = new Vector3(0.08394533f, 6.2959f, 0.08394533f);
            g.transform.localPosition = Vector3.zero;
            mapSprites.Add(room.gridPos, mapper);

        }

        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                Vector2 drawPosss = new Vector2(i - gridSizeX, j - gridSizeY);

                if (!takenPositions.Contains(drawPosss))
                {
                    bool isNearTakenPosition = false;

                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            Vector2 nearbyPosition = new Vector2(i - gridSizeX + x, j - gridSizeY + y);

                            if (takenPositions.Contains(nearbyPosition))
                            {
                                isNearTakenPosition = true;
                                break;
                            }
                        }

                        if (isNearTakenPosition)
                        {
                            break;
                        }
                    }

                    if (isNearTakenPosition)
                    {
                        drawPosss.x *= 16;//aspect ratio of map sprite
                        drawPosss.y *= 16;
                        GameObject g = GameObject.Instantiate(DebugBuildPrefab[Random.RandomRange(0,DebugBuildPrefab.Count)], drawPosss, new Quaternion(180, 0f, 0f, 0f));
                        g.gameObject.transform.parent = mapRoot;
                        g.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
                    }
                }
            }
        }
        SetValues();
    }

    public void SetValues()
    {
        foreach (var item in rooms)
        {
            if (item == null)
            {
                continue; //skip where there is no room
            }
            if (item.doorTop)
                mapSprites[item.gridPos].up = ReturnObject(item.gridPos, takenPositions, Vector2.up);
            if (item.doorBot)
                mapSprites[item.gridPos].down = ReturnObject(item.gridPos, takenPositions, Vector2.down);
            if (item.doorRight)
                mapSprites[item.gridPos].right = ReturnObject(item.gridPos, takenPositions, Vector2.right);
            if (item.doorLeft)
                mapSprites[item.gridPos].left = ReturnObject(item.gridPos, takenPositions, Vector2.left);
            LG += mapSprites[item.gridPos].PostGen;
        }

    }

    public void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x, y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                if (y - 1 < 0)
                { //check above
                    rooms[x, y].doorBot = false;
                }
                else
                {
                    rooms[x, y].doorBot = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                { //check bellow
                    rooms[x, y].doorTop = false;
                }
                else
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0)
                { //check left
                    rooms[x, y].doorLeft = false;
                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                { //check right
                    rooms[x, y].doorRight = false;
                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }

    }
}

