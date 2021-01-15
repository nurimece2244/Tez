using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
   
    private int _itemId;
    [SerializeField]  private Sprite _buttonTexture;
    [SerializeField] private RawImage buttonImage;
  

    public int ItemId
    {
        set { _itemId = value; }
      
    }

    public Sprite ButtonTexture
    {
        set { _buttonTexture = value;

            buttonImage.texture = _buttonTexture.texture; 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.OnEntered(gameObject))
        {
            transform.localScale = Vector3.one * 2;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        
    }

    void SelectObject()
    {
        DataHandler.Instance.SetFurniture(_itemId);
      
    }

    
}


