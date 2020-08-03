using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DaggerfallWorkshop.Game.UserInterface{
    public class DaggerfallUIToCanvas : MonoBehaviour
    {
        GameObject playerAdvanced;
        GameObject smoothFollower;
        static GameObject VRUI;
        static Canvas VRUICanvas;
        public GameObject VRUIPrefab;
        // Start is called before the first frame update
        void Start()
        {
            playerAdvanced = GameObject.Find("PlayerAdvanced");
            smoothFollower = GameObject.Find("PlayerAdvanced/SmoothFollower");

            VRUI = Instantiate(VRUIPrefab);
            VRUICanvas = VRUI.GetComponentInChildren<Canvas>();
            
            VRUI.transform.parent = smoothFollower.transform;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public static void TextureToSprite(string name, Rect rect, Texture2D texture, float scroll){
            // Get dimensions of texture and create sprite
            rect = new Rect(0f, 0f, texture.width, texture.height);
            Sprite UIElement = Sprite.Create(texture, rect, new Vector2(0f,0f));

            // Check if a UI element of this name already exists
            GameObject thisObject = GameObject.Find(name);

            // If it doesn't exist, make one
            if (thisObject == null){
                GameObject newObj = new GameObject();
                newObj.name = name;
                Image image = newObj.AddComponent<Image>();
                image.sprite = UIElement;

                // Do unique setup stuff based on the UI object
                if (name == "compass"){
                    CreateCompassMask(newObj);
                    ConfigureCompass(newObj);
                }
                if (name == "compassbox"){
                    ConfigureCompassBox(newObj);
                }
            }
            // If it does exist, update it
            else {
                if (thisObject.name == "compass"){
                    UpdateCompass(thisObject, scroll);
                }
                if (thisObject.name == "compassbox"){
                    UpdateCompassBox(thisObject);
                }
            }

        }
        static void CreateCompassMask(GameObject parentObject){
            GameObject compassMask = new GameObject();

            // Create image mask and make it transparent
            Image image = compassMask.AddComponent<Image>();
            image.color = new Color(0f, 0f, 0f, 0.01f);

            compassMask.AddComponent<Mask>();
            compassMask.name = "compassMask";

            // Set VRUICanvas as mask's parent
            compassMask.transform.parent = VRUICanvas.transform;
            // Scale the mask
            compassMask.transform.localScale = new Vector3(1f, 0.5f, 1f);
            // Move mask to VRUI canvas's location
            compassMask.transform.position = VRUICanvas.transform.position;
        }

        /// <summary>
        ///     Parent compass to compassMask
        /// </summary>
        static void ConfigureCompass(GameObject compass){
            GameObject compassMask = GameObject.Find("compassMask");
            compass.transform.parent = compassMask.transform;
            compass.transform.position = compassMask.transform.position;
            compass.transform.localScale = new Vector3(4f,0.5f,1f);
        }

        static void UpdateCompass(GameObject compass, float scroll){
            GameObject compassMask = GameObject.Find("compassMask");
            Vector3 scrollPosition = compassMask.transform.position;

            compass.transform.position = scrollPosition;
        }

        /// <summary>
        ///     Set compassbox scale and parent it to compassMask
        /// </summary>
        static void ConfigureCompassBox(GameObject compassBox){
            GameObject compassMask = GameObject.Find("compassMask");
            compassBox.transform.parent = compassMask.transform;
            compassBox.transform.position = compassMask.transform.position;
            compassBox.transform.localScale = new Vector3(1f, 0.5f, 1f);
        }

        static void UpdateCompassBox(GameObject compassBox){
            // GameObject compassMask = GameObject.Find("compassMask");
            // compassBox.transform.position = compassMask.transform.position;
            // compassBox.transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
    }
}