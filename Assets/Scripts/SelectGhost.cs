using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//script written by Oliver Jenkinson for this project (with reference to previously written code from GDS212 Game 2)

public class SelectGhost : MonoBehaviour
{
    public GameObject hitGhost, selectedGhost, hitGrave, selectedGrave, lockItInPanel;

    public TextMeshProUGUI selectedGraveText, selectedGhostText;

    public string nullGraveText, nullGhostText, correctMatchText, incorrectMatchText;

    public Color hitGhostColour, selectedGhostColour, originalGhostColour, hitGraveColour, selectedGraveColour, graveColour, selectionDotColor, originalSelectionDotColor;

    public GameStateUI gameStateUI;

    public AudioClip matchConfirmedAudio, matchIncorrectAudio;

    
    // Start is called before the first frame update
    void Start()
    {
        originalSelectionDotColor = gameObject.GetComponent<MeshRenderer>().material.color;

        selectedGraveText.text = nullGraveText;
        selectedGhostText.text = nullGhostText;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = selectionDotColor;

            if (selectedGhost == null)
            {
                if (hit.transform.CompareTag("GhostObject")) //<< if a ghost is selected
                {
                    hitGhost = hit.transform.gameObject;

                    //if (!hitGhost.GetComponent<Ghost>().isIdentified)
                    //{
                    //    //selectedGhostText.text = 
                    //}

                    if (!hitGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled) //<< track original ghost colour
                    {
                        hitGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = true;
                        //originalGhostColour = hitGhost.GetComponent<MeshRenderer>().material.color;
                    }

                    //hitGhost.GetComponent<MeshRenderer>().material.color = hitGhostColour; //<< change colour if mouse is hovering over ghost

                    if (!hitGhost.transform.parent.GetComponent<Ghost>().isIdentified) //<< check if ghost is identified
                    {
                        selectedGhostText.text = hitGhost.transform.parent.GetComponent<Ghost>().ghostComment.text; //<< if not identified display their comment on the UI
                    }
                    else selectedGhostText.text = hitGhost.transform.parent.GetComponent<Ghost>().detectableIdentity; //<< if identified display their identitity

                    if (Input.GetButtonDown("Fire1"))
                    {
                        selectedGhost = hitGhost;
                        //if (!selectedGhost.transform.parent.GetComponent<Ghost>().isIdentified)
                        //    selectedGhostText.text = selectedGhost.transform.parent.GetComponent<Ghost>().ghostComment.text;
                        //else selectedGhostText.text = selectedGhost.transform.parent.GetComponent<Ghost>().detectableIdentity;
                        selectedGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = true;
                        //selectedGhost.GetComponent<MeshRenderer>().material.color = selectedGhostColour;
                    }

                }
                else
                {
                    if (hitGhost != null)
                    {
                        //hitGhost.GetComponent<MeshRenderer>().material.color = originalGhostColour;
                        hitGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = false;
                        if (selectedGhostText.text != correctMatchText && selectedGhostText.text != incorrectMatchText)
                            selectedGhostText.text = nullGhostText;
                        hitGhost = null;
                    }
                }
            }
            else
            {
                hitGhost = null;

                if (!selectedGhost.transform.parent.GetComponent<Ghost>().isIdentified)
                    selectedGhostText.text = selectedGhost.transform.parent.GetComponent<Ghost>().ghostComment.text;
                else selectedGhostText.text = selectedGhost.transform.parent.GetComponent<Ghost>().detectableIdentity;
            }

            //if (selectedGhost != null & hitGhost != null)
            //{
            //    hitGhost = null;
            //}


            if (selectedGrave == null)
            {
                if (hit.transform.CompareTag("Grave"))
                {
                    hitGrave = hit.transform.gameObject;
                    //originalGhostColour = hitGhost.GetComponent<MeshRenderer>().material.color;
                    hitGrave.GetComponent<MeshRenderer>().material.color = hitGraveColour;
                    selectedGraveText.text = hitGrave.GetComponent<GenerateGrave>().graveText.text;

                    if (Input.GetButtonDown("Fire1"))
                    {

                        hitGrave.GetComponent<MeshRenderer>().material.color = selectedGraveColour;
                        selectedGraveText.text = hitGrave.GetComponent<GenerateGrave>().graveText.text;
                        selectedGrave = hitGrave;

                    }
                }
                else
                {
                    if (hitGrave != null)
                    {
                        hitGrave.GetComponent<MeshRenderer>().material.color = graveColour;
                        if (selectedGraveText.text != correctMatchText && selectedGraveText.text != incorrectMatchText)
                            selectedGraveText.text = nullGraveText;
                        hitGrave = null;
                    }
                }
            }
            else hitGrave = null;


            //if (selectedGrave != null & hitGrave != null)
            //{
            //    hitGrave = null;
            //}



        }
        else
        {
            if (hitGrave != null)
            {
                hitGrave.GetComponent<MeshRenderer>().material.color = graveColour;
                selectedGraveText.text = nullGraveText;
                hitGrave = null;
            }

            if (hitGhost != null)
            {
                //hitGhost.GetComponent<MeshRenderer>().material.color = originalGhostColour;
                hitGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = false;
                selectedGhostText.text = nullGhostText;
                hitGhost = null;
            }

            gameObject.GetComponent<MeshRenderer>().material.color = originalSelectionDotColor;
        }

