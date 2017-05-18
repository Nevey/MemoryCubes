using UnityEngine;

public class SpriteFinder
{
    public SpriteFinder()
    {

    }

    public Sprite FindSprite(TargetColors color)
    {
        string name = color.ToString();

        return FindSprite(name);
    }

    public Sprite FindSprite(string name)
    {
        return Resources.Load<Sprite>("Images/TargetBar/" + name);
    }
}
