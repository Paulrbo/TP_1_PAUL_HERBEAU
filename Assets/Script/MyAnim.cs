using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnim : MonoBehaviour
{
    [SerializeField] private float speed = 1; //variable qui s'occupe de changer la vitesse de rotation
    [SerializeField] public int nbreCube = 1; //variable qui s'occupe de changer le nombre de cubes qui tournent
    private int lastNbreCube = 0; //variable qui s'occupe de mémoriser le nombre de cubes présents avant un changement (pour détecter dès que l'on change le nombre de cubes)
    [SerializeField] private float rayonCercle = 1; //variable qui s'occupe de changer le rayon du cercle qui définit le mouvement des cubes
    float timecounter = 0; //variable temporelle qui va servir à créer le mouvement circulaire

    //définit les cubes
    public GameObject cube;
    //organise les cubes en liste
    public List<GameObject> cubes= new List<GameObject>();

    private void Update()
    {
        //on rend le mouvement constant et proportionnel à la valeur de la vitesse (pour pouvoir l'augmenter en temps réél)
        timecounter += Time.deltaTime * speed;
        
        //détecte un ajout de cubes
        if (nbreCube > lastNbreCube)
        {
            //répète l'opération pour chaque cube en plus
            for (int j = lastNbreCube; j < nbreCube; j++)
            {
                //initialise l'emplacement du nouveau cube
                GameObject prefab = Instantiate(cube, new Vector3(0,0,0), Quaternion.identity);
                //ajoute un nouveau cube
                cubes.Add(prefab);
            }
            //synchronise l'ancien nombre de cubes avec le nouveau pour pouvoir détecter un nouveau changement
            lastNbreCube = nbreCube;
        }
        //détecte une suppression de cubes
        if (nbreCube < lastNbreCube)
        {
            //parcours le tableau de valeurs
            for(int j = lastNbreCube-1; j >= nbreCube; j--)
            {
                //supprime les dernières lignes qui ne correspondent à aucun cube
                DestroyImmediate(cubes[j]);

            }
            //synchronise l'ancien nombre de cubes avec le nouveau pour pouvoir détecter un nouveau changement
            lastNbreCube = nbreCube;
        }
        //parcours le tableau de GameObject
        int i = 1;
        foreach(GameObject pref in cubes)
        {
            
            if (pref == null) cubes.Remove(pref);
            //ces deux lignes vont définir le mouvement circulaire aux cubes en prenant compte le nombre de cubes pour bien les agencer trigonomiquement
            float x = Mathf.Cos(((float)i*Mathf.PI*2/nbreCube )+ timecounter) * rayonCercle;
            float y = Mathf.Sin(((float)i * Mathf.PI *2/ nbreCube)+ timecounter ) * rayonCercle;
           //on place ce déplacement dans une variable
            Vector3 pos = new Vector3(x, y, 0);
            //applique au prefab le mouvement
            pref.transform.position = pos;
            
            //cette prochaine ligne permet simplement de, si on le souhaite, faire tourner les cubes sur eux-mêmes, pour le fun
            //pref.transform.eulerAngles = pos*10; 
            
            i++;
        }
        
    }
}
