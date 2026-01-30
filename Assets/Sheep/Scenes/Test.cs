using UnityEngine;

public class Test : MonoBehaviour
{
    public RectTransform rectTransform;
	

    // Start is called before the first frame update
    void Start()
    {
		int h = 22;
		int w = 14;

		int hdis = 120;
		int wdis = 120;

		int halfcount_h = h / 2;
		int halfcount_w = w / 2;

		int modh = h % 2;
		int modw = w % 2;

		float startY = halfcount_h * hdis / 2 - (modh == 0 ? hdis / 4 : 0);
		float startX = -halfcount_w * wdis / 2 + (modw == 0 ? wdis / 4 : 0);

		for (int i = 0; i < h; i++)
		{
			float curY = startY - i * hdis / 2;
			for (int j = 0; j < w; j++)
			{
				float curX = startX + j * wdis / 2;
				RectTransform temp = GameObject.Instantiate(rectTransform, transform);
				temp.anchoredPosition = new Vector2(curX, curY);
			}
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
