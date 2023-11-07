using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SaveManager_SQLite{
    public class ManagerScript : MonoBehaviour{
        [SerializeField] private DatabaseManager databaseManager;
        [SerializeField] private TMP_InputField keyInput;
        [SerializeField] private TMP_InputField valueInput;
        [SerializeField] private TextMeshProUGUI output;

        [SerializeField] private Button insertBtn;
        [SerializeField] private Button printAllBtn;
        [SerializeField] private Button findBtn;

        private void Awake(){
            insertBtn.onClick.AddListener(InsertData);
            printAllBtn.onClick.AddListener(PrintAllData);
            findBtn.onClick.AddListener(FindData);
        }

        private void PrintAllData(){
        
        }

        private void InsertData(){
            var key = keyInput.text.Trim();
            var value = valueInput.text.Trim();
            databaseManager.InsertInto<string>("StringValues",key, value);
        }

        private void FindData(){
            if (databaseManager.FindItem(keyInput.text.Trim(), out var value)){
                output.text = value;
            }
        }

        private void UpdateData(){
            
        }
    }
}
