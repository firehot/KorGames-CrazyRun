using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public float disableTime;
    private BoxCollider myCollider;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            myCollider.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            GameManager.instance.CollectDiamond();
        }
    }

    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);

        if (myCollider!=null)
        {
            myCollider.enabled = true;
        }

    }

    private IEnumerator DisableDiamond()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void EnableDiamond()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);

        if (myCollider != null)
        {
            myCollider.enabled = true;
        }
    }
}
