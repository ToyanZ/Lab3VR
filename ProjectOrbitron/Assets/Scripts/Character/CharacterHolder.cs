using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CharacterHolder : InterfaceData
{
    public Character selectedCharacter;
    public Character currentCharacter;
    public bool respawn = true;
    public float respawnTime = 5;
    float respawnCount = 0;
    bool respawning = false;

    [Space(20)]
    public RectTransform parentHud;
    public List<AbilityLauncher> abilitiesSelected;
    public List<ProgressBarIE> hudSelected;

    [Space(20)]
    public ProgressBarIE healthBar;
    public ProgressBarIE ammoBar;
    [HideInInspector] public List<ProgressBarIE> currentAbilities;
    [HideInInspector] public List<ProgressBarIE> currentHud;

    bool creatingCharacter = false;
    public void CreateCharacter()
    {
        if(!creatingCharacter) StartCoroutine(CreateCharecterIE());

        
    }

    IEnumerator CreateCharecterIE()
    {
        creatingCharacter = true;
        //Crear personaje
        currentCharacter = Instantiate(selectedCharacter);

        //Crear habilidad
        int i = 0;
        foreach (AbilityLauncher launcher in abilitiesSelected)
        {
            AbilityLauncher abilityLauncher = Instantiate(launcher);
            abilityLauncher.sender = currentCharacter.player.target;
            abilityLauncher.transform.parent = transform;
            abilityLauncher.transform.localPosition = Vector3.zero;
            abilityLauncher.transform.localScale = Vector3.one;
            //abilityLauncher.onDataUpdatedEventsPrivate = new List<OnDataUpdated>
            //{
            //    new OnDataUpdated("OnAbilityLaunched", 0, new UnityEngine.Events.UnityEvent<InterfaceData>())
            //};

            //Unir con Inputs
            if (currentCharacter.actionReferences[i] != null)
            {
                string keyName = "";
                //if (currentCharacter.player.playerInput.currentControlScheme == currentCharacter.player.inputActions.GamepadScheme.name.ToString())
                //{

                //}
                //else
                //{
                //    string[] path = currentCharacter.actionReferences[i].action.bindings[0].effectivePath.Split("/");
                //    keyName = path[1];
                //}
                string[] path = currentCharacter.actionReferences[i].action.bindings[1].effectivePath.Split("/");
                keyName = path[1];


                abilityLauncher.inputKeyName = keyName;
                currentCharacter.actionReferences[i].action.performed += (InputAction.CallbackContext c) =>
                {
                    abilityLauncher.Launch();
                };
            }

            //Unir con HUD
            ProgressBarIE newProgressBar = Instantiate(hudSelected[i]);
            while (abilityLauncher.onDataUpdatedEventsPrivate == null) yield return null;
            while (abilityLauncher.onDataUpdatedEventsPrivate.Count == 0) yield return null;
            abilityLauncher.onDataUpdatedEventsPrivate[0].Event.AddListener((InterfaceData interfaceData) =>
            {
                newProgressBar.Refresh(abilityLauncher);
            });
            currentHud.Add(newProgressBar);
            newProgressBar.rectTransform.SetParent(parentHud);

            i++;
        }

        while (currentCharacter.player.target.onDataUpdatedEventsPrivate == null) yield return null;
        while (currentCharacter.player.target.onDataUpdatedEventsPrivate.Count == 0) yield return null;
        currentCharacter.player.target.onDataUpdatedEventsPrivate[0].Event.AddListener((InterfaceData interfaceData) =>
        {
            healthBar.Refresh(currentCharacter.player.target);
        });

        while (currentCharacter.player.weapon.onDataUpdatedEventsPrivate == null) yield return null;
        while (currentCharacter.player.weapon.onDataUpdatedEventsPrivate.Count == 0) yield return null;
        currentCharacter.player.weapon.onDataUpdatedEventsPrivate[0].Event.AddListener((InterfaceData interfaceData) =>
        {
            ammoBar.Refresh(currentCharacter.player.weapon);
        });
        creatingCharacter = false;
    }
    public void RespawnCharacter()
    {
        if (!respawning) StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        respawning = true;
        
        respawnCount = respawnTime;
        while(respawnCount > 0)
        {
            respawnCount -= Time.deltaTime;
            DataUpdate(this, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        currentCharacter.Resurrect();
        
        respawning = false;
    }
}
