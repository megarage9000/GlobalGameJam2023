using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    IMMOBOLIZED, INVINCIBLE, DESTROYED, DISARMED, SPEDUP, FURY
};

public class EntityStateController : MonoBehaviour
{
    // Status and time remaining for status
    public Dictionary<Status, uint> statusDict;

    public void applyStatusForDuration(Status status, uint seconds)
    {
        statusDict[status] = seconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        statusDict = new Dictionary<Status, uint>();
    }

    IEnumerator timedUpdates()
    {
        foreach (Status s in statusDict.Keys)
        {
            if (statusDict[s] == 1)
            {
                statusDict.Remove(s);
            } else
            {
                statusDict[s]--;
            }
        }

        yield return new WaitForSeconds(1.0f);
    }
}
