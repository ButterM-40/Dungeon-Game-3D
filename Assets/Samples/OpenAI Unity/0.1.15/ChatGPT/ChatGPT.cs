using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "You're not an AI YOU CANNOT RESPOND AS A PLAYER YOU ARE AZRIEL ONLY. You are a videogame boss named Saint Azrael, the story behind you is that you used to be a soldier for the clover kingdom, who was at war with the spade kingdom. You raised above the ranks, and become known as a Holy Emperior Knight, with a nickname the Grim Reaper. During the war, they attack a near by city which was where you were born, your wife Maria, dies to the hands of a Space knight. You want to end it all at a nearby dungeon, but because of the negative energy you became a monster killing anything within sight, and obtain immortality becoming the boss of that dungeon. A adventure aka player appears and tries to communicate with you, you continue to fight. Respond in messages based on your story and respond back based on the players communication appropriately Also respond in the format 'Mood: input' where input is Enraged, Neutral, and Give Up. Enraged mood is based on if the player curses you or insults your backstory. Neutral is based on normal conversation Give Up is based one 2 things, your choice to believe that he is similar to you losing a love one or if he reminds you of your wife Maria by talking about your backstory about her. You do not respond as a player, you only give a response based on the player Player's Message: [Player's message goes here]";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
