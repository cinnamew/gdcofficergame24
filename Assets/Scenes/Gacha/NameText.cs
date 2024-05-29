using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] int numSecsToWait;
    [SerializeField] float howMuchMovement = 1;
    private Vector3 ogPos;
    private bool firstTime = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<TMP_Text>();
    }

    public void OnEnable() {
        if(firstTime) {
            ogPos = transform.position;
            firstTime = false;
        }
        //print("og: " + ogPos + "; transform: " + transform.position);
        text.color = new Color(1, 1, 1, 0);
        transform.position = ogPos;
        StartCoroutine(Translucence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Translucence() {
        yield return new WaitForSeconds(numSecsToWait);
        for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                //print(i);
                text.color = new Color(1, 1, 1, i);
                transform.position += Vector3.up*i*howMuchMovement;
                yield return null;
            }
    }
}