        if (selectedGhost != null && selectedGrave != null)
        {
            lockItInPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckMatch();
            }
        }
        else lockItInPanel.SetActive(false);

        if (Input.GetButtonDown("Fire2"))
        {
            if (selectedGhost != null)
            {
                //selectedGhost.GetComponent<MeshRenderer>().material.color = originalGhostColour;
                selectedGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = false;
                selectedGhostText.text = nullGhostText;

                selectedGhost = null;
            }

            if (selectedGrave != null)
            {
                selectedGrave.GetComponent<MeshRenderer>().material.color = graveColour;
                selectedGraveText.text = nullGraveText;

                selectedGrave = null;
            }
        }

        if (selectedGhost != null && selectedGhostText.text != selectedGhost.transform.parent.GetComponent<Ghost>().ghostComment.text)
        {
            selectedGhostText.text = selectedGhost.transform.parent.GetComponent<Ghost>().ghostComment.text;
        }

    }

    void CheckMatch()
    {
        if (selectedGhost.transform.parent.GetComponent<Ghost>().Grave == selectedGrave)
        {
            selectedGhostText.text = correctMatchText;
            selectedGraveText.text = correctMatchText;
            //play a sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(matchConfirmedAudio);
            selectedGhost.transform.parent.GetComponent<Ghost>().isIdentified = true;
            selectedGhost.transform.parent.GetComponent<Ghost>().wayPoint = new Vector3(selectedGrave.transform.position.x, 1f, selectedGrave.transform.position.z - 2f);

            if (selectedGhost != null)
            {
                //selectedGhost.GetComponent<MeshRenderer>().material.color = originalGhostColour;
                selectedGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = false;
                selectedGhost = null;
            }

            if (selectedGrave != null)
            {
                selectedGrave.GetComponent<MeshRenderer>().material.color = graveColour;
                selectedGraveText.text = nullGraveText;

                selectedGrave = null;
            }

            gameStateUI.CountMatches();
        }
        else
        {
            // play a sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(matchIncorrectAudio);
            selectedGhostText.text = incorrectMatchText;
            selectedGraveText.text = incorrectMatchText;

            if (selectedGhost != null)
            {
                //selectedGhost.GetComponent<MeshRenderer>().material.color = originalGhostColour;
                selectedGhost.GetComponent<Outline_MouseSelect>().outlineRenderer.enabled = false;
                selectedGhost = null;
            }

            if (selectedGrave != null)
            {
                selectedGrave.GetComponent<MeshRenderer>().material.color = graveColour;
                selectedGraveText.text = nullGraveText;

                selectedGrave = null;
            }
        }
    }


}
