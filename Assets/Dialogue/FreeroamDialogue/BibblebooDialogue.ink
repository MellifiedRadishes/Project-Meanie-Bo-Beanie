INCLUDE ../Global.ink
{dialogue_state:
    - 0:
    -> first_meeting
    - 1:
    -> visited

}
====first_meeting====
~ ChangeSpeaker("BIBBLEBOO")
- Oh, I really hope Walnut likes what I got him for his birthday…
- Oh, oh… Do you promise you can keep a secret?
- I got him a jar of Crunchy Peanut Butter… Do you think he’ll like it…?
*   "No"
    -> end_convo
*   "He’s more of a jelly kind of guy"
    -> end_convo
=== end_convo ===
~ UpdateDialogueState(1)
Oh… Oh, no…
-> END
====visited=====
~ ChangeSpeaker("BIBBLEBOO")
 …I knew I should’ve gotten him a fourth window…
-> END