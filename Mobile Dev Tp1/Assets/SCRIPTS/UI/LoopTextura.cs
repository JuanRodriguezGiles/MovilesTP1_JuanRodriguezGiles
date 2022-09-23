using UnityEngine;

public class LoopTextura : MonoBehaviour
{
    public float Intervalo = 1;
    public int player;

    public Texture2D[] mobileImages;
    public Texture2D[] player1Images;
    public Texture2D[] player2Images;
    private int Contador;
    private float Tempo;

    // Use this for initialization
    private void Start()
    {
#if UNITY_STANDALONE
        GetComponent<Renderer>().material.mainTexture = player == 1 ? player1Images[0] : player2Images[0];
#elif UNITY_ANDROID
        GetComponent<Renderer>().material.mainTexture = mobileImages[0];
#endif
    }

    private void Update()
    {
        Tempo += Time.deltaTime;

        if (Tempo >= Intervalo)
        {
            Tempo = 0;
            Contador++;
            if (Contador >= player1Images.Length) Contador = 0;
            
#if UNITY_STANDALONE
            GetComponent<Renderer>().material.mainTexture = player == 1 ? player1Images[Contador] : player2Images[Contador];
#elif UNITY_ANDROID
        GetComponent<Renderer>().material.mainTexture = mobileImages[Contador];
#endif
        }
    }
}