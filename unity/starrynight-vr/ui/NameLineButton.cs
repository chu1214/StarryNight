// Unity 프로젝트는 유료 에셋이 포함되어 있어서, 핵심 기능과 관련된 코드들만 기재했습니다.
using UnityEngine;
using UnityEngine.UI;

public class NameLineButton : MonoBehaviour
{
    public bool clicked = false;
    
    public void click()
    {
        GameObject[] names = GameObject.FindGameObjectsWithTag("NameToggle");
        GameObject[] lines = GameObject.FindGameObjectsWithTag("LineToggle");
        
        // 끄기
        if (!clicked)
        {
            var child = gameObject.GetComponentInChildren<Text>();
            gameObject.GetComponent<Image>().color = new Color(48/255f, 48/255f, 48/255f, 100/255f);
            child.color = new Color(1, 1, 1, 100/255f);
            child.text = "별자리 OFF";
            
            // 이름 끄기
            foreach (var name in names)
            {
                Color c = name.GetComponent<MeshRenderer>().materials[0].color;
                name.GetComponent<MeshRenderer>().materials[0].color = new Color(c.r, c.g, c.b, 0);
            }
            
            // 별자리 선 끄기
            foreach (var line in lines)
            {
                line.GetComponent<LineRenderer>().startWidth = 0;
            }
        }
        // 켜기
        else
        {
            var child = gameObject.GetComponentInChildren<Text>();
            gameObject.GetComponent<Image>().color = new Color(48/255f, 48/255f, 48/255f, 1);
            child.color = new Color(1, 1, 1, 1);
            child.text = "별자리 ON";
            
            // 이름 켜기
            foreach (var name in names)
            {
                Color c = name.GetComponent<MeshRenderer>().materials[0].color;
                name.GetComponent<MeshRenderer>().materials[0].color = new Color(c.r, c.g, c.b, 255);
            }
            
            // 별자리 선 켜기
            foreach (var line in lines)
            {
                line.GetComponent<LineRenderer>().startWidth = 2;
            }
        }
        clicked = !clicked;
    }
}
