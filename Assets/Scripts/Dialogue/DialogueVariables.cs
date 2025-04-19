using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.IO;
public class DialogueVariables
{

    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(TextAsset globalsJSON) {
        Story glovalVariablesStory = new Story(globalsJSON.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in glovalVariablesStory.variablesState) { 
            Ink.Runtime.Object value = glovalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }
    public void StartListening(Story story) {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }


    private void VariableChanged(string name, Ink.Runtime.Object value) {
        if (variables.ContainsKey(name)) {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
