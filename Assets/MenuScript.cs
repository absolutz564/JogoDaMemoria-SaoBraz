using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton; // Refer�ncia ao bot�o que leva para a nova cena
    private string playerNameKey = "CurrentUser";

    private void Start()
    {
        startButton.interactable = false;

        //// Verifica se j� existe um nome de jogador salvo em PlayerPrefs
        //if (PlayerPrefs.HasKey(playerNameKey))
        //{
        //    string savedPlayerName = PlayerPrefs.GetString(playerNameKey);
        //    nameInputField.text = savedPlayerName;
        //    startButton.interactable = true;
        //}

        // Adicione um listener de evento para o campo de entrada
        nameInputField.onValueChanged.AddListener(UpdateStartButton);

        // Inicialmente, desabilite o bot�o de in�cio
    }

    private void UpdateStartButton(string text)
    {
        // Habilita o bot�o de in�cio apenas se o campo de entrada n�o estiver vazio
        startButton.interactable = !string.IsNullOrEmpty(text);
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;

        // Verifique se o campo de entrada n�o est� vazio
        if (!string.IsNullOrEmpty(playerName))
        {
            // Salve o nome do jogador em PlayerPrefs
            PlayerPrefs.SetString(playerNameKey, playerName);
            PlayerPrefs.Save();
            Debug.Log("Nome do jogador salvo: " + playerName);

            // Aqui voc� pode fazer a transi��o para a cena do ranking
            // SceneManager.LoadScene("CenaDoRanking");
        }
        else
        {
            Debug.LogWarning("Nome do jogador n�o pode estar vazio.");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
