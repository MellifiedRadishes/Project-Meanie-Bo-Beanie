INCLUDE ../Global.ink
{dialogue_state:
    - 0:
    -> first_meeting
    - 1:
    -> visited

}
====first_meeting====
~ ChangeSpeaker("MR. SCUTTLE")
- Ché s'approccia la riviera del sangue in la qual bolle qual che per vïolenza in altrui noccia.
~ ChangeSpeaker("SCOUT")
~ UpdateDialogueState(1)
- Did you catch any of that? My French kind of sucks.
-> END
====visited=====
~ ChangeSpeaker("MR. SCUTTLE")
Cautela, persona violenta.
-> END