using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    int count = -1;
    private GameObject currentRoom;
    private int[] pastRooms = {0,0};
    public GameObject[] rooms = new GameObject[10];
    private LinkedList<GameObject> map = new LinkedList<GameObject>();


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
    void GetNextRoom(PointerEventData data) {
      int index = Random.Range(0,9);
      while (InArray(index,pastRooms)) {
        index = Random.Range(0,9);
      }
      GameObject room = rooms[index];
      // Add clues and decor to the room
      map.AddFirst(room);
      pastRooms[(count+1)%2] = index;
      count = count + 1;
      Destroy(currentRoom);
      currentRoom = Instantiate(room,new Vector3(0,0,0),Quaternion.identity);
    }

    void OpenRooms(PointerEventData data) {

    }

    void GhostEncounter(PointerEventData data) {

    }

    GameObject GetCurrentRoom() {
        return currentRoom;
    }

    void Start() {

      int index = Random.Range(0,9);
      pastRooms[(count+1)%2] = index;
      count = count + 1;
      currentRoom = Instantiate(rooms[index],new Vector3(0,0,0),Quaternion.identity);

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
}
