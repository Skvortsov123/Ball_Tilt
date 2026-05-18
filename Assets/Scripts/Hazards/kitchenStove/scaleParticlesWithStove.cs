using System.Collections.Generic;
using UnityEngine;

public class scaleParticlesWithStove : MonoBehaviour
{
    private HashSet<GameObject> particls = new HashSet<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        findParticleChildren();
        scaleParticles();
    }

    private void findParticleChildren()
    {  
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<ParticleSystem>())
            particls.Add(transform.GetChild(i).gameObject);
        }
    }
    private void scaleParticles()
    {
        foreach(GameObject g in particls){
            g.transform.localScale = this.transform.localScale;
        }
    }

}
