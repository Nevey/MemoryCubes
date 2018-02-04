using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CubeCountView : MonoBehaviour, IOnUIViewInitialize, IOnUIViewShow
{
    private Text cubeCountText;

    private string originString;

    public void OnUIViewInitialize()
    {
        cubeCountText = GetComponent<Text>();

        originString = cubeCountText.text;
    }

    public void OnUIViewShow()
    {
        cubeCountText.text = string.Format(originString, LevelController.Instance.CurrentCubeCount.ToString());
    }
}