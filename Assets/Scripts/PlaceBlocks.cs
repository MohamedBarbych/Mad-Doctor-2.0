using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlaceBlocks : MonoBehaviour, IResettable
{
    public static PlaceBlocks Instance { get; private set; }
    public Tile wallTile;
    public Tile[] emptyTiles;
    public Tilemap emptyMap;
    public Tilemap wallMap;
    public Transform playerTf;
    public GameObject basicGuyPrefab;
    public GameObject bossPrefab; // Add a reference to the boss prefab
    public GameObject coinPrefab;
    public long totalRooms = 50;
    public static int coinCount = 0 ;
    

    private const int MaxMapSize = 50;
    private List<Room> rooms = new List<Room>();
    private Tile sessionTile;

    public UnityEvent OnCoinsGenerated; // Event to notify when coins are generated

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (OnCoinsGenerated == null)
                OnCoinsGenerated = new UnityEvent();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeDungeon();
    }

    public void InitializeDungeon()
    {
        GenerateDungeon();
        GridManager.Instance.CreateGrid();
        PlacePlayerInFirstRoom();
        PlaceEnemiesInDungeon();
        PlaceBossInLastRoom(); // Call method to place the boss
        PlaceCoinsInDungeon();
        OnCoinsGenerated.Invoke(); // Trigger the event after coins are generated
    }

    public void Reset()
    {
        ClearMaps();
        ClearEnemiesAndCoins();
    }

    public void OnSceneReload()
    {
        InitializeDungeon();
    }

    void OnDestroy()
    {
        if (ResetManager.Instance != null)
        {
            ResetManager.Instance.UnregisterResettable(this);
        }
    }

    public void ResetDungeon()
    {
        ClearMaps();
        ClearEnemiesAndCoins();
        InitializeDungeon();
    }

    public void ResetBlocks()
    {
        ClearMaps();
        ClearEnemiesAndCoins();
        InitializeDungeon();
    }

    void ClearEnemiesAndCoins()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }
    }

    void ClearMaps()
    {
        emptyMap.ClearAllTiles();
        wallMap.ClearAllTiles();
    }

    void GenerateDungeon()
    {
        ClearMaps();
        rooms.Clear();
        Vector2Int mapSize = new Vector2Int(MaxMapSize, MaxMapSize);

        sessionTile = emptyTiles[Random.Range(0, emptyTiles.Length)];

        for (int i = 0; i < totalRooms; i++)
        {
            Room room = GenerateRandomRoom(mapSize);
            while (IsOverlapping(room, rooms))
            {
                room = GenerateRandomRoom(mapSize);
            }
            rooms.Add(room);
            PlaceRoom(room, sessionTile);
        }

        ConnectRoomsWithCorridors();
    }

    Room GenerateRandomRoom(Vector2Int mapSize)
    {
        Vector2Int size = new Vector2Int(Random.Range(5, 10), Random.Range(5, 10));
        Vector2Int location = new Vector2Int(Random.Range(0, mapSize.x - size.x), Random.Range(0, mapSize.y - size.y));
        return new Room(location, size);
    }

    bool IsOverlapping(Room newRoom, List<Room> existingRooms)
    {
        foreach (var room in existingRooms)
        {
            if (newRoom.Overlaps(room))
            {
                return true;
            }
        }
        return false;
    }

    void PlaceRoom(Room room, Tile sessionTile)
    {
        for (int x = room.location.x; x < room.location.x + room.size.x; x++)
        {
            for (int y = room.location.y; y < room.location.y + room.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                emptyMap.SetTile(pos, sessionTile);
            }
        }
        OutlineRoom(room);
    }

    void OutlineRoom(Room room)
    {
        for (int x = room.location.x - 1; x <= room.location.x + room.size.x; x++)
        {
            Vector3Int top = new Vector3Int(x, room.location.y + room.size.y, 0);
            Vector3Int bottom = new Vector3Int(x, room.location.y - 1, 0);
            PlaceTileIfEmpty(top, wallTile);
            PlaceTileIfEmpty(bottom, wallTile);
        }

        for (int y = room.location.y - 1; y <= room.location.y + room.size.y; y++)
        {
            Vector3Int left = new Vector3Int(room.location.x - 1, y, 0);
            Vector3Int right = new Vector3Int(room.location.x + room.size.x, y, 0);
            PlaceTileIfEmpty(left, wallTile);
            PlaceTileIfEmpty(right, wallTile);
        }
    }

    public void PlacePlayerInFirstRoom()
    {
        if (rooms.Count > 0)
        {
            Vector2Int center = rooms[0].Center();
            playerTf.position = new Vector3(center.x + 2, center.y + 2, playerTf.position.z);
        }
    }

    void PlaceEnemiesInDungeon()
    {
        for (int i = 0; i < rooms.Count - 1; i++) // Place enemies in all rooms except the last one
        {
            Vector3 spawnPosition = new Vector3(rooms[i].Center().x, rooms[i].Center().y, 0);
            GameObject enemy = Instantiate(basicGuyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyFollowAStar>().player = playerTf;

            Meander meander = enemy.GetComponent<Meander>();
            if (meander != null)
            {
                meander.InitializeEnemy();
            }
        }
    }

    void PlaceBossInLastRoom()
    {
        if (rooms.Count > 0 && bossPrefab != null)
        {
            Room lastRoom = rooms[rooms.Count - 1];
            Vector3 spawnPosition = new Vector3(lastRoom.Center().x, lastRoom.Center().y, 0);
            GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            boss.GetComponent<EnemyFollowAStar>().player = playerTf;

            Meander meander = boss.GetComponent<Meander>();
            if (meander != null)
            {
                meander.InitializeEnemy();
            }
        }
    }

    void PlaceCoinsInDungeon()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("Coin prefab is not assigned in the inspector!");
            return;
        }

        foreach (var room in rooms)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2Int coinPosition = room.GetRandomPositionInsideRoom();
                Vector3 spawnPosition = new Vector3(coinPosition.x, coinPosition.y, 0);
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                coinCount++;
            }
        }
    }

    bool PlaceTileIfEmpty(Vector3Int chosenPos, Tile chosenTile)
    {
        if ((wallMap.GetTile(chosenPos) == null) && (emptyMap.GetTile(chosenPos) == null))
        {
            wallMap.SetTile(chosenPos, chosenTile);
            return true;
        }
        return false;
    }

    void ConnectRoomsWithCorridors()
    {
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            CreateCorridor(rooms[i].Center(), rooms[i + 1].Center());
        }
    }

    void CreateCorridor(Vector2Int start, Vector2Int end)
    {
        Vector2Int current = start;
        while (current.x != end.x)
        {
            Vector3Int pos = new Vector3Int(current.x, current.y, 0);
            emptyMap.SetTile(pos, sessionTile);
            wallMap.SetTile(pos, null);

            PlaceCorridorWalls(pos);
            current.x += (end.x > current.x) ? 1 : -1;
        }

        while (current.y != end.y)
        {
            Vector3Int pos = new Vector3Int(current.x, current.y, 0);
            emptyMap.SetTile(pos, sessionTile);
            wallMap.SetTile(pos, null);

            PlaceCorridorWalls(pos);
            current.y += (end.y > current.y) ? 1 : -1;
        }
    }

    void PlaceCorridorWalls(Vector3Int corridorPos)
    {
        Vector3Int[] adjacentPositions = {
            new Vector3Int(corridorPos.x - 1, corridorPos.y, 0),
            new Vector3Int(corridorPos.x + 1, corridorPos.y, 0),
            new Vector3Int(corridorPos.x, corridorPos.y - 1, 0),
            new Vector3Int(corridorPos.x, corridorPos.y + 1, 0)
        };

        foreach (var pos in adjacentPositions)
        {
            PlaceTileIfEmpty(pos, wallTile);
        }
    }

    public class Room
    {
        public Vector2Int location;
        public Vector2Int size;
        public List<Room> connectedRooms = new List<Room>();

        public float Left => location.x;
        public float Right => location.x + size.x;
        public float Bottom => location.y;
        public float Top => location.y + size.y;

        public Room(Vector2Int location, Vector2Int size)
        {
            this.location = location;
            this.size = size;
        }

        public bool Overlaps(Room other)
        {
            return !(Right < other.Left || Left > other.Right || Top < other.Bottom || Bottom > other.Top);
        }

        public Vector2Int Center()
        {
            return new Vector2Int(location.x + size.x / 2, location.y + size.y / 2);
        }

        public Vector2Int GetRandomPositionInsideRoom()
        {
            int x = Random.Range(location.x + 1, location.x + size.x - 1);
            int y = Random.Range(location.y + 1, location.y + size.y - 1);
            return new Vector2Int(x, y);
        }
    }
}
