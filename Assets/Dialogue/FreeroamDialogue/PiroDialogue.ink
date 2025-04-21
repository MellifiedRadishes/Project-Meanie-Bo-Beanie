INCLUDE ../Global.ink
{dialogue_state:
    - 0:
    -> first_meeting
    - 1:
    -> sample_obtained
    - 2:
    -> sample_unobtained

}
==== first_meeting ====
~ ChangeSpeaker("PIRO")
- Hey there, Meanie! Just grabbing some of my WORLD-FAMOUS Berry Smoothies for the party!
- Ginger had asked me to bring some refreshments… I think she’s setting up the decorations at Walnut’s right now.
- Anyways, want a sample? They taste better when they’re fresh!
*   "No"
    ~ UpdateDialogueState(2)
    Oh.. I mean, that’s alright. You can still try them at the party!
    See you, then!
    -> END
*   “I’ll be the judge of that!”
    ~ UpdateDialogueState(1)
    Here you go! Let me know how it tastes!
    …Unless you think it tastes bad. Which it won’t!
    -> END
    
==== sample_obtained ====
~ ChangeSpeaker("PIRO")
- I hope Ginger bought enough cups!
-> END

==== sample_unobtained ====
Hey Meanie! Did you change your mind? They're still fresh!
*   "Still No"
    Oh.. ok... maybe at the party then...
    See you, then!
    -> END
*   “Fiiine...”
    ~ UpdateDialogueState(1)
    Alrighty! Here you go! Let me know how it tastes!
    …Unless you think it tastes bad. Which it won’t!
    -> END