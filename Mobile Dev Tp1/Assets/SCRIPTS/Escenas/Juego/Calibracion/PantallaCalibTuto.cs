using UnityEngine;
using UnityEngine.UI;

public class PantallaCalibTuto : MonoBehaviour
{
    public float Intervalo = 1.2f;
    public int player;
    public ContrCalibracion ContrCalib;

    public Image canvasImage;

    public Sprite[] player1PcImages;
    public Sprite[] player2PcImages;
    public Sprite[] mobileImages;
    
    public Sprite ImaReady;

    private int EnCursoTuto;
    private float TempoIntCalib;
    private float TempoIntTuto;
    
    private void Update()
    {
        switch (ContrCalib.EstAct)
        {
            case ContrCalibracion.Estados.Calibrando:
                //pongase en posicion para iniciar
                TempoIntCalib += Time.deltaTime;
                if (TempoIntCalib >= Intervalo)
                {
#if UNITY_ANDROID
                    canvasImage.sprite = mobileImages[3];
#elif UNITY_STANDALONE
                    canvasImage.sprite = canvasImage.sprite == player1PcImages[3] ? player2PcImages[3] : player1PcImages[3];
#endif
                    TempoIntCalib = 0;
                }
                break;
            case ContrCalibracion.Estados.Tutorial:
                //tome la bolsa y depositela en el estante
                TempoIntTuto += Time.deltaTime;
                if (TempoIntTuto >= Intervalo)
                {
                    TempoIntTuto = 0;
                    if (EnCursoTuto + 1 < player1PcImages.Length - 1)
                        EnCursoTuto++;
                    else
                        EnCursoTuto = 0;
                }
#if UNITY_ANDROID
                canvasImage.sprite = mobileImages[EnCursoTuto];
#elif UNITY_STANDALONE
                canvasImage.sprite = player == 1 ? player1PcImages[EnCursoTuto] : player2PcImages[EnCursoTuto];
#endif
                break;
            case ContrCalibracion.Estados.Finalizado:
                canvasImage.sprite = ImaReady;
                break;
        }
    }
}