using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    public TMPro.TMP_InputField input_text;
    public TMPro.TMP_InputField output_text;
    public GameObject container;
    bool on = false;

    List<string> outputs;
    int max_outputs = 30;

    void AddItem(string item_code) {
        Item item = Resources.Load("Items/" + item_code) as Item;
        if(item == null) {
            throw new System.Exception("Wrong item code");
        }
        PlayerClass.instance.inventory.AddItem(item);
        Output("Added item " + item.name + " to inventory");
    }

    void SetWeapon(string weapon_code) {
        Weapon weapon = Resources.Load("Weapons/" + weapon_code) as Weapon;
        if(weapon == null) {
            throw new System.Exception("Wrong weapon code");
        }
        PlayerClass.instance.inventory.SetWeapon(weapon);
        Output("Weapon changed to " + weapon.name);
    }

    void RunCommand(string[] fields) {
        switch(fields[0]) {
            case "add_item":
                AddItem(fields[1]);
                break;
            case "set_weapon":
                SetWeapon(fields[1]);
                break;
            default:
                Output("Unrecognized command");
                break;
        }
    }

    void Start() {
        outputs = new List<string>();
    }

    void Output(string line) {
        line += "\n";
        outputs.Add(line);
        if(outputs.Count > max_outputs) {
            outputs.Remove(outputs[0]);
        }
        UpdateOutputText();
    }

    void UpdateOutputText() {
        string output = "";
        foreach (string line in outputs)
        {
            output += line;
        }
        output_text.text = output;
    }

    void Flip() {
        if(!on && InGameUIManager.instance.openMenu) return;
        on = !on;
        container.SetActive(on);
        InGameUIManager.instance.openMenu = on;
        if(on) input_text.ActivateInputField();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backslash)) {
            Flip();
        }
        if(on && Input.GetKeyDown(KeyCode.Return)) {
            input_text.ActivateInputField();
        }
    }

    public void Submit(string submitted) {
        input_text.text = "";

        Output("> " + submitted);

        string[] fields = submitted.Split(" ");
        try {
            RunCommand(fields);
        }
        catch (System.Exception e) {
            Output(e.Message);
        }

        input_text.ActivateInputField();
    }
}
