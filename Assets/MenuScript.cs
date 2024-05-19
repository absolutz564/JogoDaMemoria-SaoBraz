using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton; // Referência ao botão que leva para a nova cena
    private string playerNameKey = "CurrentUser";

    private void Start()
    {
        startButton.interactable = false;

        //// Verifica se já existe um nome de jogador salvo em PlayerPrefs
        //if (PlayerPrefs.HasKey(playerNameKey))
        //{
        //    string savedPlayerName = PlayerPrefs.GetString(playerNameKey);
        //    nameInputField.text = savedPlayerName;
        //    startButton.interactable = true;
        //}

        // Adicione um listener de evento para o campo de entrada
        nameInputField.onValueChanged.AddListener(UpdateStartButton);

        // Inicialmente, desabilite o botão de início
    }

    private void UpdateStartButton(string text)
    {
        // Habilita o botão de início apenas se o campo de entrada não estiver vazio
        startButton.interactable = !string.IsNullOrEmpty(text);
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;

        // Verifique se o campo de entrada não está vazio
        if (!string.IsNullOrEmpty(playerName))
        {
            // Salve o nome do jogador em PlayerPrefs
            PlayerPrefs.SetString(playerNameKey, playerName);
            PlayerPrefs.Save();
            Debug.Log("Nome do jogador salvo: " + playerName);

            // Aqui você pode fazer a transição para a cena do ranking
            // SceneManager.LoadScene("CenaDoRanking");
        }
        else
        {
            Debug.LogWarning("Nome do jogador não pode estar vazio.");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
