using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    int count = -1;
    private GameObject currentRoom;
    private int[] pastRooms = {0,0};
    public GameObject[] rooms = new GameObject[5];
    private GameObject firstRoom;
    private GameObject prevRoom;
    private LinkedList<GameObject> map = new LinkedList<GameObject>();
    public GameObject ghost;
    bool SpookieBoi = false;


    // Checks if num is in the array
    bool InArray(int num, int[] array) {
        for (int i = 0; i < array.Length; i++) {
          if (array[i] == num) {
            return true;
          }
        }
        return false;
    }

    // Creates the next room
    public void GetNextRoom() {
      int randNum = Random.Range(0,5);
        while (InArray(randNum,pastRooms)) {
            randNum = Random.Range(0,5);
        }
        GameObject room = rooms[randNum];
        map.AddFirst(room);
        pastRooms[(count+1)%2] = randNum;
        count = count + 1;
        prevRoom = currentRoom;
        var roomPos = currentRoom.transform.Find("GameObject").transform.Find("exit");
        var localExitRot = roomPos.localRotation;
        currentRoom = Instantiate(room, roomPos.position, roomPos.localRotation);
        currentRoom.transform.localRotation = roomPos.rotation;
    }

    public void GetPrevRoom()
    {
        int randNum = Random.Range(0, 5);
        while (InArray(randNum, pastRooms))
        {
            randNum = Random.Range(0, 5);
        }
        GameObject room = rooms[randNum];
        // Add clues and decor to the room
        map.AddFirst(room);
        pastRooms[(count + 1) % 2] = randNum;
        count = count + 1;
        prevRoom = currentRoom;
        var roomPos = currentRoom.transform;
        var localExitRot = roomPos.rotation;
        currentRoom = Instantiate(room, roomPos.position, roomPos.localRotation);
        roomPos = prevRoom.transform;
        localExitRot = roomPos.localRotation;
        currentRoom.transform.Rotate(Vector3.up,180);
    }

    public void RemoveRoom()
    {
        Destroy(prevRoom);
    }

    void OpenRooms() {
        LinkedListNode<GameObject> prev = map.First;
        LinkedListNode<GameObject> current = map.First;
        for (int index = 0; index < map.Count;index++){
          if (current.Value != currentRoom) {
               // Instantiate(current.Value,prev.Value.transform.Find("exit").position,transform.rotation);
            }
            prev = current;
            current = current.Next;
        }
    }

    //PointerEventData data
    void GhostEncounter() {
        //ghost = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        GetNextRoom();
        GameObject nextRoom = map.First.Value;
        Vector3 offset = new Vector3(-1,.75f,.5f);
        Vector3 ghostspawn = this.transform.position + (nextRoom.transform.position);
        ghost = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        SpookieBoi = true;
    }

   void UpdateGhost()
    {
        float speed = 2.0f;
        GameObject target = map.First.Value;
        float step = speed * Time.deltaTime; // calculate distance to move
        Debug.Log(target.transform.TransformPoint(target.transform.localPosition));
        ghost.transform.position = Vector3.MoveTowards(ghost.transform.position, -target.transform.position, step);
    }

    void Start() {
        int index = Random.Range(0,5);
        pastRooms[(count+1)%2] = index;
        count = count + 1;
        firstRoom = rooms[index];
        map.AddFirst(rooms[index]);
        currentRoom = Instantiate(rooms[index],new Vector3(-1,0,-1),Quaternion.identity);

        // This sets up the event listeners

        // EventTrigger doorEvent = GetComponents<EventTrigger>(); Letting go of door event
        // EventTrigger.Entry door = new EventTrigger.Entry();
        // door.eventID = INSERT EVENT TYPE HERE
        // door.callback.AddListener((data) => {GetNextRoom;});
        // doorEvent.triggers.Add(door);

        // EventTrigger fullMap = GetComponents<EventTrigger>(); Finding the final clue
        // EventTrigger.Entry lastClue = new EventTrigger.Entry();
        // lastClue.eventID = INSERT EVENT TYPE HERE
        // lastClue.callback.AddListener((data) => {OpenRooms;});
        // fullMap.triggers.Add(lastClue);

        // EventTrigger foundGhost = GetComponents<EventTrigger>(); In room with ghost encounter
        // EventTrigger.Entry  spookieBoi = new EventTrigger.Entry();
        // spookieBoi.eventID = INSERT EVENT TYPE HERE
        // spookieBoi.callback.AddListener((data) => {GhostEncounter;});
        // foundGhost.tiggers.Add(spookieBoi);

    }

    private void Update()
    {
        
       /*if (Input.GetKeyUp("space")) {
            GetNextRoom();
        } else if(Input.GetKeyUp("left shift"))
        {
            RemoveRoom();
        } else if(Input.GetKeyUp("right shift"))
        {
            GetPrevRoom();
        } else if (Input.GetKeyUp("g"))
        {
            GhostEncounter();

        }
        if (SpookieBoi)
        {
            UpdateGhost();
        }*/
    }
}
