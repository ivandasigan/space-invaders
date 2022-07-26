using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Text _tutorialText;
    [SerializeField]
    private Sprite[] _tutorialFrameSprites;
    [SerializeField]
    private Image _tutorialFrame;
    [SerializeField]
    private GameObject _tutorialPanel;
    private int _frameIndex = 0;
    private List<string> _tutorialTexts = new List<string>();
    // Start is called before the first frame update
    void Start()
    {

        _tutorialText.text = "W - Forward\nA - Turn Left\nS - Backward\nD - Turn Right";

        _tutorialTexts.Add("W - Forward\nA - Turn Left\nS - Backward\nD - Turn Right");
        _tutorialTexts.Add("Arrow up - Forward\nArrow Left - Turn Left\nArrrow Down - Backward\nArrow Right - Turn Right");
        _tutorialTexts.Add("Spacebar - Fire laser");
      

        _anim = _tutorialText.GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Tutorial text animator is null");
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Next()
    {
        Color textColor = _tutorialText.color;
        textColor.a = 0.0f;
        if (_frameIndex >= _tutorialFrameSprites.Length)
        {
            _frameIndex = 0;
            
        } else
        {
            
            //_tutorialFrame.sprite = _tutorialFrameSprites[_frameIndex];
            _tutorialPanel.GetComponent<Image>().sprite = _tutorialFrameSprites[_frameIndex];
            _tutorialText.text = _tutorialTexts[_frameIndex];
        
            _frameIndex++;
        }
    }
    IEnumerator TriggerAnimationFadein()
    {

        yield return new WaitForSeconds(3.0f);
        _anim.ResetTrigger("OnNextFadein");
    }
}
