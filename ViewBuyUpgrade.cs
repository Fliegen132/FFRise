using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ViewBuyUpgrade : MonoBehaviour, IService
{
   private string currentAnim;
   [SerializeField] private Animator anim;

   [SerializeField] private Button button;
   private StudyOrUpgrade _studyOrUpgrade;

   private InputSystem _inputSystem;
   private void Awake()
   {
      anim = GetComponent<Animator>();
   }

   private void Start()
   {
      _studyOrUpgrade = ServiceLocator.Current.Get<StudyOrUpgrade>();
      _inputSystem = ServiceLocator.Current.Get<InputSystem>();
   }

   public void SetUpdateBtn(Tile tile, string text, Dictionary<string, int> resources)
   {
      button.onClick.RemoveAllListeners();
      button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
      _studyOrUpgrade.SetTile(tile);

      button.onClick.AddListener(() =>_studyOrUpgrade.Upgrade(resources));
   }

   public void SetStudiedBtn(string text)
   {
      button.onClick.RemoveAllListeners();
      button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;

      button.onClick.AddListener(() =>_studyOrUpgrade.Study());
   }

   public void SetAnimation(string _currentAnim)
   {
      if(currentAnim == _currentAnim)
         return;

      currentAnim = _currentAnim;
      anim.Play(currentAnim);
   }
   public void EndCloseAnim()
   {
      _inputSystem.choosed = false;
   }
   public void EndAOpenAnim()
   {
      _inputSystem.choosed = true;
   }

   public void StartCloseAnim()
   {
      SetAnimation("Close");
   }
}
