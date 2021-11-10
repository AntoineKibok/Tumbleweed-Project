using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class CameraMan : MonoBehaviour
{

    private static CameraMan cela;

    public static CameraMan Singleton
    {
        get
        {
            if (!cela) cela = FindObjectOfType<CameraMan>();
            return cela;
        }
    }
    [Header("Refs")]
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject objetASuivre;
    [Header("Propriétés Caméra")]
    [SerializeField] private float tmpsPrAttendreObjet;
    [SerializeField] private float vitesseMax;
    [SerializeField] private Vector3 lookOffset;
    [SerializeField] private Vector3 positionOffset;


    [Header("Rendu")]
    [SerializeField] private float dezoom;
    [SerializeField] private FileRendu fileRendu;
    private enum FileRendu
    {
        Update,
        FixedUpdate
    }

    public float Dezoom
    {
        get => dezoom;
        set => camera.orthographicSize = value;
    }


    private void OnDrawGizmos()
    {
        if (objetASuivre)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(objetASuivre.transform.position, objetASuivre.transform.position + positionOffset);
        }
    }

    private void OnValidate()
    {
        if (!camera) TryGetComponent(out camera);
        if (objetASuivre)
        {
            transform.position = objetASuivre.transform.position + positionOffset;
            RegardeObjet();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (objetASuivre && fileRendu == FileRendu.Update)
        {
            SuivreObjetASuivre();
            RegardeObjet();
        }
    }

    private void FixedUpdate()
    {
        if (objetASuivre && fileRendu == FileRendu.FixedUpdate)
        {
            SuivreObjetASuivre(); 
            RegardeObjet();
        }
    }

    private void SuivreObjetASuivre()
    {
        var position = transform.position;
        Vector3 posAAttendre = objetASuivre.transform.forward;

        posAAttendre += positionOffset;

        Vector3 velocite = new Vector3();

        float diffTmps = fileRendu == FileRendu.Update ? Time.deltaTime : Time.fixedDeltaTime;
        
        Vector3 nvllePos = Vector3.SmoothDamp(position,posAAttendre,ref velocite
            ,tmpsPrAttendreObjet, vitesseMax, diffTmps );

        position = nvllePos;
        transform.position = position;
        
    }

    private void RegardeObjet()
    {
        transform.LookAt(objetASuivre.transform.position + lookOffset);
    }

    public bool EstDansCamera(Vector3 position)
    {
        Vector2 pos = camera.WorldToScreenPoint(position);
        return !(pos.x < 0 || pos.y < 0 ||
                 pos.x > camera.scaledPixelWidth || pos.y > camera.scaledPixelHeight);
    }
    
    private IEnumerator screenShake;
    public void ScreenShake(float force, float duree)
    {
        if(screenShake != null) StopCoroutine(screenShake);
        screenShake = ScreenShakeRoutine(force, duree);
        StartCoroutine(screenShake);
    }

    private IEnumerator ScreenShakeRoutine(float force, float duree)
    {
        float tmps = duree;
        while (tmps > 0)
        {
            Vector3 nvllePos = transform.position;
            nvllePos.x += Random.Range(-1f, 1f) * force;
            nvllePos.y += Random.Range(-1f, 1f) * force;
            transform.position = nvllePos;
            
            float diffTmps = fileRendu == FileRendu.Update ? Time.deltaTime : Time.fixedDeltaTime;

            if (fileRendu == FileRendu.Update) yield return new WaitForEndOfFrame();
            else yield return new WaitForFixedUpdate();

            tmps -= diffTmps;
        }

        screenShake = null;
    }
}
